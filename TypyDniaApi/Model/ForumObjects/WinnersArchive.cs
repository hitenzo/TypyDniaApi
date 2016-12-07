using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TypyDniaApi.Model.ForumObjects
{
    public class WinnersArchive
    {
        public Dictionary<DateTime, MonthResult> MonthlyResults { get; set; } 
    }
}