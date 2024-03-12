using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.R6S
{
    public class OPEQ : Equipment
    {
        public SpriteMap _sprite;
        protected Sprite _pickupSprite;
        public Operators oper;
        public Duck duckOwner;
        public bool created;

        public int operIconFrame;
        public ulong netIndex;

        public string name = "Operator";
        public string description = 
            "Line1 \n\n" +
            "Line2 \n\n" +
            "Line3 \n\n" +
            "Line4 \n\n" +
            "Line5 \n\n" +
            "Line6 \n\n" +
            "Line7";

        public OPEQ(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            thickness = 10f;
            _equippedDepth = 4;
            center = new Vec2(16, 16);
            collisionOffset = new Vec2(-12f, -12f);
            collisionSize = new Vec2(24f, 42f);
            _equippedCollisionOffset = new Vec2(-12f, -12f);
            _equippedCollisionSize = new Vec2(24f, 24f);
            _equippedDepth = 3;
        }

        protected override bool OnDestroy(DestroyType type = null)
        {
            return false;
        }

        public override void Update()
        {
            if (owner != null)
            {
                Duck d = owner as Duck;
                if(equippedDuck == d)
                {
                    duckOwner = d;
                }
                if (oper != null && created == false && duckOwner.profile.localPlayer)
                {
                    Level.Add(oper);
                    oper.opeq = this;
                    created = true;
                }
                if (oper != null)
                {
                    if (duckOwner != null)
                    {
                        oper.duckOwner = duckOwner;
                        //duckOwner.position = new Vec2(-99999999, -99999999);
                        if (duckOwner.inputProfile != null)
                        {
                            if (duckOwner.inputProfile.genericController != null)
                            {
                                oper.genericController = duckOwner.inputProfile.genericController;
                            }
                        }
                        if (name == "" || name == null)
                        {
                            oper.name = duckOwner.profile.name;
                            oper.netIndex = duckOwner.profile.networkIndex;
                            netIndex = duckOwner.profile.networkIndex;
                            //DuckNetwork.SendToEveryone(new NMName(oper, oper.name));
                        }
                        oper.name = duckOwner.profile.name;
                    }

                    if (duckOwner.profile != null)
                    {
                        if (duckOwner.profile.localPlayer)
                        {
                            oper.moveLeft = (Keyboard.Down(PlayerStats.keyBindings[2]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[2]));

                            oper.moveRight = (Keyboard.Down(PlayerStats.keyBindings[3]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[3]));

                            oper.jump = (Keyboard.Pressed(PlayerStats.keyBindings[0]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[0]));

                            oper.crouching = (Keyboard.Pressed(PlayerStats.keyBindings[1]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[1]));

                            if(oper.genericController != null && oper.controller)
                            {
                                oper.moveLeft = (oper.duckOwner.inputProfile.leftStick.x) < -0.2f;
                                oper.moveRight = (oper.duckOwner.inputProfile.leftStick.x) > 0.2f;
                                oper.jump = oper.duckOwner.inputProfile.genericController.MapPressed(4096);
                                oper.crouching = oper.duckOwner.inputProfile.genericController.MapPressed(536870912);
                            }
                        }
                        if (duckOwner.profile.localPlayer)
                        {
                            oper.local = true;
                        }
                        else
                        {
                            oper.local = false;
                        }
                    }
                }
                
                Vec2 collide = new Vec2();
                if (d.crouch == true && d.sliding == true)
                {
                    //centery = 16; 
                    collide = new Vec2(26, 16);
                }
                if (d.crouch == true && d.sliding == false)
                {
                    //centery = 19;
                    collide = new Vec2(24, 22);
                }
                if (d.crouch == false && d.sliding == false)
                {
                    //centery = 19;
                    collide = new Vec2(20, 28);
                }
                collisionOffset = -collide / 2;
                collisionSize = collide;
                _equippedCollisionOffset = -collide / 2;
                _equippedCollisionSize = collide;             
            }
            base.Update();
        }
    }
}
