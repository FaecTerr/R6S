using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Furniture")]
    public class Helicopter : Thing
    {
        SpriteMap _sprite;
        public float animation;
        Vec2 posi;

        public Helicopter()
        {
            _sprite = new SpriteMap(GetPath("Sprites/Decorations/Helicopter.png"), 128, 64);
            _sprite.CenterOrigin();

            _sprite.AddAnimation("idle", 0.5f, true, new int[] { 0, 1, 2});
            _sprite.SetAnimation("idle");

            center = new Vec2(64, 32);
            graphic = _sprite;

            depth = -0.6f;

            collisionSize = new Vec2(128, 64);
            collisionOffset = new Vec2(-64, -32);
        }

        public override void Draw()
        {
            if (animation > 0)
            {
                animation -= 0.03f;
                _sprite.angle = (5 - animation) * 0.1f;
                _sprite.alpha = (animation) / 5;
                Graphics.Draw(_sprite, posi.x + (5 - animation) * 50, posi.y - (5 - animation) * 14);
            }
            foreach (Operators op in Level.CheckRectAll<Operators>(topLeft, bottomRight))
            {
                if (op.local && !op.observing && op.team == "Att" && op.priorityTaken < 0.5f)
                {
                    Vec2 pos = Level.current.camera.position;
                    Vec2 cameraSize = Level.current.camera.size;
                    Vec2 Unit = cameraSize / new Vec2(320, 180);

                    SpriteMap _widebutton = new SpriteMap(GetPath("Sprites/Keys.png"), 34, 17);
                    _widebutton.CenterOrigin();
                    _widebutton.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                    SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                    _button.CenterOrigin();
                    _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                    if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[4]))
                    {
                        _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[4]);
                        Graphics.Draw(_widebutton, position.x, position.y - 16, 1.2f);
                    }
                    else
                    {
                        _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[4]);
                        Graphics.Draw(_button, position.x, position.y - 16, 1.2f);
                    }
                }
            }

            base.Draw();
        }

        public override void Update()
        {
            base.Update();
            Vec2 pos = position;
            if(Level.Nearest<HelicopterPort>(position.x, position.y) != null)
            {
                pos = Level.Nearest<HelicopterPort>(position.x, position.y).position;
            }
                        
            foreach (Operators op in Level.CheckRectAll<Operators>(topLeft, bottomRight))
            {
                if(op.local && (Keyboard.Pressed(PlayerStats.keyBindings[4]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[4])) && !op.observing && op.team == "Att" && op.priorityTaken < 0.5f)
                {
                    op.position = pos;
                    posi = pos + new Vec2(0, -64);
                    animation = 5;
                    op._sleeping = false;
                }
            }
        }
    }
    [EditorGroup("Faecterr's|Furniture")]
    public class HelicopterPort : Thing
    {
        SpriteMap _sprite;
        public HelicopterPort()
        {
            _sprite = new SpriteMap(GetPath("Sprites/Decorations/HP.png"), 18, 18);
            _sprite.CenterOrigin();

            center = new Vec2(9, 9);
            graphic = _sprite;

            collisionSize = new Vec2(32, 32);
            collisionOffset = new Vec2(-16, -16);
            hugWalls = WallHug.Floor;
        }
    }
}
