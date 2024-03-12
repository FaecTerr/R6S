using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Glance : Device
    {
        public bool enabled;
        public bool disabled;

        public float sinceactivation;

        public Glance(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/JackalTracker.png"), 32, 32, false);
            graphic = _sprite;

            _sprite.frame = 0;
            center = new Vec2(16f, 16f);

            index = 38;

            team = "Def";

            CooldownTime = 16f;
            Cooldown = 1;

            ShowCounter = true;
            CooldownRestorationModifier = 0.8f;
            requiresTakeOut = false;
        }

        public override void PocketActivation()
        {
            base.PocketActivation(); 
            if (!enabled && Cooldown <= MinimalCooldownValue)
            {

            }
            else
            {
                enabled = !enabled;
                if(user != null && user.HasEffect("EMP'd"))
                {
                    enabled = false;
                }
                if (enabled)
                {
                    Cooldown -= CooldownTakeoffOnActivation;
                    Level.Add(new SoundSource(position.x, position.y, 96, "SFX/Devices/GlanceOn.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 96, "SFX/Devices/GlanceOn.wav", "J"));
                }
                else
                {
                    Level.Add(new SoundSource(position.x, position.y, 96, "SFX/Devices/GlanceOff.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 96, "SFX/Devices/GlanceOff.wav", "J"));
                }
            }
        }

        public override void Update()
        {
            base.Update();
            UsageCount = 59;


            if (user != null)
            {
                if (enabled)
                {
                    user.seeTSmokeFrames = 9;

                    user.flashImmuneFrames = 9;
                    Cooldown -= 0.01666666f / CooldownTime;
                    if (sinceactivation < 1)
                    {
                        sinceactivation += 0.02f;
                    }
                }
                else
                {
                    if (Cooldown < 1 && sinceactivation <= 0)
                    {
                        Cooldown += 0.01666666f / CooldownTime * CooldownRestorationModifier;
                    }

                    if (sinceactivation > 0)
                    {
                        sinceactivation -= 0.01f;
                    }
                }
            }

            if (Cooldown <= 0)
            {
                if (enabled)
                {
                    enabled = false;                    
                }
            }


            if (user != null)
            {
                if (user.isDead)
                {
                    if (enabled)
                    {
                        enabled = false;
                    }
                }
                if(user.HasEffect("EMP'd") && enabled)
                {
                    enabled = false;
                }
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
            Graphics.DrawRect(pos + new Vec2(-1, -1), pos + cameraSize + new Vec2(1, 1), Color.Blue * 0.2f * modifier, 1f, true);
            Graphics.DrawCircle(pos + new Vec2(0.25f * cameraSize.x, 0.5f * cameraSize.y), cameraSize.y * 0.3f * modifier, Color.Blue * 0.4f * modifier, Unit.x, 1f, 7);
            Graphics.DrawCircle(pos + new Vec2(0.75f * cameraSize.x, 0.5f * cameraSize.y), cameraSize.y * 0.3f * modifier, Color.Blue * 0.4f * modifier, Unit.x, 1f, 7); 
            Graphics.DrawCircle(pos + new Vec2(0.25f * cameraSize.x, 0.0f * cameraSize.y), cameraSize.y * 0.3f * modifier, Color.Blue * 0.4f * modifier, Unit.x, 1f, 7);
            Graphics.DrawCircle(pos + new Vec2(0.75f * cameraSize.x, 0.0f * cameraSize.y), cameraSize.y * 0.3f * modifier, Color.Blue * 0.4f * modifier, Unit.x, 1f, 7);
            Graphics.DrawCircle(pos + new Vec2(0.25f * cameraSize.x, 1f * cameraSize.y), cameraSize.y * 0.3f * modifier, Color.Blue * 0.4f * modifier, Unit.x, 1f, 7);
            Graphics.DrawCircle(pos + new Vec2(0.75f * cameraSize.x, 1f * cameraSize.y), cameraSize.y * 0.3f * modifier, Color.Blue * 0.4f * modifier, Unit.x, 1f, 7);


            Graphics.DrawCircle(pos + new Vec2(0.0f * cameraSize.x, 0.25f * cameraSize.y), cameraSize.y * 0.15f * modifier, Color.Blue * 0.4f * modifier, Unit.x, 1f, 7);
            Graphics.DrawCircle(pos + new Vec2(0.0f * cameraSize.x, 0.75f * cameraSize.y), cameraSize.y * 0.15f * modifier, Color.Blue * 0.4f * modifier, Unit.x, 1f, 7); 
            Graphics.DrawCircle(pos + new Vec2(1.0f * cameraSize.x, 0.25f * cameraSize.y), cameraSize.y * 0.15f * modifier, Color.Blue * 0.4f * modifier, Unit.x, 1f, 7);
            Graphics.DrawCircle(pos + new Vec2(1.0f * cameraSize.x, 0.75f * cameraSize.y), cameraSize.y * 0.15f * modifier, Color.Blue * 0.4f * modifier, Unit.x, 1f, 7);
            Graphics.DrawCircle(pos + new Vec2(0.5f * cameraSize.x, 0.25f * cameraSize.y), cameraSize.y * 0.15f * modifier, Color.Blue * 0.4f * modifier, Unit.x, 1f, 7);
            Graphics.DrawCircle(pos + new Vec2(0.5f * cameraSize.x, 0.75f * cameraSize.y), cameraSize.y * 0.15f * modifier, Color.Blue * 0.4f * modifier, Unit.x, 1f, 7);
        }
    }
}
