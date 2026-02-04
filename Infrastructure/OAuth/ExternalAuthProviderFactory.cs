using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.OAuth;

public class ExternalAuthProviderFactory : IExternalAuthProviderFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, Type> _providers;

    public ExternalAuthProviderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _providers = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        {
            { "Google", typeof(GoogleAuthProvider) }
        };
    }

    public IExternalAuthProvider GetProvider(string provider)
    {
        if (!_providers.TryGetValue(provider, out var providerType))
        {
            throw new NotSupportedException($"Provider '{provider}' is not supported.");
        }

        return (IExternalAuthProvider)_serviceProvider.GetRequiredService(providerType);
    }
}
