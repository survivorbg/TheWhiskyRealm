﻿using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Infrastructure.Constants.CommentDataConstants;
using static TheWhiskyRealm.Core.Constants.MessageConstants;

namespace TheWhiskyRealm.Core.Models.Article
{
    public class CommentEditViewModel
    {
        public int Id { get; set; }    

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CommentMaxContentLength, MinimumLength = CommentMinContentLength, ErrorMessage = LengthMessage)]
        public string Content { get; set; } = string.Empty;

        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; } = string.Empty;
    }
}