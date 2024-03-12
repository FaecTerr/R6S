using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Guns|Devices|IHUD")]
    public class Garra : Device
    {
        public List<Device> scannedDevices = new List<Device>();
        public SpriteMap _garra = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/GarraState.png"), 16, 16);

        public float inAnimation;

        public Garra(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Garra.png"), 32, 14, false);
            graphic = _sprite;
            center = new Vec2(16f, 7f);
            collisionOffset = new Vec2(-3f, -3f);
            collisionSize = new Vec2(6f, 6f);
            weight = 0.9f;
            placeable = false;
            breakable = false;
            zeroSpeed = false;
            scannable = false;

            depth = 0.6f;

            jammResistance = true;

            index = 3;
            _holdOffset = new Vec2(7, 0);
            _garra.CenterOrigin();
        }

        public override void Update()
        {
            base.Update();
            if(owner == null)
            {
                frame = 0;
            }
            if (oper != null)
            {
                if (oper.holdObject == this && oper.local && (Keyboard.Pressed(PlayerStats.keyBindings[13]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[13])))
                {

                }
            }
        }

        public void DrawHookUI(Vec2 point)
        {
            if(user != null)
            {
                if ((point - user.position).length > 160 || Level.CheckLine<Block>(point, user.position) != null)
                {
                    _garra.frame = 1;
                    _garra.color = Color.Orange;
                }
                else
                {
                    _garra.frame = 0;
                    _garra.color = Color.White;
                }
                Graphics.Draw(_garra, point.x, point.y);
            }
        }

        public override void Draw()
        {
            if (user != null && user.aim != null && user.holdObject == this)
            {
                if (Level.CheckPoint<DoorFrame>(user.aim) != null)
                {
                    Vec2 point = Level.CheckPoint<DoorFrame>(user.aim).position;

                    DrawHookUI(point);
                }
                else if (Level.CheckPoint<HatchLeftover>(user.aim) != null)
                {
                    Vec2 point = Level.CheckPoint<HatchLeftover>(user.aim).position;

                    DrawHookUI(point);
                }
            }
            base.Draw();
        }
    }
}
