using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TypyDniaApi.Model.Requests
{
    public class SeasonRequest
    {
        [Required]
        public string Years { get; set; }
        [Required]
        public string League { get; set; }
    }
}