using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_net.Dto.Fight
{
    public class FightRequestDto
    {
        public List<int> charactersIds { get; set; }=new List<int>();
    }
}