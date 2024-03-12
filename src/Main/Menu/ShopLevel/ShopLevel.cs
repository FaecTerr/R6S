using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DuckGame.R6S
{
    public class ShopLevel : Level
    {
        public List<int> unlockedOpearotrs = new List<int>();

        public int xpos;
        public int ypos;

        public Color c = Color.White;
        
        public Profile p;

        private int _quality;
        public int quality 
        {
            get
            {
                return _quality;
            }
            set
            {
                if(value > 4)
                {
                    value = 4;
                }
                if(value < 0)
                {
                    value = 0;
                }
                _quality = value;
            }
        }

        public SinWave _pulse = 0.1f;
        public SinWave _pulse2 = 0.06f;

        public int screen; //0 - main, 1 - packs, 2 - Operators unlock, 3 - Operator customize, 4 - Weapon customize

        public float moving;
        public SpriteMap _background;
        public SpriteMap _exit;

        public float pos;

        public double h;
        public double s = 1;
        public double v = 1;

        public bool unlockingItem;
        public int unlockTimer;

        public string lastSkin = " ";

        public float animation;

        public int totalOperators;

        public float epic;

        // 320 x 180

        public override void Initialize()
        {
            //Music.Load(Mod.GetPath<R6S>("Music/OptionsMusic.ogg"), true);
            //Music.Play("OptionsMusic");

            Add(new BackButton(20, 166 + 320 * 2));

            DevConsole.Log("Started adding icons");
            OperatorsSelect();
            DevConsole.Log("Finished adding icons");
            for (int i = 0; i < 3; i++)
            {
                Add(new CategoryButton(40, 50 + 40 * i) { screen = i + 1 });
            }

            Add(new QualityButton(30, 400));
            Add(new QualityButton(70, 400) { add = true });

            Add(new OpenPackButton(50, 430));
            
            Add(new PromoCodeInput (30, 168 + 320 * 1));
            Add(new SettingsButton (26, 160));
            
            base.Initialize();
        }

        public ShopLevel() : base()
        {
            Layer.Game.fade = 0f;
            Layer.Foreground.fade = 0f;

            _background = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/Background.png"), 320, 240);
            _background.depth = 0.5f;

            _exit = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/ExitButton.png"), 20, 20);
        }

        public void UpdateSlidersMoving()
        {
            if (unlockingItem)
            {
                if (screen == 1)
                {
                    if (moving > -120)
                    {
                        moving -= 1f;
                    }
                    if (unlockTimer > 1)
                    {
                        unlockTimer--;
                    }
                }
                if (screen == 2)
                {
                    if (unlockTimer > 0)
                    {
                        unlockTimer--;
                    }
                }
                if(screen == 3)
                {
                    unlockingItem = false;
                }
                if (Mouse.left == InputState.Pressed)
                {
                    unlockTimer--;
                }
                if (unlockTimer <= 0)
                {
                    unlockingItem = false;
                }
            }
            if (!unlockingItem)
            {
                if (screen == 1)
                {
                    if (moving != 0)
                    {
                        moving *= 0.96f;
                    }
                }
                if (screen == 2)
                {
                    moving += -Mouse.scroll * 0.08f;
                    if (moving > 4)
                    {
                        moving *= 0.96f;
                    }
                    if (totalOperators <= 20)
                    {
                        if (moving < 0)
                        {
                            moving *= 0.96f;
                        }
                    }
                    else
                    {
                        if (moving < -(totalOperators / 4 - 4) * 32)
                        {
                            moving *= 0.96f;
                        }
                    }
                }
                if (screen == 3)
                {
                    moving += -Mouse.scroll * 0.08f;
                    if (Keyboard.Down(PlayerStats.keyBindings[19]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[19]))
                    {
                        moving += -Mouse.scroll * 0.24f;
                    }
                    else
                    {
                        moving += -Mouse.scroll * 0.08f;
                    }

                    if (moving < -PlayerStats.totalOperators * 180f)
                    {
                        moving *= 0.999f;
                    }
                    if (moving > 40)
                    {
                        moving *= 0.96f;
                    }
                }
            }
            if (screen == 0)
            {
                ReturnToMainMenu();
                moving = 0;
            }
            camera.position = new Vec2(0, 320 * screen);
        }

        public override void Update()
        {
            Music.volumeMult = 9;
            base.Update();
            UpdateSlidersMoving();

            Profile p = Profiles.DefaultPlayer1;

            if (!unlockingItem)
            {
                epic -= 0.02f;
            }

            if(Keyboard.Pressed(Keys.Escape))
            {
                if (screen == 0)
                {
                    current = new TitleScreen();
                }
                else
                {
                    if (!unlockingItem)
                    {
                        ReturnToMainMenu();
                    }
                    //ReturnToMainMenu();
                }
            }

            if(screen != 2)
            {
                foreach(Confirm c in current.things[typeof(Confirm)])
                {
                    c.op = null;
                }
            }
        }

        public virtual void ReturnToMainMenu()
        {
            if (R6S.upd.mainMenu == null)
            {
                R6S.upd.mainMenu = new MainMenu();
                current = R6S.upd.mainMenu;
            }
            else
            {
                current = R6S.upd.mainMenu;
                (R6S.upd.mainMenu as MainMenu).Load();
            }
        }

        public virtual void OpenPack()
        {
            string name = "";
            int mat = 25;
            if (quality == 0)
            {
                name = SkinsCollectionManager.GetRandomSkin();
            }
            if(quality == 1)
            {
                name = SkinsCollectionManager.GetRandomLockedCommonSkin();
                mat = 100;
            }
            if (quality == 2)
            {
                name = SkinsCollectionManager.GetRandomLockedRareSkin();
                mat = 175;
            }
            if (quality == 3)
            {
                name = SkinsCollectionManager.GetRandomLockedEpicSkin();
                mat = 250;
            }
            if (quality == 4)
            {
                name = SkinsCollectionManager.GetRandomLockedLegendarySkin();
                mat = 350;
            }

            if (!PlayerStats.openedCustoms.Contains(name))
            {
                PlayerStats.openedCustoms.Add(name);
                lastSkin = name;
                PlayerStats.Save();
            }
            else
            {
                PlayerStats.materials += mat;
                lastSkin = "+ " + Convert.ToString(mat) +  " materials \n(duplicate)";
                unlockTimer = 60;
            }
        }

        public virtual void OperatorsSelect()
        {
            Confirm c = new Confirm(84, 166 + 320 * 2);
            Add(c);

            int rCon = Rando.Int(20);


            Vec2 camPos = current.camera.position;
            Vec2 Unit = current.camera.size / new Vec2(320, 180);

            int k = 0;
            for (int i = 0; i < PlayerStats.totalDefenders; i++)
            {
                OPEQ op = PlayerStats.GetDefenderByCallID(i, new Vec2(0, 0));
                int m = k / 2;
                int l = k % 2;
                if (op != null && op.oper != null)
                {
                    BuyOperator b = new BuyOperator(camPos.x + 18 * Unit.x + 26 * m * Unit.y, camPos.y + 40 * Unit.y + 26 * l * Unit.y + 320 * 2) { operatorID = op.oper.operatorID };
                    Add(b);
                    totalOperators += 1;
                    k++;
                    if (op.oper.operatorID == rCon)
                    {
                        c.op = b;
                        b.selected = true;
                        c.selected = true;
                    }
                }
            }
            k = 0;
            for (int i = 0; i < PlayerStats.totalAttackers; i++)
            {
                OPEQ op = PlayerStats.GetAttackerByCallID(i, new Vec2(0, 0));
                int m = k / 2;
                int l = k % 2 + 2;
                if (op != null && op.oper != null)
                {
                    BuyOperator b = new BuyOperator(camPos.x + 18 * Unit.x + 26 * m * Unit.y, camPos.y + 40 * Unit.y + 26 * l * Unit.y + 320 * 2) { operatorID = op.oper.operatorID };
                    Add(b);
                    totalOperators += 1;
                    k++;
                    if (op.oper.operatorID == rCon)
                    {
                        c.op = b;
                        b.selected = true;
                        c.selected = true;
                    }
                }
            }
        }

        public void DrawBuyOper()
        {
            Vec2 camSize = new Vec2(current.camera.width, current.camera.width);
            Vec2 realCamSize = new Vec2(current.camera.width, current.camera.height);
            Vec2 camPos = new Vec2(current.camera.position.x, current.camera.position.y);

            SpriteMap _arrow = new SpriteMap(Mod.GetPath<R6S>("Sprites/AlphaPack/arrows.png"), 13, 9);
            _arrow.CenterOrigin();
            _arrow.depth = 0.6f;


            SpriteMap _keys = new SpriteMap(Mod.GetPath<R6S>("Sprites/keys.png"), 17, 17);
            _keys.CenterOrigin();
            _keys.depth = 0.6f;

            if (moving < 3)
            {
                _keys.frame = 139;
                _arrow.frame = 0;
                Graphics.Draw(_arrow, 20, 320 * 2 + 140);
                Graphics.Draw(_keys, 20 + 17, 320 * 2 + 140);

                if (Mouse.positionScreen.x > 20 - _arrow.width * 0.5f && Mouse.positionScreen.x < 20 + 17 + _keys.width * 0.5f &&
                    Mouse.positionScreen.y > 320 * 2 + 140 - _keys.height * 0.5f && Mouse.positionScreen.y < 320 * 2 + 140 + _keys.height * 0.5f && Mouse.left == InputState.Down)
                {
                    moving += 1.2f;
                }
            }
            if (moving > -((totalOperators / 4 - 5) + 0.5f) * 26)
            {
                _keys.frame = 140;
                _arrow.frame = 1;
                Graphics.Draw(_arrow, 134, 320 * 2 + 140);
                Graphics.Draw(_keys, 134 - 17, 320 * 2 + 140);


                if (Mouse.positionScreen.x > 134 - 17 - _keys.width * 0.5f && Mouse.positionScreen.x < 134 + _arrow.width * 0.5f &&
                    Mouse.positionScreen.y > 320 * 2 + 140 - _keys.height * 0.5f && Mouse.positionScreen.y < 320 * 2 + 140 + _keys.height * 0.5f && Mouse.left == InputState.Down)
                {
                    moving -= 1.2f;
                }
            }

            if (unlockingItem && PlayerStats.openedOperators.Count > 0)
            {
                Mouse.position = new Vec2(320, 180);

                unlockTimer--;
                moving = 600;

                float alp = 1;
                if (unlockTimer > 500)
                {
                    alp = 0.01f * (600 - unlockTimer);
                }
                if (unlockTimer < 60)
                {
                    alp = 0.1f * unlockTimer / 6;
                }
                Graphics.DrawRect(current.camera.position - new Vec2(2, 2), current.camera.position + current.camera.size + new Vec2(2, 2), Color.White * alp, 1.1f, true);

                SpriteMap _s = new SpriteMap(Mod.GetPath<R6S>("Sprites/OperatorIcons.png"), 24, 24);

                OPEQ opeq = PlayerStats.GetOpeqByID(PlayerStats.openedOperators[PlayerStats.openedOperators.Count - 1], new Vec2());

                _s.frame = opeq.oper.operatorID;
                _s.CenterOrigin();

                if (unlockTimer < 330 && unlockTimer > 120)
                {
                    float alph = 1;
                    if (unlockTimer > 280)
                    {
                        alph = 0.02f * (330 - unlockTimer);
                    }
                    if (unlockTimer < 170)
                    {
                        alph = 0.02f * (unlockTimer - 120);
                    }

                    Graphics.DrawString("Unlocked", new Vec2(camera.position.x + 190, camera.position.y + 100), Color.Black * alph, 1.2f);
                }

                if (unlockTimer < 500)
                {
                    if (unlockTimer > 400)
                    {
                        _s.scale += new Vec2((500 - unlockTimer) * 0.008f, (500 - unlockTimer) * 0.008f);
                    }
                    else
                    {
                        _s.scale += new Vec2(100 * 0.008f, 100 * 0.008f);
                    }

                    float alph = 1;
                    if (unlockTimer > 475)
                    {
                        alph = 0.04f * (500 - unlockTimer);
                    }
                    if (unlockTimer < 60)
                    {
                        alph = 0.1f * unlockTimer / 6;
                    }

                    _s.alpha = alph;
                    Graphics.Draw(_s, camera.position.x + 150, camera.position.y + 80, 1.2f);
                    Graphics.DrawString(opeq.name, new Vec2(camera.position.x + 190, camera.position.y + 80), Color.Black * alph, 1.2f);
                }
            }
        }

        public void DrawOperUnlock()
        {
            Color c = Color.White;

            Sprite spr = new Sprite(Mod.GetPath<R6S>("Sprites/AlphaPack/pack.png"));
            spr.CenterOrigin();
            spr.scale = new Vec2(1.2f + _pulse * 0.1f, 1.2f + _pulse * 0.1f);
            spr.angle = _pulse2 * 0.2f;

            Graphics.Draw(spr, 240 + moving, 400, 0.4f);

            int cost = 1000;

            if (quality == 1)
            {
                cost = 100;
            }
            if (quality == 2)
            {
                cost = 175;
            }
            if (quality == 3)
            {
                cost = 250;
            }
            if (quality == 4)
            {
                cost = 350;
            }
            if (quality == 5)
            {
                cost = 450;
            }

            if (quality == 0)
            {
                Graphics.DrawStringOutline("Open alpha pack \n(" + Convert.ToString(cost) + " renown)", new Vec2(20 + moving * 3, 460), Color.Yellow, Color.Black, 0.5f, null, 0.8f);
            }
            else
            {
                Graphics.DrawStringOutline("Open alpha pack \n(" + Convert.ToString(cost) + " materials)", new Vec2(20 + moving * 3, 460), Color.Aqua, Color.Black, 0.5f, null, 0.8f);
            }

            string text = "Collection: ";

            Color col = Color.White;

            if (quality == 0)
            {
                HSV hsve = new HSV();

                int red = 0;
                int gre = 0;
                int blu = 0;

                hsve.HsvToRgb(h, s, v, out red, out gre, out blu);

                text += Convert.ToString(PlayerStats.openedCustoms.Count);
                text += "/";
                text += Convert.ToString(SkinsCollectionManager.allCollection.Count);

                col = new Vec3(red, gre, blu).ToColor();
            }

            if (quality == 1)
            {
                int k = 0;
                foreach (SkinElement s in SkinsCollectionManager.commonCollection)
                {
                    if (PlayerStats.openedCustoms.Contains(s.name))
                    {
                        k++;
                    }
                }
                text += Convert.ToString(k);
                text += "/";
                text += Convert.ToString(SkinsCollectionManager.commonCollection.Count);

                col = Color.Wheat;
            }

            if (quality == 2)
            {
                int k = 0;
                foreach (SkinElement s in SkinsCollectionManager.rareCollection)
                {
                    if (PlayerStats.openedCustoms.Contains(s.name))
                    {
                        k++;
                    }
                }
                text += Convert.ToString(k);
                text += "/";
                text += Convert.ToString(SkinsCollectionManager.rareCollection.Count);

                col = Color.LightBlue;
            }

            if (quality == 3)
            {
                int k = 0;
                foreach (SkinElement s in SkinsCollectionManager.epicCollection)
                {
                    if (PlayerStats.openedCustoms.Contains(s.name))
                    {
                        k++;
                    }
                }
                text += Convert.ToString(k);
                text += "/";
                text += Convert.ToString(SkinsCollectionManager.epicCollection.Count);

                col = Color.Purple;
            }

            if (quality == 4)
            {
                int k = 0;
                foreach (SkinElement s in SkinsCollectionManager.legendaryCollection)
                {
                    if (PlayerStats.openedCustoms.Contains(s.name))
                    {
                        k++;
                    }
                }
                text += Convert.ToString(k);
                text += "/";
                text += Convert.ToString(SkinsCollectionManager.legendaryCollection.Count);

                col = Color.Yellow;
            }

            Graphics.DrawStringOutline(text, new Vec2(190 - moving * 3, 352), col, Color.Black, 0.5f, null, 0.8f);

            SpriteMap q = new SpriteMap(Mod.GetPath<R6S>("Sprites/AlphaPack/skinQuality.png"), 9, 9);
            q.CenterOrigin();
            q.frame = quality;
            Graphics.Draw(q, 50 + moving, 400);

            if (unlockTimer == 1)
            {
                Graphics.DrawStringOutline("Press LMB to continue", new Vec2(90, 480), Color.Purple, Color.Black, 0.5f, null, 0.8f);
            }

            if (quality == 1)
            {
                c = Color.Wheat;
            }
            if (quality == 2)
            {
                c = Color.LightBlue;
            }
            if (quality == 3)
            {
                c = Color.Purple;
            }
            if (quality == 4)
            {
                c = Color.Yellow;
            }


            if (unlockingItem)
            {
                if (moving <= -100 && unlockTimer < 200)
                {
                    string name = lastSkin;
                    string oper = "";
                    string appearence = name;

                    foreach (SkinElement skin in SkinsCollectionManager.allCollection)
                    {
                        if (skin.name == lastSkin)
                        {
                            if (skin.AppearenceName != "")
                            {
                                appearence = skin.AppearenceName;
                            }
                            if (skin.type == 0)
                            {
                                int size = 32;
                                if (skin.mainThing == "DP27" || skin.mainThing == "G8A1" || skin.mainThing == "RP41" || skin.mainThing == "OTs11" || skin.mainThing == "C7E")
                                {
                                    size = 48;
                                }
                                SpriteMap sprit = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/skins/" + skin.location + ".png"), size, 32);
                                sprit.CenterOrigin();

                                sprit.scale = new Vec2(2, 2);

                                if (skin.animated)
                                {
                                    animation += skin.animationSpeed;
                                }
                                if (animation >= skin.frames)
                                {
                                    animation = 0;
                                }
                                sprit.frame = (int)animation;

                                Graphics.Draw(sprit, 200, 410);

                                oper = skin.mainThing;

                                if (skin.rarity == 0)
                                {
                                    c = Color.Wheat;
                                }
                                if (skin.rarity == 1)
                                {
                                    c = Color.LightBlue;
                                }
                                if (skin.rarity == 2)
                                {
                                    c = Color.Purple;
                                }
                                if (skin.rarity == 3)
                                {
                                    c = Color.Yellow;
                                }
                                if(skin.rarity == 4)
                                {
                                    c = Color.DarkRed;

                                    epic += 0.02f;
                                }

                                if (quality == 0)
                                {
                                    SpriteMap sprit1 = new SpriteMap(Mod.GetPath<R6S>("Sprites/AlphaPack/skinQuality"), 9, 9);
                                    sprit1.frame = skin.rarity + 1;
                                    sprit1.CenterOrigin();

                                    sprit1.scale = new Vec2(2, 2);
                                    Graphics.Draw(sprit1, 256, 410);
                                }
                            }
                            if (skin.type == 1)
                            {
                                SpriteMap sprit = new SpriteMap(Mod.GetPath<R6S>("Sprites/Operators/skins/" + skin.location + ".png"), 32, 32);
                                sprit.CenterOrigin();

                                sprit.scale = new Vec2(2, 2);

                                if (skin.animated)
                                {
                                    animation += skin.animationSpeed;
                                }
                                if (animation >= skin.frames)
                                {
                                    animation = 0;
                                }
                                sprit.frame = (int)animation;

                                Graphics.Draw(sprit, 200, 410);

                                oper = skin.mainThing;

                                if (skin.rarity == 0)
                                {
                                    c = Color.Wheat;
                                }
                                if (skin.rarity == 1)
                                {
                                    c = Color.LightBlue;
                                }
                                if (skin.rarity == 2)
                                {
                                    c = Color.Purple;
                                }
                                if (skin.rarity == 3)
                                {
                                    c = Color.Yellow;
                                }
                                if (skin.rarity == 4)
                                {
                                    c = Color.DarkRed;

                                    epic += 0.02f;
                                }

                                if (quality == 0)
                                {
                                    SpriteMap sprit1 = new SpriteMap(Mod.GetPath<R6S>("Sprites/AlphaPack/skinQuality"), 9, 9);
                                    sprit1.frame = skin.rarity + 1;
                                    sprit1.CenterOrigin();

                                    sprit1.scale = new Vec2(2, 2);
                                    Graphics.Draw(sprit1, 256, 410);
                                }
                            }
                            if (skin.type == 2)
                            {
                                SpriteMap sprit = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/" + skin.location + ".png"), 56, 134);
                                sprit.CenterOrigin();
                                sprit.frame = 1;
                                sprit.scale = new Vec2(2, 2);

                                /*if (skin.animated)
                                {
                                    animation += skin.animationSpeed;
                                }
                                if (animation >= skin.frames)
                                {
                                    animation = 0;
                                }
                                sprit.frame = (int)animation;*/

                                Graphics.Draw(sprit, 200 + 24, 410 + 54);

                                oper = skin.mainThing;

                                if (skin.rarity == 0)
                                {
                                    c = Color.Wheat;
                                }
                                if (skin.rarity == 1)
                                {
                                    c = Color.LightBlue;
                                }
                                if (skin.rarity == 2)
                                {
                                    c = Color.Purple;
                                }
                                if (skin.rarity == 3)
                                {
                                    c = Color.Yellow;
                                }
                                if (skin.rarity == 4)
                                {
                                    c = Color.DarkRed;

                                    epic += 0.02f;
                                }

                                if (quality == 0)
                                {
                                    SpriteMap sprit1 = new SpriteMap(Mod.GetPath<R6S>("Sprites/AlphaPack/skinQuality"), 9, 9);
                                    sprit1.frame = skin.rarity + 1;
                                    sprit1.CenterOrigin();

                                    sprit1.scale = new Vec2(2, 2);
                                    Graphics.Draw(sprit1, 300, 500);
                                }
                            }
                        }
                    }

                    if (name[0] == '+')
                    {
                        SpriteMap _ren = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/Renown.png"), 32, 32);
                        _ren.CenterOrigin();
                        _ren.scale = new Vec2(1f, 1f);
                        _ren.frame = 1;
                        Graphics.Draw(_ren, 200, 410);
                        if (unlockTimer > 200)
                        {
                            unlockTimer = 200;
                        }
                    }
                    if (Graphics._biosFont != null && appearence != null)
                    {
                        Graphics.DrawStringOutline(appearence, new Vec2(160, 440), c, Color.Black, 0.4f, null, 1f);
                        Graphics.DrawStringOutline(oper, new Vec2(160, 460), Color.White, Color.Black, 0.4f, null, 1f);
                    }
                }
                else
                {
                    string name = lastSkin;

                    foreach (SkinElement skin in SkinsCollectionManager.allCollection)
                    {
                        if (skin.name == lastSkin)
                        {
                            int qual = skin.rarity;

                            if (unlockTimer > 360 && moving <= -100)
                            {
                                if (qual == 0)
                                {
                                    unlockTimer = 230;
                                }
                                if (qual == 1)
                                {
                                    unlockTimer = 275;
                                }
                                if (qual == 2)
                                {
                                    unlockTimer = 320;
                                }
                            }


                            SpriteMap sprit1 = new SpriteMap(Mod.GetPath<R6S>("Sprites/AlphaPack/skinQuality"), 9, 9);
                            sprit1.frame = skin.rarity + 1;
                            sprit1.CenterOrigin();

                            sprit1.scale = new Vec2(4, 4);
                            if (moving <= -10)
                            {
                                Graphics.Draw(sprit1, 220, 410);
                            }
                        }
                    }
                }
            }
            h++;
            if (h > 360)
            {
                h = 0;
            }

            for (int i = 0; i < 16; i++)
            {
                HSV hsv = new HSV();

                int r = 0;
                int g = 0;
                int b = 0;

                hsv.HsvToRgb(h, s, v, out r, out g, out b);


                if (quality == 0)
                {
                    c = new Vec3(r, g, b).ToColor();

                }


                float step = 20;
                float ind = 10;
                Graphics.DrawLine(new Vec2(ind + step * i, 315), new Vec2(ind + step * i, 315f + (ind + Math.Abs(7 - i)) * (_pulse + 1)), c, step / 2, 0.2f);
                Graphics.DrawLine(new Vec2(ind + step * i, 505f - (5 + Math.Abs(7 - i)) * (_pulse + 1)), new Vec2(ind + step * i, 505), c, step / 2, 0.2f);
            }
        }

        public void DrawMainMenu()
        {
            int newsBlock = 2;
            for (int i = 0; i < newsBlock; i++)
            {
                SpriteMap _border = new SpriteMap(Mod.GetPath<R6S>("Sprites/ModMenu/NewsBlock.png"), 128, 72);
                _border.frame = 0;
                _border.CenterOrigin();
                _border.depth = -0.4f;
                Graphics.Draw(_border, 0, 230, 50 + 80 * i);
                _border.frame = i + 1;
                _border.depth = -0.5f;
                Graphics.Draw(_border, i + 1, 230, 50 + 80 * i);
            }
        }

        public override void PostDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                Vec2 camSize = new Vec2(current.camera.width, current.camera.width);
                Vec2 realCamSize = new Vec2(current.camera.width, current.camera.height);
                Vec2 camPos = new Vec2(current.camera.position.x, current.camera.position.y);
                
                SpriteMap _renown = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/Renown.png"), 32, 32);
                _renown.CenterOrigin();
                _renown.scale = new Vec2(0.25f, 0.25f);

                if (screen < 3)
                {
                    Graphics.Draw(_renown, current.camera.position.x + 254, current.camera.position.y + 12, 1);
                    _renown.frame = 1;
                    Graphics.Draw(_renown, current.camera.position.x + 254, current.camera.position.y + 19, 1);

                    Graphics.DrawStringOutline(Convert.ToString(PlayerStats.renown), new Vec2(current.camera.position.x + 264, current.camera.position.y + 10), Color.Yellow, Color.Black, 1f, null, 0.5f);
                    Graphics.DrawStringOutline(Convert.ToString(PlayerStats.materials), new Vec2(current.camera.position.x + 264, current.camera.position.y + 18), Color.Yellow, Color.Black, 1f, null, 0.5f);
                }

                SpriteMap _key = new SpriteMap(Mod.GetPath<R6S>("Sprites/keys.png"), 17, 17);
                _key.CenterOrigin();
                _key.frame = PlayerStats.GetFrameOfButton(Keys.Escape);

                Graphics.Draw(_key, 16 + camPos.x, 12 + camPos.y, 1f);
                Graphics.DrawStringOutline("EXIT", camPos + new Vec2(30, 9), Color.White, Color.Black, 1f, null, 1f);

                SpriteMap _cursor = new SpriteMap(Mod.GetPath<R6S>("Sprites/Aim.png"), 17, 17);
                _cursor.CenterOrigin();

                _cursor.position = Mouse.positionScreen;
                _cursor.frame = 31;
                Graphics.Draw(_cursor, _cursor.position.x, _cursor.position.y, 1);

                if(screen == 0)
                {
                    DrawMainMenu();
                }

                if (screen == 1)
                {
                    DrawOperUnlock();
                }

                if(screen == 2)
                {
                    DrawBuyOper();
                }

            }
            if (layer == Layer.Game)
            {

            }
            if (layer == Layer.Background)
            {
                Graphics.Draw(_background, 0, current.camera.position.x, current.camera.position.y);

                SpriteMap _bg = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/BackgroundEpic.png"), 320, 240);
                _bg.depth = 0.51f;
                _bg.alpha = epic;

                Graphics.Draw(_bg, 0, Level.current.camera.position.x, Level.current.camera.position.y);
            }
            base.PostDrawLayer(layer);

        }
    }
}
