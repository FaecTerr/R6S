using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Furniture")]
    public class Generator : Thing
    {
        SpriteMap _sprite;
        public float cooldown;
        bool turn = true;

        public Generator()
        {
            _sprite = new SpriteMap(GetPath("Sprites/Decorations/Generator.png"), 55, 48);
            _sprite.CenterOrigin();

            _sprite.AddAnimation("idle", 0.5f, true, new int[] { 0 });
            _sprite.AddAnimation("run", 0.5f, true, new int[] { 1, 2, 3, 4 });
            _sprite.SetAnimation("run");

            center = new Vec2(22.5f, 24f);
            graphic = _sprite;

            depth = -0.6f;

            collisionSize = new Vec2(55, 48);
            collisionOffset = new Vec2(-22.5f, -24);
        }

        public override void Draw()
        {
            foreach (Operators op in Level.CheckRectAll<Operators>(topLeft, bottomRight))
            {
                if (op.local && !op.observing && op.priorityTaken < 0.5f)
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
            if(cooldown > 0)
            {
                cooldown--;
            }
            foreach (Operators op in Level.CheckRectAll<Operators>(topLeft, bottomRight))
            {
                if (op.local && cooldown <= 0 && (Keyboard.Released(PlayerStats.keyBindings[4]) || Keyboard.Released(PlayerStats.keyBindingsAlternate[4])) && !op.observing && op.priorityTaken < 0.5f)
                {
                    turn = !turn;
                    cooldown = 50;

                    if (turn)
                    {
                        _sprite.SetAnimation("run");
                    }
                    else
                    {
                        _sprite.SetAnimation("idle");
                    }

                    Level.Add(new SoundSource(position.x, position.y, 320, "SFX/generator_on.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, "SFX/generator_on.wav", "J"));

                    foreach (LightningSun s in Level.current.things[typeof(LightningSun)])
                    {
                        if (turn)
                        {
                            s.TurnOn();
                        }
                        else
                        {
                            s.TurnOff();
                        }
                    }
                }
            }
        }
    }
}
