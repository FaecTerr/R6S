using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Gamemode")]
    public class GMTutorial : GamemodeScripter
    {
        public EditorProperty<int> Team;
        public EditorProperty<int> Operator;
        public GMTutorial()
        {
            loadPhase = 0;
            preparationPhase = 0;
            actionPhase = 1800;

            confirmed = 1;
            loaded = 1;
            isTH = true;
            //Ash
            //selected = PlayerStats.GetOpeqByID(17, position);

            Operator = new EditorProperty<int>(0, null, 0, PlayerStats.totalOperatorsPlanned, 1);
            Team = new EditorProperty<int>(0, null, 0, 1, 1);
        }

        public override void Initialize()
        {
            base.Initialize();
            for (int i = 0; i < PlayerStats.totalOperatorsPlanned; i++)
            {
                if(i != Operator)
                {
                    banned[i] = i;
                }
            }
            GetSpawnPosition();
            if (!(Level.current is Editor))
            {
                team = Team;
                if(Team == 0)
                {
                    defenders = 1;
                }
                else
                {
                    attackers = 1;
                }
                currentPhase = 1;
                selectedId = Operator;
                selected = PlayerStats.GetOpeqByID(17, position);
                //Level.Add(selected);
            }
        }
    }
}
