using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetCore_OpeningHoursQuestion.Domain.Models;
using DotNetCore_OpeningHoursQuestion.Domain.Services;
using System.Net;
using Newtonsoft.Json;

namespace DotNetCore_OpeningHoursQuestion.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OpeningHoursController : ControllerBase
    {
        public IOpeningHoursService _service;
        public ILogger _logger;
        public OpeningHoursController(IOpeningHoursService service, ILogger<OpeningHoursController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [Route("FromJson/Display")]
        [HttpPost]
        public IActionResult DisplayFormattedOpeningHours(OpeningHours openingHours)
        {
            try
            {
                _logger.LogInformation($"API Request. Params: {JsonConvert.SerializeObject(openingHours)}");
                var response = _service.FormatOpeningHours(openingHours);


                _logger.LogInformation($"API Response: {response}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"API Exception: {ex.ToString()}");
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);

            }
        }

        [Route("FromString/Display")]
        [HttpPost]
        public IActionResult DisplayFormattedOpeningHours([FromBody] string openingHours)
        {
            try
            {
                _logger.LogInformation($"API Request. Params: {openingHours}");
                var jsonModel = JsonConvert.DeserializeObject<OpeningHours>(openingHours);
                var response = _service.FormatOpeningHours(jsonModel);

                _logger.LogInformation($"API Response: {response}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"API Exception: {ex.ToString()}");
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);

            }
        }
    }
}
