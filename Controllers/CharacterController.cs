using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace projet_net.Controllers
{
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
    }
}