using TheWhiskyRealm.Core.Models.Article;

namespace TheWhiskyRealm.Core.Contracts;

public interface IArticleService
{
    Task<ICollection<ArticleAllViewModel>> GetAllArticlesAsync();
    Task<bool> ArticleExistsAsync(int id);
    Task<ArticleDetailsViewModel?> GetArticleDetailsAsync(int id);
    Task<bool> IsTheArticleAuthorAsync(string userId, int id);
    Task<ArticleEditViewModel?> GetArticleEditAsync(int id);
    Task EditArticleAsync(ArticleEditViewModel model);
    Task<int> AddArticleAsync(ArticleAddViewModel model, string userId);
    Task DeleteArticleAsync(int id);
}
