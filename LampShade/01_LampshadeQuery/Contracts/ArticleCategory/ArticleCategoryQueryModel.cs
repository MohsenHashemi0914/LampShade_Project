﻿using _01_LampshadeQuery.Contracts.Article;

namespace _01_LampshadeQuery.Contracts.ArticleCategory
{
    public class ArticleCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Description { get; set; }
        public byte ShowOrder { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public List<string> KeywordList { get; set; }
        public string MetaDescription { get; set; }
        public string? CanonicalAddress { get; set; }
        public short ArticlesCount { get; set; }
        public List<ArticleQueryModel> Articles { get; set; } = new();
    }
}
