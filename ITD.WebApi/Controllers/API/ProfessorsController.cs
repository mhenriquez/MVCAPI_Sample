using ITD.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ITD.WebApi.Controllers.API
{
    public class ProfessorsController : ApiController
    {
        // GET: api/professors
        [HttpGet]
        public IHttpActionResult GetProfessors()
        {
            using (var context = new KeonCenterContext())
            {
                var sql = "SELECT Professor_ID as ID, First_Name, Last_Name, Email FROM tlProfessors WHERE Professor_ID IN (SELECT DISTINCT Professor_ID FROM tlProfessors_Courses WHERE Term = (SELECT Value FROM tlSettings WHERE Property = @TERM) AND Course_ID IN (SELECT DISTINCT Course_ID FROM tlTests_Courses WHERE Test_ID IN (SELECT Test_ID FROM tlTests WHERE IsActive = 1))) ORDER BY Last_Name";
                List<ktcProfessor> model = context.Database.SqlQuery<ktcProfessor>(sql, new SqlParameter("@TERM", "CurrentTerm")).ToList();
                return Ok(model);
            }
        }

        // GET: api/professors/33981
        [HttpGet]
        [ResponseType(typeof(ktcProfessor))]
        public async Task<IHttpActionResult> GetProfessor(int id)
        {
            using (var context = new KeonCenterContext())
            {
                var sql = "SELECT Professor_ID as ID, First_Name, Last_Name, Email FROM tlProfessors WHERE Professor_ID IN (SELECT DISTINCT Professor_ID FROM tlProfessors_Courses WHERE Professor_ID = @PID AND Term = (SELECT Value FROM tlSettings WHERE Property = @TERM) AND Course_ID IN (SELECT DISTINCT Course_ID FROM tlTests_Courses WHERE Test_ID IN (SELECT Test_ID FROM tlTests WHERE IsActive = 1)))";
                ktcProfessor model = await context.Database.SqlQuery<ktcProfessor>(sql, new SqlParameter("@PID", id), new SqlParameter("@TERM", "CurrentTerm")).SingleOrDefaultAsync();

                if (model == null)
                {
                    return NotFound();
                }

                return Ok(model);
            }
        }
    }
}
