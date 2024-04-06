using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Comment;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Controllers;

public class CommentController : BaseController
{
    private readonly ICommentService commentService;
    private readonly IArticleService articleService;

    public CommentController(ICommentService commentService, IArticleService articleService)
    {
        this.commentService = commentService;
        this.articleService = articleService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(CommentAddViewModel model)
    {
        if(await articleService.ArticleExistsAsync(model.ArticleId) == false)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = User.Id();

        await commentService.AddCommentAsync(model,userId);

        return RedirectToAction("Details","Article", new { id = model.ArticleId });
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var comment = await commentService.GetCommentByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }
        var userId = User.Id();

        if (await commentService.GetCommentAuthorIdAsync(id) != userId)
        {
            return Unauthorized();
        }

        var article = await articleService.GetArticleDetailsAsync(comment.ArticleId);
        if(article == null)
        {
            return NotFound();
        }

        var model = new CommentEditViewModel()
        {
            Id = id,
            ArticleId = comment.ArticleId,
            Content = comment.Content,
            ArticleTitle = article.Title
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CommentEditViewModel model)
    {
        if (await commentService.CommentExistsAsync(model.Id) == false)
        {
            return NotFound();
        }

        if (await articleService.ArticleExistsAsync(model.ArticleId) == false)
        {
            return NotFound();
        }

        var userId = User.Id();

        if(await commentService.GetCommentAuthorIdAsync(model.Id) != userId)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await commentService.EditCommentAsync(model);

        return RedirectToAction("Details", "Article", new { id = model.ArticleId });
    }
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await commentService.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        
        if (await articleService.ArticleExistsAsync(comment.ArticleId) == false)
        {
            return NotFound();
        }

        var userId = User.Id();

        if (await commentService.GetCommentAuthorIdAsync(id) != userId)
        {
            return Unauthorized();
        }

        await commentService.DeleteCommentAsync(id);

        return RedirectToAction("Details", "Article", new { id = comment.ArticleId });
    }
}
