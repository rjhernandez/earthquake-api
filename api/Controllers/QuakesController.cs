using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GeoJSON.Net.Feature;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsgsClient;
using UsgsClient.Quakes;

namespace EarthquakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Admin")]
    public class QuakesController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<FeatureCollection>> Get()
        {
            var svc = Usgs
                .Quakes
                .Feed();

            var features = await svc
                .Summary(
                    Magnitude.Significant,
                    Timeframe.Week);

            return Ok(features);
        }   
    }
}
