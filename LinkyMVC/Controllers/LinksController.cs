using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Linky.DataAccessLayer.Repositories.Abstract;
using Linky.Entities.Models;
using LinkyMVC.Models.InputModels;
using Microsoft.AspNet.Identity;

namespace LinkyMVC.Controllers
{
    public class LinksController : Controller
    {
        private readonly ILinkRepository _repository;
        private readonly IMapper _mapper;

        public LinksController(ILinkRepository linkRepository, IMapper mapper)
        {
            _repository = linkRepository;
            _mapper = mapper;
        }

        // GET: Links
        public async Task<ActionResult> Index()
        {
            var links = await _repository.GetLinksAsync(User.Identity.GetUserId());
            return View(links);
        }

        // GET: Links/Create
        public ActionResult Create()
        {
            //ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: Links/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Label,URL")] LinkInputModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var link = _mapper.Map<Link>(model);
            link.ApplicationUserId = User.Identity.GetUserId();

            var result = await _repository.SaveLinkAsync(link);

            if(!result)
            {
                return View("Error");
            }

            return RedirectToAction("Index");


            //ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "Email", link.ApplicationUserId);

        }

        /*
        // GET: Links/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = await db.Links.FindAsync(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // GET: Links/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = await db.Links.FindAsync(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "Email", link.ApplicationUserId);
            return View(link);
        }

        // POST: Links/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Label,URL,Clicks,CreatedAt,ApplicationUserId")] Link link)
        {
            if (ModelState.IsValid)
            {
                db.Entry(link).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "Email", link.ApplicationUserId);
            return View(link);
        }

        // GET: Links/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = await db.Links.FindAsync(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Link link = await db.Links.FindAsync(id);
            db.Links.Remove(link);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        */
    }
}
