using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingApp.Models.ViewModels
{
    // This class is so we can pass data from the controller to the view, and then from the
    // view to the pagingTagHelper class, so that the tag helpers can be returned to the view

    //The information in this MODEL is set in the controller and then passed to the view
    public class PagingInfo
    {
        public int NumPlayersPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumPlayers { get; set; }

        // This property is calculated using a lambda function
        // Gets an integer that is the ceiling from the divsion, then casts that ceiling to an int.
        public int NumPages => (int) Math.Ceiling((float)TotalNumPlayers / NumPlayersPerPage);

    }
}
