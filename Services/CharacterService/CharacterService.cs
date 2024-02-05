using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projet_net.Data;

namespace projet_net.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        private readonly DataContext dataBase;
        public CharacterService(IMapper map, DataContext _dataBase)
        {
            dataBase = _dataBase;
            mapper=map;
        }
        public List<GetDtoCharacter> Add(AddDtoCharacter newChar)
        {
            //const charr=mapper.Map<GetDtoCharacter>(newChar);
            var character=mapper.Map<Character>(newChar);

            dataBase.Characters.Add(character);
            dataBase.SaveChanges();
            return dataBase.Characters.Select(ele=>mapper.Map<GetDtoCharacter>(ele)).ToList();
        }

        public List<GetDtoCharacter> delete(int id)
        {
            var character=dataBase.Characters.FirstOrDefault(ele=>ele.Id==id);
            if(character is null)
                throw new Exception($"charcter with id :{id} not found");
            
            dataBase.Characters.Remove(character);
            dataBase.SaveChanges();
            return dataBase.Characters.Select(ele=>mapper.Map<GetDtoCharacter>(ele)).ToList();

        }

        public List<GetDtoCharacter> getAll()
        {
            var dbCharacters=dataBase.Characters;
            return dbCharacters.Select(ele=>mapper.Map<GetDtoCharacter>(ele)).ToList();
        }
        public GetDtoCharacter getById(int id)
        {
            var dbCharacters=dataBase.Characters;
            var ch=dbCharacters.FirstOrDefault(i=>i.Id == id);

            if(ch is not null)
                return mapper.Map<GetDtoCharacter>(ch);

            throw new Exception("character not found with this id");
        }
        public GetDtoCharacter Update(UpdateDtoCharacter updateChar)
        {
            var character=dataBase.Characters.FirstOrDefault(ele=>ele.Id == updateChar.Id);

                if(character is null)
                    throw new Exception("character not found");

                mapper.Map(updateChar,character);   
                /*
                character.Defense=updateChar.Defense;
                character.Strength=updateChar.Strength;
                character.HitPoints=updateChar.HitPoints;
                character.Intelligence=updateChar.Intelligence;
                character.Name=updateChar.Name;
                character.Class=updateChar.Class;*/

                dataBase.SaveChanges();

            return mapper.Map<GetDtoCharacter>(character);
        }
    }
}