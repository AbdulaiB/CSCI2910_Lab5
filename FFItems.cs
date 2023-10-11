using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_PART1_TEST
{
    public class FFItems
    {
        public int LevelItem { get; set; }
        public string Name_en { get; set; }

        public FFItems() { }

        public FFItems(int level, string itemName)
        {
            this.LevelItem = level;
            this.Name_en = itemName;
        }

        public override string ToString()
        {
            string msg = "";
            msg += $"I.Lv{this.LevelItem} Name: {this.Name_en}";

            return msg;
        }
    }
}
