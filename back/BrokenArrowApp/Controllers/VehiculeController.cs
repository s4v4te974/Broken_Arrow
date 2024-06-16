﻿using BrokenArrowApp.Models.Dtos.Responses;
using BrokenArrowApp.Service;
using BrokenArrowApp.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BrokenArrowApp.Controllers
{
    [Route(ConstUtils.ROOT_URL)]
    [ApiController]
    public class VehiculeController(IVehiculeService vehiculeService) : ControllerBase
    {

        private readonly IVehiculeService _vehiculeService = vehiculeService;

        [HttpGet(ConstUtils.ALL_VEHICULE_URL)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<IEnumerable<VehiculeResponse>>> GetAllVehicules()
        {
            IEnumerable<VehiculeResponse> vehiculesResponse = await _vehiculeService.GetAllVehiculesAsync();
            return vehiculesResponse == null || !vehiculesResponse.Any() ? NotFound() : Ok(vehiculesResponse);
        }

        [HttpPost(ConstUtils.SPECIFIC_VEHICULE + "{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<VehiculeResponse>> GetSpecificVehicule(Guid id)
        {
            VehiculeResponse? vehiculeResponse = await _vehiculeService.GetSpecificVehiculeAsync(id);
            return vehiculeResponse == null ? NotFound() : Ok(vehiculeResponse);
        }
    }
}
