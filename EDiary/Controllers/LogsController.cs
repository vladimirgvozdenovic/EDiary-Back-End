using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EDiary.Controllers
{
    [RoutePrefix("ediary/logs")]
    public class LogsController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            string path = HttpContext.Current.Server.MapPath("~/logs/") + "app-log.txt";
            List<String> logs = new List<String>();

            StreamReader reader = new StreamReader(path);
            while (true)
            {
                String line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }

                logs.Add(line);
            }

            return Ok(logs);
        }
    }
}
