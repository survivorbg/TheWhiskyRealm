using TheWhiskyRealm.Core.Models.Article;

namespace TheWhiskyRealm.Core.Contracts;

public interface ICommentService
{
    Task<ICollection<CommentViewModel>> GetCommentsForArticleAsync(int id);
    Task AddCommentAsync(CommentAddViewModel model,string userId);
    Task<CommentViewModel?> GetCommentByIdAsync(int id);
    Task<bool> CommentExistsAsync(int id);
    Task<string> GetCommentAuthorIdAsync(int id);
    Task EditCommentAsync(CommentEditViewModel model);
    Task DeleteCommentAsync(int id);
}
