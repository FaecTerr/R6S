using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class HEL : ExPoD
    {
        public SinWave _pulse = 0.1f;
        public SinWave _pulse2 = -0.1f;

        public HEL(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/JackalTracker.png"), 32, 32, false);
            graphic = _sprite;

            _sprite.frame = 0;
            center = new Vec2(16f, 16f);

            index = 39;

            team = "Att";

            CooldownTime = 12f;
            Cooldown = 1;

            ShowCounter = true;
            CooldownRestorationModifier = 1.2f;

            requiresTakeOut = false;
        }

        public override void ApplyEffect()
        {
            base.ApplyEffect(); 
            if (user != null)
            {
                user.silentStep = true;
                user.invisibleForCams = 9;
                user.undetectable = 9;
            }
        }

        public override void WearOffEffect()
        {
            base.WearOffEffect(); 
            if (user != null)
            {
                user.silentStep = false;
            }
        }

        public override void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                if (sinceactivation > 0 && user != null)
                {
                    if (!user.observing && user.local)
                    {                        
                        DrawAbilityEffect(sinceactivation / 1);
                    }
                    foreach (ObservationThing obs in Level.CheckCircleAll<ObservationThing>(position, 160))
                    {
                        if (obs.observing && !user.local && obs.team != team)
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
            int iter = 18;
            for (int i = 0; i < iter + 1; i++)
            {
                modifier *= (iter * 0.5f - (float)Math.Abs(i - iter * 0.5f)) / iter + 0.7f;
                modifier *= (iter * 0.5f - (float)Math.Abs(i - iter * 0.5f)) / iter + 0.7f;

                if (i % 2 == 0)
                {
                    Graphics.DrawLine(pos + new Vec2(-1, -1 + i * cameraSize.y / iter), pos + new Vec2(cameraSize.x * (0.12f + 0.05f * _pulse) * modifier, -1 + i * cameraSize.y / iter), Color.Yellow * 0.3f, cameraSize.y / iter, 1f); 
                    Graphics.DrawLine(pos + new Vec2(-1 + cameraSize.x, -1 + i * cameraSize.y / iter), pos + new Vec2(3 + cameraSize.x - cameraSize.x * (0.12f + 0.05f * _pulse2) * modifier, -1 + i * cameraSize.y / iter), Color.Yellow * 0.3f, cameraSize.y / iter, 1f);
                }
                else
                {
                    Graphics.DrawLine(pos + new Vec2(-1, -1 + i * cameraSize.y / iter), pos + new Vec2(cameraSize.x * (0.12f + 0.05f * _pulse2) * modifier, -1 + i * cameraSize.y / iter), Color.Yellow * 0.3f, cameraSize.y / iter, 1f);
                    Graphics.DrawLine(pos + new Vec2(-1 + cameraSize.x, -1 + i * cameraSize.y / iter), pos + new Vec2(3 + cameraSize.x - cameraSize.x * (0.12f + 0.05f * _pulse) * modifier, -1 + i * cameraSize.y / iter), Color.Yellow * 0.3f, cameraSize.y / iter, 1f);
                }
            }
        }
    }
}
