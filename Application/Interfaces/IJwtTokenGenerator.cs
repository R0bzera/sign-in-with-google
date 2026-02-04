using Domain.Entities;

namespace Application.Interfaces;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}
