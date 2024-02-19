using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_net.Dto.Fight
{
    public class SkillAttackDto
    {
        public int AttackerId { get; set; }
        public int OpponentId { get; set; }
        public int SkillId { get; set; }
    }
}