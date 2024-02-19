using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projet_net.Dto.Weapon;

namespace projet_net.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<GetDtoCharacter>AddWeapon(AddWeaponDto newWeapon);
    }
}