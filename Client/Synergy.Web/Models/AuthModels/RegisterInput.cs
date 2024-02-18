namespace Synergy.Web.Models.AuthModels;

//public record RegisterInput(string Username, string Email, string Password);

public record RegisterInput(string GivenName, string LastName, string Photo, string Title, string TeamId, MemberContact ContractDto, CreateUserDto CreateUser);
public record CreateUserDto(string Username, string Email, string Password);
public record MemberContact(string PhoneNumber, string Address);