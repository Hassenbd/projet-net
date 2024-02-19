using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_net.Dto.Weapon
{
    public class AddWeaponDto
    {
        public string Name { get; set; }=string.Empty;
        public int Damage { get; set; }
        public int CharcterId { get; set; }
    }
}