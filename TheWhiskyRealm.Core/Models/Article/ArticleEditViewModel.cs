using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
using static TheWhiskyRealm.Infrastructure.Constants.ArticleDataConstants;


namespace TheWhiskyRealm.Core.Models.Article;

public class ArticleEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(ArticleMaxTitleLength, MinimumLength = ArticleMinTitleLength, ErrorMessage = LengthMessage)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(ArticleMaxContentLength, MinimumLength = ArticleMinContentLength, ErrorMessage = LengthMessage)]
    public string Content { get; set; } = string.Empty;

    [Required(ErrorMessage = RequiredMessage)]
    [Display(Name = "Image URL")]
    public string ImageUrl { get; set; } = string.Empty;


    [Required(ErrorMessage = RequiredMessage)]
    [Display(Name = "Article Type")]
    public string ArticleType { get; set; } = string.Empty;

    public IEnumerable<string> ArticleTypeOptions { get; set; } = Enumerable.Empty<string>();
}