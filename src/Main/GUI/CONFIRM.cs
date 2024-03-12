using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Confirm : Button, IDrawToDifferentLayers
    {
        public BuyOperator op;
        public float hold;

        private SinWave _pulse = 0.5f;
        private int notEnough;

        public Confirm(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/GUI/ConfirmFrame.png"), 76, 17, false);
            _sprite.center = new Vec2(38, 8.5f);
            center = new Vec2(38, 8.5f);
            graphic = _sprite;
            layer = Layer.Foreground;
            collisionSize = new Vec2(76, 17);
            collisionOffset = new Vec2(-38, -8.5f);
            depth = 0.6f;
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if(pLayer == Layer.Foreground)
            {
                if (op != null)
                {
                    SpriteMap _s = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
                    _s.frame = op.operatorID;
                    _s.CenterOrigin();
                    Graphics.Draw(_s, position.x + 60, position.y);
                    Graphics.DrawRect(topLeft, bottomLeft + new Vec2(76 * hold * scale.x, 0), Color.White, 1f, true, 0);

                    if (!PlayerStats.openedOperators.Contains(op.operatorID))
                    {
                        SpriteMap _renown = new SpriteMap(GetPath("Sprites/GUI/Renown.png"), 32, 32);
                        _renown.CenterOrigin();
                        _renown.scale = new Vec2(0.2f, 0.2f);
                        Graphics.Draw(_renown, position.x + 11, position.y + 10, 1);

                        Color c = Color.White;

                        if (notEnough > 0)
                        {
                            notEnough--;
                            c = new Vec3(255, 200 - (_pulse + 1) * (255 / 3), 200 - (_pulse + 1) * (255 / 3)).ToColor();
                        }

                        Graphics.DrawStringOutline("1000", new Vec2(position.x - 11, position.y + 8), c, Color.Black, 1f, null, 0.5f);
                    }
                }
            }
        }

        public override void OnHover()
        {
            base.OnHover();
            _sprite.frame = 1;
        }

        public override void NotHovered()
        {
            base.NotHovered();
            _sprite.frame = 0;
            hold = 0;
        }

        public override void OnHoldActivation()
        {
            base.OnHoldActivation();
            if (Level.current is ShopLevel && op != null)
            {
                if (!PlayerStats.openedOperators.Contains(op.operatorID))
                {
                    if (PlayerStats.renown >= 1000)
                    {
                        hold += 0.01f;

                        if (hold >= 1)
                        {
                            if (op != null)
                            {
                                PlayerStats.openedOperators.Add(op.operatorID);
                                PlayerStats.renown -= 1000;
                                PlayerStats.Save();
                                (Level.current as ShopLevel).unlockingItem = true;
                                (Level.current as ShopLevel).unlockTimer = 600;
                            }
                            hold = 0;
                        }
                    }
                    else
                    {
                        notEnough = 60;
                    }
                }
            }
        }

        public override void OnReleaseActivation()
        {
            base.OnReleaseActivation();
            hold = 0;
        }

        public override void OnActivation()
        {
            base.OnActivation();
            if (!(Level.current is ShopLevel))
            {
                foreach (GamemodeScripter g in Level.current.things[typeof(GamemodeScripter)])
                {
                    g.addSelection = false;
                    //picked = true;
                    g.screen += 1;
                    if (g.localDuck != null)
                    {
                        g.confirmed += 1;
                        DuckNetwork.SendToEveryone(new NMConfirmSelection());
                    }
                    //g.addSelection = false;
                }
            }
        }

        public override void Update()
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

            collisionSize = new Vec2(76, 17) * scale;
            collisionOffset = new Vec2(-38, -8.5f) * scale;
          
            base.Update();
        }
    }
}


