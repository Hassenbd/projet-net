using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace projet_net.Models
{
    public class User
    {
        public int Id { get; set; }
        public string userName { get; set; }=string.Empty;
        public byte [] passwordHash { get; set; }=new byte[0];
        public byte [] passwordSalt { get; set; }=new byte[0];
        public List<Character>? Characters {get;set;}
    }
}