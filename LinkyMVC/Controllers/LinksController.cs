using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Linky.Entities.Models;
using Linky.Services.Abstract;
using LinkyMVC.Models.InputModels;
using LinkyMVC.Models.OutputModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace LinkyMVC.Controllers
{
    [Authorize]
    public class LinksController : Controller
    {
        private readonly ILinkService _linkService;
        private readonly IMapper _mapper;

        public LinksController(ILinkService linkService, IMapper mapper)
        {
            _linkService = linkService;
            _mapper = mapper;
        }

        // GET: Links
        public async Task<ActionResult> Index()
        {
            var links = await _linkService.GetUserLinksAsync(User.Identity.GetUserId());
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var link = _mapper.Map<Link>(model);
            var response = await _linkService.AddLinkAsync(link, User.Identity.GetUserId());

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return View(model);
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

            var link = await _linkService.FindByIdAsync(id.Value);

            if (link.ApplicationUserId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            if (link == null)
            {
                return HttpNotFound();
            }

            var countryData = _mapper.Map<IEnumerable<CountryDataPoint>>(link.CountryCounters);
            ViewBag.CountryData = JsonConvert.SerializeObject(countryData);

            var dailyData = _mapper.Map<IEnumerable<DailyDataPoint>>(_linkService.GetDailyStatistics(link, 7));
            ViewBag.DailyData = JsonConvert.SerializeObject(dailyData);

            return View(link);
        }

        // GET: Links/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var link = await _linkService.FindByIdAsync(id.Value);

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

            var link = await _linkService.FindByIdAsync(model.Id);
            if (link.ApplicationUserId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            _mapper.Map(model, link);
            var response = await _linkService.EditLinkAsync(link);

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return View(model);
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

            var link = await _linkService.FindByIdAsync(id.Value);

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
            var link = await _linkService.FindByIdAsync(id);

            if (link == null)
            {
                return HttpNotFound();
            }

            if (link.ApplicationUserId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            var response = await _linkService.DeleteLinkAsync(link);

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<ActionResult> RedirectLink(string url)
        {
            var response = await _linkService.HandleClickAsync(url, HttpContext.Request.UserHostAddress);

            if (!response.Success)
            {
                ViewBag.ErrorMessage = response.ErrorMessage;
                return View("Error");
            }

            return Redirect(response.URL);
        }
    }
}
