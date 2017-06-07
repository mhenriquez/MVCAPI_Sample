using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using ITD.WebApi.Models;

namespace ITD.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private KeonCenterContext db = new KeonCenterContext();

        // GET: Home
        public async Task<ActionResult> Index()
        {
            IEnumerable<CurrentTests> model = await db.CurrentTests.ToListAsync();

            foreach (CurrentTests test in model)
            {
                test.GetSections();
            }

            return View(model);
        }

        // GET: Home/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            //returns bad request if value is empty or is a negative number
            if (id == null || id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CurrentTests model = await db.CurrentTests.FindAsync(id);

            //get course sections
            try
            {
                model.GetSections();
            }
            catch
            {
                return HttpNotFound();
            }

            //check if model is null
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
