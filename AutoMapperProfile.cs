using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace projet_net
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddDtoCharacter,Character>();
            CreateMap<Character,GetDtoCharacter>();
            CreateMap<UpdateDtoCharacter,Character>();
            CreateMap<Weapon,GetWeaponDto>(); 
            CreateMap<Skill,GetSkillDto>(); 
        }
    }
}