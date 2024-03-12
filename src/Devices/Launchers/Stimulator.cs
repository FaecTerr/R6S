using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Weapon")]
    public class Stimulator : Launchers
    {
        public Stimulator(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/StimulatorGun.png"), 18, 10, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(7f, 5f);
            collisionSize = new Vec2(10f, 10f);
            collisionOffset = new Vec2(-5f, -5f);
            _canRaise = true;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            breakable = false;
            zeroSpeed = false;
            scannable = false;

            jammResistance = true;

            DeviceCost = 20;
            descriptionPoints = "Stim shot";

            Missiles1 = 3;
            index = 14;


            placeSound = "SFX/Devices/DocHeal.wav";
        }

        public override void LaunchMissile()
        {
            base.LaunchMissile();
            Level.Add(new SoundSource(position.x, position.y, 240, placeSound, "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, placeSound, "J"));
        }

        public override void Update()
        {
            base.Update();
            if(oper != null)
            {
                if(oper.holdObject == this && Missiles1 > 0)
                {
                    if(oper.local && (Keyboard.Pressed(PlayerStats.keyBindings[9]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[9])))
                    {
                        Missiles1--;
                        reload = 2;

                        if (oper.Health > 60)
                        {
                            if (!oper.HasEffect("Overhealed"))
                            {
                                oper.effects.Add(new Overheal() { timer = 3 + (oper.Health - 60) / 2, maxTimer = 3 + (oper.Health - 60) / 2, team = oper.team });
                            }
                            else
                            {
                                oper.GetEffect("Overhealed").timer = 23;
                                oper.GetEffect("Overhealed").maxTimer = 23;
                            }
                        }

                        oper.Health += 40;
                        
                        Level.Add(new SoundSource(position.x, position.y, 240, placeSound, "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, placeSound, "J"));
                    }
                }
            }
        }

        public override void SetMissile()
        {
            missile = new HealBullet(position.x + 6f * offDir, position.y + 2f);
            base.SetMissile();
        }
    }
}
