using ITD.Checkin.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ITD.Checkin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            using (var context = new KeonCenterContext())
            {
                string sql = "SELECT WHERE ID = 0";
                return View(await context.Database.SqlQuery<Appointment>(sql).ToListAsync());
            }
        }

        // GET: /Details/123456
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new KeonCenterContext())
            {
                string sql = "SELECT WHERE ID = @STID";
                var appointment = await context.Database.SqlQuery<Appointment>(sql, new SqlParameter("@STID", id)).SingleOrDefaultAsync();

                if (appointment == null)
                {
                    return HttpNotFound();
                }

                return View(appointment);
            }
        }

        // GET: /Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: /Edit/12345
        public ActionResult Edit(int? id)
        {
            return RedirectToAction("Index");
        }

        // POST: /Edit/12345
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                using (var context = new KeonCenterContext())
                {
                    string sql = "UPDATE ID = @STID";
                    context.Database.ExecuteSqlCommand(sql, new SqlParameter("@STID", id));
                    await context.SaveChangesAsync();
                }
            }

            //HttpResponse msg = new HttpResponse(TextWriter.Null);
            //msg.Write("Hello World!!");

            return RedirectToAction("Index");
        }

        // GET: /Delete/12345
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: /Delete/12345
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
