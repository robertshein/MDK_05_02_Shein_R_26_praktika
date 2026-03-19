using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika26_Shein.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public DateTime RentStart {get; set;}
        public int Duration { get; set; }
        public int IdClub { get; set; }
    }
}
