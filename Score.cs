using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whack_A_Mole
{
    public class Score
    {
        public string name { get;  }
        public int score { get; }

        public Score(string a, int b)
        {
            this.name = a;
            this.score = b;
        }

        public string outputString()
        {
            return name + " " + score;
        }
    }
}
