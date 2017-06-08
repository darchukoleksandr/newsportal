using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Web_MVC.Models;

namespace Web_MVC.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly ApplicationUserManager _userManager;
        private readonly int _pageSize = 5;

        public ApplicationUserManager UserManager => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public ArticlesController()
        {
        }

        public ArticlesController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET: Articles
        public ActionResult Index(int? page)
        {
            if (page == null)
                page = 1;
            var countApprovedArticles = _db.Articles.Count(article => article.ApprovedDate != null);
            if (countApprovedArticles % _pageSize == 0)
                ViewBag.pageCount = countApprovedArticles / _pageSize;
            else
                ViewBag.pageCount = countApprovedArticles / _pageSize + 1;

            return View(_db.Articles
                .Where(article => article.ApprovedDate != null)
                .OrderBy(article => article.ArticleId)
                .Skip((int) ((page - 1) * _pageSize))
                .Take(_pageSize)
                .ToList());
        }

        // GET: Approve
        [Authorize(Roles = "Admin")]
        public ActionResult UnApproved(int? page)
        {
            if (page == null)
                page = 1;
            var countUnapprovedArticles = _db.Articles.Count(article => article.ApprovedDate == null);
            if (countUnapprovedArticles % _pageSize == 0)
                ViewBag.pageCount = countUnapprovedArticles / _pageSize;
            else
                ViewBag.pageCount = countUnapprovedArticles / _pageSize + 1;

            return View("Index", _db.Articles
                .Where(article => article.ApprovedDate == null)
                .OrderBy(article => article.ArticleId)
                .Skip((int)((page - 1) * _pageSize))
                .Take(_pageSize)
                .ToList());
        }

        public ActionResult UserNews(int? page, string user)
        {
            return View("Index", _db.Articles
                .Where(article => article.Author.Email == user)
                .ToList());
        }

        [Authorize]
        public ActionResult MyNews(int? page)
        {
            var userId = User.Identity.GetUserId();
            if (page == null)
                page = 1;
            var userCountArticles = _db.Articles.Count(article => article.Author.Id == userId);
            if (userCountArticles % _pageSize == 0)
                ViewBag.pageCount = userCountArticles / _pageSize;
            else
                ViewBag.pageCount = userCountArticles / _pageSize + 1;

            return View("Index", _db.Articles
                .Where(article => article.Author.Id == userId)
                .OrderBy(article => article.ArticleId)
                .Skip((int)((page - 1) * _pageSize))
                .Take(_pageSize)
                .ToList());
        }

        [HttpGet]
        public ActionResult Search(string name)
        {
            return View("Index", _db.Articles
                .Where(article => article.ArticleName.Contains(name))
                .ToList());
        }

        public PartialViewResult RecentNewsPartial()
        {
            return PartialView("_RecentNews", _db.Articles.Where(article => article.ApprovedDate != null).Take(5).Select(article => new RecentPosts { ArticleId = article.ArticleId, ArticleName = article.ArticleName }).ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Approve(int id)
        {
            var approverId = User.Identity.GetUserId();
            var applicationUser = await _db.Users.FirstAsync(user => user.Id == approverId);
            var article = await _db.Articles.FindAsync(id);
            if (article != null)
            {
                article.ApprovedDate = DateTime.Now;
                article.Approver = applicationUser;
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UnApprove(int id)
        {
            var article = await _db.Articles.FindAsync(id);
            if (article != null)
            {
                article.ApprovedDate = null;
            }
            _db.SaveChanges();

            return RedirectToAction("Index");
//            return View("Index", _db.Articles.Where(articles => articles.Approver == null).ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Article article = _db.Articles.Find(id);

            if (article == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(article);
        }

        // GET: Articles/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var currentUserId = User.Identity.GetUserId();
            article.Author = _db.Users.FirstOrDefault(x => x.Id == currentUserId);
            article.CreatedDate = DateTime.Now;
            _db.Articles.Add(article);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Articles/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleId,ArticleName,ArticleText,CreatedDate")] Article article)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(article).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userId = User.Identity.GetUserId();
            Article article = _db.Articles.Find(id);
            if (article == null)
                return RedirectToAction("Index");
            if (article.Author.Id != userId && !User.IsInRole("Admin"))
                return RedirectToAction("Index");
            _db.Articles.Remove(article);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
