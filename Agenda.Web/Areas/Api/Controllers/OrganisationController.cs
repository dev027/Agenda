// <copyright file="OrganisationController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Web.Areas.Api.Controllers
{
    /// <summary>
    /// Organisation Controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        /// <summary>
        /// Gets all organisations.
        /// </summary>
        /// <example>
        /// GET: /api/Organisation/Get .
        /// </example>
        /// <returns>List of organisations.</returns>
        [HttpGet]
        public IList<string> Get()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the organisation with the specified id.
        /// </summary>
        /// <example>
        /// GET: api/Organisation/5 .
        /// </example>
        /// <param name="id">Organisation Id.</param>
        /// <returns>Organisation.</returns>
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates an organisation.
        /// </summary>
        /// <example>
        /// POST: api/Organisation .
        /// </example>
        /// <param name="value">Organisation details.</param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates and organisation.
        /// </summary>
        /// <example>
        /// PUT: api/Organisation/5 .
        /// </example>
        /// <param name="id">Organisation iD.</param>
        /// <param name="value">SOMETHING.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an organisation.
        /// </summary>
        /// <example>
        /// DELETE: api/Organisation/5 .
        /// </example>
        /// <param name="id">Organisation Id.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
