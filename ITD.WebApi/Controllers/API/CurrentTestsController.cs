using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ITD.WebApi.Models;

namespace ITD.WebApi.Controllers
{
    public class CurrentTestsController : ApiController
    {
        private KeonCenterContext db = new KeonCenterContext();

        // GET: api/CurrentTests
        public IQueryable<CurrentTests> GetCurrentTests()
        {
            IQueryable<ITD.WebApi.Models.CurrentTests> tests = db.CurrentTests.OrderBy(p => p.TestCloses);
            foreach (CurrentTests test in tests)
                test.GetSections();
            return tests;
        }

        // GET: api/CurrentTests/5
        [ResponseType(typeof(CurrentTests))]
        public async Task<IHttpActionResult> GetCurrentTests(int id)
        {
            CurrentTests currentTests = await db.CurrentTests.FindAsync(id);
            currentTests.GetSections();
            if (currentTests == null)
            {
                return NotFound();
            }

            return Ok(currentTests);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CurrentTestsExists(int id)
        {
            return db.CurrentTests.Count(e => e.TestID == id) > 0;
        }
    }
}