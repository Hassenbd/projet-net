using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projet_net.Dto.Skill;
using projet_net.Dto.Weapon;
using projet_net.Models;

namespace projet_net.Dto.Character
{
    public class GetDtoCharacter
    {
        public int Id {get;set;}
        public string? Name  {get;set;}
        public int HitPoints {get;set;}=100;
        public int Strength {get;set;}=10;
        public int Defense {get;set;}=10;
        public int Intelligence {get;set;}=10;
        public RpgClass Class {get;set;}=RpgClass.Knight;
        public GetWeaponDto? getWeaponDto {get;set;}
        public List<GetSkillDto>? Skills {get;set;}
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}