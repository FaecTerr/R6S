using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class SonicBurst : Thing
    {
        public int lifeTime = 60;
        public SonicBurst(float xpos, float ypos) : base(xpos, ypos)
        {
        }
        public override void Update()
        {
            if (lifeTime > 0)
            {
                foreach (Duck d in Level.CheckCircleAll<Duck>(this.position, 48f))
                {
                    if (d.ragdoll == null && Level.CheckLine<Block>(d.position, this.position) == null)
                    {
                        d.GoRagdoll();
                    }
                }
                lifeTime--;
                if(lifeTime <= 0)
                {
                    Level.Remove(this);
                }
            }
            base.Update();
        }
    }
}
