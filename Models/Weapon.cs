using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_net.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public int Damage { get; set; }
        public Character? character { get; set; }
        public int CharacterId { get;set; }
    }
}