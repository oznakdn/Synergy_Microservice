using Confluent.Kafka;
using MongoDB.Driver;
using Synergy.IdentityService.Domain.Models;
using Synergy.Shared.Constants;
using System.Text.Json;

namespace Synergy.IdentityService.MessageWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IMongoCollection<User> _collection;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Synergy_Identity");
            _collection = database.GetCollection<User>("User");
        
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                Acks = Acks.All,
                BootstrapServers = "localhost:29092",
                ClientId = "CreatedMember",
                GroupId = "CreatedMemberGroup",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(MessageTopic.CREATED_MEMBER);

            while (!stoppingToken.IsCancellationRequested)
            {
                var message = consumer.Consume(stoppingToken);

                string key = message.Message.Key;
           
                var user = JsonSerializer.Deserialize<CreateUserDto>(message.Message.Value);

                if (user is not null)
                {
                    var existUser = await _collection.Find(x => x.Email == user.Email || x.Username == user.Username).SingleOrDefaultAsync();
                    if(existUser is null)
                    {
                        await _collection.InsertOneAsync(new Domain.Models.User
                        {
                            Email = user.Email,
                            Password = user.Password,
                            MemberId = key,
                            Username = user.Username
                        });

                        _logger.LogInformation("User has created successfully.");
                    }
                    else
                    {
                        _logger.LogInformation("User is already exists!");
                    }
                    

                }
                else
                {
                    _logger.LogInformation("User has not created!");
                }
            }
        }
    }
}
