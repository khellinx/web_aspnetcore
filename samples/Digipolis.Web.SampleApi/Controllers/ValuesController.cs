﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Digipolis.Errors.Exceptions;
using Digipolis.Web.Api;
using Digipolis.Web.SampleApi.Configuration;
using Digipolis.Web.SampleApi.Logic;
using Digipolis.Web.SampleApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Digipolis.Web.Api.Models;
using Digipolis.Web.Swagger;
using Digipolis.Web.Api.ApiExplorer;
using Digipolis.Web.Api.Filters;

namespace Digipolis.Web.SampleApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ValuesController : Controller
    {
        private readonly IValueLogic _valueLogic;

        public ValuesController(IValueLogic valueLogic)
        {
            _valueLogic = valueLogic;
        }

        /// <summary>
        /// Get all values 
        /// </summary>
        /// <param name="queryOptions">Query options from uri</param>
        /// <returns>An array of value objects</returns>
        [HttpGet()]
        [ProducesDefaultResponses(200, SuccessResponseType = typeof(PagedResult<ValueDto>))]
        [AllowAnonymous]
        [Versions(Versions.V1, Versions.V2)]
        public IActionResult Get([FromQuery]PageOptions queryOptions)
        {
            int total;
            var values = _valueLogic.GetAll(queryOptions, out total);
            var result = new PagedResult<ValueDto>(queryOptions, total, values);
            return Ok(result);
        }

        /// <summary>
        /// Get a value by id
        /// </summary>
        /// <param name="id">The id of the value</param>
        /// <returns>A value object</returns>
        [HttpGet("{id}")]
        [ProducesDefaultResponses(200, SuccessResponseType = typeof(ValueDto))]
        [AllowAnonymous]
        [Versions(Versions.V1, Versions.V2)]
        public IActionResult Get(int id)
        {
            var value = _valueLogic.GetById(id);
            return Ok(value);
        }

        /// <summary>
        /// Add a new value
        /// </summary>
        /// <param name="value">A value object</param>
        /// <returns>The created value object</returns>
        [HttpPost]
        [ValidateModelState]
        [ProducesDefaultResponses(201, SuccessResponseType = typeof(ValueDto))]
        [AllowAnonymous]
        [Versions(Versions.V1, Versions.V2)]
        public IActionResult Post([FromBody, Required] ValueDto value)
        {
            value = _valueLogic.Add(value);
            return CreatedAtAction("Get", new { id = value.Id }, value);
        }

        /// <summary>
        /// Update an existing value object
        /// </summary>
        /// <param name="id">The id of the value</param>
        /// <param name="value">The updated value object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModelState]
        [ProducesDefaultResponses(200, SuccessResponseType = typeof(ValueDto))]
        [Versions(Versions.V1, Versions.V2)]
        public IActionResult Put(int id, [FromBody, Required] ValueDto value)
        {
            value = _valueLogic.Update(id, value);
            return Ok(value);
        }

        /// <summary>
        /// Delete a value by it's Id
        /// </summary>
        /// <param name="id">The value's Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesDefaultResponses(200, SuccessResponseType = typeof(ValueDto))]
        [Versions(Versions.V2)]
        public IActionResult Delete(int id)
        {
            _valueLogic.Delete(id);
            return NoContent();
        }
    }
}
