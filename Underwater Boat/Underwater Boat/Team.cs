using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underwater_Boat
{
    class Team
    {
        public string teamname { get; set; }
        public List<Sub> members { get; set; }
        public Team(string teamname)
        {
            members = new List<Sub>();
            this.teamname = teamname;
        }
    }
}
