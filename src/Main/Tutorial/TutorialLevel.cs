using System;
using System.Collections.Generic;

namespace DuckGame.R6S
{
    public class TutorialLevel : Level
    {
        public bool spawned;

        public TutorialLevel(string level)
        {
            _level = level;
        }
        
        public override void Initialize()
        {
            //base.Initialize();
            IEnumerable<DXMLNode> objectsNode = DuckXML.Load(_level).Element("Level").Elements("Objects");
            if (objectsNode != null)
            {
                foreach (DXMLNode node in objectsNode.Elements("Object"))
                {
                    Thing t = Thing.LegacyLoadThing(node, true);
                    if (t != null)
                    {
                        AddThing(t);
                    }
                }
            }

            //base.Initialize();

            //DevConsole.Log(_level);
        }
        
        public override void Update()
        {
            base.Update();

            if (!spawned)
            {
                Vec2 spawnPos = new Vec2();

                foreach (Spawn op in current.things[typeof(Spawn)])
                {
                    spawnPos = op.position;
                }


                foreach (Operators op in current.things[typeof(Operators)])
                {
                    op.position = spawnPos;
                    spawned = true;
                }
            }
        }
    }
}