using ITD.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Http;

namespace ITD.WebApi.Controllers.API
{
    public class TwitterController : ApiController
    {
        private static string oAuthConsumerKey = "TjoMoX23uTraV0f8YiuKw";
        private static string oAuthConsumerSecret = "Nd2UCtWDVNROeHXZ1NT1PcD8gr70vzQHeFQ2KJbXQQ";
        private static string oAuthUrl = "https://api.twitter.com/oauth2/token";
        private static string screenname = "ucfbusiness";

        public IHttpActionResult GetTwitterProfile()
        {
            #region Authentication Response

            var authHeaderFormat = "Basic {0}";

            var authHeader = string.Format(authHeaderFormat, Convert.ToBase64String(
                Encoding.UTF8.GetBytes(
                    Uri.EscapeDataString(oAuthConsumerKey) + ":" + Uri.EscapeDataString((oAuthConsumerSecret))
                    )
                ));

            var postBody = "grant_type=client_credentials";

            HttpWebRequest authRequest = (HttpWebRequest)WebRequest.Create(oAuthUrl);

            authRequest.Headers.Add("authorization", authHeader);
            authRequest.Method = "POST";
            authRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            authRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (Stream stream = authRequest.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                stream.Write(content, 0, content.Length);
            }

            authRequest.Headers.Add("Accept-Encoding", "gzip");

            WebResponse authResponse = authRequest.GetResponse();

            #endregion

            #region Deserialize Authentication Token into an Object

            TwitterAuthenticationToken twitAuthResponse;

            using (authResponse)
            {
                using (var reader = new StreamReader(authResponse.GetResponseStream()))
                {
                    var objectText = reader.ReadToEnd();
                    twitAuthResponse = JsonConvert.DeserializeObject<TwitterAuthenticationToken>(objectText);
                }
            }

            #endregion

            #region Json Response

            var twitJsonUrl = string.Format("https://api.twitter.com/1.1/users/show.json?screen_name={0}", screenname);

            HttpWebRequest twitJsonRequest = (HttpWebRequest)WebRequest.Create(twitJsonUrl);
            twitJsonRequest.Headers.Add("Authorization", string.Format("{0} {1}", twitAuthResponse.token_type, twitAuthResponse.access_token));
            twitJsonRequest.Method = "GET";

            WebResponse twitJsonResponse = twitJsonRequest.GetResponse();

            #endregion

            #region Deserialize Account Fields into an Object

            TwitterAccount twitResponse;

            using (authResponse)
            {
                using (var reader = new StreamReader(twitJsonResponse.GetResponseStream()))
                {
                    var objectText = reader.ReadToEnd();
                    twitResponse = JsonConvert.DeserializeObject<TwitterAccount>(objectText);
                }
            }

            #endregion

            return Ok(twitResponse);
        }
    }
}
