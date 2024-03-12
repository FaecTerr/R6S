using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BackButton : Button
    {
        public BackButton(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/GUI/FuckGoBack.png"), 32, 17, false);
            _sprite.center = new Vec2(16f, 8.5f);
            center = new Vec2(16, 8.5f);
            graphic = _sprite;
            layer = Layer.Foreground;
            collisionSize = new Vec2(32, 17);
            collisionOffset = new Vec2(-16, -8.5f);
            depth = 0.6f;
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
        }

        public override void OnActivation()
        {
            base.OnActivation();
            if (!(Level.current is ShopLevel))
            {
                foreach (GamemodeScripter g in Level.current.things[typeof(GamemodeScripter)])
                {
                    if (g.screen == 1)
                    {

                    }
                    if (g.screen == 2)
                    {
                        g.LoadoutRemove();
                        g.OperatorsSelect(new int[0]);

                        DuckNetwork.SendToEveryone(new NMChangeOper(g.localDuck));

                        g.selectedOperators.Remove(g.selected);
                        g.selected = null;
                        g.screen -= 1;
                        g.addSelection = true;
                    }
                    if (g.screen == 3)
                    {
                        g.LoadoutSelect();
                        g.addSelection = true;
                        g.screen -= 1;
                    }
                }
            }
            else
            {
                (Level.current as ShopLevel).ReturnToMainMenu();
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

            collisionSize = new Vec2(32, 17) * scale;
            collisionOffset = new Vec2(-16, -8.5f) * scale;

            base.Update();
        }
    }
}


