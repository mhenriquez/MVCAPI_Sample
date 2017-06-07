using ITD.Checkin.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ITD.Checkin.Controllers
{
    public class RecentController : ApiController
    {
        // GET: api/Checkin
        public IHttpActionResult Get()
        {
            using (var context = new KeonCenterContext())
            {
                string sql = "SQL WHERE @DATETIME";
                var appointment = context.Database.SqlQuery<Appointment>(sql, new SqlParameter("@DATETIME", "2016-05-04 00:00:00")).ToList();
                return Ok(appointment);
            }
        }
    }
}
