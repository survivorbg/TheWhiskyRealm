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
}
