using TheWhiskyRealm.Core.Models.Article;

namespace TheWhiskyRealm.Core.Contracts;

public interface IArticleService
{
    Task<ICollection<ArticleAllViewModel>> GetAllArticlesAsync();
    Task<bool> ArticleExistsAsync(int id);
    Task<ArticleDetailsViewModel?> GetArticleDetailsAsync(int id);
}
