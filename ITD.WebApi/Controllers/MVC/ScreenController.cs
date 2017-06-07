using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ITD.WebApi.Models;

namespace ITD.WebApi.Controllers
{
    public class ScreenController : Controller
    {
        private KeonCenterContext db = new KeonCenterContext();
        
        // GET: Screen
        public async Task<ActionResult> Index()
        {
            IEnumerable<ITD.WebApi.Models.CurrentTests> currentTests = await db.CurrentTests.OrderBy(p => p.TestCloses).ToListAsync();

            foreach (CurrentTests test in currentTests)
            {
                test.GetSections();
            }

            return View(currentTests);
        }

        [HttpGet]
        public async Task<PartialViewResult> Exams()
        {
            string[] sqlString = new string[] {
                "SELECT Value FROM  tlSettings WHERE Property = 'TestSubmission:StartTimeWeekday'",
                "SELECT Value FROM  tlSettings WHERE Property = 'TestSubmission:EndTimeWeekday'",
                "SELECT Value FROM  tlSettings WHERE Property = 'TestSubmission:StartTimeFriday'",
                "SELECT Value FROM  tlSettings WHERE Property = 'TestSubmission:EndTimeFriday'",
                "SELECT Value FROM  tlSettings WHERE Property = 'TestSubmission:StartTimeSaturday'",
                "SELECT Value FROM  tlSettings WHERE Property = 'TestSubmission:EndTimeSaturday'"};

            SqlDataArray times = new SqlDataArray();
            times.FillArrayValues(sqlString);

            ViewData["StartTimeWeekday"] = times.SqlArray[0];
            ViewData["EndTimeWeekday"] = times.SqlArray[1];
            ViewData["StartTimeFriday"] = times.SqlArray[2];
            ViewData["EndTimeFriday"] = times.SqlArray[3];
            ViewData["EndTimeSaturday"] = times.SqlArray[4];
            ViewData["EndTimeSaturday"] = times.SqlArray[5];
            
            IEnumerable<ITD.WebApi.Models.CurrentTests> model = await db.CurrentTests.OrderBy(p => p.TestCloses).ToListAsync();
            foreach (CurrentTests test in model)
            {
                test.GetSections();
            }
            return PartialView("_CurrentExams", model);
        }

        [HttpGet]
        public PartialViewResult Stats()
        {
            #region DateTime

            DateTime endOfWeek = DateTime.Today;

            do
            {
                endOfWeek = endOfWeek.AddDays(1);
            } while (endOfWeek.DayOfWeek != DayOfWeek.Saturday);

            #endregion

            #region SQL

            string[] sqlString = new string[] {
                //- Seats Available:
                "SELECT (SELECT Value FROM  tlSettings WHERE Property = 'AvailableSeats') - (SELECT COUNT(DISTINCT NID) FROM  vCurrent_Patrons)", 
                //- Tests Ending Today:
                "SELECT ISNULL(SUM(Students_Enrolled - Tests_Taken), 0) FROM tlTests WHERE Close_Date_Time BETWEEN GETDATE() AND (CONVERT(CHAR(10),GETDATE(),120) + ' 23:59:59') AND (dbo.tlTests.IsActive = 1)",
                //- Tests Ending this Week:
                "SELECT ISNULL(SUM(Students_Enrolled - Tests_Taken), 0) FROM dbo.tlTests WHERE Close_Date_Time BETWEEN GETDATE() AND '" + endOfWeek.ToShortDateString() + " 23:59:59'"};

            ModelState.Clear();

            SqlDataArray stats = new SqlDataArray();
            stats.FillArrayValues(sqlString);

            ViewData["SeatsAvailable"] = stats.SqlArray[0];
            ViewData["ClosingToday"] = stats.SqlArray[1];
            ViewData["ClosingWeek"] = stats.SqlArray[2];

            #endregion

            return PartialView("_CurrentStats");
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
