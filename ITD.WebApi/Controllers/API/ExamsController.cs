using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;
using ITD.WebApi.Models;

namespace ITD.WebApi.Controllers.API
{
    public class ExamsController : ApiController
    {
        private KeonCenterContext db = new KeonCenterContext();

        // GET: api/exams
        public IQueryable<CurrentTests> Get()
        {
            IQueryable<CurrentTests> currentTests = db.CurrentTests.OrderBy(p => p.TestCloses);

            foreach (CurrentTests test in currentTests)
            {
                test.GetSections();
            }

            return currentTests;
        }

        // GET: api/exams/7006
        [ResponseType(typeof(CurrentTests))]
        public async Task<IHttpActionResult> Get(int id)
        {
            CurrentTests currentTests = await db.CurrentTests.FindAsync(id);

            if (currentTests == null)
            {
                return NotFound();
            }

            currentTests.GetSections();

            return Ok(currentTests);
        }
    }
}
