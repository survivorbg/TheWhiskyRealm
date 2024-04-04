using TheWhiskyRealm.Core.Models.Article;

namespace TheWhiskyRealm.Core.Contracts;

public interface ICommentService
{
    Task<ICollection<CommentViewModel>> GetCommentsForArticleAsync(int id);
    Task AddCommentAsync(CommentAddViewModel model,string userId);

}
