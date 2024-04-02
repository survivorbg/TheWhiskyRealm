using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Article;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class ArticleService : IArticleService
{
    private readonly IRepository repo;

    public ArticleService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<bool> ArticleExistsAsync(int id)
    {
        return await repo
            .AllReadOnly<Article>()
            .AnyAsync(a=>a.Id == id);
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
            .Where(a=>a.Id == id)
            .Select(a => new ArticleDetailsViewModel
            {
                Id = a.Id,
                ArticleType = a.Type.ToString(),
                AuthorId = a.PublisherUser.Id,
                AuthorName = a.PublisherUser.UserName,
                Content = a.Content,
                DateCreated = a.DateCreated.ToString(),
                ImageUrl = a.ImageUrl,
                Title = a.Title
            })
            .FirstOrDefaultAsync();
    }
}
