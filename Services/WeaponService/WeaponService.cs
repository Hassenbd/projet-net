using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using projet_net.Data;
using projet_net.Dto.Weapon;

namespace projet_net.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext dataBase;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public WeaponService(DataContext _dataBase,IMapper _mapper,IHttpContextAccessor _httpContextAccessor)
        {
            dataBase=_dataBase;
            mapper=_mapper;
            httpContextAccessor=_httpContextAccessor;
        }
        public async Task<GetDtoCharacter> AddWeapon(AddWeaponDto newWeapon)
        {
            var _character=await dataBase.Characters.FirstOrDefaultAsync(ch=>ch.Id==newWeapon.CharcterId && ch.user!.Id==GetUserId());
            if(_character is null)
                throw new Exception("character not found");
            
            var weapon=new Weapon
            {
                Name=newWeapon.Name,
                Damage=newWeapon.Damage,
                character=_character,
            };

            dataBase.Weapons.Add(weapon);
            await dataBase.SaveChangesAsync();

            return mapper.Map<GetDtoCharacter>(_character);
        }
        private int GetUserId()
        {
            return int.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
    }
}