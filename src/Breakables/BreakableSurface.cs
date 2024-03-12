using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class HatchLeftover : Thing 
    {
        
        public HatchLeftover(float xval, float yval) : base(xval, yval)
        {

        }

        public override void Update()
        {
            base.Update();
        }
    }

    public class SurfaceStationary : Thing, IDrawToDifferentLayers
    {
        public bool isHatch;
        public bool horizontal;
        public bool vertical;

        private int reinforcementCooldown;
        public int ElectroFrames;
        public float reinforcement;
        public bool electrified;

        public bool reinforced;
        public float health = 100000;

        public SurfaceStationary(float xval, float yval) : base(xval, yval)
        {

        }

        public override void Update()
        {
            GamemodeScripter gm = null;
            collisionOffset = new Vec2(-collisionSize.x * 0.5f, -collisionSize.y * 0.5f);
            foreach (GamemodeScripter g in Level.current.things[typeof(GamemodeScripter)])
            {
                gm = g;
            }
            base.Update();

            if (electrified)
            {
                if (ElectroFrames <= 0) //visual effect of electricity
                {
                    Level.Add(new Electricity(x, y, 32f, alpha));
                    Level.Add(new Electricity(topLeft.x, topLeft.y, 32f, alpha));
                    Level.Add(new Electricity(bottomRight.x, bottomRight.y, 32f, alpha));
                    electrified = false;
                    ElectroFrames = 8;
                }
                if (ElectroFrames > 0)
                {
                    ElectroFrames--;
                    if (ElectroFrames == 4)
                    {
                        Level.Add(new Electricity(x, y, 32f, alpha));
                        Level.Add(new Electricity(topRight.x, topRight.y, 32f, alpha));
                        Level.Add(new Electricity(bottomLeft.x, bottomLeft.y, 32f, alpha));
                    }
                }
                foreach (Operators d in Level.CheckRectAll<Operators>(topLeft, bottomRight)) //electricing operators
                {
                    if (d.elecFrames <= 0 && d.team != "Def")
                    {
                        d.elecFrames = 25;
                    }
                }
                foreach (Device d in Level.CheckRectAll<Device>(topLeft - new Vec2(3f, 3f), bottomRight + new Vec2(3f, 3f))) //breaking by electricity
                {
                    if (d.breakable == true && d.setted == true && d.destructingByElectricity && electrified)
                    {
                        d.Break();
                    }
                }
            }

            if (gm != null)
            {
                float maxValue = collisionSize.x * 8f;
                float actValue = 0;
                if (horizontal)
                {
                    if (health <= 0)
                    {
                        foreach (BreakableSurface b in Level.CheckRectAll<BreakableSurface>(topLeft, bottomRight))
                        {
                            if (b.breakableMode != "U")
                            {
                                Level.Remove(b);
                            }
                        }
                        Level.Remove(this);
                    }
                }
                if (!reinforced)
                {
                    foreach (Operators o in Level.CheckRectAll<Operators>(topLeft - new Vec2(6f, 2f), bottomRight + new Vec2(6f, 2f)))
                    {
                        if (o.team == "Def" && gm.reinforcementPool > 0 && o.local && !o.observing
                            && o.mode == "normal" && reinforcementCooldown <= 0 && Level.CheckRect<Operators>(topLeft, bottomRight) == null)
                        {
                            if (o.controller && o.genericController != null && o.duckOwner != null)
                            {
                                if (o.duckOwner.inputProfile.leftStick.y > 0.2f)
                                {
                                    if (reinforcement <= 0)
                                    {
                                        Level.Add(new SoundSource(position.x, position.y, 320, "SFX/reinforce.wav", "J") { showTime = 6 });
                                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, "SFX/reinforce.wav", "J"));
                                    }
                                    reinforcement += 0.01666666f;
                                    if (reinforcement >= 4f)
                                    {
                                        float direction = 0;
                                        if (vertical)
                                        {
                                            direction = Math.Sign(o.position.x - position.x);
                                        }
                                        Reinforce(direction);
                                        if (o.local)
                                        {
                                            PlayerStats.renown += 5;
                                            PlayerStats.Save();
                                            Level.Add(new RenownGained() { description = "Reinforcement", amount = 10 });
                                        }
                                        DuckNetwork.SendToEveryone(new NMReinforce(topLeft, bottomRight, direction));
                                    }
                                }
                                else
                                {
                                    reinforcement = 0;
                                    reinforcementCooldown = 30;
                                    foreach (SoundSource s in Level.CheckRectAll<SoundSource>(topLeft, bottomRight))
                                    {
                                        s.Cancel();
                                    }
                                }
                            }
                            else
                            {
                                if (Keyboard.Down(PlayerStats.keyBindings[4]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[4]))
                                {
                                    if (reinforcement <= 0)
                                    {
                                        Level.Add(new SoundSource(position.x, position.y, 320, "SFX/reinforce.wav", "J") { showTime = 6 });
                                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, "SFX/reinforce.wav", "J"));
                                    }
                                    reinforcement += 0.01666666f;
                                    o.unableToJump = 10;
                                    o.unableToMove = 10;
                                    if (reinforcement >= 4f)
                                    {
                                        float direction = 0;
                                        if (vertical)
                                        {
                                            direction = Math.Sign(o.position.x - position.x);
                                        }
                                        Reinforce(direction);
                                        if (o.local)
                                        {
                                            PlayerStats.renown += 5;
                                            PlayerStats.Save();
                                            Level.Add(new RenownGained() { description = "Reinforcement", amount = 10 });
                                        }
                                        DuckNetwork.SendToEveryone(new NMReinforce(topLeft, bottomRight, direction));
                                    }
                                }
                                else
                                {
                                    reinforcement = 0;
                                    reinforcementCooldown = 30;
                                    foreach (SoundSource s in Level.CheckRectAll<SoundSource>(topLeft, bottomRight))
                                    {
                                        s.Cancel();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (reinforcementCooldown > 0)
            {
                reinforcementCooldown--;
            }
        }
        public void Reinforce(float side = 0)
        {
            GamemodeScripter gm = null;
            reinforced = true;
            foreach (GamemodeScripter g in Level.current.things[typeof(GamemodeScripter)])
            {
                gm = g;
            }
            if (vertical)
            {
                if(side >= 0) //Forced from the right
                {
                    foreach(BreakableSurface b in Level.CheckRectAll<BreakableSurface>(topLeft, bottomRight))
                    {
                        if(b.breakableMode == "S")
                        {
                            Level.Remove(b);
                        }
                        if(b.breakableMode == "E" && b.position.x > position.x)
                        {
                            Level.Remove(b);
                        }

                        /*if (b.breakableMode == "E" && b.position.x < position.x)
                        {
                            for (int i = 0; i < (int)((collisionSize.y - 6) / 6) + 2; i++)
                            {
                                b.hitPoses.Add(position.y - (bottom - i * 6 - 3));
                                b.thicks.Add(0.5f);
                            }
                        }*/
                    }
                    Level.Add(new BreakableSurface(position.x + 4.5f, position.y, 1, collisionSize.y) { breakableMode = "H", horizontal = false, vertical = true, lightColored = true });
                    Level.Add(new BreakableSurface(position.x + 3.5f, position.y, 1, collisionSize.y) { breakableMode = "H", horizontal = false, vertical = true });

                    for (int i = 0; i < (int)((collisionSize.y - 6) / 6) + 2; i++)
                    {
                        Level.Add(new BreakableSurface(position.x - 1, bottom - i * 6 - 3, 8, 1) { breakableMode = "H", horizontal = false, vertical = true });
                    }
                    health = 10000000;
                }
                if(side < 0) //Forced from the left
                {
                    foreach (BreakableSurface b in Level.CheckRectAll<BreakableSurface>(topLeft, bottomRight))
                    {
                        if (b.breakableMode == "S")
                        {
                            Level.Remove(b);
                        }
                        if (b.breakableMode == "E" && b.position.x < position.x)
                        {
                            Level.Remove(b);
                        }

                        /*if (b.breakableMode == "E" && b.position.x > position.x)
                        {
                            for (int i = 0; i < (int)((collisionSize.y - 6) / 6) + 2; i++)
                            {
                                b.hitPoses.Add(position.y - (bottom - i * 6 - 3));
                                b.thicks.Add(0.5f);
                            }
                        }*/
                    }
                    Level.Add(new BreakableSurface(position.x - 4.5f, position.y, 1, collisionSize.y) { breakableMode = "H", horizontal = false, vertical = true, lightColored = true });
                    Level.Add(new BreakableSurface(position.x - 3.5f, position.y, 1, collisionSize.y) { breakableMode = "H", horizontal = false, vertical = true });

                    for (int i = 0; i < (int)((collisionSize.y - 6) / 6) + 2; i++)
                    {
                        Level.Add(new BreakableSurface(position.x + 1, bottom - i * 6 - 3, 8, 1) { breakableMode = "H", horizontal = false, vertical = true });
                    }
                }
            }
            if (horizontal)
            {
                foreach (BreakableSurface b in Level.CheckRectAll<BreakableSurface>(topLeft, bottomRight))
                {
                    Level.Remove(b);
                }

                Level.Add(new BreakableSurface(position.x, position.y - 4.5f, collisionSize.x, 3) { breakableMode = "H", horizontal = true, vertical = false, lightColored = true}); 
                Level.Add(new BreakableSurface(position.x, position.y - 2f, collisionSize.x, 2) { breakableMode = "H", horizontal = true, vertical = false });
                Level.Add(new BreakableSurface(position.x, position.y + 0.5f, collisionSize.x, 3) { breakableMode = "H", horizontal = true, vertical = false, lightColored = true });
                health = 10000000;
            }

            if (gm != null)
            {
                gm.reinforcementPool -= 1;
            }
        }

        public virtual void Breach(float damage)
        {
            if(reinforced && horizontal)
            {
                health -= damage;
            }
        }
        public virtual void OnDrawLayer(Layer layer)
        {
            base.Draw();
            if (layer == Layer.Foreground)
            {
                GamemodeScripter gm = null;
                foreach (GamemodeScripter g in Level.current.things[typeof(GamemodeScripter)])
                {
                    gm = g;
                }
                if (!reinforced)
                {
                    foreach (Operators o in Level.CheckRectAll<Operators>(topLeft - new Vec2(6f, 2f), bottomRight + new Vec2(6f, 2f)))
                    {
                        if (gm.reinforcementPool > 0 && !o.observing && o.team == "Def" && o.local && Level.CheckRect<Operators>(topLeft, bottomRight) == null)
                        {
                            Vec2 pos = Level.current.camera.position;
                            Vec2 cameraSize = Level.current.camera.size;
                            Vec2 Unit = cameraSize / new Vec2(320, 180);

                            string text = "Hold   to reinforce";
                            Graphics.DrawStringOutline(text, Level.current.camera.position + new Vec2(160 - 4 * text.Length, 40) * Unit, Color.White, Color.Black, 0.6f, null, 1f * Unit.x);

                            SpriteMap _widebutton = new SpriteMap(GetPath("Sprites/Keys.png"), 34, 17);
                            _widebutton.CenterOrigin();
                            _widebutton.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                            SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                            _button.CenterOrigin();
                            _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                            if (o.controller)
                            {
                                _button.frame = PlayerStats.GetFrameOfButtonGP("LStickUP");
                                Graphics.Draw(_button, pos.x + (160 - text.Length / 2 * 9 + 49) * Unit.x, pos.y + 44 * Unit.x, 1);
                            }
                            else
                            {
                                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[4]))
                                {
                                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[4]);
                                    Graphics.Draw(_widebutton, pos.x + (160 - text.Length / 2 * 9 + 49) * Unit.x, pos.y + 44 * Unit.x, 1);
                                }
                                else
                                {
                                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[4]);
                                    Graphics.Draw(_button, pos.x + (160 - text.Length / 2 * 9 + 49) * Unit.x, pos.y + 44 * Unit.x, 1);
                                }
                            }

                            Graphics.DrawRect(topLeft, bottomRight, Color.Yellow, 1f, false, 0.5f);

                            if (vertical)
                            {
                                Graphics.DrawLine(new Vec2(position.x, bottom), new Vec2(position.x, bottom - (height * (reinforcement / 4))), Color.Yellow, width, 0.9f);
                            }
                            if (horizontal)
                            {
                                Graphics.DrawLine(new Vec2(left, position.y), new Vec2(left + (width * (reinforcement / 4)), position.y), Color.Yellow, height, 0.9f);
                            }
                        }
                    }
                }
            }
        }
    }

    public class BreakableSurface : Block, IDrawToDifferentLayers, IPlatform
    {
        public float xSize = 16;
        public float ySize = 16;

        public SpriteMap _sprite;

        public float minX = 0.5f;
        public float minY = 0.5f;

        public List<float> hitPoses = new List<float>();
        public List<float> thicks = new List<float>();

        public Vec2 hPos;
        //public float _xSize;
        //public float _ySize;

        public bool horizontal;
        public bool vertical;

        /// <summary>
        /// Can be destroyed by: 
        /// E - every type of bullet or explosion and etc
        /// S - Shotgun or explosions (mostly barrels, not bulletproof)
        /// H - Hardbreach (bulletproof)
        /// U - Unbreakable
        /// </summary>
        public string breakableMode;
        public bool lightColored;

        public BreakableSurface(float xpos, float ypos, float x = 16, float y = 16) : base(xpos, ypos)
        {
            xSize = x;
            ySize = y;

            center = new Vec2(xSize / 2, ySize / 2);
            graphic = _sprite;

            collisionSize = new Vec2(xSize, ySize);
            thickness = 1f;
            collisionOffset = new Vec2(-xSize/2, -ySize/2);

            //physicsMaterial = PhysicsMaterial.Metal;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update()
        {
            base.Update();

            if (breakableMode == "H")
            {
                thickness = 6f;
            }
            if(breakableMode == "U")
            {
                thickness = 0;
            }

            if (removeFromLevel)
            {
                collisionSize = new Vec2(0.01f, 0.01f);
                active = false;
                solid = false;
                Level.Remove(this);
            }
            if (vertical)
            {
                if (Level.CheckLine<Block>(new Vec2(position.x, top - 5), new Vec2(position.x, bottom + 5), this) == null || 
                    collisionSize.y < minY || collisionSize.x < minX || height < minY)
                {
                    collisionSize = new Vec2(0.01f, 0.01f);
                    Level.Add(WoodDebris.New(position.x, position.y));
                    active = false;
                    solid = false;
                    Level.Remove(this);
                }
            }
            else if (horizontal)
            {
                if (Level.CheckLine<Block>(new Vec2(left - 4, position.y), new Vec2(right + 4, position.y), this) == null || 
                    collisionSize.y < minY || collisionSize.x < minX || width < minX)
                {
                    collisionSize = new Vec2(0.01f, 0.01f);
                    Level.Add(WoodDebris.New(position.x, position.y));
                    active = false;
                    solid = false;
                    Level.Remove(this);
                }
            }
            else
            {
                active = false;
                solid = false;
                Level.Remove(this);
            }
            UpdateBreaking();
        }

        public void UpdateBreaking()
        {
            if (hitPoses.Count > 0)
            {
                SortHitPoses();

                //Merging two nearby positions into one, if they collide
                if (hitPoses.Count > 1)
                {
                    for (int i = 1; i < hitPoses.Count; i++)
                    {
                        if (hitPoses[i] - hitPoses[i - 1] <= (thicks[i] + thicks[i - 1]) / 2)
                        {
                            float dif = hitPoses[i] - hitPoses[i - 1];

                            hitPoses[i] = (hitPoses[i] + hitPoses[i - 1]) / 2;
                            thicks[i] = thicks[i - 1] + thicks[i] - dif;

                            thicks[i - 1] = 0;
                        }
                    }
                }

                //Removing null-effect collisions
                while (thicks[0] <= 0)
                {
                    thicks.Remove(0);
                    hitPoses.Remove(0);
                }

                SortHitPoses();

                //Do breaking
                if (hitPoses.Count > 0 && thicks.Count > 0 && thicks[0] > 0)
                {
                    if (vertical)
                    {
                        RecreateFromVertical(hitPoses[0], thicks[0]);
                    }
                    if (horizontal)
                    {
                        RecreateFromHorizontal(hitPoses[0], thicks[0]);
                    }
                }
            }
        }

        public void SortHitPoses()
        {
            for (int i = 0; i < hitPoses.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (hitPoses[j] < hitPoses[i])
                    {
                        float k = hitPoses[j];
                        hitPoses[j] = hitPoses[i];
                        hitPoses[i] = k;

                        k = thicks[j];
                        thicks[j] = thicks[i];
                        thicks[i] = k;
                    }
                }
            }
        }

        public void RecreateFromVertical(float pos, float size)
        {
            //DevConsole.Log(Convert.ToString(pos), Color.White);

            hitPoses.RemoveAt(0);
            thicks.RemoveAt(0);

            //DevConsole.Log("cleared [0]", Color.White);

            List<float> topPoses = new List<float>();
            List<float> bottomPoses = new List<float>();

            List<float> topThicks = new List<float>();
            List<float> bottomThicks = new List<float>();


            if (hitPoses.Count > 0 && thicks.Count > 0)
            {
                foreach (float f in hitPoses)
                {
                    if (f < pos)
                    {
                        topPoses.Add(f);
                        topThicks.Add(thicks[hitPoses.IndexOf(f)]);
                    }
                    else
                    {
                        bottomPoses.Add(f);
                        bottomThicks.Add(thicks[hitPoses.IndexOf(f)]);
                    }
                }
            }

            //DevConsole.Log("Bottom and top parts prepared", Color.White);

            BreakableSurface topPart = new BreakableSurface(position.x, topLeft.y + ySize - (ySize * 0.5f - pos) * 0.5f + size * 0.5f, xSize, ySize * 0.5f - pos - size)
            { hitPoses = topPoses, thicks = topThicks, breakableMode = breakableMode, vertical = true, minX = minX, minY = minY, lightColored = lightColored };
            Level.Add(topPart);

            BreakableSurface bottomPart = new BreakableSurface(position.x, topLeft.y + (ySize * 0.5f + pos) * 0.5f - size * 0.5f, xSize, ySize * 0.5f + pos - size)
            { hitPoses = bottomPoses, thicks = bottomThicks, breakableMode = breakableMode, vertical = true, minX = minX, minY = minY, lightColored = lightColored };
            Level.Add(bottomPart);

            //DevConsole.Log("Bottom and top parts created", Color.White);

            foreach(Rocky r in Level.CheckRectAll<Rocky>(topLeft + new Vec2(-1, -1), bottomRight + new Vec2(1, 1)))
            {
                if(r.stickedTo == this)
                {
                    if(Level.CheckRect<BreakableSurface>(topLeft, bottomRight, this) != null)
                    {
                        r.stickedTo = Level.CheckRect<BreakableSurface>(topLeft, bottomRight, this);
                    }
                    else
                    {
                        if (r.sensitiveToSurface)
                        {
                            Level.Remove(r);
                        }
                    }
                }
            }

            Level.Add(WoodDebris.New(position.x, position.y));
            Level.Remove(this);
        }

        public void RecreateFromHorizontal(float pos, float size)
        {
            //DevConsole.Log(Convert.ToString(pos), Color.White);

            hitPoses.RemoveAt(0);
            thicks.RemoveAt(0);

            List<float> topPoses = new List<float>(); //Right
            List<float> bottomPoses = new List<float>(); //Left

            List<float> topThicks = new List<float>();
            List<float> bottomThicks = new List<float>();

            if (hitPoses.Count > 0 && thicks.Count > 0)
            {
                foreach (float f in hitPoses)
                {
                    if (f < pos)
                    {
                        topPoses.Add(f);
                        topThicks.Add(thicks[hitPoses.IndexOf(f)]);
                    }
                    else
                    {
                        bottomPoses.Add(f);
                        bottomThicks.Add(thicks[hitPoses.IndexOf(f)]);
                    }
                }
            }

            BreakableSurface topPart = (new BreakableSurface(topLeft.x + xSize - (xSize * 0.5f - pos) * 0.5f + size * 0.5f,       position.y, xSize * 0.5f - pos - size,     ySize) 
            { hitPoses = topPoses, thicks = topThicks, breakableMode = breakableMode, horizontal = true, minX = minX, minY = minY, lightColored = lightColored });
            Level.Add(topPart);

            BreakableSurface bottomPart = new BreakableSurface(topLeft.x + (xSize * 0.5f + pos) * 0.5f - size * 0.5f,               position.y, xSize * 0.5f + pos - size,     ySize) 
            { hitPoses = bottomPoses, thicks = bottomThicks, breakableMode = breakableMode, horizontal = true, minX = minX, minY = minY, lightColored = lightColored };
            Level.Add(bottomPart);

            foreach (Rocky r in Level.CheckRectAll<Rocky>(topLeft + new Vec2(-1, -1), bottomRight + new Vec2(1, 1)))
            {
                if (r.stickedTo == this)
                {
                    if (Level.CheckRect<BreakableSurface>(topLeft, bottomRight, this) != null)
                    {
                        r.stickedTo = Level.CheckRect<BreakableSurface>(topLeft, bottomRight, this);
                    }
                    else
                    {
                        if (r.sensitiveToSurface)
                        {
                            Level.Remove(r);
                        }
                    }
                }
            }

            Level.Add(WoodDebris.New(position.x, position.y));
            Level.Remove(this);
        }

        public virtual void Explosion(Vec2 center, float radius, string type)
        {           
            if ((breakableMode == "H" && type == "H") || (type == "S" && breakableMode == "S") || (type != "N" && breakableMode == "E"))
            {
                Vec2 hit = new Vec2(center - position);
                if (horizontal)
                {
                    float thick = (float)Math.Sqrt(radius * radius - (center.y - position.y) * (center.y - position.y));
                    if (thick >= 1f)
                    {
                        hitPoses.Add(hit.x);
                        thicks.Add(thick);
                    }
                }
                if (vertical)
                {
                    float thick = (float)Math.Sqrt(radius * radius - (center.x - position.x) * (center.x - position.x));
                    if (thick >= 1f)
                    {
                        hitPoses.Add(hit.y);
                        thicks.Add(thick);
                    }
                }
            }
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            hPos = hitPos;
            Vec2 hit = new Vec2(hitPos-position);

            if (bullet != null)
            {
                float penetration = bullet.ammo.penetration + bullet.ammo.bulletThickness;
                //float penetration = bullet.ammo.bulletThickness;
                //RecreateFromVertical(hit.y, penetration);

                //RecreateFromHorizontal(hit.x, penetration);

                if(bullet.ammo is ATHighCalSniper)
                {
                    penetration *= 3;
                }
                if(bullet.ammo is ATShotgun)
                {
                    penetration *= 0.7f;
                }

                if (breakableMode == "E" || ((bullet.ammo is ATHighCalSniper || bullet.ammo is ATShotgun) && breakableMode == "S"))
                {
                    if (horizontal)
                    {
                        hitPoses.Add(hit.x);
                        thicks.Add(penetration);
                    }
                    if (vertical)
                    {
                        hitPoses.Add(hit.y);
                        thicks.Add(penetration);
                    }
                }
                if(breakableMode == "E")
                {
                    bullet.ammo.penetration -= 0.1f;
                }
                if (breakableMode == "S")
                {
                    bullet.ammo.penetration -= 0.06f;
                }
                if (breakableMode == "U")
                {
                    bullet.ammo.penetration -= 0.2f;
                }
            }
            return base.Hit(bullet, hitPos);
        }

        public virtual void OnDrawLayer(Layer layer)
        {
            base.Draw();
            if (layer == Layer.Game)
            {
                Color c = Color.Orange;
                float drawPrior = 0;
                if (breakableMode == "E")
                {
                    c = Color.IndianRed;
                }
                if (breakableMode == "S")
                {
                    c = Color.BurlyWood;
                    if (lightColored)
                    {
                        c = Color.Bisque;
                    }
                    drawPrior = 0.02f;
                }
                if (breakableMode == "H")
                {
                    c = Color.Gray;
                    if (lightColored)
                    {
                        c = Color.LightSteelBlue;
                    }
                    drawPrior = 0.05f;
                }
                if (breakableMode == "U")
                {
                    c = Color.Ivory;
                    drawPrior = 0.01f;
                }
                Graphics.DrawRect(hPos, hPos, Color.Red, 1f, true, 1);
                Graphics.DrawRect(topLeft, bottomRight, c, 0.5f + drawPrior, true, 1);
                if (breakableMode != "U")
                {
                    Vec2 off = new Vec2(1, 1);
                    if (horizontal)
                    {
                        off = new Vec2(1, 0);
                    }
                    Graphics.DrawRect(topLeft - off, bottomRight + off, (c.ToVector3() - new Vec3(15, 15, 15)).ToColor(), 0.4f + drawPrior, true, 1f);
                }
            }
        }
    }
}
