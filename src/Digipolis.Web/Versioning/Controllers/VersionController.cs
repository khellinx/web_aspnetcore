using System;
using Digipolis.Web.Routing;
using Digipolis.Web.Versioning.Models;
using Microsoft.AspNetCore.Mvc;

namespace Digipolis.Web.Versioning
{
    [Route(Routes.VersionController)]
    public class VersionController : Controller
    {
        private IVersionProvider _versionProvider;

        public VersionController(IVersionProvider versionProvider)
        {
            _versionProvider = versionProvider;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(AppVersion), 200)]
        [ProducesResponseType(typeof(VersionError), 500)]
        public IActionResult Get()
        {
            try
            {
                return new OkObjectResult(_versionProvider.GetCurrentVersion());
            }
            catch (Exception ex)
            {
                var error = new VersionError(ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }
        }
    }
}
