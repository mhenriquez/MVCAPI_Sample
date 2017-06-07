using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITD.Checkin.Models
{
    public class Photo
    {
        public int nid { get; set; }
        public string iso { get; set; }
        public byte[] photo { get; set; }
    }
}