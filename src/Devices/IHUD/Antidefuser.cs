using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Guns|Devices|IHUD")]
    public class AntiDefuser : Device
    {
        public float defuse;

        public int count = 1;

        public AntiDefuser(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/deactive.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            weight = 0.9f;
            placeable = false;
            breakable = false;
            zeroSpeed = false;
            scannable = false;

            depth = 0.6f;

            cantProne = true;
            isSecondary = true;
            index = 23;

            //oneHand = true;
            //mainHand = true;

            index = 3;
        }

        public override void Update()
        {
            base.Update();
            if(oper != null)
            {
                if (oper.holdObject == this)
                {
                    if (Keyboard.Released(PlayerStats.keyBindings[5]) || Keyboard.Released(PlayerStats.keyBindingsAlternate[5]))
                    {
                        oper.BackToWeapon(20);
                        foreach (DefuserAP d in Level.CheckCircleAll<DefuserAP>(position, 32))
                        {
                            defuse = 0;
                            d.defuse = 0;
                        }
                    }
                    foreach (DefuserAP d in Level.CheckCircleAll<DefuserAP>(position, 32))
                    {
                        if (Level.CheckLine<Block>(d.position, oper.position) == null && !d.defused)
                        {
                            defuse += 0.01666666f;
                            d.defuse += 0.01666666f;
                            if(d.defuse >= d.defuseTime)
                            {
                                if (d.g != null)
                                {
                                    d.defused = true;
                                    d.g.defused = true;
                                    DuckNetwork.SendToEveryone(new NMGamemodeEvent(d.g, "defuse", d.timer));

                                    Level.Add(new InfoFeedTab("", oper.name + " deactivatded defuser") { args = new string[1] { "news" } });
                                    
                                    defuse = 0;
                                    d.defuse = 0;

                                    if (count > 0 && oper.local)
                                    {
                                        count--;
                                        PlayerStats.renown += DeviceCost;
                                        PlayerStats.Save();
                                        Level.Add(new RenownGained() { description = "Defuser deactivated", amount = DeviceCost });
                                    }
                                }
                                oper.BackToWeapon(20);
                            }
                        }
                        else
                        {
                            defuse = 0;
                            d.defuse = 0;
                            oper.BackToWeapon(20);
                        }
                    } 
                }
            }
        }

        public override void OnDrawLayer(Layer layer)
        {
            if(layer == Layer.Foreground)
            {
                Vec2 pos = Level.current.camera.position;
                Vec2 cameraSize = Level.current.camera.size;
                Vec2 Unit = cameraSize / new Vec2(320, 180);

                foreach (DefuserAP d in Level.CheckCircleAll<DefuserAP>(position, 32))
                {
                    if (d.defuse > 0)
                    {
                        Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200 * (d.defuse / d.defuseTime), cameraSize.y / 180 * 120), Color.White, 1f, 1f);
                        Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200, cameraSize.y / 180 * 120), Color.Gray, 1f, 0.95f);
                    }
                }
            }
            base.OnDrawLayer(layer);
        }
    }
}
