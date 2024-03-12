using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|IHUD")]
    public class HackingDevice : Device, IDrawToDifferentLayers
    {
        public bool disabled;
        public float calling;
        public float callTime = 2;

        public HackingDevice(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/tablet.png"), 12, 12, false);
            graphic = _sprite;
            center = new Vec2(6f, 6f);
            collisionOffset = new Vec2(-4f, -6f);
            collisionSize = new Vec2(8f, 12f);
            weight = 0.9f;

            placeable = false;
            breakable = false;
            zeroSpeed = false;
            scannable = false;

            index = 29;
            UsageCount = 2;
            ShowCounter = true;

            CooldownTime = 20;
        }

        public override void Update()
        {
            base.Update();

            if(UsageCount <= 0 && !disabled)
            {
                //UsageCount = 1;
                disabled = true;
                ShowCounter = false;
            }

            if(Cooldown > 0)
            {
                Cooldown -= 0.0166666f / CooldownTime;
            }

            if (oper != null && Cooldown <= 0)
            {
                if (oper.local && !oper.HasEffect("Jammed") && !disabled && (Keyboard.Down(PlayerStats.keyBindings[13]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[13])))
                {
                    calling += 0.01666666f;

                    oper.unableToMove = 10;
                    oper.unableToJump = 10;
                    
                    if (calling > callTime)
                    {
                        Cooldown = 1;

                        UsageCount--;
                        calling = 0;
                        foreach (Operators op in Level.current.things[typeof(Operators)])
                        {
                            if (op.team != team)
                            {
                                op.effects.Add(new PhoneCalled());
                            }
                        }
                        DuckNetwork.SendToEveryone(new NMPhoneCalled(team));
                    }
                }
                else
                {
                    calling = 0;
                }
            }
        }
        public override void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                base.OnDrawLayer(Layer.Foreground);

                Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);
                Vec2 pos = Level.current.camera.position;
                Vec2 cameraSize = Level.current.camera.size;

                if (calling > 0)
                {
                    Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200 * (calling / callTime), cameraSize.y / 180 * 120), Color.White, 1f, 1f);
                    Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200, cameraSize.y / 180 * 120), Color.Gray, 2f, 0.98f);
                }
            }
            if (layer == Layer.Game)
            {
                base.OnDrawLayer(Layer.Game);
            }
        }
    }
}
