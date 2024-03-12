using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Scripting")]
    public class InfoScreen : Thing, IDrawToDifferentLayers
    {
        public Terrorist connectedTo;
        public int shots;
        public InfoScreen()
        {
            collisionSize = new Vec2(48, 24);
            collisionOffset = new Vec2(-24, -12);
        }

        public override void Update()
        {
            if(connectedTo == null)
            {
                connectedTo = Level.CheckRect<Terrorist>(topLeft + new Vec2(-800, 0), bottomRight + new Vec2(800, 0));
            }
            else
            {
                
            }
            base.Update();
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if(pLayer == Layer.Blocks)
            {
                Graphics.DrawRect(topLeft, bottomRight, Color.Black * 0.8f, -0.8f);
                if (connectedTo != null)
                {
                    if (connectedTo.Dummy)
                    {
                        string text = "";
                        float txtScale = 1f;
                        Color c = Color.Wheat;


                        text = Convert.ToString(connectedTo.lastDamage);
                        if (connectedTo.lastHitIsHeadshot)
                        {
                            c = Color.OrangeRed;
                        }
                        Graphics.DrawString(text, position - new Vec2(4 * text.Length, 4), c, -0.78f, null, txtScale);

                        text = "Dealt damage: " + Convert.ToString(100 - connectedTo.Health); 
                        txtScale = 0.4f;
                        c = Color.Wheat;
                        Graphics.DrawString(text, position - new Vec2(4 * text.Length * 0.5f, -8), c, -0.78f, null, txtScale);

                        Operators op = Level.current.NearestThing<Operators>(position);
                        if (op != null)
                        {
                            text = "Dist: " + Convert.ToString(Math.Round((connectedTo.position - op.position).length / 20, 1)) + "m";
                            txtScale = 0.4f;
                            Graphics.DrawString(text, position - new Vec2(4 * text.Length * txtScale, 8), c, -0.78f, null, txtScale);
                        }
                    }
                }
            }
        }
    }
}
