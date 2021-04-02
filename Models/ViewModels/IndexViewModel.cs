using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingApp.Models.ViewModels
{
    public class IndexViewModel
    {
        // This portion is set in the IndexController Function:
        // Bowlers is for listing out the bowlers
        public IEnumerable<Bowler> Bowlers { get; set; }
        //PagingInfo is for the pagination and is used by tag helper
        public PagingInfo PagingInfo { get; set; }
        // Team is used for routing/pagination
        public string Team { get; set; }

        public IEnumerable<Team> Teams { get; set; }

    }
}
