using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDSpellsApp.Models
{
    public class SpellModel
    {
        //Might not need Index
        public string Index { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string[] Desc { get; set; }
        public string Duration { get; set; }
        public string Range { get; set; }
        public string Url { get; set; }
    }
}
