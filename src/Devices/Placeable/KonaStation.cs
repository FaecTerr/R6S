using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class KonaStation : Placeable
    {
        public KonaStation(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/KonaStation.png"), 16, 10, false);
            graphic = this._sprite;
            center = new Vec2(8f, 5);
            collisionSize = new Vec2(16f, 10f);
            collisionOffset = new Vec2(-8f, -5f);
            setTime = 1f;
            CheckRect = new Vec2(9f, 0f);
            weight = 0.9f;
            thickness = 0.1f;

            placeable = true;
            breakable = true;
            electricible = false;

            index = 54;
            UsageCount = 3;

            DeviceCost = 15;

            destroyedPoints = "Kona station destroyed";
            cantProne = true;
        }

        public override void SetAfterPlace()
        {
            afterPlace = new KonaStationAP(position.x, position.y);
            base.SetAfterPlace();
        }

    }
    public class KonaStationAP : Rocky
    {
        public Vec2 gPos;
        public float radius = 64;

        SpriteMap _cd = new SpriteMap(Mod.GetPath<R6S>("Sprites/whiteDot.png"), 1, 1);
        public KonaStationAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/KonaStation.png"), 16, 10, false);

            graphic = _sprite;
            center = new Vec2(8f, 5);
            collisionSize = new Vec2(16f, 10f);
            collisionOffset = new Vec2(-8f, -5f);

            weight = 0.9f;
            thickness = 0.1f;

            placeable = true;
            breakable = true;
            electricible = false;
            sticky = true;
            bulletproof = false;
            scannable = true;

            DeviceCost = 15;

            destroyedPoints = "Kona station destroyed";
            CooldownTime = 1f;

            placings = 10;
            setFrames = (int)radius * 4;
            UsageCount = 1;
        }
        public override void Update()
        {
            if (!jammed)
            {
                if (Cooldown <= 0 && setFrames <= 0) 
                {
                    Operators healed = null;
                    foreach (Operators operators in Level.CheckCircleAll<Operators>(position, radius))
                    {
                        if (Level.CheckLine<Block>(operators.position, position) == null)
                        {
                            if (healed != null)
                            {
                                if (healed.Health > operators.Health)
                                {
                                    healed = operators;
                                }
                                else
                                {
                                    if (healed.team != operators.team && operators.team == team)
                                    {
                                        healed = operators;
                                    }
                                    else if ((healed.position - position).length > (operators.position - position).length)
                                    {
                                        healed = operators;
                                    }
                                }
                            }
                            else
                            {
                                healed = operators;
                            }
                        }
                    }
                    if(healed != null)
                    {
                        healed.effects.Add(new Overheal());
                        UsageCount = 0;
                        Cooldown = CooldownTime;
                    }
                }
                else
                {
                    Cooldown -= 1 / (60 * 30);
                    if(Cooldown <= 0)
                    {
                        UsageCount = 1;
                        //PlaySFX
                    }
                }
            }
            base.Update();
        }
        public override void Draw()
        {
            base.Draw();
            if (setFrames > 0 && setted && oper != null && oper.local)
            {
                Graphics.DrawCircle(position, radius - setFrames / 4, Color.White, 2f, 1f, 32);
                setFrames -= 1;
            }
            if(Cooldown > 0 && setted && oper != null && oper.local)
            {
                for (float i = 0; i < CooldownTime; i += 0.01f)
                {
                    float angled = Cooldown / CooldownTime * (float)Math.PI * 2;
                    _cd.scale = new Vec2(1.6f, 0.6f);
                    _cd.CenterOrigin();

                    _cd.angle = angled;

                    Graphics.Draw(_cd, position.x + (0.5f + 8f * (float)Math.Cos(angled)),
                        Level.current.camera.position.y + (0.5f + 8f * (float)Math.Sin(angled)));
                }
            }
        }
    }
}
