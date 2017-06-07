using ITD.WebApi.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace ITD.WebApi.Controllers.API
{
    public class ClosingTodayController : ApiController
    {
        // GET: api/closingtoday
        [HttpGet]
        public async Task<IHttpActionResult> GetExamsClosingToday()
        {
            using (var context = new KeonCenterContext())
            {
                var sql = "SELECT ISNULL(SUM(Students_Enrolled - Tests_Taken), 0) FROM tlTests WHERE Close_Date_Time BETWEEN GETDATE() AND CONVERT(CHAR(10),GETDATE(),120) + @TIME AND IsActive = 1";
                var count = await context.Database.SqlQuery<int>(sql, new SqlParameter("@TIME", " 23:59:59")).SingleOrDefaultAsync();

                if (count < 0)
                {
                    return NotFound();
                }

                var json = new { ClosingToday = count };
                return Ok(json);
            }
        }
    }
}
