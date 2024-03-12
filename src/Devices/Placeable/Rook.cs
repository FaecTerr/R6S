using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Deployable")]
    public class RookArmor : Placeable
    {
        public SpriteMap _sprite2;
        public int plates = 4;
        public float equipping = 0;
        public List<Operators> equippedDucks = new List<Operators>();

        public RookArmor(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/RookArmor.png"), 24, 10, false);
            _sprite.CenterOrigin();
            graphic = _sprite;
            _sprite2 = new SpriteMap(GetPath("Sprites/Devices/RookArmorCounter.png"), 12, 10, false);
            _sprite2.CenterOrigin();
            center = new Vec2(12f, 5f);
            collisionSize = new Vec2(22f, 8f);
            collisionOffset = new Vec2(-11f, -4f);
            setTime = 1f;
            CheckRect = new Vec2(24f, 0f);
            electricible = false;
            scannable = false;
            canPick = false;

            UsageCount = 1;

            index = 12;

            cantProne = true;

            placeSound = "SFX/Devices/RookArmorPlaced.wav";
        }

        public override void SetAfterPlace()
        {
            afterPlace = new RookArmorAP(position.x, position.y);
            base.SetAfterPlace();
        }
    }


    public class RookArmorAP : Device
    {
        public SpriteMap _sprite2;
        public int plates = 4;
        public float equipping = 0;
        public List<Operators> equippedDucks = new List<Operators>();

        public RookArmorAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/RookArmor.png"), 24, 10, false);
            _sprite.CenterOrigin();
            graphic = this._sprite;
            _sprite2 = new SpriteMap(GetPath("Sprites/Devices/RookArmorCounter.png"), 12, 10, false);
            _sprite2.CenterOrigin();
            center = new Vec2(12f, 5f);
            collisionSize = new Vec2(22f, 8f);
            collisionOffset = new Vec2(-11f, -4f);
            setTime = 1f;
            thickness = 2;
            electricible = false;
            scannable = false;
            canPick = false;
            breakable = true;
            health = 66;

            jammResistance = true;
        }
        public override void Update()
        {
            base.Update();
            setted = false;
            if (equipping > 0)
            {
                equipping -= 0.01666666f;
            }
            _sprite.frame = 1;
            foreach (Operators d in Level.CheckRectAll<Operators>(this.topLeft, this.bottomRight))
            {
                if (plates > 0 && !equippedDucks.Contains(d) && (Keyboard.Down(PlayerStats.keyBindings[4]) || Keyboard.Down(PlayerStats.keyBindings[4])))
                {
                    if (equipping == 0)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 80, "SFX/Devices/TakeArmor.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 80, "SFX/Devices/TakeArmor.wav", "J"));
                    }
                    equipping += 0.03222222f;
                    if (equipping >= 1f)
                    {
                        equipping = 0;
                        equippedDucks.Add(d);
                        plates--;
                        d.Armor += 1;
                        d.DeathTime = 60;
                        d.maxDowns += 1;

                        bool exits = false;
                        foreach (Effect f in d.effects)
                        {
                            if (f.name == "Extra armor")
                            {
                                exits = true;
                                f.timer = 0.1f;
                            }
                        }
                        if (exits == false)
                        {
                            d.effects.Add(new ExtraArmor());
                        }

                        if (oper != null)
                        {
                            if (oper.local)
                            {
                                PlayerStats.renown += 5;
                                PlayerStats.Save();
                                Level.Add(new RenownGained() { description = "Armor plate", amount = 5 });
                            }
                        }
                    }
                }
            }

        }

        public override void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                foreach (Operators d in Level.CheckRectAll<Operators>(topLeft, bottomRight))
                {
                    if (canPick && (d.position - position).length < 32f && Level.CheckLine<Block>(position, d.position) == null && d.local)
                    {
                        Vec2 pos = Level.current.camera.position;
                        Vec2 cameraSize = Level.current.camera.size;
                        Vec2 Unit = cameraSize / new Vec2(320, 180);

                        string text = "Hold   to pick up";
                        Graphics.DrawString(text, pos + new Vec2(cameraSize.x / 320 * 160 - text.Length / 2 * 9, cameraSize.y / 180 * 70), Color.White, 1f, null);

                        SpriteMap _widebutton = new SpriteMap(GetPath("Sprites/Keys.png"), 34, 17);
                        _widebutton.CenterOrigin();
                        _widebutton.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                        SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                        _button.CenterOrigin();
                        _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                        if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[4]))
                        {
                            _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[4]);
                            Graphics.Draw(_widebutton, pos.x + (160 - text.Length / 2 * 9 + 44) * Unit.x, pos.y + 73 * Unit.x, 1);
                        }
                        else
                        {
                            _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[4]);
                            Graphics.Draw(_button, pos.x + (160 - text.Length / 2 * 9 + 44) * Unit.x, pos.y + 73 * Unit.x, 1);
                        }

                        if (equipping > 0)
                        {
                            Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200 * equipping, cameraSize.y / 180 * 120), Color.White, 1f, 1f);
                            Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200, cameraSize.y / 180 * 120), Color.Gray, 1f, 0.95f);
                        }
                    }
                }
            }
            base.OnDrawLayer(layer);
        }


        public override void Draw()
        {
            base.Draw();

            if (plates <= 0)
            {
                _sprite2.frame = 2;
            }
            else
            {
                _sprite2.frame = 0;
            }
            if (_sprite2 != null)
            {
                Graphics.Draw(_sprite2, position.x, position.y - 12f);
            }
        }
    }
}