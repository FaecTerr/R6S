using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Throwable")]
    public class Selma : Throwable
    {
        public Selma(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Selma.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            setTime = 0.8f;
            index = 53;

            sticky = true;

            weightR = 7;

            UsageCount = 3;

            placeSound = "SFX/Devices/RtilaStick.wav";
            pickupSound = "SFX/Devices/RtilaPick.wav";

            minimalTimeOfHolding = 0.75f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new SelmaAP(position.x, position.y);
            base.SetRocky();
        }
    }

    public class SelmaAP : Rocky
    {
        public bool setNext = true;
        float detonationTime = 3.2f;
        bool initiateExplosion;
        public SelmaAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Selma.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 1;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(6f, 6f);
            collisionOffset = new Vec2(-3f, -3f);

            sticky = true;

            scannable = true;
            breakable = true;
            electricible = true;
        }

        public override void Set()
        {
            base.Set();
            //SOUND
        }

        public override void Update()
        {
            base.Update();
            if(setted && stickedTo != null && (stickedTo is Device || stickedTo is BreakableSurface))
            {
                if (Dir.x != 0 && setNext && stickedTo != null && (stickedTo is BreakableSurface) &&
    Level.CheckPoint<BreakableSurface>(new Vec2(stickedTo.position.x, position.y + 9)) != null)
                {
                    Level.Add(new SelmaAP(position.x, position.y + 9) { setNext = false, detonationTime = detonationTime * 2, stickedTo = stickedTo, mainDevice = mainDevice, setted = true, 
                        Dir = Dir, enablePhysics = false, offDir = offDir });
                    setNext = false;
                }
                if (!initiateExplosion)
                {
                    initiateExplosion = true;
                    for (int i = 0; i < 9; i++)
                    {
                        Level.Add(Spark.New(position.x + 16f * Dir.x, position.y, new Vec2(Rando.Float(1, 2) * offDir, Rando.Float(-2, 2))));
                    }
                }
            }
            if (initiateExplosion)
            {
                canPick = false;
                if(detonationTime > 0)
                {
                    detonationTime -= 0.01f;
                }
                else
                {
                    float radius = 5;
                    if(Dir.y != 0)
                    {
                        radius = 2;
                    }
                    Level.Add(new Explosion(position.x + Math.Abs(Dir.x) * offDir * 4, position.y + (Dir.y) * 4, radius, 5, "S") { shootedBy = oper });
                    Level.Add(new Explosion(position.x + Math.Abs(Dir.x) * offDir * 4, position.y + (Dir.y) * 4, radius, 5, "H") { shootedBy = oper });


                    Level.Add(new Explosion(position.x + Math.Abs(Dir.x) * offDir * 12, position.y + (Dir.y) * 12, radius, 5, "S") { shootedBy = oper });
                    Level.Add(new Explosion(position.x + Math.Abs(Dir.x) * offDir * 12, position.y + (Dir.y) * 12, radius, 5, "H") { shootedBy = oper });


                    DuckNetwork.SendToEveryone(new NMExplosion(position + new Vec2(Math.Abs(Dir.x) * 4 * offDir, (Dir.y) * 4), radius, 5, "S", oper));
                    DuckNetwork.SendToEveryone(new NMExplosion(position + new Vec2(Math.Abs(Dir.x) * 4 * offDir, (Dir.y) * 4), radius, 5, "H", oper));

                    DuckNetwork.SendToEveryone(new NMExplosion(position + new Vec2(Math.Abs(Dir.x) * 12 * offDir, (Dir.y) * 12), radius, 5, "S", oper));
                    DuckNetwork.SendToEveryone(new NMExplosion(position + new Vec2(Math.Abs(Dir.x) * 12 * offDir, (Dir.y) * 12), radius, 5, "H", oper));
                    
                    foreach (SurfaceStationary sf in Level.CheckCircleAll<SurfaceStationary>(position, radius * 2))
                    {
                        sf.Breach(500000);
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
