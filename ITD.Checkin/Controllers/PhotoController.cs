using ITD.Checkin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace ITD.Checkin.Controllers
{
    public class PhotoController : ApiController
    {
        [HttpGet]
        [TypeConverter(typeof(Photo))]
        public async Task<HttpResponseMessage> Get(string nid, string iso)
        {
            using (var context = new KeonCenterContext())
            {
                string sql = "SQL WHERE NID = @NID AND ISO = @ISO";
                Photo student = await context.Database.SqlQuery<Photo>(sql, new SqlParameter("@NID", nid), new SqlParameter("@ISO", iso)).FirstOrDefaultAsync();

                byte[] data = student.photo;

                var stream = new MemoryStream(data);

                var httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                httpResponse.Content = new ByteArrayContent(stream.ToArray());
                httpResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                return httpResponse;
            }
        }
    }
}
