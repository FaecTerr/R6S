using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|IHUD")]
    public class CCEShield : BallisticShield
    {
        public CCEShield(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/CCEShield.png"), 12, 29, false);
            graphic = _sprite;
            center = new Vec2(6f, 14.5f);
            collisionSize = new Vec2(12f, 29f);
            collisionOffset = new Vec2(-6f, -14.5f);
            thickness = 4f;

            oneHand = false;
            _holdOffset = new Vec2(4.5f, -2f);

            _editorName = "CCE Shield";
            weaponClass = "Sheild";

            cantCrouch = true;
            cantProne = true;
            lockSprint = true;
        }
        public override void Update()
        {
            base.Update();

            if (user != null)
            {
                if (user.mode == "slide" || user.mode == "crouch")
                {
                    user.GoStand();
                }
                if (!user.HasEffect("Jammed"))
                {
                    if (Keyboard.Down(PlayerStats.keyBindings[13]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[13]))
                    {
                        foreach(Operators operators in Level.CheckRectAll<Operators>(topLeft, bottomRight))
                        {
                            if(operators.team != team)
                            {
                                if (!operators.HasEffect("CCEstun"))
                                {
                                    operators.effects.Add(new CCEstun());
                                }
                                if (operators.HasEffect("CCEstun"))
                                {
                                    operators.GetEffect("CCEstun").timer += 0.016667f;
                                }
                            }
                        }
                        Cooldown -= 0.01666666f;
                    }
                }
            }
        }
    }
}

