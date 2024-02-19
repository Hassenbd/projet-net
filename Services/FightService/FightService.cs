using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using projet_net.Data;
using projet_net.Dto.Fight;

namespace projet_net.Services.FightService
{
    public class FightService:IFightService
    {
        private readonly DataContext dataBase;
        public FightService(DataContext _dataBase)
        {
            dataBase=_dataBase;
        }

        public async Task<AttackResultDto> SkillAttack(SkillAttackDto skillAttack)
        {
            var character=await dataBase.Characters
            .Include(ele=>ele.Skills)
            .FirstOrDefaultAsync(ele=>ele.Id==skillAttack.AttackerId);

            var opponent=await dataBase.Characters
            .FirstOrDefaultAsync(ele=>ele.Id==skillAttack.OpponentId);

            if(character is null || opponent is null || character.Skills is null)
                throw new Exception("some thing has wrong");

            var skill=await dataBase.Skills
            .FirstOrDefaultAsync(ele=>ele.Id==skillAttack.SkillId);

            if(skill is null)
                throw new Exception("skill is not exist");

            int damage=skill.Damage+ new Random().Next(character.Intelligence);
            damage-=new Random().Next(opponent.Defeats);

            if(damage>0)
                opponent.HitPoints-=damage;

             await dataBase.SaveChangesAsync();   
             
            if(opponent.HitPoints<=0)
            {
                throw new Exception($"{opponent.Name} has been defeated!");
            } 

            

            var res=new AttackResultDto
            {
                Attacker=character.Name!,
                Opponent=opponent.Name!,
                AttackerHP=character.HitPoints,
                OpponentHP=opponent.HitPoints,
                Damage=damage
            };

            return res;
        }

        public async Task<AttackResultDto> WeaponAttack(WeaponAttackDto weaponAtt)
        {
            var character=await dataBase.Characters
            .Include(ele=>ele.weapon)
            .FirstOrDefaultAsync(ele=>ele.Id==weaponAtt.AttackerId);

            var opponent=await dataBase.Characters
            .FirstOrDefaultAsync(ele=>ele.Id==weaponAtt.OpponentId);

            if(character is null || opponent is null || character.weapon is null)
                throw new Exception("some thing has wrong");

            int damage=character.weapon.Damage+ new Random().Next(character.Strength);
            damage-=new Random().Next(opponent.Defeats);

            if(damage>0)
                opponent.HitPoints-=damage;

             await dataBase.SaveChangesAsync();   
             
            if(opponent.HitPoints<=0)
            {
                throw new Exception($"{opponent.Name} has been defeated!");
            } 

            

            var res=new AttackResultDto
            {
                Attacker=character.Name!,
                Opponent=opponent.Name!,
                AttackerHP=character.HitPoints,
                OpponentHP=opponent.HitPoints,
                Damage=damage
            };

            return res;
        }
    }
}