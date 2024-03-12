using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class InvisibilityForDrones : ExPoD
    {        
        public SinWave _pulse = 0.085f;
        public SinWave _pulse2 = 0.065f;

        public InvisibilityForDrones(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/vigilDevice.png"), 16, 16, false);
            graphic = _sprite;

            _sprite.frame = 0;
            center = new Vec2(8f, 8f);

            index = 28;
            
            team = "Def";

            CooldownTime = 12f;
            _pulse2.value += 0.35f;
            Cooldown = 1;

            ShowCounter = true;
            requiresTakeOut = false;
        }

       
        public override void ApplyEffect()
        {
            base.ApplyEffect();
            if (user != null)
            {
                user.invisibleForCams = 15;
            }
        }

        public override void WearOffEffect()
        {
            base.WearOffEffect();
            
        }


        public override void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                if (sinceactivation > 0 && user != null)
                {
                    if (!user.observing && user.local)
                    {
                        DrawAbilityEffect();
                    }
                    foreach(ObservationThing obs in Level.CheckCircleAll<ObservationThing>(position, 160))
                    {
                        if(obs.observing && !user.local && obs.team != team)
                        {
                            float modifier = 1 - (obs.position - user.position).length / 320;
                            DrawAbilityEffect(modifier);
                        }
                    }
                }
            }
            base.OnDrawLayer(layer);
        }

        public void DrawAbilityEffect(float modifier = 1)
        {
            Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);
            Vec2 pos = Level.current.camera.position;
            Vec2 cameraSize = Level.current.camera.size;

            int wide = 256;
            //float move = 4.8f + 0.2f * (wide / 64);
            float move = 5.2f;

            for (int i = 0; i < wide; i++)
            {
                if (i % 2 == 0)
                {
                    Graphics.DrawRect(pos + new Vec2(1f * 64 / wide + (move * 64 / wide) * i, 185 - (10 + (_pulse2 + 1) * 2 * (float)Math.Sqrt(Math.Abs(i - (wide - 1) / 2))) * sinceactivation * modifier) * Unit,
                    pos + new Vec2(3f * 64 / wide + (move * 64 / wide) * i, 185) * Unit,
                    Color.White * 0.4f, 1.1f);

                    Graphics.DrawRect(pos + new Vec2(1.75f * 64 / wide + (move * 64 / wide) * i, 182 + _pulse * 2 - (10 + (_pulse2 + 1) * 2 * (float)Math.Sqrt(Math.Abs(i - (wide - 1) / 2))) * sinceactivation * modifier) * Unit,
                    pos + new Vec2(2.25f * 64 / wide + (move * 64 / wide) * i, 185) * Unit,
                    Color.White * 0.4f, 1.1f);
                }
                else
                {
                    Graphics.DrawRect(pos + new Vec2(1f * 64 / wide + (move * 64 / wide) * i, 185 - (10 + (1 - _pulse2) * 2 * (float)Math.Sqrt(Math.Abs(i -  (wide - 1) / 2))) * sinceactivation * modifier) * Unit,
                    pos + new Vec2(3f * 64 / wide + (move * 64 / wide) * i, 185) * Unit,
                    Color.White * 0.4f, 1.1f);

                    Graphics.DrawRect(pos + new Vec2(1.75f * 64 / wide + (move * 64 / wide) * i, 182 + _pulse * 2 - (10 + (1 - _pulse2) * 2 * (float)Math.Sqrt(Math.Abs(i - (wide - 1) / 2))) * sinceactivation * modifier) * Unit,
                    pos + new Vec2(2.25f * 64 / wide + (move * 64 / wide) * i, 185) * Unit,
                    Color.White * 0.4f, 1.1f);
                }
            }
        }
    }
}