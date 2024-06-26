﻿using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Article;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Enums;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class ArticleService : IArticleService
{
    private readonly IRepository repo;

    public ArticleService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<int> AddArticleAsync(ArticleAddViewModel model, string userId)
    {
        var article = new Article()
        {
            DateCreated = DateTime.Now,
            Content = model.Content,
            ImageUrl = model.ImageUrl,
            PublisherUserId = userId,
            Title = model.Title,
            Type = (ArticleType)Enum.Parse(typeof(ArticleType), model.ArticleType)
        };

        await repo.AddAsync(article);
        await repo.SaveChangesAsync();

        return article.Id;
    }


    public async Task<bool> ArticleExistsAsync(int id)
    {
        return await repo
            .AllReadOnly<Article>()
            .AnyAsync(a => a.Id == id);
    }

    public async Task DeleteArticleAsync(int id)
    {
        var article = await repo.GetByIdAsync<Article>(id);
        if(article != null)
        {
            repo.Delete(article);
        }
        await repo.SaveChangesAsync();
    }

    public async Task EditArticleAsync(ArticleEditViewModel model)
    {
        var article = await repo.GetByIdAsync<Article>(model.Id);

        if (article != null)
        {
            article.Title = model.Title;
            article.Content = model.Content;
            article.ImageUrl = model.ImageUrl;
            article.Type = (ArticleType)Enum.Parse(typeof(ArticleType), model.ArticleType);
        }
        await repo.SaveChangesAsync();
    }

    public async Task<ICollection<ArticleAllViewModel>> GetAllArticlesAsync()
    {
        return await repo
            .AllReadOnly<Article>()
            .Select(a => new ArticleAllViewModel
            {
                ArticleType = a.Type.ToString(),
                Id = a.Id,
                ImageUrl = a.ImageUrl,
                Title = a.Title
            })
            .ToListAsync();

    }

    public async Task<ArticleDetailsViewModel?> GetArticleDetailsAsync(int id)
    {
        return await repo
            .AllReadOnly<Article>()
            .Where(a => a.Id == id)
            .Select(a => new ArticleDetailsViewModel
            {
                Id = a.Id,
                ArticleType = a.Type.ToString(),
                AuthorId = a.PublisherUser.Id,
                AuthorName = a.PublisherUser.UserName,
                Content = a.Content,
                DateCreated = a.DateCreated.ToString("g"),
                ImageUrl = a.ImageUrl,
                Title = a.Title
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ArticleEditViewModel?> GetArticleEditAsync(int id)
    {
        return await repo
            .All<Article>()
            .Where(a => a.Id == id)
            .Select(a => new ArticleEditViewModel
            {
                ArticleType = a.Type.GetDisplayName(),
                Content = a.Content,
                Id = a.Id,
                ImageUrl = a.ImageUrl,
                Title = a.Title
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<ArticleAllViewModel>> GetUserArticlesAsync(string userId)
    {
        return await repo
            .AllReadOnly<Article>()
            .Where(a=>a.PublisherUserId == userId)
            .Select(a => new ArticleAllViewModel
            {
                ArticleType = a.Type.ToString(),
                Id = a.Id,
                ImageUrl = a.ImageUrl,
                Title = a.Title
            })
            .ToListAsync();
    }

    public async Task<bool> IsTheArticleAuthorAsync(string userId, int id)
    {
        return await repo
            .AllReadOnly<Article>()
            .AnyAsync(a => a.Id == id && a.PublisherUserId == userId);
    }
}
