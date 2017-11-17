using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Standard.Services;
using Standard.ViewModels;
using static Changelog.Dtos;

namespace Standard.Controllers
{
	public class ChangelogController : Controller
    {
        private IChangelogData _changelogData;

        public ChangelogController(IChangelogData changelogData)
        {
            _changelogData = changelogData;
        }

		public IActionResult Index()
        {
            var model = new ChangelogViewModel()
            {
                Releases = _changelogData.GetAll()
            };

            return View(model);
        }
    }
}
