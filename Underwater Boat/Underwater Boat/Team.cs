using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underwater_Boat
{
    public class Team
    {
        public string TeamName { get; set; }
        public List<Sub> Members{ get; set; }

        public Team(string teamName)
        {
            Members = new List<Sub>();
            this.TeamName = teamName;
        }
    }
}
