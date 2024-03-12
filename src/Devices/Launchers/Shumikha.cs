using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Weapon")]
    public class Shumikha : Launchers
    {


        public Shumikha(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Shumikha.png"), 32, 10, false);
            graphic = this._sprite;
            _sprite.frame = 0;
            center = new Vec2(16f, 5f);
            collisionSize = new Vec2(16f, 10f);
            collisionOffset = new Vec2(-8f, -5f);
            _canRaise = true;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            breakable = false;
            zeroSpeed = false;
            scannable = false;

            jammResistance = true;

            DeviceCost = 10;
            descriptionPoints = "Shumikha launcher";

            Missiles1 = 10;
            speed = 0.2f;
            vlSpeed = 1.5f;
            index = 4;

            placeSound = "SFX/Devices/ShumikhaLaunch.wav";
        }

        public override void SetMissile()
        {
            Vec2 pos = new Vec2(position.x + (float)(Math.Cos(angle) * 16 * offDir), position.y - (float)(Math.Sin(angle) * 3));
            missile = new ShumikhaGrenade(pos.x, pos.y);
            base.SetMissile();
        }
    }
}
