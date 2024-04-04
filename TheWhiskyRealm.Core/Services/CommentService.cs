using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Article;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class CommentService : ICommentService
{
    private readonly IRepository repo;

    public CommentService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task AddCommentAsync(CommentAddViewModel model, string userId)
    {
        if (model != null)
        {
            Comment comment = new Comment()
            {
                Content = model.Content,
                UserId = userId,
                ArticleId = model.ArticleId,
                PostedDate = DateTime.Now,
            };
            await repo.AddAsync(comment);
        }
        await repo.SaveChangesAsync();
    }

    public async Task<bool> CommentExistsAsync(int id)
    {
        return await repo //TODO Check everywhere for missed async/await
            .AllReadOnly<Comment>()
            .AnyAsync(c=>c.Id== id);
    }

    public async Task DeleteCommentAsync(int id)
    {
        var comment = await repo.GetByIdAsync<Comment>(id);
        if (comment != null)
        {
            await repo.DeleteById<Comment>(id);
        }
        await repo.SaveChangesAsync();
    }

    public async Task EditCommentAsync(CommentEditViewModel model)
    {
        var comment = await repo.GetByIdAsync<Comment>(model.Id);
        if(comment != null)
        {
            comment.Content = model.Content;
        }
        await repo.SaveChangesAsync();
    }

    public async Task<string> GetCommentAuthorIdAsync(int id)
    {
        var comment = await repo.GetByIdAsync<Comment>(id);
        if(comment != null)
        {
            return comment.UserId;
        }
        return string.Empty;
    }

    public async Task<CommentViewModel?> GetCommentByIdAsync(int id)
    {
        return await repo
             .All<Comment>()
             .Where(c => c.Id == id)
             .Select(c => new CommentViewModel
             {
                 ArticleId = c.Article.Id,
                 AuthorName = c.User.UserName,
                 Content = c.Content,
                 DatePosted = c.PostedDate.ToString(),
                 Id = c.Id
             })
             .FirstOrDefaultAsync();
    }

    public async Task<ICollection<CommentViewModel>> GetCommentsForArticleAsync(int id)
    {
        return await repo
            .All<Comment>()
            .Where(c => c.ArticleId == id)
            .OrderByDescending(c => c.PostedDate)
            .Select(c => new CommentViewModel
            {
                ArticleId = c.ArticleId,
                AuthorName = c.User.UserName,
                Content = c.Content,
                DatePosted = c.PostedDate.ToString("g"),
                Id = c.Id
            })

            .ToListAsync();
    }
}
