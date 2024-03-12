using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class EquipmentSelection : Button
    {
        public int itemID; //type of loadout
        public int slotID; //num of loadout
        public Operators oper;
        public GamemodeScripter gs;


        public EquipmentSelection(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/DevicesIcons.png"), 24, 24, false);

            _select = new SpriteMap(Mod.GetPath<R6S>("Sprites/SelectionFrame.png"), 48, 27, false);
            _select.center = new Vec2(24, 13.5f);

            center = new Vec2(24, 13.5f);

            //_sprite.center = new Vec2(8, 8);
            //graphic = _sprite;

            layer = Layer.Foreground;

            collisionSize = new Vec2(48, 27);
            collisionOffset = new Vec2(-24f, -13.5f);
            depth = 0.6f;
        }

        public override void OnActivation()
        {
            SFX.Play(GetPath("SFX/UI/UIClick.wav"));

            if (oper != null)
            {
                if (PlayerStats.operPreferences.Count < oper.operatorID + 20)
                {

                }
                if (itemID == 1)
                {
                    oper.MainGun = oper.Primary[slotID];

                    if (oper.operatorID >= 0)
                    {
                        PlayerStats.operPreferences[oper.operatorID * 20] = Convert.ToString(slotID);
                    }

                }
                if (itemID == 2)
                {
                    oper.SecondGun = oper.Secondary[slotID];
                    if (oper.operatorID >= 0)
                    {
                        PlayerStats.operPreferences[oper.operatorID * 20 + 6] = Convert.ToString(slotID);
                    }
                }
                if (itemID == 3)
                {
                    oper.SecondDevice = oper.Devices[slotID];
                    if (oper.operatorID >= 0)
                    {
                        PlayerStats.operPreferences[oper.operatorID * 20 + 12] = Convert.ToString(slotID);
                    }
                }
                PlayerStats.Save();
                foreach (GamemodeScripter g in Level.current.things[typeof(GamemodeScripter)])
                {
                    if (itemID == 1)
                    {
                        g.selectedPr = slotID;
                    }
                    if (itemID == 2)
                    {
                        g.selectedSc = slotID;
                    }
                    if (itemID == 3)
                    {
                        g.selectedDv = slotID;
                    }
                    //picked = true;
                    //g.screen += 1;
                    //g.addSelection = false;
                }
            }
            base.OnActivation();
        }

        public override void OnHover()
        {
            base.OnHover();
            _select.frame = 1;
        }

        public override void NotHovered()
        {
            base.NotHovered();
            //_select.frame = 0;
        }

        public override void Update()
        {
            collisionSize = new Vec2(48, 27) * scale;
            collisionOffset = new Vec2(-24f, -13.5f) * scale;

            if (oper != null)
            {
                if (itemID == 1)
                {
                    _sprite = oper.Primary[slotID]._sprite;
                    //graphic = _sprite;
                    _sprite.center = new Vec2(_sprite.width / 2, _sprite.height / 2);
                }
                if (itemID == 2)
                {
                    _sprite = oper.Secondary[slotID]._sprite;
                    //graphic = _sprite;
                    _sprite.center = new Vec2(_sprite.width / 2, _sprite.height / 2);
                }
                if (itemID == 3)
                {
                    _sprite = oper.Devices[slotID]._sprite;
                    //graphic = _sprite;
                    _sprite.center = new Vec2(_sprite.width / 2, _sprite.height / 2);
                }
            }

            if (!picked && !locked)
            {
                if (_sprite.scale.x > targetSize)
                {
                    _sprite.scale = new Vec2(_sprite.scale.x - 0.04f, _sprite.scale.y - 0.04f);
                }
                if (_sprite.scale.x < targetSize)
                {
                    _sprite.scale = new Vec2(_sprite.scale.x + 0.04f, _sprite.scale.y + 0.04f);
                }
                if (scale.x > targetSize)
                {
                    scale = new Vec2(scale.x - 0.04f, scale.y - 0.04f);
                }
                if (scale.x < targetSize)
                {
                    scale = new Vec2(scale.x + 0.04f, scale.y + 0.04f);
                }
            }

            
            foreach (GamemodeScripter gm in Level.current.things[typeof(GamemodeScripter)])
            {
                if(gs == null)
                {
                    gs = gm;
                }
                if (gs.selected != null)
                {
                    if (itemID == 1)
                    {
                        _select.frame = 0;
                        if (gs.selectedPr == slotID)
                        {
                            _select.frame = 1;
                            gs.selected.oper.gunFirstIndex = slotID;
                        }
                    }
                    if (itemID == 2)
                    {
                        _select.frame = 0;
                        if (gs.selectedSc == slotID)
                        {
                            _select.frame = 1;
                            gs.selected.oper.gunSecondIndex = slotID;
                        }
                    }
                    if (itemID == 3)
                    {
                        _select.frame = 0;
                        if (gs.selectedDv == slotID)
                        {
                            _select.frame = 1;
                            gs.selected.oper.DeviceIndex = slotID;
                        }
                    }
                }
            }

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            _select.scale = _sprite.scale;

            Graphics.Draw(_select, position.x, position.y, 0.5f);

            if (oper != null)
            {
                if (itemID == 1)
                {
                    Graphics.Draw(_sprite, position.x, position.y, 0.6f);
                }
                if (itemID == 2)
                {
                    Graphics.Draw(_sprite, position.x, position.y, 0.6f);
                }
                if (itemID == 3)
                {
                    Graphics.Draw(_sprite, position.x, position.y, 0.6f);
                }
            }

            if (selected && oper != null)
            {
                SpriteMap _dot = new SpriteMap(GetPath("Sprites/blackDot.png"), 1, 1);
                _dot.scale = new Vec2(160f, 180f);
                _dot.CenterOrigin();
                _dot.alpha = 0.4f;

                Graphics.Draw(_dot, Level.current.camera.position.x + 170, Level.current.camera.position.y, 0.3f);

                if (itemID == 1)
                {
                    Graphics.DrawStringOutline(oper.Primary[slotID].name, Level.current.camera.position + new Vec2(190, 10), Color.White, Color.Black, 0.6f, null, 1.2f);       


                    int heightDM = 40;
                    Graphics.DrawStringOutline("Damage", Level.current.camera.position - new Vec2(119, 190 - heightDM) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 0.8f);
                    int damage = oper.Primary[slotID].damage;
                    Graphics.DrawStringOutline(Convert.ToString(damage), new Vec2(Level.current.camera.position.x - 40 + 320, Level.current.camera.position.y + heightDM - 10), Color.White, Color.Black, 0.6f, null, 0.8f);                    
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightDM), Level.current.camera.position + new Vec2(200 + 100, heightDM), Color.DarkGray, 3f, 0.2f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightDM), Level.current.camera.position + new Vec2(200 + (float)(damage), heightDM), Color.White, 3f, 0.6f);                    


                    int heightFR = 70;
                    Graphics.DrawStringOutline("Fire rate", Level.current.camera.position - new Vec2(119, 190 - heightFR) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 0.8f);
                    int firerate = (int)(60 / oper.Primary[slotID].fireRate);
                    Graphics.DrawStringOutline(Convert.ToString(firerate), new Vec2(Level.current.camera.position.x - 38 + 320, Level.current.camera.position.y + heightFR - 10), Color.White, Color.Black, 0.6f, null, 0.8f);

                    if (firerate < 0)
                    {
                        firerate = 0;
                    }
                    if (firerate > 1000)
                    {
                        firerate = 1000;
                    }

                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightFR), Level.current.camera.position + new Vec2(200 + 100, heightFR), Color.DarkGray, 3f, 0.2f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightFR), Level.current.camera.position + new Vec2(200 + 0.1f * (firerate), heightFR), Color.White, 3f, 0.6f);
                    

                    int heightAC = 100;
                    Graphics.DrawStringOutline("Accuracy", Level.current.camera.position - new Vec2(119, 190 - heightAC) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 0.8f);
                    int Accuracy = (int)(oper.Primary[slotID].accuracy*100);
                    Graphics.DrawStringOutline(Convert.ToString(Accuracy), new Vec2(Level.current.camera.position.x - 38 + 320, Level.current.camera.position.y + heightAC - 10), Color.White, Color.Black, 0.6f, null, 0.8f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightAC), Level.current.camera.position + new Vec2(200 + 100, heightAC), Color.DarkGray, 3f, 0.2f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightAC), Level.current.camera.position + new Vec2(200 + (float)(Accuracy), heightAC), Color.White, 3f, 0.6f);


                    int heightMG = 130;
                    Graphics.DrawStringOutline("Magazine", Level.current.camera.position - new Vec2(119, 190 - heightMG) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 0.8f);
                    int Magazine = (int)(oper.Primary[slotID].maxAmmo);
                    if(Magazine > 100)
                    {
                        Magazine = 100;
                    }
                    Graphics.DrawStringOutline(Convert.ToString(oper.Primary[slotID].maxAmmo), new Vec2(Level.current.camera.position.x - 38 + 320, Level.current.camera.position.y + heightMG - 10), Color.White, Color.Black, 0.6f, null, 0.8f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightMG), Level.current.camera.position + new Vec2(200 + 100, heightMG), Color.DarkGray, 3f, 0.2f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightMG), Level.current.camera.position + new Vec2(200 + (float)(Magazine), heightMG), Color.White, 3f, 0.6f);
                    
                    //Graphics.DrawStringOutline(Convert.ToString((60 / oper.Primary[slotID].fireRate) * 60), new Vec2(Level.current.camera.position.x - 80 + 320, Level.current.camera.position.y + 160), Color.White, Color.Black, 0.6f, null, 1.5f);
                }
                if (itemID == 2)
                {
                    Graphics.DrawStringOutline(oper.Secondary[slotID].name, Level.current.camera.position - new Vec2(130, 170) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 1.2f);


                    int heightDM = 40;
                    Graphics.DrawStringOutline("Damage", Level.current.camera.position - new Vec2(119, 190 - heightDM) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 0.8f);
                    int damage = oper.Secondary[slotID].damage;
                    Graphics.DrawStringOutline(Convert.ToString(damage), new Vec2(Level.current.camera.position.x - 40 + 320, Level.current.camera.position.y + heightDM - 10), Color.White, Color.Black, 0.6f, null, 0.8f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightDM), Level.current.camera.position + new Vec2(200 + 100, heightDM), Color.DarkGray, 3f, 0.2f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightDM), Level.current.camera.position + new Vec2(200 + (float)(damage), heightDM), Color.White, 3f, 0.6f);

                    
                    int heightFR = 70;
                    Graphics.DrawStringOutline("Fire rate", Level.current.camera.position - new Vec2(119, 190 - heightFR) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 0.8f);
                    int firerate = (int)(60 / oper.Secondary[slotID].fireRate);
                    Graphics.DrawStringOutline(Convert.ToString(firerate), new Vec2(Level.current.camera.position.x - 38 + 320, Level.current.camera.position.y + heightFR - 10), Color.White, Color.Black, 0.6f, null, 0.8f);

                    if (firerate < 0)
                    {
                        firerate = 0;
                    }
                    if (firerate > 1000)
                    {
                        firerate = 1000;
                    }

                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightFR), Level.current.camera.position + new Vec2(200 + 100, heightFR), Color.DarkGray, 3f, 0.2f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightFR), Level.current.camera.position + new Vec2(200 + 0.1f * (firerate), heightFR), Color.White, 3f, 0.6f);
                    

                    int heightAC = 100;
                    Graphics.DrawStringOutline("Accuracy", Level.current.camera.position - new Vec2(119, 190 - heightAC) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 0.8f);
                    int Accuracy = (int)(oper.Secondary[slotID].accuracy * 100);
                    Graphics.DrawStringOutline(Convert.ToString(Accuracy), new Vec2(Level.current.camera.position.x - 38 + 320, Level.current.camera.position.y + heightAC - 10), Color.White, Color.Black, 0.6f, null, 0.8f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightAC), Level.current.camera.position + new Vec2(200 + 100, heightAC), Color.DarkGray, 3f, 0.2f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightAC), Level.current.camera.position + new Vec2(200 + (float)(Accuracy), heightAC), Color.White, 3f, 0.6f);


                    int heightMG = 130;
                    Graphics.DrawStringOutline("Magazine", Level.current.camera.position - new Vec2(119, 190 - heightMG) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 0.8f);
                    int Magazine = (int)(oper.Secondary[slotID].maxAmmo);
                    if (Magazine > 100)
                    {
                        Magazine = 100;
                    }
                    Graphics.DrawStringOutline(Convert.ToString(oper.Secondary[slotID].maxAmmo), new Vec2(Level.current.camera.position.x - 38 + 320, Level.current.camera.position.y + heightMG - 10), Color.White, Color.Black, 0.6f, null, 0.8f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightMG), Level.current.camera.position + new Vec2(200 + 100, heightMG), Color.DarkGray, 3f, 0.2f);
                    Graphics.DrawLine(Level.current.camera.position + new Vec2(200, heightMG), Level.current.camera.position + new Vec2(200 + (float)(Magazine), heightMG), Color.White, 3f, 0.6f);
                    
                    //Graphics.DrawStringOutline(Convert.ToString((60 / oper.Primary[slotID].fireRate) * 60), new Vec2(Level.current.camera.position.x - 80 + 320, Level.current.camera.position.y + 160), Color.White, Color.Black, 0.6f, null, 1.5f);
                }
                if (itemID == 3)
                {
                    Graphics.DrawStringOutline(oper.Devices[slotID].editorName, Level.current.camera.position - new Vec2(130, 170) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 1.2f);
                    Graphics.Draw(oper.Devices[slotID]._sprite, Level.current.camera.position.x - 30 + 320, Level.current.camera.position.y - 160 + 180, 0.6f);


                }
            }
        }
    }
}


