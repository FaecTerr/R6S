using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.R6S
{
    public class AmmoBox : Placeable
    {
        public bool picked;

        public AmmoBox(float xval, float yval) : base(xval, yval)
        {
            center = new Vec2(8f, 12f);
            weight = 0f;
            collisionOffset = new Vec2(-8f, -6f);
            collisionSize = new Vec2(14f, 10f);
            zeroSpeed = true;
            scannable = true;
            placeable = true;
            _sprite = new SpriteMap(GetPath("Sprites/Defuser.png"), 16, 16, false);
            base.graphic = _sprite;

            UsageCount = 1;
            setTime = 7;
            _sprite.frame = 1;
            canPick = false;

            DeviceCost = 100;
            descriptionPoints = "Defuser planted";

        }

        public override void SetAfterPlace()
        {
            afterPlace = new DefuserAP(position.x, position.y);
            base.SetAfterPlace();
        }
    }

    [EditorGroup("Faecterr's|Scripting")]
    public class AmmoBoxAP : Placeable
    {
        public float radius = 32;
        public float takeTime = 0.3f;

        public float taking = 0;
        public int useRemained = 0;
        public bool inf;
        public EditorProperty<int> uses;
        public EditorProperty<float> useTime;
        public AmmoBoxAP(float xval, float yval) : base(xval, yval)
        {
            center = new Vec2(16f, 12f);
            weight = 0f;
            collisionOffset = new Vec2(-14f, -5f);
            collisionSize = new Vec2(28f, 8f);
            zeroSpeed = true;
            scannable = true;
            placeable = true;
            _sprite = new SpriteMap(GetPath("Sprites/ammoBox.png"), 32, 16, false);

            base.graphic = _sprite;
            canPick = false;
            canPickUp = false;

            hugWalls = WallHug.Floor; 
            uses = new EditorProperty<int>(-1, null, -1, 5, 1);
            useTime = new EditorProperty<float>(0.3f, null, 0.5f, 4f, 0.2f);
        }

        public override void Initialize()
        {
            useRemained = uses;
            if(uses < 0)
            {
                inf = true;
            }
            takeTime = useTime;
            base.Initialize();
        }

        public override void Update()
        {
            if (useRemained > 0 || inf)
            {
                int i = 0;
                foreach (Operators oper in Level.CheckCircleAll<Operators>(position, radius))
                {
                    i++;
                    if (oper.local && (Keyboard.Down(PlayerStats.keyBindings[4]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[4])) && oper.priorityTaken <= 4.1f)
                    {
                        oper.unableToMove = 10;
                        oper.unableToJump = 10;
                        taking += 0.01666666f;
                        if (taking > takeTime)
                        {
                            useRemained--;
                            taking = 0;
                            if (oper.MainDevice.UsageCount < 5)
                            {
                                oper.MainDevice.UsageCount++;
                                if (oper.MainDevice is Launchers)
                                {
                                    (oper.MainDevice as Launchers).Missiles1++;
                                    if ((oper.MainDevice as Launchers).maxMode > 0)
                                    {
                                        (oper.MainDevice as Launchers).Missiles2++;
                                    }
                                }
                            }
                            if (oper.SecondDevice.UsageCount < 5)
                            {
                                oper.SecondDevice.UsageCount++;
                            }
                            if (oper.MainGun.magazine < oper.MainGun.maxAmmo * 5)
                            {
                                oper.MainGun.magazine = oper.MainGun.maxAmmo * 5;
                            }
                            oper.MainGun.ammo = oper.MainGun.maxAmmo + (oper.MainGun.canBeTacticallyReloaded ? 1 : 0);
                            if (oper.SecondGun.magazine < oper.SecondGun.maxAmmo * 5)
                            {
                                oper.SecondGun.magazine = oper.SecondGun.maxAmmo * 5;
                            }
                            oper.SecondGun.ammo = oper.SecondGun.maxAmmo + (oper.SecondGun.canBeTacticallyReloaded ? 1 : 0);
                        }
                    }
                    else
                    {
                        taking = 0;
                    }
                }
                if (i == 0)
                {
                    taking = 0;
                }
            }
            
            base.Update();
        }

        public override void OnDrawLayer(Layer layer)
        {
            base.OnDrawLayer(layer);
            if(layer == Layer.Foreground)
            {
                Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);
                Vec2 pos = Level.current.camera.position;
                Vec2 cameraSize = Level.current.camera.size;

                if (taking > 0)
                {
                    Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * (60 + 200 * (taking / takeTime)), cameraSize.y / 180 * 120), Color.White, 1f, 1f);
                    Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * (60 + 200), cameraSize.y / 180 * 120), Color.Gray, 2f, 0.98f);
                }
            }
        }
    }
    [EditorGroup("Faecterr's|Scripting")]
    public class HealBoxAP : Placeable
    {
        public float radius = 32;
        public float takeTime = 0.3f;

        public float taking = 0;
        public int useRemained = 0;
        public bool inf;
        public EditorProperty<int> uses;
        public EditorProperty<float> useTime;
        public HealBoxAP(float xval, float yval) : base(xval, yval)
        {
            center = new Vec2(16f, 12f);
            weight = 0f;
            collisionOffset = new Vec2(-14f, -5f);
            collisionSize = new Vec2(28f, 8f);
            zeroSpeed = true;
            scannable = true;
            placeable = true;
            _sprite = new SpriteMap(GetPath("Sprites/healBox.png"), 32, 16, false);

            base.graphic = _sprite;
            canPick = false;
            canPickUp = false;

            hugWalls = WallHug.Floor;
            uses = new EditorProperty<int>(-1, null, -1, 5, 1);
            useTime = new EditorProperty<float>(0.3f, null, 0.5f, 4f, 0.2f);
        }
        public override void Initialize()
        {
            useRemained = uses;
            if (uses < 0)
            {
                inf = true;
            }
            takeTime = useTime;
            base.Initialize();
        }
        public override void Update()
        {
            if (useRemained > 0 || inf)
            {
                int i = 0;
                foreach (Operators oper in Level.CheckCircleAll<Operators>(position, radius))
                {
                    i++;
                    if (oper.local && (Keyboard.Down(PlayerStats.keyBindings[4]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[4])) && oper.priorityTaken <= 4.1f)
                    {
                        oper.unableToMove = 10;
                        oper.unableToJump = 10;
                        taking += 0.01666666f;
                        if (taking > takeTime)
                        {
                            useRemained--;
                            taking = 0;
                            oper.Health = 100;
                        }
                    }
                    else
                    {
                        taking = 0;
                    }
                }
                if (i == 0)
                {
                    taking = 0;
                }

                base.Update();
            }
        }

        public override void OnDrawLayer(Layer layer)
        {
            base.OnDrawLayer(layer);
            if (layer == Layer.Foreground)
            {
                Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);
                Vec2 pos = Level.current.camera.position;
                Vec2 cameraSize = Level.current.camera.size;

                if (taking > 0)
                {
                    Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * (60 + 200 * (taking / takeTime)), cameraSize.y / 180 * 120), Color.White, 1f, 1f);
                    Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * (60 + 200), cameraSize.y / 180 * 120), Color.Gray, 2f, 0.98f);
                }
            }
        }
    }
}
