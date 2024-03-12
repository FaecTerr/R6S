using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|IHUD")]
    public class BShield : BallisticShield
    {
        public BShield(float xpos, float ypos) : base(xpos, ypos)
        {
            _editorName = "Ballistic shield";
        }
    }
}

