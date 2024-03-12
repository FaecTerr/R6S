using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class LocalOutsideBlock : Block, IDrawToDifferentLayers
    {
        public LocalOutsideBlock(float x, float y) : base(x, y)
        {
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(0f, 0f);
            thickness = 0;
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Game)
            {
                foreach (Operators opert in Level.CheckRectAll<Operators>(topLeft - new Vec2(16, 16), bottomRight + new Vec2(16, 16)))
                {
                    if (opert.local && opert.team == "Def")
                    {
                        foreach(LocalOutsideBlock l in Level.current.things[typeof(LocalOutsideBlock)])
                        {
                            Graphics.DrawRect(l.topLeft, l.bottomRight, Color.Red * 0.5f);
                        }                        
                    }
                }
            }
        }

        public override void Update()
        {
            base.Update();

            foreach(GamemodeScripter gm in Level.current.things[typeof(GamemodeScripter)])
            {
                if(gm.team == 1)
                {
                    foreach (PhysicsObject p in Level.current.things[typeof(PhysicsObject)])
                    {
                        if (!(p is Operators))
                        {
                            p.UpdatePhysics();
                        }
                    }
                    Level.Remove(this);
                    
                }
                else
                {
                    if(gm.currentPhase == 4 || gm.currentPhase == 5)
                    {
                        foreach (PhysicsObject p in Level.current.things[typeof(PhysicsObject)])
                        {
                            if(!(p is Operators))
                            {
                                p.UpdatePhysics(); 
                            }
                        }
                        Level.Remove(this);
                    }
                }
            }
        }
    }
}
