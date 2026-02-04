namespace Application.Interfaces;

public interface IExternalAuthProviderFactory
{
    IExternalAuthProvider GetProvider(string provider);
}
