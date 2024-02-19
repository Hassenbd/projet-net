using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;


namespace projet_net.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService serv;
        public CharacterController(ICharacterService Serv)
        {
            serv=Serv;
        }

        [HttpGet]
        public ActionResult<List<GetDtoCharacter>> Get()
        {
            return Ok(serv.getAll());
        }

        [HttpGet("{id}")]
        public ActionResult<GetDtoCharacter>GetSingle(int id)
        {
            return Ok(serv.getById(id));
        }

        [HttpPost]
        public ActionResult<List<GetDtoCharacter>>Add(AddDtoCharacter newchar)
        {
            return Ok(serv.Add(newchar));
        }

        [HttpPut]
        public ActionResult<GetDtoCharacter>Update(UpdateDtoCharacter updatechar)
        {
            return Ok(serv.Update(updatechar));
        }

        [HttpDelete]
        public ActionResult<GetDtoCharacter>Delete(int id)
        {
            return Ok(serv.delete(id));
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<GetDtoCharacter>>AddSkill(AddCharacterSkillDto newCharSkill)
        {
            return Ok(await serv.AddCharacterSkill(newCharSkill));
        }
    }
}