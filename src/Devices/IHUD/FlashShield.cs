using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|IHUD")]
    public class FlashShield : BallisticShield, IDrawToDifferentLayers
    {
        public SpriteMap _flash;
        
        public int flashFrames = 36;
        private SinWave _pulse = 0.1f;

        //public int usings = 4;

        public FlashShield(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/FlashShield.png"), 32, 32, false);
            _flash = new SpriteMap(GetPath("Sprites/Devices/Flash.png"), 12, 14, false);
            _flash.CenterOrigin();
            _sprite.CenterOrigin();
            graphic = _sprite;

            _holdOffset = new Vec2(3f, -3f);
            
            holdback = true;
            jammResistance = false;

            color = Color.White;
            
            shieldKnife = new FlashShieldKnife(position.x, position.y);
        }

        public virtual void Flash()
        {
            _sprite.SetAnimation("flash");

            flashFrames = 0;
            if (oper != null)
            {
                if (oper.local)
                {
                    DuckNetwork.SendToEveryone(new NMBlitzFlash(oper.netIndex));
                }
                if (mainDevice == null && oper.local)
                {
                    PlayerStats.renown += 15;
                    PlayerStats.Save();
                    Level.Add(new RenownGained() { description = "Flash shield", amount = 15 });
                }
            }

            Level.Add(new SoundSource(position.x, position.y, 180, "SFX/Devices/FlashShield.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 180, "SFX/Devices/FlashShield.wav", "J"));

            _sprite.frame = 1;

            foreach (Operators d in Level.CheckRectAll<Operators>(position + new Vec2(-32f + 32 * offDir, -64f), position + new Vec2(32f + 32 * offDir, 64f)))
            {
                if (d != null && Level.CheckLine<Block>(position, d.position) == null)
                {
                    if (d.team != "Att" && d.local)
                    {
                        Level.Add(new Stunlight(position.x, position.y, 2f, 104f));
                        Level.Add(new Flashlight(position.x, position.y, 1f, 104f));
                    }
                }
            }
        }

        public override void Update()
        {
            base.Update();

            if (_sprite.frame == 3)
            {
                _sprite.frame = 0;
            }
            if (_sprite.frame == 2)
            {
                _sprite.frame = 3;
            }
            if (_sprite.frame == 1)
            {
                _sprite.frame = 2;
            }

            if (oper != null)
            {
                if (oper.mode != "crouch" && oper.mode != "normal")
                {
                    oper.ChangeWeapon(30, 2);
                    DuckNetwork.SendToEveryone(new NMChangeInventoryItem(30, 2, oper));
                }
                
                if (offDir > 0)
                {
                    scale = new Vec2(1, 1);
                }
                else
                {
                    scale = new Vec2(1, 1);
                }
            }
        }

        public override void OnDrawLayer(Layer layer)
        {
            base.OnDrawLayer(layer);
            if(layer == Layer.Foreground)
            {
                if (flashFrames < 36)
                {
                    flashFrames++;
                }
                Graphics.Draw(_flash, flashFrames / 5, x, y, 1f);
            }
        }
    }
}

