using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class OperatorSelection : Button
    {
        public OPEQ opeq;
        public Duck d;
        public int operatorID;

        public Vec2 Scale = new Vec2(1, 1);

        public OperatorSelection(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/OperatorIcons.png"), 24, 24, false);
            graphic = _sprite;
            _sprite.center = new Vec2(12, 12);
            center = new Vec2(12, 12);
            collisionSize = new Vec2(24, 24);
            collisionOffset = new Vec2(-12, -12);
            depth = 0.4f;
            layer = Layer.Foreground;
        }

        public override void OnActivation()
        {
            SFX.Play(GetPath("SFX/UI/UIClick.wav"));
            if (PlayerStats.openedOperators.Contains(operatorID))
            {
                if (opeq != null && gm != null)
                {
                    foreach (Duck duck in Level.current.things[typeof(Duck)])
                    {
                        if (duck.profile.localPlayer && !duck.dead)
                        {
                            d = duck;
                        }
                    }

                    bool flag = false;
                    foreach (OPEQ oper in gm.selectedOperators)
                    {
                        if (oper.name == opeq.name)
                        {
                            flag = true;
                        }
                    }
                    if (operatorID < 0)
                    {
                        flag = false;
                    }
                    if (!flag)
                    {
                        opeq.duckOwner = d;
                        d.Equip(opeq);
                        gm.selectedId = operatorID;
                        picked = true;
                        opeq.netIndex = d.profile.networkIndex;
                        if (opeq.oper != null)
                        {
                            opeq.oper.netIndex = d.profile.networkIndex;
                        }

                        gm.screen += 1;
                        gm.addSelection = false;
                        gm.selectedOperators.Add(opeq);
                        gm.selected = opeq;

                        DuckNetwork.SendToEveryone(new NMSelectedOper(operatorID, d.profile.networkIndex, gm.localDuck.profile.name));
                    }
                }
            }
            base.OnActivation();
        }

        public override void OnHover()
        {
            base.OnHover();

            opeq = PlayerStats.GetOpeqByID(operatorID, position);
        }

        public override void NotHovered()
        {
            base.NotHovered();
        }

        public override void Update()
        {
            if (_sprite != null)
            {
                _sprite.frame = operatorID;
                if(operatorID < 0)
                {
                    _sprite.frame = 78;

                }
            }
            foreach (GamemodeScripter g in Level.current.things[typeof(GamemodeScripter)])
            {
                foreach(OPEQ op in g.selectedOperators)
                {
                    if (opeq != null)
                    {
                        if (op.name == opeq.name)
                        {
                            alpha = 0.5f;
                        }
                        else
                        {
                            alpha = 1f;
                        }
                    }
                }
            }
            if (!picked && !locked)
            {
                if (_sprite.scale.x > targetSize * Scale.x)
                {
                    _sprite.scale = new Vec2(_sprite.scale.x - 0.01f, _sprite.scale.y - 0.01f);
                }
                if (_sprite.scale.x < targetSize * Scale.x)
                {
                    _sprite.scale = new Vec2(_sprite.scale.x + 0.01f, _sprite.scale.y + 0.01f);
                }
                if (scale.x > targetSize * Scale.x)
                {
                    scale = new Vec2(scale.x - 0.04f, scale.y - 0.04f);
                }
                if (scale.x < targetSize * Scale.x)
                {
                    scale = new Vec2(scale.x + 0.04f, scale.y + 0.04f);
                }
            }

            collisionSize = new Vec2(20, 20) * scale;
            collisionOffset = new Vec2(-10, -10) * scale;

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();

            if (!PlayerStats.openedOperators.Contains(operatorID))
            {
                SpriteMap _s = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
                _s.scale = _sprite.scale;
                _s.position = _sprite.position;
                _s.center = _sprite.center;

                _s.alpha = 0.8f;
                _s.frame = 76;

                Graphics.Draw(_s, _s.position.x, _s.position.y, 0.94f);
            }

            if (opeq != null && selected)
            {
                Graphics.DrawStringOutline(opeq.name, Level.current.camera.position - new Vec2(120, 160) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 1.5f);
                Graphics.Draw(opeq._sprite, Level.current.camera.position.x - 146 + 320, Level.current.camera.position.y - 167 + 180, 0.6f);

                Graphics.DrawStringOutline(opeq.description, Level.current.camera.position - new Vec2(145, 135) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 0.65f);

                if (opeq != null)
                {
                    SpriteMap _ping = new SpriteMap(GetPath("Sprites/Cameras/ObservationMarker.png"), 24, 24);
                    _ping.CenterOrigin();

                    SpriteMap _dot = new SpriteMap(GetPath("Sprites/blackDot.png"), 1, 1);
                    _dot.scale = new Vec2(160f, 180f);
                    _dot.CenterOrigin();
                    _dot.alpha = 0.4f;

                    Graphics.Draw(_dot, Level.current.camera.position.x + 170, Level.current.camera.position.y, 0.3f);

                    Graphics.DrawStringOutline("Armor", Level.current.camera.position - new Vec2(130, 20) + new Vec2(320, 160), Color.White, Color.Black, 0.6f, null, 1f);

                    for (int i = 0; i < opeq.oper.Armor; i++)
                    {
                        _ping.frame = 0;
                        Graphics.Draw(_ping, Level.current.camera.position.x - 130 + 20 * i + 320, Level.current.camera.position.y + 160, 0.6f);
                    }
                    for (int i = 0; i < 3 - opeq.oper.Armor; i++)
                    {
                        _ping.frame = 4;
                        Graphics.Draw(_ping, Level.current.camera.position.x - 90 - 20 * i + 320, Level.current.camera.position.y + 160, 0.6f);
                    }

                    Graphics.DrawStringOutline("Speed", Level.current.camera.position - new Vec2(60, 20) + new Vec2(320, 160), Color.White, Color.Black, 0.6f, null, 1f);

                    for (int i = 0; i < opeq.oper.Speed; i++)
                    {
                        _ping.frame = 0;
                        Graphics.Draw(_ping, Level.current.camera.position.x - 60 + 20 * i + 320, Level.current.camera.position.y + 160, 0.6f);
                    }
                    for (int i = 0; i < 3 - opeq.oper.Speed; i++)
                    {
                        _ping.frame = 4;
                        Graphics.Draw(_ping, Level.current.camera.position.x - 20 - 20 * i + 320, Level.current.camera.position.y + 160, 0.6f);
                    }
                }
            }
        }
    }
}
