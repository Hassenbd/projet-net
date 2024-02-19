using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projet_net.Dto.Fight;

namespace projet_net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService fightService;
        public FightController(IFightService _fightService)
        {
            fightService = _fightService;
        }

        [HttpPost("Weapon")]
        public async Task<ActionResult<AttackResultDto>> WeaponAttack(WeaponAttackDto weaponAtt)
        {
            return Ok(await fightService.WeaponAttack(weaponAtt));
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<AttackResultDto>> SkillAttack(SkillAttackDto skillAttack)
        {
            return Ok(await fightService.SkillAttack(skillAttack));
        }
    }
}