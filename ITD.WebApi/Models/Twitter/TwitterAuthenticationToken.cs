
namespace ITD.WebApi.Models
{
    using System;
    using System.Collections.Generic;

    class TwitterAuthenticationToken
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
    }
}
