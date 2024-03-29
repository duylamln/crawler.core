﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Crawler.API.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IFirebaseUserInfoService _firebaseAccountService;

        public ValuesController(IFirebaseUserInfoService firebaseAccountService)
        {
            _firebaseAccountService = firebaseAccountService;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            //var acc = await _firebaseAccountService.GetByEmail("duylamln@gmail.com");
            var acc = await _firebaseAccountService.GetByUId("RtWOkvlr0VdwqMwWEXOYyuap8FO2---");
            return Ok(acc);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
