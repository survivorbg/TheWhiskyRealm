﻿using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Whisky;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Enums;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class AwardService : IAwardService
{
    private readonly IRepository repo;

    public AwardService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<bool> AwardExistAsync(int id)
    {
        return await repo
            .AllReadOnly<Award>()
            .AnyAsync(a => a.Id == id);
    }

    public async Task EditAwardAsync(AwardViewModel model)
    {
        var award = await repo.GetByIdAsync<Award>(model.Id);
        if(model != null)
        {
            award.Title = model.Title;
            award.AwardsCeremony = model.AwardsCeremony;
            award.Year = model.Year;
            award.MedalType = (MedalType)Enum.Parse(typeof(MedalType), model.MedalType);
        }
        await repo.SaveChangesAsync();
        
    }

    public async Task<ICollection<AwardViewModel>> GetAllWhiskyAwards(int id)
    {
        return await repo
            .AllReadOnly<Award>()
            .Where(a => a.WhiskyId == id)
            .Select(a => new AwardViewModel
            {
                Id = a.Id,
                AwardsCeremony = a.AwardsCeremony,
                MedalType = a.MedalType.ToString(),
                Title = a.Title,
                Year = a.Year,
                WhiskyId = a.Whisky.Id
            })
            .ToListAsync();
    }

    public async Task<AwardViewModel?> GetAwardByIdAsync(int id)
    {
        return await repo
            .All<Award>()
            .Where(a => a.Id == id)
            .Select(a => new AwardViewModel
            {
                Id = a.Id,
                AwardsCeremony = a.AwardsCeremony,
                Title = a.Title,
                Year = a.Year,
                WhiskyId = a.Whisky.Id,
                MedalType = a.MedalType.ToString()

            })
            .FirstOrDefaultAsync();
    }
}
