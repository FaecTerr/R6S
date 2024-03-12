using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Ping : Thing, IDrawToDifferentLayers
    {
        public SpriteMap _sprite;
        public float lifetime = 5f;
        public int fram;
        public Color color;

        public string Team;
        public Device device;
        public IDP defuser;

        SpriteMap _item = new SpriteMap(Mod.GetPath<R6S>("Sprites/DevicesIcons.png"), 24, 24);
        SpriteMap _sitem = new SpriteMap(Mod.GetPath<R6S>("Sprites/SDevicesIcons.png"), 24, 24);

        public Ping(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Ping.png"), 17, 19);
            _sprite.center = new Vec2(8.5f, 8.5f);

            _item.CenterOrigin();
            _sitem.CenterOrigin();

            _item.scale = new Vec2(0.4f, 0.4f);
            _sitem.scale = new Vec2(0.4f, 0.4f);
        }
        public override void Initialize()
        {
            base.Initialize();
            device = Level.CheckPoint<Device>(position);

            if(device != null && device.owner == null)
            {
                if(device is WoodenBarricadeAP)
                {
                    device = null;
                    return;
                }
                _sprite.frame = 1; 
                _sprite.center = new Vec2(8.5f, 7.5f);
            }
            defuser = Level.CheckPoint<IDP>(position);
            if(defuser != null && defuser.owner == null)
            {
                _sprite.frame = 1;
                _sprite.center = new Vec2(8.5f, 7.5f);
            }
        }

        public override void Update()
        {
            if(lifetime >= 0)
            {
                lifetime -= 0.01666666f;
            }
            else
            {
                Level.Remove(this);
            }
            _sprite.frame = fram;
            base.Update();
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if(pLayer == Layer.Foreground)
            {
                foreach (Operators oper in Level.current.things[typeof(Operators)])
                {
                    if (oper.team == Team)
                    {
                        Vec2 pos = position;
                        float angled = Maths.PointDirection(position, oper.position);
                        float dist = (position - oper.position).length / 16;
                        
                        if (pos.x < Level.current.camera.position.x + Level.current.camera.size.x * 0.05f)
                        {
                            pos.x = Level.current.camera.position.x + Level.current.camera.size.x * 0.05f;
                        }
                        if (pos.x > Level.current.camera.position.x + Level.current.camera.size.x * 0.95f)
                        {
                            pos.x = Level.current.camera.position.x + Level.current.camera.size.x * 0.95f;
                        }
                        if (pos.y < Level.current.camera.position.y + Level.current.camera.size.y * 0.05f)
                        {
                            pos.y = Level.current.camera.position.y + Level.current.camera.size.y * 0.05f;
                        }
                        if (pos.y > Level.current.camera.position.y + Level.current.camera.size.y * 0.95f)
                        {
                            pos.y = Level.current.camera.position.y + Level.current.camera.size.y * 0.95f;
                        }

                        _sprite.alpha = lifetime / 1f;
                        _sitem.alpha = lifetime / 1f;
                        _item.alpha = lifetime / 1f;
                        _sprite.angleDegrees = -angled + 90;
                        _sprite.color = color;
                        _sprite.scale = new Vec2(0.5f, 0.5f); 
                        if (defuser != null)
                        {
                            _sprite.frame = 1;
                            _sprite.scale = new Vec2(1.25f, 1.25f);
                            if (Team == "Att")
                            {
                                _sprite.color = Color.Blue;
                            }
                            else
                            {
                                _sprite.color = Color.Orange;
                            }

                            _sitem.frame = 22;
                            Graphics.Draw(_sitem, pos.x, pos.y, depth + 1);
                        }
                        else if (device != null && !(device is GunDev))
                        {
                            _sprite.frame = 1;
                            _sprite.scale = new Vec2(1.25f, 1.25f);
                            if (device.team == Team)
                            {
                                _sprite.color = Color.Blue;
                            }
                            else
                            {
                                _sprite.color = Color.Orange;
                            }
                            if (device.mainDevice != null)
                            {
                                if (device.mainDevice.isSecondary)
                                {
                                    _sitem.frame = device.mainDevice.index;
                                    Graphics.Draw(_sitem, pos.x, pos.y, depth + 1);
                                }
                                else
                                {
                                    _item.frame = device.mainDevice.index;
                                    Graphics.Draw(_item, pos.x, pos.y, depth + 1);
                                }
                            }
                        }
                        Graphics.Draw(_sprite, pos.x, pos.y, depth);
                        Graphics.DrawStringOutline(Math.Round(dist, 1).ToString(), pos + new Vec2(Math.Round(dist, 1).ToString().Length * (-4) * 0.4f, -4 * 0.4f + 8), Color.White * lifetime / 1f, Color.Black * lifetime / 1f, depth, null, 0.4f);
                    }
                }
            }
        }
    }
}
