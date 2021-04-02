using BowlingApp.Models;
using BowlingApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Add a private variable, available only within this class, to contain the context!
        private BowlingLeagueContext context;
        // Pass in the xontext to the HomeController Constructor
        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext c)
        {
            _logger = logger;
            //set the context to the BowlingLeagueContext that gets passed in!
            context = c;
        }

        // Pass in the teamid and the pagenum (set the pagenum default = 1)
        public IActionResult Index(long? teamid, string teamname, int pagenum = 1)
        {
            int numPerPage = 5;

            // Return a new instance of IndexView model that is created by setting the values to all the data
            return View(new IndexViewModel
            {
                // Set the Bowlers property of the IndexView Models with linqcommands and the context
                Bowlers = (context.Bowlers.Where(b => b.TeamId == teamid || teamid == null).Distinct()
                .Skip((pagenum - 1) * numPerPage)
                .Take(numPerPage)),

                // Set the PagingInfo Property as a newly instantiated object of type Paging info and use the paramter
                // and local variable and some linq
                PagingInfo = new PagingInfo
                {
                    NumPlayersPerPage = numPerPage,
                    CurrentPage = pagenum,
                    // Use linq and an embedded if statement
                    // if teamid is null, we want all the players in the database
                    // if teamid isn't null, we want only the number of players that have that team (that teamid)
                    TotalNumPlayers = (teamid == null ? context.Bowlers.Distinct().Count() : context.Bowlers.Where(b => b.TeamId == teamid).Count())
                    // NumPages gets set when it is called

                },
                Team = teamname,
                Teams = (context.Teams.Distinct())

            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
