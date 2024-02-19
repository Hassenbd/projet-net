using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projet_net.Dto.Fight;

namespace projet_net.Services.FightService
{
    public interface IFightService
    {
        public Task<AttackResultDto>WeaponAttack(WeaponAttackDto weaponAtt);
        public Task<AttackResultDto>SkillAttack(SkillAttackDto weaponAtt);
    }
}