using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projet_net.Dto.Character;
using projet_net.Models;

namespace projet_net.Services.CharacterService
{
    public interface ICharacterService
    {
        List<GetDtoCharacter> getAll();
        GetDtoCharacter getById(int id);
        List<GetDtoCharacter> Add(AddDtoCharacter newChar);
        GetDtoCharacter Update(UpdateDtoCharacter updateChar);
       List<GetDtoCharacter> delete(int id);
    }
}