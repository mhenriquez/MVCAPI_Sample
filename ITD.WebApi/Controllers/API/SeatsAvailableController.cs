using ITD.WebApi.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace ITD.WebApi.Controllers.API
{
    public class SeatsAvailableController : ApiController
    {
        // GET: api/SeatsAvailable
        [HttpGet]
        public async Task<IHttpActionResult> GetSeatsAvailable()
        {
            using (var context = new KeonCenterContext())
            {
                var sql = "SELECT (SELECT Value FROM  tlSettings WHERE Property = @PROP) - (SELECT COUNT(DISTINCT NID) FROM  vCurrent_Patrons)";
                var count = await context.Database.SqlQuery<int>(sql, new SqlParameter("@PROP", "AvailableSeats")).SingleOrDefaultAsync();

                if (count < 0)
                {
                    return NotFound();
                }

                var json = new { SeatsAvailable = count };
                return Ok(json);
            }
        }
    }
}
