using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class SideSelectionButton : Button
    {
        public string team = "Def";

        public SideSelectionButton(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/GUI/TEAM.png"), 32, 32, false);
            _sprite.center = new Vec2(16f, 16f);
            center = new Vec2(16, 16f);
            graphic = _sprite;
            layer = Layer.Foreground;
            collisionSize = new Vec2(32, 32);
            collisionOffset = new Vec2(-16, -16f);
            depth = 0.6f;
        }

        public override void OnActivation()
        {
            SFX.Play(GetPath("SFX/UI/UIClick.wav"));
            if (gm != null)
            {
                if (gm.localDuck.profile != null)
                {
                    if (team == "Def" && gm.defenders < 4)
                    {
                        gm.team = 0;
                        gm.defenders += 1;
                        R6S.upd.teamAqua.Add(gm.localDuck.profile);
                        DuckNetwork.SendToEveryone(new NMTeam(0, gm.localDuck.profile));
                    }
                    if (team == "Att" && gm.attackers < 4)
                    {
                        gm.team = 1;
                        gm.attackers += 1;
                        R6S.upd.teamMagma.Add(gm.localDuck.profile);
                        DuckNetwork.SendToEveryone(new NMTeam(1, gm.localDuck.profile));
                    }
                    //picked = true;
                    //g.currentPhase += 1;

                    gm.loaded += 1;
                    DuckNetwork.SendToEveryone(new NMConfirmLoading());

                    gm.SideSelectRemove();
                }
            }
        }

        public override void Update()
        {
            if (team == "Def")
            {
                _sprite.frame = 0;
            }
            if (team == "Att")
            {
                _sprite.frame = 1;
            }

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

            collisionSize = new Vec2(32, 32) * scale;
            collisionOffset = new Vec2(-16, -16f) * scale;

            base.Update();
        }
    }
}


