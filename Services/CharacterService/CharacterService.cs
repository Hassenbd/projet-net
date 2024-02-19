using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projet_net.Data;

namespace projet_net.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        private readonly DataContext dataBase;
        private readonly IHttpContextAccessor httpContext;
        public CharacterService(IMapper map, DataContext _dataBase , IHttpContextAccessor _httpContext)
        {
            httpContext = _httpContext;
            dataBase = _dataBase;
            mapper = map;
        }

        private int GetUserId()
        {
            return int.Parse(httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public List<GetDtoCharacter> Add(AddDtoCharacter newChar)
        {
            //const charr=mapper.Map<GetDtoCharacter>(newChar);
            var character = mapper.Map<Character>(newChar);
            character.user= dataBase.Users.FirstOrDefault(us=>us.Id==GetUserId());
            dataBase.Characters.Add(character);
            dataBase.SaveChanges();
            return dataBase.Characters.Where(ele=>ele.user!.Id==GetUserId()).Select(ele => mapper.Map<GetDtoCharacter>(ele)).ToList();
        }
        public List<GetDtoCharacter> delete(int id)
        {
            var character = dataBase.Characters.Where(ele=>ele.user!.Id==GetUserId()).FirstOrDefault(ele => ele.Id == id);
            if (character is null)
                throw new Exception($"charcter with id :{id} not found");

            dataBase.Characters.Remove(character);
            dataBase.SaveChanges();
            return dataBase.Characters.Select(ele => mapper.Map<GetDtoCharacter>(ele)).ToList();

        }
        public List<GetDtoCharacter> getAll()
        {
            int userId=GetUserId();
            var dbCharacters = dataBase.Characters
            .Include(ele=>ele.weapon)
            .Include(ele=>ele.Skills)
            .Where(ele=>ele.user!.Id==userId)
            .Select(ele => mapper.Map<GetDtoCharacter>(ele));
            
            return dbCharacters.ToList();
        }
        public GetDtoCharacter getById(int id)
        {
            var dbCharacters = dataBase.Characters;
            var ch = dbCharacters
            .Include(ele=>ele.weapon)
            .Include(ele=>ele.Skills)
            .FirstOrDefault(i => i.Id == id && i.user!.Id==GetUserId());

            if (ch is not null)
                return mapper.Map<GetDtoCharacter>(ch);

            throw new Exception("character not found with this id");
        }
        public GetDtoCharacter Update(UpdateDtoCharacter updateChar)
        {
            var character = dataBase.Characters.Include(c=>c.user).FirstOrDefault(ele => ele.Id == updateChar.Id);

            if (character is null || character.user!.Id!=GetUserId())
                throw new Exception("character not found");

            mapper.Map(updateChar, character);
            /*
            character.Defense=updateChar.Defense;
            character.Strength=updateChar.Strength;
            character.HitPoints=updateChar.HitPoints;
            character.Intelligence=updateChar.Intelligence;
            character.Name=updateChar.Name;
            character.Class=updateChar.Class;
            */

            dataBase.SaveChanges();

            return mapper.Map<GetDtoCharacter>(character);
        }

        public async Task<GetDtoCharacter> AddCharacterSkill(AddCharacterSkillDto newcharSkill)
        {
            var character=await dataBase.Characters
            .Include(ele=>ele.weapon)
            .Include(ele=>ele.Skills)
            .FirstOrDefaultAsync(ele=>ele.Id==newcharSkill.CharacterId &&ele.user!.Id==GetUserId());

            if(character is null)
                throw new Exception("charcter is not found");
            
            var skill=await dataBase.Skills.FirstOrDefaultAsync(sk=>sk.Id==newcharSkill.SkillId);

            if(skill is null)
                throw new Exception("skill not found");
            
            character.Skills!.Add(skill);
            await dataBase.SaveChangesAsync();

            return mapper.Map<GetDtoCharacter> (character);
        }
    }
}