using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Weapon")]
    public class GunDev : Device
    {
        public List<Bullet> firedBullets = new List<Bullet>();
        //public Duck ownDuck;
        //public Operators oper;
        public int ammo = 1; //Current amount of ammo loaded in gun
        public int maxAmmo = 1; //Gun capacity of ammo

        public float fireRate = 0.01f;

        public bool canFire = true;

        public Vec2 barrel = new Vec2(12, -2);

        public Vec2 _barrel
        {
            get
            {
                float pX = barrel.x * (float)Math.Sin(ang) + barrel.y * (float)Math.Cos(ang) * offDir;
                float pY = barrel.x * (float)Math.Cos(ang) - barrel.y * (float)Math.Sin(ang) * offDir;

                return new Vec2(pX, pY);
            }
            set
            {
                float pX = barrel.x * (float)Math.Sin(ang) + barrel.y * (float)Math.Cos(ang) * offDir;
                value.x = pX;

                float pY = barrel.x * (float)Math.Cos(ang) - barrel.y * (float)Math.Sin(ang) * offDir;
                value.y = pY;
            }
        }


        public float accuracy = 1; //So apparently, I dont remember what is accuracy for
        public float arc = 30;

        public float _arc 
        { 
            get 
            {
                float localArc = arc;
                if (oper != null)
                {
                    if (oneHand && !mainHand)
                    {
                        if (oper.holdObject != null)
                        {
                            if (oper.holdObject2 == this && oper.holdObject is GunDev)
                            {
                                if ((oper.holdObject as GunDev).shield)
                                {
                                    localArc *= 1.4f;
                                }
                            }
                        }
                    }
                }
                if (underGrip == 1)
                {
                    localArc *= 0.8f;
                }

                if (isADS)
                {
                    if (!isShotgun)
                    {
                        localArc = 0;
                    }
                    else
                    {
                        localArc *= 1f;
                    }
                }
                else
                {
                    if (isShotgun)
                    {
                        localArc *= 1.25f;
                    }
                }

                if (localArc >= 179)
                {
                    localArc = 178;
                }
                return localArc; 
            }
            set
            {
                _arc = value;
            }
        }

        public float range = 240f;

        /// <summary>
        /// In percents when the damage starts dropping to minDamageDrop
        /// </summary>
        public float damageDropDistance = 0.5f;

        /// <summary>
        /// In percents when damage stops dropping and equals minDamageDrop
        /// </summary>
        public float maxDropDistance = 0.8f;

        /// <summary>
        /// In percents minimum damage of gun
        /// </summary>
        public float minDamageDrop = 0.5f;

        public bool overwriteDamageDrop;
            
        public float penetration = 0.6f;

        public float ADStime = 1;
        public bool isADS;
        public float ADS;
        public float xScope = 1;

        public bool shield;
        public bool holdback;

        public float gunMobility = 0.9f;
        public float gunADSMobility = 0.4f;
        public float notADSdamageMultiplier = 0.75f;

        public int framesSinceShot = 20;
        public bool lockInAnimation;

        public Vec2 barrelOffsetPos;
        public string weaponClass = "None";

        public int trackType = 0;

        /// <summary>
        /// 1. Suppressor - Removes tracer and -10% damage;   
        /// 2. Muzzle breaker - from -30% of recoil for first 3 bullets;   
        /// 3. Flash hider - Remove fire flash (lmao) and +40% of stability;   
        /// </summary>

        public int muzzle; //0 - None, 1 - Suppressor, 2 - Muzzle breaker, 3 - Flash hider

        /// <summary>
        /// 1. Straight - Remove 30% of vertical recoil;   
        /// 2. Angled - +50% scoping speed;   
        /// </summary>
        public int grip; //0 - None, 1 - Straight grip, 2 - Angled grip

        /// <summary>
        /// 1. Laser sight - Increase accuracy on 20% while not in ADS;   
        /// 2. Weighting - Increase horizontal recoil by 10% and decrease vertical by 20%;   
        /// </summary>
        public int underGrip; //0 - None, 1 - Laser sight, 2 - Weighting

        public float hRecoil;
        public float uRecoil;
        public float dRecoil;
        public Vec2 recoil;
        public Vec2 addRecoil;
        public int damage = 30;
        public int magazine = 100; //Total amount of bullets

        public float reload;
        public float timeToReload;
        public float timeToTacticalReload;
        public float ang;
        public float shotFrames;
        public bool reloading;

        public bool lockUntilReleaseAction;
        public bool isShotgun;
        public bool semiAuto = true;
        public int bulletsPerShot = 1;
        public bool manuallyReload;
        public bool highPowered;

        public float yStability = 0f;
        public float xStability = 0f;

        public bool canBeTacticallyReloaded = true;
        public bool tacticalReload;

        public string name = " ";
        
        public GamemodeScripter game;

        public string fireSound = "SFX/guns/pistol_generic_01.wav";
        public string tacticalReloadSound = "SFX/guns/reload_tactical.wav";
        public string reloadSound = "SFX/guns/reload_emergency.wav";

        private Vec2 CenterFixed;

        public HandAnimation reloadAnimation = new HandAnimation();

        public List<float> horizontalPattern = new List<float>()
        {
            0, 1, -1, 1, -1, 1,
            -1, 1, -1, 1, -1,
            -1, 1, -1, 1, -1,
            -1, 1, -1, 1, -1,
            -1, 1, -1, -1, 1,
            1, -1, 1, -1, 1,
            1, 1, -1, 1, 1,
            1, 1, 1, -1, 1,
            -1, -1, 1, 1, 1,
            1, 1, 1, -1, 1,
            1, 1, 1, 1, 1,
            1, -1, 1, 1, 1,
            1, 1, -1, 1, 1,
            1, 1, 1, 1, 1,
            1, -1, 1, 1, 1,
            1, 1, 1, -1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, -1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, -1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, -1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, -1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1
        };

        public List<float> verticalPattern = new List<float>()
        {
            0, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1,
            1, 1, 1, 1, 1
        };

        public GunDev(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("AK12.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;
            
            center = new Vec2(11.5f, 4.5f);
            collisionSize = new Vec2(33, 9f);
            collisionOffset = new Vec2(-11.5f, -4.5f);
            setTime = 1f;

            _canRaise = true;
            
            placeable = false;
            scannable = false;
            zeroSpeed = false;
            breakable = false;
            jammResistance = true;
            lockSprint = false;

            thickness = 0;
            reticle = 6;

            switchOffTime = 10;

            reloadAnimation.AddHand2KeyFrame(0, -35, new Vec2(-6, 4));
            reloadAnimation.AddHand2KeyFrame(12, -10, new Vec2(0, 3));
            reloadAnimation.AddHand2KeyFrame(2, -15, new Vec2(2, 0));
            reloadAnimation.AddHand2KeyFrame(6, -15, new Vec2(0, 0));
            reloadAnimation.AddHand2KeyFrame(3, -15, new Vec2(0, 0));
            reloadAnimation.AddHand2KeyFrame(8, 0, new Vec2(5, -3));
            reloadAnimation.AddHand2KeyFrame(12, -15, new Vec2(-5, -3));
            reloadAnimation.AddHand2KeyFrame(8, 0, new Vec2(0, 0));
        }

        public virtual void CorrectSprite()
        {
            center = CenterFixed;
        }

        public override void Initialize()
        {
            CenterFixed = center;
            base.Initialize();
        }

        public override void Update()
        {
            if (game == null)
            {
                foreach (GamemodeScripter g in Level.current.things[typeof(GamemodeScripter)])
                {
                    game = g;
                }
            }
            if (Math.Abs(addRecoil.x) > 0.1f || Math.Abs(addRecoil.y) > 0.1f)
            {
                float f = 0.8f;
                float d = 0.2f;
                float xMod = 1;
                float yMod = 1;

                if (oper != null)
                {
                    if (oper.controller)
                    {
                        f = 0.9f;
                        d = 0.75f;
                    }

                    if (grip == 1)
                    {
                        //xMod *= 0.8f; 
                    }
                                        
                    Mouse.position += addRecoil * f * new Vec2(xMod * oper.offDir, yMod) * (float)((oper.aim - barrelOffsetPos).length / (Math.Sqrt(Math.Pow(Level.current.camera.size.x, 2) + Math.Pow(Level.current.camera.size.y, 2)) * 0.5f));
                    addRecoil *= d;
                }
            }


            base.Update();

            if (oper != null)
            {
                Vec2 vec2 = oper.aim - position + recoil + (oper.position - position);
                Vec2 vec3 = new Vec2(vec2.x, vec2.y * -1f);
                float num2 = (float)Math.Atan(vec3.y / vec3.x);
                velocity = oper.velocity;


                float width = barrel.x;
                float height = barrel.y;


                float ang = 0;

                if (!lockInAnimation)
                {
                    if (!shield)
                    {
                        ang = angle;
                        ang += Maths.DegToRad(90);
                    }
                    else
                    {
                        angle *= oper.ADS;
                    }
                }

                float pX = width * (float)Math.Sin(ang) + height * (float)Math.Cos(ang) * offDir;
                float pY = width * (float)Math.Cos(ang) - height * (float)Math.Sin(ang) * offDir;

                barrelOffsetPos = new Vec2(position.x + pX * offDir, position.y - pY * offDir);

                //Can't fire is made for shields, so you can rotate them or at least move mouse (I believe)
                if ((oper.controller && oper.genericController != null) || !oper.controller)
                {

                }

                if (oper.controller && oper.genericController != null)
                {
                    oper.aim = oper.position + recoil + new Vec2(80 * offDir, 0);
                    if ((Math.Abs(oper.duckOwner.inputProfile.rightStick.x) > 0.2f) || (Math.Abs(oper.duckOwner.inputProfile.rightStick.y) > 0.2f))
                    {
                        oper.aim = oper.position + recoil + new Vec2(oper.duckOwner.inputProfile.rightStick.x, -oper.duckOwner.inputProfile.rightStick.y) * 80f;
                    }
                }
                else
                {
                    oper.aim = Mouse.positionScreen + recoil;
                }

                if (shotFrames > 0)
                {
                    shotFrames -= 0.016666667f;
                }

                if (Keyboard.Released(PlayerStats.keyBindings[13]) || Keyboard.Released(PlayerStats.keyBindingsAlternate[13]) && lockUntilReleaseAction)
                {
                    lockUntilReleaseAction = false;
                }               

                if(framesSinceShot < 20)
                {
                    framesSinceShot++;
                }

                if (canFire)
                {
                    if (oper.duckOwner != null)
                    {
                        if ((oper.genericController != null && oper.controller) || !oper.controller)
                        {
                            float x = 1f;
                            if (muzzle == 3)
                            {
                                x = 0.6f;
                            }

                            recoil *= new Vec2(xStability, yStability) * x;
                            addRecoil *= new Vec2(xStability, yStability) * x;

                            tacticalReload = false;
                            if (oper.local)
                            {
                                if (own != null && oper.duckOwner.inputProfile != null)
                                {

                                    if ((ammo < maxAmmo && magazine > 0) || (ammo < maxAmmo + 1 && canBeTacticallyReloaded && magazine > 0))
                                    {
                                        if (ammo > 0 && canBeTacticallyReloaded)
                                        {
                                            tacticalReload = true;
                                        }
                                        if (ammo <= 0 && magazine > 0 && !reloading)
                                        {
                                            reloading = true;
                                            reload = timeToReload;
                                        }
                                        else
                                        {
                                            if (oper.controller)
                                            {
                                                if (oper.duckOwner.inputProfile.genericController.MapPressed(16384, false) && !reloading)
                                                {
                                                    SFX.Play(Mod.GetPath<R6S>(tacticalReloadSound), 1f);
                                                    reloading = true;
                                                    if (!tacticalReload)
                                                    {
                                                        reload = timeToReload;
                                                    }
                                                    else
                                                    {
                                                        reload = timeToTacticalReload;
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                if ((Keyboard.Pressed(PlayerStats.keyBindings[20]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[20])) && !reloading)
                                                {
                                                    SFX.Play(Mod.GetPath<R6S>(tacticalReloadSound), 1f);
                                                    reloading = true;
                                                    if (!tacticalReload)
                                                    {
                                                        reload = timeToReload;
                                                    }
                                                    else
                                                    {
                                                        reload = timeToTacticalReload;
                                                    }

                                                }
                                            }
                                            if (reloading)
                                            {
                                                Reload(ammo, tacticalReload);
                                            }
                                        }
                                    }
                                    else if ((ammo <= 0 && reloading == false) || (reloading == true && magazine > 0))
                                    {
                                        Reload(ammo, false);
                                    }
                                }

                                if(shotFrames <= 0)
                                {
                                    if (oper.controller)
                                    {
                                        if ((oper.duckOwner.inputProfile.genericController.MapDown(4194304, false) && semiAuto) || (oper.duckOwner.inputProfile.genericController.MapPressed(4194304, false) && !semiAuto))
                                        {
                                            if (own != null && shotFrames <= 0 && ammo > 0 && !reloading)
                                            {
                                                if (oper != null && oper.lockWeaponChange <= 0)
                                                {
                                                    shotFrames += fireRate;
                                                    Fire();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (((Keyboard.Down(PlayerStats.keyBindings[13]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[13])) && semiAuto) 
                                            || ((Keyboard.Pressed(PlayerStats.keyBindings[13]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[13])) && !semiAuto))
                                        {
                                            if (own != null && shotFrames <= 0 && ammo > 0 && !reloading && !lockUntilReleaseAction)
                                            {
                                                if (oper != null && oper.lockWeaponChange <= 0)
                                                {
                                                    shotFrames += fireRate;
                                                    Fire();
                                                }
                                            }
                                            if(own != null && ammo <= 0 && magazine <= 0)
                                            {
                                                if(takenSlot == 1)
                                                {
                                                    oper.ChangeWeapon(20, 2);
                                                }
                                                else
                                                {
                                                    oper.ChangeWeapon(20, 1);
                                                }
                                            }
                                        }
                                    }
                                }

                                if (!lockInAnimation)
                                {
                                    if (!oper.sprinting && oper.delay <= 0 && oper.lockWeaponChange <= 0)
                                    {
                                        angle = 2 * Maths.PI - num2;
                                    }
                                    if((offDir > 0 && oper.aim.x < barrelOffsetPos.x) || (offDir < 0 && oper.aim.x > barrelOffsetPos.x))
                                    {
                                        //angle = (float)Math.PI - num2;
                                    }
                                }

                                if (oper.controller)
                                {
                                    float mod = 1;
                                    mod = (xScope - 1) * oper.ADS + 1;

                                    oper.aim = oper.position + recoil + addRecoil * 0.75f + new Vec2(80 * offDir * mod, 0);
                                    if ((Math.Abs(oper.duckOwner.inputProfile.rightStick.x) > 0.2f) || (Math.Abs(oper.duckOwner.inputProfile.rightStick.y) > 0.2f) && isADS)
                                    {
                                        oper.aim = oper.position + recoil + addRecoil * 0.75f + new Vec2(oper.duckOwner.inputProfile.rightStick.x, -oper.duckOwner.inputProfile.rightStick.y) * 80f * mod;
                                    }
                                }
                                else
                                {
                                    oper.aim = Mouse.positionScreen + recoil * (float)((oper.aim - barrelOffsetPos).length / (Math.Sqrt(Math.Pow(Level.current.camera.size.x, 2) + Math.Pow(Level.current.camera.size.y, 2)) * 0.5f));
                                }
                            }



                            if (ammo > 0 && oper.local)
                            {
                                if (oper.controller)
                                {
                                    if ((Math.Abs(oper.duckOwner.inputProfile.rightStick.x) > 0.1f || Math.Abs(oper.duckOwner.inputProfile.rightStick.y) > 0.1f))
                                    {
                                        isADS = true;
                                    }
                                    if ((Math.Abs(oper.duckOwner.inputProfile.rightStick.x) < 0.1f && Math.Abs(oper.duckOwner.inputProfile.rightStick.y) < 0.1f))
                                    {
                                        isADS = false;
                                    }
                                }
                                else
                                {
                                    if (Mouse.right == InputState.Down && oper.local && (!Keyboard.Down(PlayerStats.keyBindings[19]) && !Keyboard.Down(PlayerStats.keyBindingsAlternate[19])))
                                    {
                                        if (!isADS)
                                        {
                                            Level.Add(new SoundSource(position.x, position.y, 240, "SFX/ADSIn.wav", "J"));
                                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, "SFX/ADSIn.wav", "J"));
                                        }
                                        isADS = true;
                                    }
                                    if (Mouse.right == InputState.None)
                                    {
                                        if (isADS)
                                        {

                                            Level.Add(new SoundSource(position.x, position.y, 240, "SFX/ADSOut.wav", "J"));
                                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, "SFX/ADSOut.wav", "J"));
                                        }
                                        isADS = false;                                        
                                    }
                                }
                            }
                            else
                            {
                                isADS = false;
                            }
                            if (oper.local && (Keyboard.Down(PlayerStats.keyBindings[19]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[19])))
                            {
                                isADS = false;
                            }


                            if (isADS)
                            {
                                oper.xScope = xScope;
                                oper.isADS = true;
                                if (oper._aim != null)
                                {
                                    oper._aim.frame = reticle;
                                }
                                float Sx = 1;
                                if (grip == 2)
                                {
                                    Sx = 3;
                                }
                                if (oper.ADS < (xScope - 1) / 4)
                                {
                                    oper.ADS += 0.005f * Sx * (xScope - 1);
                                }
                                else
                                {
                                    oper.ADS = (xScope - 1) / 4;
                                }
                            }
                            else
                            {
                                float Sx = 1;
                                if (grip == 2)
                                {
                                    Sx = 3;
                                }
                                oper.isADS = false;
                                oper._aim.frame = 1;
                                if (oper.ADS > 0)
                                {
                                    oper.ADS -= 0.005f * Sx * (xScope - 1);
                                }
                                else
                                {
                                    oper.ADS = 0;
                                }
                            }

                            oper.inADS = oper.ADS / ((xScope - 1) / 4);
                        }
                    }
                }
            }
        }

        public virtual void Fire()
        {
            if (canFire && oper != null)
            {
                if (!overwriteDamageDrop)
                {
                    if (weaponClass == "DMR")
                    {
                        damageDropDistance = 0.6f;
                        maxDropDistance = 0.9f;
                        minDamageDrop = 0.6f;
                    }

                    if (weaponClass == "Pistol")
                    {
                        damageDropDistance = 0.35f;
                        maxDropDistance = 0.75f;
                        minDamageDrop = 0.4f;
                    }

                    if (weaponClass == "Shotgun")
                    {
                        damageDropDistance = 0.25f;
                        maxDropDistance = 0.5f;
                        minDamageDrop = 0.4f;
                    }

                    if (weaponClass == "AR")
                    {
                        damageDropDistance = 0.5f;
                        maxDropDistance = 0.8f;
                        minDamageDrop = 0.5f;
                    }

                    if (weaponClass == "SMG")
                    {
                        damageDropDistance = 0.4f;
                        maxDropDistance = 0.7f;
                        minDamageDrop = 0.45f;
                    }

                    if (weaponClass == "LMG")
                    {
                        damageDropDistance = 0.45f;
                        maxDropDistance = 0.85f;
                        minDamageDrop = 0.45f;
                    }

                    if(weaponClass == "Slug")
                    {
                        damageDropDistance = 0.5f;
                        maxDropDistance = 0.95f;
                        minDamageDrop = 0.75f;
                    }
                }

                ammo--;

                float dir = 0f;
                if (oper.offDir > 0)
                {
                    dir = 360 - angleDegrees;
                }
                else
                {
                    dir = 180f - angleDegrees;
                }

                Level.Add(new SoundSource(position.x, position.y, 800, fireSound, "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 800, fireSound, "J"));
                List<Bullet> firedBullets = new List<Bullet>();

                float hsMod = 1.5f;
                if (weaponClass == "DMR")
                {
                    hsMod = 2f;
                }
                if (weaponClass == "Pistol" || weaponClass == "Shotgun")
                {
                    hsMod = 1f;
                }
                if (weaponClass == "SMG")
                {
                    hsMod = 1.25f;
                }
                if (weaponClass == "LMG")
                {
                    hsMod = 1.75f;
                }

                //SFX.Play("plasmaFire", 1f, 1f, 0f, false);
                for (int i = 0; i < bulletsPerShot; i++)
                {
                    AmmoType shrap = new ATSniper();
                    if (highPowered)
                    {
                        shrap = new ATHighCalSniper();
                    }
                    if (isShotgun)
                    {
                        shrap = new ATShotgun();
                    }
                    shrap.accuracy = 1;
                    
                    if(oneHand && !mainHand)
                    {
                        if(oper.holdObject != null)
                        {
                            if(oper.holdObject2 == this && oper.holdObject is GunDev)
                            {
                                if((oper.holdObject as GunDev).shield)
                                {

                                }
                            }
                        }
                    }
                    if (underGrip == 1)
                    {

                    }

                    float damageMod = 1;

                    if (isADS)
                    {
                        if (!isShotgun)
                        {

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (isShotgun)
                        {
                            
                        }
                        else
                        {
                            damageMod *= notADSdamageMultiplier;
                        }
                    }

                    Vec2 pos = new Vec2(barrelOffsetPos.x, barrelOffsetPos.y);

                    if(Level.CheckLine<Block>(oper.position + oper.handPosition, pos) != null || Level.CheckLine<DeployableShieldAP>(oper.position + oper.handPosition, pos) != null)
                    {
                        //return;
                        pos = oper.position + oper.handPosition;
                    }

                    shrap.accuracy = 1;
                    shrap.range = range * xScope;
                    shrap.bulletSpeed = 120f;
                    shrap.bulletThickness = penetration;
                    shrap.penetration = penetration;

                    float minAngle = -_arc * 0.5f;
                    float a = dir;
                    if (!isShotgun)
                    {
                        a += Rando.Float(minAngle, minAngle + (float)_arc); 
                    }
                    else
                    {
                        float step = 1f / bulletsPerShot;
                        a += Rando.Float(minAngle + step * i * (float)_arc,
                            minAngle + step * (i + 1f) * (float)_arc);
                    }                    

                    Bullet bullet = new Bullet(pos.x, pos.y, shrap, a, null, false, -1f, false, true);
                    
                    //TRACER
                    ATTracer tracer = new ATTracer();
                    tracer.range = range * xScope;
                    tracer.penetration = penetration;
                    tracer.accuracy = 1;
                    tracer.speedVariation = 0;

                    Bullet bul = new Bullet(pos.x, pos.y, tracer, a, owner, false, -1f, true, true);

                    framesSinceShot = 0;
                    //Bullet
                    bullet.traced = true;
                    bullet.owner = oper;
                    bullet.ammo.penetration = penetration;

                    float dmg = 1;
                    if (muzzle == 1)
                    {
                        dmg = 0.9f;
                    }
                    dmg *= damageMod;


                    if (oper.local)
                    {
                        Level.Add(bullet);
                        firedBullets.Add(bullet);
                        bool trace = true;
                        if(muzzle == 1)
                        {
                            trace = false;
                            bullet.alpha = 0;
                            bul.alpha = 0;
                            bul.currentTravel = bul.end;
                            bullet.currentTravel = bullet.end;
                        }

                        int invincibiltyFrames = (int)(fireRate / 0.01666666 + 2);

                        if(weaponClass == "Shotgun" || weaponClass == "DMR" || weaponClass == "Pistol" || weaponClass == "Slug")
                        {
                            invincibiltyFrames = 0;
                        }
                        float thicknessMod = 1;
                        if(weaponClass == "Slug")
                        {
                            thicknessMod = 3;
                        }

                        NewBullet nb = new NewBullet(pos.x, pos.y, bul.start, bul.end, bul.end, invincibiltyFrames, 0.2f, trace)
                        { damage = (int)(damage * dmg), thickness = bullet.ammo.penetration * thicknessMod / 2, maxDistance = range,
                            minDamageDrop = minDamageDrop, maxDropDistance = maxDropDistance, damageDropDistance = damageDropDistance, shootedBy = oper, 
                            trackType = trackType, headshotModifier = hsMod };

                        if (nb != null && oper != null)
                        {
                            nb.ignore.Add(oper);
                            if (oper.head != null)
                            {
                                nb.ignore.Add(oper.head);
                            }
                            DuckNetwork.SendToEveryone(new NMFireNewBullet(pos.x, pos.y, bul.start, bul.end, invincibiltyFrames, 0.2f, oper, (int)(damage * dmg), bullet.ammo.penetration / 2, range, trace, damageDropDistance, minDamageDrop, maxDropDistance, hsMod));
                        }
                        

                        Level.Add(nb);
                    }

                    if (Network.isActive)
                    {
                        NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                        Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                        firedBullets.Clear();
                    }
                }

                float hX = 1;
                float vX = 1;
                float X = 1;
                
                if (grip == 1)
                {
                    vX *= 0.7f;
                }


                if(underGrip == 2)
                {
                    hX *= 1.1f;
                    vX *= 0.8f;
                }                

                if(muzzle == 2)
                {
                    if(ammo > maxAmmo - 4)
                    {
                        X *= 0.7f;
                    }
                }

                Vec2 rec = new Vec2(hRecoil * horizontalPattern[ammo] * hX * X, -Rando.Float(Math.Min(Math.Abs(dRecoil), Math.Abs(uRecoil)), Math.Max(Math.Abs(dRecoil), Math.Abs(uRecoil))) * verticalPattern[ammo] * vX * X);

                if (oper.controller)
                {
                    rec *= 1;
                }

                addRecoil += rec * (float)((oper.aim - barrelOffsetPos).length / (Math.Sqrt(Math.Pow(Level.current.camera.size.x, 2) + Math.Pow(Level.current.camera.size.y, 2)) * 0.5f));

                DuckNetwork.SendToEveryone(new NMChangeState("fire", oper));
                //this._sprite.angleDegrees = 0;
            }
        }

        public virtual void Reload(int bullets, bool tacticalRel)
        {
            if (canFire && oper != null && oper.local)
            {
                if ((Keyboard.Pressed(PlayerStats.keyBindings[14]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[14]) ||
                    Keyboard.Pressed(PlayerStats.keyBindings[13]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[13]) ||
                    Keyboard.Pressed(PlayerStats.keyBindings[19]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[19]) ||
                    oper.sprinting || oper.isADS) && ammo > 0)
                {
                    reloading = false;
                    reload = timeToReload;
                }
                if (!manuallyReload)
                {
                    if (ammo <= 0 && reloading == false)
                    {
                        reload = timeToReload;
                        reloading = true;
                        SFX.Play(Mod.GetPath<R6S>(reloadSound), 1f);
                        oper.currentHandAnimation = reloadAnimation;
                    }
                    if (reloading == true)
                    {
                        reload -= 0.01666666f;
                        if (reload <= 0f)
                        {
                            if (tacticalRel)
                            {
                                ammo = maxAmmo + 1;
                                magazine -= (maxAmmo + 1 - bullets);
                                if (magazine < 0)
                                {
                                    ammo = maxAmmo + 1 + magazine;
                                    magazine = 0;
                                }
                            }
                            else if (!tacticalRel)
                            {
                                ammo = maxAmmo;
                                magazine -= (maxAmmo - bullets);
                                if (magazine < 0)
                                {
                                    ammo = maxAmmo + magazine;
                                    magazine = 0;
                                }
                            }

                            reloading = false;
                        }
                    }
                }
                else
                {
                    if (reloading == false)
                    {
                        if (bullets <= 0)
                        {
                            reload = timeToReload;
                        }
                        else
                        {
                            reload = timeToTacticalReload;
                        }
                        reloading = true;
                    }
                    if (reloading == true)
                    {
                        reload -= 0.01666666f;
                        if (reload <= 0f && magazine > 0)
                        {
                            ammo += 1;
                            magazine -= 1;
                            reloading = false;
                            if (ammo < maxAmmo && magazine > 0)
                            {
                                Reload(ammo, true);
                            }
                        }
                    }
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (own != null && canFire && oper != null)
            {
                if (own.profile.localPlayer)
                {
                    /*string text = Convert.ToString(ammo) + " / " + Convert.ToString(magazine);
                    if (game == null)
                    {
                        BitmapFont _font = new BitmapFont("smallFont", 8, -1);
                    }
                    if (!reloading)
                    {
                        Graphics.DrawStringOutline(name, Level.current.camera.position + Level.current.camera.size - new Vec2(oper._aim.scale.x * 80, oper._aim.scale.y * 15), Color.White, Color.Black, 1f, null, Level.current.camera.height / 320);
                    }
                    else
                    {
                        Graphics.DrawStringOutline("Reloading", Level.current.camera.position + Level.current.camera.size - new Vec2(oper._aim.scale.x * 80, oper._aim.scale.y * 15), Color.OrangeRed, Color.Black, 1f, null, Level.current.camera.height / 320);
                    }
                    Graphics.DrawStringOutline(text, Level.current.camera.position + Level.current.camera.size - new Vec2(oper._aim.scale.x * 80, oper._aim.scale.y * 10), Color.White, Color.Black, 1f, null, Level.current.camera.height / 320);
                */
                }
                SpriteMap _fire = new SpriteMap(GetPath("Sprites/Guns/gunFire.png"), 32, 32);
                _fire.alpha = _sprite.alpha;


                float dir = 0f;
                if (offDir > 0)
                {
                    dir = 360 - angleDegrees;
                }
                else
                {
                    dir = 180f - angleDegrees;
                }

                float width = _sprite.width - barrel.x;
                float height = _sprite.height - barrel.y;
                _fire.center = new Vec2(8, 16);
                _fire.angleDegrees = angleDegrees;
                _fire.flipH = offDir != 1;

                SpriteMap _muzzle = new SpriteMap(GetPath("Sprites/GunModifiers.png"), 12, 5);
                SpriteMap _scope = new SpriteMap(GetPath("Sprites/GunModifiers.png"), 12, 5);
                SpriteMap _grip = new SpriteMap(GetPath("Sprites/GunModifiers.png"), 12, 5);
                SpriteMap _undergrip = new SpriteMap(GetPath("Sprites/GunModifiers.png"), 12, 5);


                _muzzle.center = new Vec2(0, 3f);
                if (muzzle == 1)
                {
                    _muzzle.frame = 3;
                    _muzzle.center = new Vec2(2, 3.5f);
                }
                if (muzzle == 2)
                {
                    _muzzle.frame = 4;
                    _muzzle.center = new Vec2(2, 3.5f);
                }
                if (muzzle == 3)
                {
                    _muzzle.frame = 5;
                    _muzzle.center = new Vec2(2, 2.5f);
                }
                if(muzzle != 0)
                {
                    _muzzle.angle = angle;
                    _muzzle.flipH = offDir != 1;
                    Graphics.Draw(_muzzle, barrelOffsetPos.x + oper.velocity.x, barrelOffsetPos.y + oper.velocity.y, depth.value + 0.1f);
                }

                if(xScope == 1.5f)
                {
                    _scope.frame = 0;
                }
                if (xScope == 2)
                {
                    _scope.frame = 1;
                }
                if(xScope == 2.5)
                {
                    _scope.frame = 2;
                }

                if(grip == 1)
                {
                    _grip.frame = 6;
                }
                if(grip == 2)
                {
                    _grip.frame = 7;
                }

                if(underGrip == 1)
                {
                    _undergrip.frame = 8;
                }
                if(underGrip == 2)
                {
                    _undergrip.frame = 9;
                }


                if (framesSinceShot <= 1)
                {
                    _fire.frame = 0;
                }
                if (framesSinceShot > 1 && framesSinceShot < 4)
                {
                    _fire.frame = 1;
                }
                if (framesSinceShot >= 4)
                {
                    _fire.frame = 2;
                }
                if (framesSinceShot <= 6)
                {
                    if(muzzle == 3)
                    {
                        _fire.alpha *= 0.5f;
                        _fire.frame = 3;
                        _fire.center = new Vec2(2, 16);
                    }
                    if(muzzle == 1)
                    {
                        _fire.frame = 3;
                        _fire.center = new Vec2(2, 16);
                    }

                    Graphics.Draw(_fire, barrelOffsetPos.x, barrelOffsetPos.y, depth.value + 0.15f);
                }

                if (oper.local)
                {
                    if (weaponClass == "DMR")
                    {
                        Sprite _sight = new Sprite("laserSightHit");
                        _sight.CenterOrigin();
                        _sight.alpha = 0.4f;


                        //SFX.Play("plasmaFire", 1f, 1f, 0f, false);
                        for (int i = 0; i < bulletsPerShot; i++)
                        {
                            AmmoType shrap = new ATHighCalSniper();

                            Vec2 pos = new Vec2(barrelOffsetPos.x, barrelOffsetPos.y);

                            if (Level.CheckLine<Block>(position, pos) != null || Level.CheckLine<DeployableShieldAP>(position, pos) != null)
                            {
                                //return;
                                pos = position;
                            }
                            //if (Level.CheckLine<Block>(oper.position + oper.handPosition, pos) != null || Level.CheckLine<DeployableShieldAP>(oper.position + oper.handPosition, pos) != null)
                            //{
                            //    //return;
                            //    pos = oper.position + oper.handPosition;
                            //}
                            shrap.range = range * xScope;
                            shrap.bulletSpeed = 120f;
                            shrap.bulletThickness = penetration;
                            shrap.penetration = penetration;
                            
                            Bullet bullet = new Bullet(pos.x, pos.y, shrap, dir, null, false, -1f, true, true);

                            _sight.color = Color.Red;
                            _sight.scale = new Vec2(0.5f, 0.5f);
                            
                            Graphics.Draw(_sight, bullet.end.x, bullet.end.y);
                        }
                    }
                }
            }
        }
    }
}