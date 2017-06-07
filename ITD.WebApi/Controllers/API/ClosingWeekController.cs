using ITD.WebApi.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace ITD.WebApi.Controllers.API
{
    public class ClosingWeekController : ApiController
    {
        // GET: api/ClosingWeek
        [HttpGet]
        public async Task<IHttpActionResult> GetExamsClosingThisWeek()
        {
            DateTime endOfWeek = DateTime.Today;
            do
            {
                endOfWeek = endOfWeek.AddDays(1);
            } while (endOfWeek.DayOfWeek != DayOfWeek.Saturday);

            using (var context = new KeonCenterContext())
            {
                var sql = "SELECT ISNULL(SUM(Students_Enrolled - Tests_Taken), 0) FROM dbo.tlTests WHERE Close_Date_Time BETWEEN GETDATE() AND @DATETIME";
                var count = await context.Database.SqlQuery<int>(sql, new SqlParameter("@DATETIME", endOfWeek.ToShortDateString() + " 23:59:59")).SingleOrDefaultAsync();

                if (count < 0)
                {
                    return NotFound();
                }

                var json = new { ClosingWeek = count };
                return Ok(json);
            }
        }
    }
}
