using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Standard.Services;
using static Changelog.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Standard.Controllers
{
    [Route("api/changelog")]
    public class ChangelogApiController : Controller
    {
		private IChangelogData _changelogData;

		public ChangelogApiController(IChangelogData changelogData)
		{
			_changelogData = changelogData;
		}

		// GET: api/values
		[HttpGet]
        public IEnumerable<ReleaseDto> Get()
        {
            return _changelogData.GetAll();
        }

        [HttpGet("get/{version}")]
        public ReleaseDto Get(string version)
        {
            return _changelogData.Get(version);
        }
    }
}
