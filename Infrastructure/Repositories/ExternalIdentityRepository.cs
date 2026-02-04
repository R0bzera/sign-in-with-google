using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ExternalIdentityRepository : IExternalIdentityRepository
{
    private readonly AppDbContext _context;

    public ExternalIdentityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ExternalIdentity?> GetByProviderAndIdAsync(string provider, string providerUserId)
    {
        return await _context.ExternalIdentities
            .FirstOrDefaultAsync(e => e.Provider == provider && e.ProviderUserId == providerUserId);
    }

    public async Task AddAsync(ExternalIdentity externalIdentity)
    {
        await _context.ExternalIdentities.AddAsync(externalIdentity);
        await _context.SaveChangesAsync();
    }
}
