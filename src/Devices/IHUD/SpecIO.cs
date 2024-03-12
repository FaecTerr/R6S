using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Guns|Devices|IHUD")]
    public class SpecIO : Device, IDrawToDifferentLayers
    {
        public float radius1 = 20;

        SpriteMap _item = new SpriteMap(Mod.GetPath<R6S>("Sprites/DevicesIcons.png"), 24, 24);
        SpriteMap _sitem = new SpriteMap(Mod.GetPath<R6S>("Sprites/SDevicesIcons.png"), 24, 24);

        bool enabled;

        public SpecIO(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/SpecIO.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            weight = 0.9f;
            placeable = false;
            breakable = false;
            zeroSpeed = false;
            scannable = true;

            depth = 0.6f;

            index = 64;

            switchOffTime = 20;

            _item.CenterOrigin();
            _sitem.CenterOrigin();

            _item.scale = new Vec2(0.8f, 0.8f);
            _sitem.scale = new Vec2(0.8f, 0.8f);

            CooldownTime = 12f;
            Cooldown = 1;
            MinimalCooldownValue = 0.2f;
            CooldownTakeoffOnActivation = 0.05f;

            ShowCooldown = true;
            ShowCounter = false;
        }

        public override void Update()
        {
            base.Update();
            if (user != null && !(user.holdObject is GunDev))
            {
                if (!enabled)
                {
                    enabled = true;
                    Cooldown -= CooldownTakeoffOnActivation;
                    unableToTake = 1;
                    if (Cooldown <= MinimalCooldownValue)
                    {
                        user.BackToWeapon(20);
                        enabled = false;
                    }
                }
                Cooldown -= 0.01666666f / CooldownTime;
                if(Cooldown <= 0)
                {
                    Cooldown = 0;
                    enabled = false;
                    user.BackToWeapon(30);
                }
            }
            else
            {
                if (enabled)
                {
                    enabled = false;
                    Cooldown -= 0.2f;
                    if(Cooldown < 0)
                    {
                        Cooldown = 0;
                    }
                }
                Cooldown += 0.01666666f / (CooldownTime);
                if(Cooldown > 1)
                {
                    Cooldown = 1;
                }
            }
        }

        public override void OnDrawLayer(Layer layer)
        {
            base.OnDrawLayer(layer);
            if(layer == Layer.Foreground)
            {
                if (user != null && user.local && enabled)
                {
                    Graphics.DrawRect(Level.current.camera.position + new Vec2(-1, -1), Level.current.camera.position + Level.current.camera.size + new Vec2(1, 1), Color.Yellow * 0.3f, depth - 1);
                    foreach (Device d in Level.current.things[typeof(Device)])
                    {
                        if (!(d is GunDev) && d.mainDevice != null && d.scannable && d.team == team)
                        {
                            Graphics.DrawCircle(d.position, radius1, Color.White, 2f, 0.5f, 7);

                            if (d.mainDevice != null)
                            {
                                if (d.mainDevice.isSecondary || d.isSecondary)
                                {
                                    _sitem.frame = d.mainDevice.index;
                                    Graphics.Draw(_sitem, d.position.x, d.position.y, depth + 1);
                                }
                                else
                                {
                                    _item.frame = d.mainDevice.index;
                                    Graphics.Draw(_item, d.position.x, d.position.y, depth + 1);
                                }
                            }
                        }

                    }
                }
            }
        }

        public override void Draw()
        {
            
            base.Draw();
        }
    }
}
