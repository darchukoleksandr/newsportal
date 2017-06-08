using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Web_MVC.Models
{
    public class RecentPosts
    {
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
    }

    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int ArticleId { get; set; }

        [Required]
        [Display(Name = "Назва новини")]
        public string ArticleName { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Новина")]
        [DataType(DataType.Html)]
        public string ArticleText { get; set; }

        [Display(Name = "Дата створення")]
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Дата підтвердження")]
        [HiddenInput(DisplayValue = false)]
        public DateTime? ApprovedDate { get; set; }

        public virtual ApplicationUser Approver { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}