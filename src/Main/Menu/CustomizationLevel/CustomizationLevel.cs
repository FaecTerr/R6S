using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DuckGame.R6S
{
    public class CustomizationLevel : Level
    {
        public List<int> unlockedOpearotrs = new List<int>();

        public int xpos;
        public int ypos;

        public Color c = Color.White;

        public Profile p;

        public int quality = 0;

        public SinWave _pulse = 0.1f;
        public SinWave _pulse2 = 0.06f;

        public int screen; //0 - main, 1 - packs, 2 - Operators unlock, 3 - Operator customize, 4 - Weapon customize

        public float moving;
        public SpriteMap _background;
        public SpriteMap _exit;

        public float pos;
        public float ENDPOS;

        public double h;
        public double s = 1;
        public double v = 1;
        
        public int animation;

        // 320 x 180

        public override void Initialize()
        {
            int size = 0;

            Vec2 startPos = new Vec2(320 * 0);

            for (int i = 0; i < PlayerStats.totalOperators; i++)
            {
                //Header for operator
                OperatorSkins oper = new OperatorSkins(startPos.x + 120, 20 + size * 24, i);
                if (oper.opeq != null && oper.opeq.oper != null)
                {
                    Add(oper);

                    size += 1;

                    if (oper.opeq != null)
                    {
                        oper.opeq.Initialize();
                        if (oper.opeq.oper != null)
                        {
                            //Default headgear
                            Add(new EquipmentSkins(startPos.x + 40, 20 + size * 24, oper.opeq.oper, oper.opeq.oper._head) { slot = 13, name = oper.opeq.name, rarity = 0 });

                            int c = 1;

                            //Custom headgear
                            foreach (SkinElement skin in SkinsCollectionManager.allCollection)
                            {
                                if (skin.mainThing.ToUpper() == oper.opeq.name && skin.type == 1)
                                {
                                    SpriteMap _headgear = new SpriteMap(Mod.GetPath<R6S>("Sprites/Operators/skins/" + skin.location + ".png"), 32, 32);

                                    Add(new EquipmentSkins(startPos.x + 40 + 30 * c, 20 + size * 24, oper.opeq.oper, _headgear) { slot = 13, name = skin.name, defaul = false, rarity = skin.rarity });
                                    c++;
                                }
                            }
                            size += 1;

                            for (int j = 0; j < oper.opeq.oper.Primary.Count; j++)
                            {
                                List<SpriteMap> skins = new List<SpriteMap>();
                                skins.Add(oper.opeq.oper.Primary[j]._sprite);
                                skins[0].CenterOrigin();

                                string name = oper.opeq.oper.Primary[j].editorName;
                                int sizey = oper.opeq.oper.Primary[j]._sprite.width;

                                int slot = 5;
                                if (j == 1)
                                {
                                    slot = 16;
                                }
                                if (j == 2)
                                {
                                    slot = 17;
                                }
                                if (skins.Count > 0)
                                {
                                    //Default Primary skin
                                    Add(new EquipmentSkins(startPos.x + 40, 20 + size * 24, oper.opeq.oper, skins[0]) { slot = slot, name = name, rarity = 0 });
                                }

                                int count = 1;

                                foreach (SkinElement skin in SkinsCollectionManager.allCollection)
                                {
                                    if (skin.mainThing == name)
                                    {
                                        skins.Add(new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/skins/" + skin.location + ".png"), 32, sizey));
                                        skins[count].CenterOrigin();
                                        if (skins.Count > count)
                                        {
                                            //Custom primary skin
                                            Add(new EquipmentSkins(startPos.x + 40 + 30 * count, 20 + size * 24, oper.opeq.oper, skins[count]) { slot = slot, name = skin.name, defaul = false, rarity = skin.rarity });
                                        }
                                        count += 1;
                                    }
                                }

                                size += 1;
                            }


                            for (int j = 0; j < oper.opeq.oper.Secondary.Count; j++)
                            {
                                List<SpriteMap> skins = new List<SpriteMap>();

                                skins.Add(oper.opeq.oper.Secondary[j]._sprite);
                                skins[0].CenterOrigin();

                                string name = oper.opeq.oper.Secondary[j].editorName;

                                int sizey = oper.opeq.oper.Secondary[j]._sprite.width;

                                int slot = 11;
                                if (j == 1)
                                {
                                    slot = 18;
                                }
                                if (j == 2)
                                {
                                    slot = 19;
                                }
                                if (skins.Count > 0)
                                {
                                    //Default secondary skin
                                    Add(new EquipmentSkins(startPos.x + 40, 20 + size * 24, oper.opeq.oper, skins[0]) { slot = slot, name = name });
                                }
                                int count = 1;
                                foreach (SkinElement skin in SkinsCollectionManager.allCollection)
                                {
                                    if (skin.mainThing == name)
                                    {
                                        skins.Add(new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/skins/" + skin.location + ".png"), 32, sizey));
                                        skins[count].CenterOrigin();
                                        if (skins.Count > count)
                                        {
                                            //Custom secondary
                                            Add(new EquipmentSkins(startPos.x + 40 + 30 * count, 20 + size * 24, oper.opeq.oper, skins[count]) { slot = slot, name = skin.name, defaul = false, rarity = skin.rarity });
                                        }
                                        count += 1;
                                    }
                                }
                                ENDPOS = 20 + size * 24 - 80;
                                size += 1;
                            }
                        }
                    }
                }
            }

            size = 0;
            Add(new GlobalSkins(startPos.x + 40 + 320, 60 + size * 24, new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/banner.png"), 56, 134)) { slot = 0 });
            int coun = 1;
            foreach (SkinElement s in SkinsCollectionManager.allCollection)
            {                
                if(s.mainThing == "Banner")
                {
                    Add(new GlobalSkins(startPos.x + 40 + 320 + 66 * coun, 60 + size * 66, new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/" + s.location + ".png"), 56, 134)) { slot = 0, name = s.name, defaul = false, rarity = s.rarity});
                    coun++;
                    if(coun > 3)
                    {
                        size++;
                        coun = 0;
                    }
                }
            }
            size++;

            for (int i = 0; i < 2; i++)
            {
                Add(new CustomizationSwitch(300 + 320 * i, 7));
            }
            int operFastTravelCurrent = 0;
            for (int i = 0; i < PlayerStats.totalOperatorsPlanned; i++)
            {
                OPEQ op = PlayerStats.GetOpeqByCallID(i, new Vec2(0, 0));
                if (op != null && op.oper != null)
                {
                    Add(new operIconFastTravel(0, 0) { opeq = op, orderID = operFastTravelCurrent });
                    operFastTravelCurrent++;
                }
            }
            base.Initialize();
        }

        public CustomizationLevel() : base()
        {
            Layer.Game.fade = 0f;
            Layer.Foreground.fade = 0f;

            _background = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/Background.png"), 320, 240);
            _background.depth = 0.5f;

            _exit = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/ExitButton.png"), 20, 20);
        }
        public override void Update()
        {
            base.Update();

            if(moving < -10)
            {
                moving = -10;
            }

            if (screen == 0)
            {
                if (moving > ENDPOS)
                {
                    moving = ENDPOS;
                }
            }

            if (screen == 1)
            {
                float globalCount = 0;
                foreach (SkinElement s in SkinsCollectionManager.allCollection)
                {
                    if (s.mainThing == "Banner")
                    {
                        globalCount += 0.25f;
                    }
                }
                if (moving > Math.Max(60 + 66 * (int)globalCount - 180, 0))
                {
                    moving = Math.Max(60 + 66 * (int)globalCount - 180, 0);
                }
            }

            if (Keyboard.Down(PlayerStats.keyBindings[19]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[19]))
            {
                moving += Mouse.scroll * 0.24f;
            }
            else
            {
                moving += Mouse.scroll * 0.08f;
            }

            camera.position = new Vec2(320 * screen, moving);

            Profile p = Profiles.DefaultPlayer1;

            if (Keyboard.Pressed(Keys.Escape))
            {  
                ReturnToMainMenu();
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

        public override void PostDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                Vec2 camSize = new Vec2(current.camera.width, current.camera.width);
                Vec2 realCamSize = new Vec2(current.camera.width, current.camera.height);
                Vec2 camPos = new Vec2(current.camera.position.x, current.camera.position.y);

                SpriteMap _cursor = new SpriteMap(Mod.GetPath<R6S>("Sprites/Aim.png"), 17, 17);
                _cursor.CenterOrigin();

                _cursor.position = Mouse.positionScreen;
                _cursor.frame = 31;
                Graphics.Draw(_cursor, _cursor.position.x, _cursor.position.y, 1);

                SpriteMap _key = new SpriteMap(Mod.GetPath<R6S>("Sprites/keys.png"), 17, 17);
                _key.CenterOrigin();
                _key.frame = PlayerStats.GetFrameOfButton(Keys.Escape);

                Graphics.Draw(_key, 16 + camPos.x, 12 + camPos.y, 1f);
                Graphics.DrawStringOutline("EXIT", camPos + new Vec2(30, 9), Color.White, Color.Black, 1f, null, 1f);

                
            }
            if (layer == Layer.Game)
            {

            }
            if (layer == Layer.Background)
            {
                Graphics.Draw(_background, 0, current.camera.position.x, current.camera.position.y);
            }
            base.PostDrawLayer(layer);

        }
    }
}
