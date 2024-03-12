using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BlackbeardShield : Device
    {
        public bool enabled;
        public bool disabled;

        public BlackbeardShield(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/JackalTracker.png"), 32, 32, false);
            graphic = _sprite;

            _sprite.frame = 0;
            center = new Vec2(16f, 16f);

            index = 43;
            team = "Att";

            ShowCounter = true;
            jammResistance = true;
            scannable = false;

            UsageCount = 4;
        }

        public override void Update()
        {
            base.Update();
            if (oper != null && UsageCount > 0)
            {
                if (user == null)
                {
                    user = oper;
                }
                enabled = !enabled;

                if (enabled)
                {

                }
                if (user.local)
                {
                    //DuckNetwork.SendToEveryone(new NMInvisForDrones(enabled));
                }
            }

            if(UsageCount <= 0)
            {
                enabled = false;
            }

            if (user != null)
            {
                if (enabled && user.holdIndex == 1)
                {
                    user.Armor = 3;
                    user.Speed = 1;
                    user.HeadshotDamageResist = 0.2f; 
                    if (user.bulletImmuneFrames == 1)
                    {
                        UsageCount--;
                    }
                }
                else
                {
                    user.Armor = 2;
                    user.Speed = 2;
                    user.HeadshotDamageResist = 0f;
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
            if (layer == Layer.Game)
            {
                if (user != null && user.holdObject != null && user.holdIndex == 1)
                {
                    SpriteMap _shield = new SpriteMap(GetPath("Sprites/Devices/BBshield.png"), 5, 18);
                    if(user.holdObject is GunDev)
                    {
                        Graphics.Draw(_shield, position.x + (user.holdObject as GunDev)._barrel.x, position.y + (user.holdObject as GunDev)._barrel.y);
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
        }
    }
}
