using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.DAL.Entity
{
    public class Catigory :BaseEntity
    {
        public int id {  get; set; }    
        public string name { get; set; }
        public string description { get; set; }
        public int age { get; set; }

    }
}
