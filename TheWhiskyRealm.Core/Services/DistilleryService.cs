﻿using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Distillery;
using TheWhiskyRealm.Core.Models.Whisky.Add;
using TheWhiskyRealm.Core.Models.Whisky.WhiskyApi;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class DistilleryService : IDistilleryService
{
    private readonly IRepository repo;

    public DistilleryService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<int> AddDistilleryAsync(DistilleryFormViewModel model)
    {
        var distillery = new Distillery();
        if (model != null)
        {
            distillery.Name = model.Name;
            distillery.YearFounded = model.YearFounded;
            distillery.RegionId = model.RegionId;
            distillery.ImageUrl = model.ImageUrl;
            await repo.AddAsync(distillery);
            await repo.SaveChangesAsync();
        }
        return distillery.Id;
    }

    public async Task<bool> DistilleryExistByName(string name, int id = 0)
    {
        return await repo
            .AllReadOnly<Distillery>()
            .AnyAsync(d => d.Name.ToLower() == name.ToLower() && d.Id != id);
    }

    public async Task<bool> DistilleryExistsAsync(int id)
    {
        return await repo
            .AllReadOnly<Distillery>()
            .AnyAsync(d => d.Id == id);
    }

    public async Task EditDistilleryAsync(DistilleryFormViewModel model)
    {
        var distillery = await repo.GetByIdAsync<Distillery>(model.Id);
        if (distillery != null)
        {
            distillery.YearFounded = model.YearFounded;
            distillery.RegionId = model.RegionId;
            distillery.ImageUrl = model.ImageUrl;
            distillery.Name = model.Name;

            await repo.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<DistilleryAddWhiskyViewModel>> GetAllDistilleriesAsync()
    {
        return await repo
            .AllReadOnly<Distillery>()
            .Select(d => new DistilleryAddWhiskyViewModel()
            {
                DistilleryId = d.Id,
                Name = d.Name,
                Country = d.Region.Country.Name
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<DistilleryRegionViewModel>> GetAllDistilleriesAsync(int regionId)
    {
        return await repo
           .AllReadOnly<Distillery>()
           .Where(d => d.RegionId == regionId)
           .Select(d => new DistilleryRegionViewModel()
           {
               Id = d.Id,
               Name = d.Name,
               YearFounded = d.YearFounded
           })
           .ToListAsync();
    }

    public async Task<IEnumerable<DistilleryViewModel>> GetAllDistilleriesAsync(int currentPage, int pageSize, string sortOrder)
    {
        var distilleries = repo.AllReadOnly<Distillery>();

        switch (sortOrder)
        {
            case "name_desc":
                distilleries = distilleries.OrderByDescending(d => d.Name);
                break;
            case "Region":
                distilleries = distilleries.OrderBy(d => d.Region.Name);
                break;
            case "region_desc":
                distilleries = distilleries.OrderByDescending(d => d.Region.Name);
                break;
            case "Country":
                distilleries = distilleries.OrderBy(d => d.Region.Country.Name);
                break;
            case "country_desc":
                distilleries = distilleries.OrderByDescending(d => d.Region.Country.Name);
                break;
            case "Year":
                distilleries = distilleries.OrderBy(d => d.YearFounded);
                break;
            case "year_desc":
                distilleries = distilleries.OrderByDescending(d => d.YearFounded);
                break;
            default:
                distilleries = distilleries.OrderBy(d => d.Id);
                break;
        }

        return await distilleries
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .Select(d => new DistilleryViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Country = d.Region.Country.Name,
                Region = d.Region.Name,
                YearFounded = d.YearFounded
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<DistilleryApiModel>> GetAllDistilleriesForApi()
    {
        return await repo
           .AllReadOnly<Distillery>()
           .Select(d => new DistilleryApiModel()
           {
               Name = d.Name,
               Country = d.Region.Country.Name,
               Region = d.Region.Name,
               YearFounded = d.YearFounded,
               Id = d.Id
           })
           .ToListAsync();
    }

    public async Task<DistilleryFormViewModel?> GetDistilleryByIdAsync(int id)
    {
        return await repo
            .All<Distillery>()
            .Where(d => d.Id == id)
            .Select(d => new DistilleryFormViewModel
            {
                Id = d.Id,
                Name = d.Name,
                ImageUrl = d.ImageUrl,
                RegionId = d.RegionId,
                YearFounded = d.YearFounded
            })
            .FirstOrDefaultAsync();
    }

    public async Task<DistilleryDetailsApiModel?> GetDistilleryDetailsForApi(int id)
    {
        return await repo
            .All<Distillery>()
            .Where(d => d.Id == id)
            .Select(d => new DistilleryDetailsApiModel
            {
                Id = d.Id,
                Name = d.Name,
                YearFounded = d.YearFounded,
                Country = d.Region.Country.Name,
                Region = d.Region.Name,
                Whiskies = d.Whiskies.Select(w => new WhiskyApiModel
                {
                    Id = w.Id,
                    Name = w.Name,
                    Age = w.Age,
                    AlcoholPercentage = w.AlcoholPercentage,
                    Description = w.Description,
                    WhiskyType = w.WhiskyType.Name,
                    Distillery = w.Distillery.Name,
                    Country = w.Distillery.Region.Country.Name,
                    Region = w.Distillery.Region.Name
                })
            })
            .FirstOrDefaultAsync();
    }

    public async Task<DistilleryInfoModel?> GetDistilleryInfoAsync(int id)
    {
        var distillery = await repo
            .AllReadOnly<Distillery>()
            .Include(d => d.Region)
            .ThenInclude(r => r.Country)
            .Where(d => d.Id == id)
            .FirstOrDefaultAsync();

        if (distillery == null)
        {
            return null;
        }

        var result = new DistilleryInfoModel
        {
            Id = id,
            Name = distillery.Name,
            Country = distillery.Region.Country.Name,
            Region = distillery.Region.Name,
            YearFounded = distillery.YearFounded,
            ImageUrl = distillery.ImageUrl
        };

        return result;
    }

    public async Task<int> GetTotalDistilleriesAsync()
    {
        return await repo
            .AllReadOnly<Distillery>()
            .CountAsync();
    }
}
