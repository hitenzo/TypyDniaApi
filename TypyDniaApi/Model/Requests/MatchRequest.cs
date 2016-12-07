using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TypyDniaApi.Model.Requests
{
    public class MatchRequest
    {
        [Required]
        public int HomeTeamId { get; set; }
        [Required]
        public string Date { get; set; }
    }
}