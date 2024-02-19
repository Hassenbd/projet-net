using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projet_net.Dto.Weapon;
using projet_net.Services.WeaponService;

namespace projet_net.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService weaponService;
        public WeaponController(IWeaponService _weaponService)
        {
            weaponService = _weaponService;
        }
        [HttpPost]
        public async Task<ActionResult<GetDtoCharacter>>Add(AddWeaponDto newWeapon)
        {
            return Ok(await weaponService.AddWeapon(newWeapon));
        }
    }
}