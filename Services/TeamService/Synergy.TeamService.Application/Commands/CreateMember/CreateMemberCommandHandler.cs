using Confluent.Kafka;
using MediatR;
using Synergy.Shared.Constants;
using Synergy.Shared.Results;
using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using System.Text.Json;

namespace Synergy.TeamService.Application.Commands.CreateMember;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result>
{
    private readonly IRepositoryManager _manager;

    public CreateMemberCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = new Member
        {
            CreatedDate = DateTime.Now,
            CreatedBy = request.CreatedBy,
            GivenName = request.CreateMember.GivenName,
            LastName = request.CreateMember.LastName,
            Photo = request.CreateMember.Photo,
            Title = request.CreateMember.Title,
            TeamId = Guid.Parse(request.CreateMember.TeamId)
        };
        _manager.Member.Insert(member);

        var contact = new Contact
        {
            MemberId = member.Id,
            PhoneNumber = request.CreateMember.ContractDto.PhoneNumber,
            Address = request.CreateMember.ContractDto.Address
        };

        _manager.Contact.Insert(contact);

        var result = await _manager.SaveAsync(cancellationToken);

        if (result > 0)
        {
            var config = new ProducerConfig
            {
                Acks = Acks.All,
                BootstrapServers = "localhost:29092",
                ClientId = "CreatedMember"
            };

            using var producer = new ProducerBuilder<string, string>(config).Build();

            var message = new Message<string, string>
            {
                Key = member.Id.ToString(),
                Value = JsonSerializer.Serialize(request.CreateMember.CreateUser)
            };

            var status = await producer.ProduceAsync(MessageTopic.CREATED_MEMBER, message);

            if (status.Status == PersistenceStatus.Persisted)
            {
                return Result.Success(204);

            }

            return Result.Failure(status.Value.Single());

        }

        return Result.Failure(400);

    }
}
