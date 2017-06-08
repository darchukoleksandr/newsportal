using System.IO;
using System.Linq;
using System.Web.Mvc;
using Web_MVC.Models;

namespace Web_MVC.Controllers
{
    public class SharedTestController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [HttpGet]
        public PartialViewResult RecentNewsPartial()
        {
            return PartialView("_RecentNews", _db.Articles.Where(article => article.ApprovedDate != null).Take(5).Select(article => new RecentPosts { ArticleId = article.ArticleId, ArticleName = article.ArticleName }).ToList());
        }
    }
}