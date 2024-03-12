using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class SilentStep : Device
    {
        public bool enabled;
        public bool disabled;
        public float sinceactivation;

        public SilentStep(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/JackalTracker.png"), 32, 32, false);
            graphic = _sprite;

            _sprite.frame = 0;
            center = new Vec2(16f, 16f);

            index = 42;
            team = "Def";

            CooldownTime = 12f;
            Cooldown = 1;

            ShowCounter = true;
            CooldownRestorationModifier = 1.2f;
            jammResistance = true;
            scannable = false;

            mainHand = true;
            oneHand = true;
        }

        public override void Update()
        {
            base.Update();
            UsageCount = 59;
            if (oper != null)
            {
                if (user == null)
                {
                    user = oper;
                }
                if (!enabled && Cooldown <= MinimalCooldownValue)
                {

                }
                else
                {
                    if (user.local)
                    {
                        //DuckNetwork.SendToEveryone(new NMInvisForDrones(enabled));
                    }
                }
            }

            if(user != null)
            {
                if(user.holdObject == this)
                {
                    if(enabled == false)
                    {
                        Cooldown -= CooldownTakeoffOnActivation;
                    }
                    enabled = true;
                }
                else
                {
                    enabled = false;
                }
            }

            if (enabled)
            {
                if (user != null)
                {
                    user.silentStep = true;
                    user.invisibleForCams = 9;
                    user.undetectable = 9;
                }
                Cooldown -= 0.01666666f / CooldownTime;
                if (sinceactivation < 1)
                {
                    sinceactivation += 0.02f;
                }
            }
            else
            {
                if (user != null)
                {
                    user.silentStep = false;
                }
                if (Cooldown < 1 && sinceactivation <= 0)
                {
                    Cooldown += 0.01666666f / CooldownTime * CooldownRestorationModifier;
                }

                if (sinceactivation > 0)
                {
                    sinceactivation -= 0.01f;
                }
            }


            if (user != null)
            {
                if (!user.isDead)
                {
                    if (enabled)
                    {

                    }
                }
                if (Cooldown <= 0)
                {
                    if (enabled)
                    {
                        enabled = false;
                        DuckNetwork.SendToEveryone(new NMInvisForDrones(enabled));
                        user.BackToWeapon(30);
                    }
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

            Sprite _overlay = new Sprite(GetPath("Sprites/VerticalGradient.png"));
            _overlay.scale = cameraSize / new Vec2(32, 32);
            _overlay.alpha = modifier;
            Graphics.Draw(_overlay, pos.x, pos.y);
        }
    }
}
