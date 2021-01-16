using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Linky.DataAccessLayer.Repositories.Abstract;
using Linky.Entities.Models;
using LinkyMVC.Models.InputModels;
using Microsoft.AspNet.Identity;

namespace LinkyMVC.Controllers
{
    [Authorize]
    public class LinksController : Controller
    {
        private readonly ILinkRepository _linkRepository;
        private readonly IMapper _mapper;

        public LinksController(ILinkRepository linkRepository, IMapper mapper)
        {
            _linkRepository = linkRepository;
            _mapper = mapper;
        }

        // GET: Links
        public async Task<ActionResult> Index()
        {
            var links = await _linkRepository.GetLinksAsync(User.Identity.GetUserId());
            return View(links);
        }

        // GET: Links/Create
        public ActionResult Create()
        {
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

            var link = await _linkRepository.GetLinkAsync(model.Label);
                
            if(link != null)
            {
                ViewBag.ErrorMessage = "A link with the specified label already exists";
                return View(model);
            }

            link = _mapper.Map<Link>(model);
            link.ApplicationUserId = User.Identity.GetUserId();

            var result = await _linkRepository.SaveLinkAsync(link);

            if(!result)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        // GET: Links/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var link = await _linkRepository.GetLinkAsync(id.Value);

            if (link.ApplicationUserId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

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

            var link = await _linkRepository.GetLinkAsync(id.Value);

            if (link == null)
            {
                return HttpNotFound();
            }

            if (link.ApplicationUserId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            var model = _mapper.Map<LinkUpdateModel>(link);
            return View(model);
        }

        // POST: Links/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Label,URL")] LinkUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var link = await _linkRepository.GetLinkAsync(model.Label);

            if (link != null)
            {
                ViewBag.ErrorMessage = "A link with the specified label already exists";
                return View(model);
            }

            link = await _linkRepository.GetLinkAsync(model.Id);

            if (link.ApplicationUserId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            _mapper.Map(model, link);

            var result = await _linkRepository.SaveLinkAsync(link);

            if (!result)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        // GET: Links/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var link = await _linkRepository.GetLinkAsync(id.Value);

            if (link == null)
            {
                return HttpNotFound();
            }

            if (link.ApplicationUserId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var link = await _linkRepository.GetLinkAsync(id);

            if (link == null)
            {
                return HttpNotFound();
            }

            if (link.ApplicationUserId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            var result = await _linkRepository.DeleteLinkAsync(link);

            if(!result)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }
    }
}
