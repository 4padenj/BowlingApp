using BowlingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingApp.Components
{
    public class TeamFilterViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;
        public TeamFilterViewComponent(BowlingLeagueContext c)
        {
            context = c;
        }
        public IViewComponentResult Invoke()
        {
            return View(context.Teams.Distinct());
        }
    }
}
