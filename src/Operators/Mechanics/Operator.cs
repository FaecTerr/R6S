using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class Operators : PhysicsObject, IDrawToDifferentLayers
    {
        public int layerID = -1;

        //health part
        public int stepFrames = 20;
        public int Health = 100;
        public int extraHealth = 0;
        public int prevHealth = 100;
        public int showFrames;
        public bool Healed;
        public float HeadshotDamageResist = 0;
        public bool beenPainShocked;

        //Dead But Not Out part
        public bool revive = false;
        public bool DBNO;
        public int wasDowned;
        public int maxDowns = 1;
        public int undeadFrames;
        protected SpriteMap _dbno;

        public float ReviveTime = 3;
        public float DeathTime = 30;
        public float timeUntilDeath = 30;
        public float timeToRevive = 3;
        private SinWave _pulse = 0.1f;
        public bool isDead;

        //Body
        public OperatorHead head;
        public SpriteMap _sprite;
        public SpriteMap _hand;
        public int Armor = 3;
        public int Speed = 1;
        public Duck duckOwner;
        public SpriteMap _head;
        public OPEQ opeq;
        public LocalOperUI ui;

        public string handAnimation;
        public int handAnimFrames;

        public Vec2 standingSize = new Vec2(8, 22);
        public Vec2 standingOffset = new Vec2(-4, -7);
        public Vec2 crouchSize = new Vec2(11f, 14f);
        public Vec2 crouchOffset = new Vec2(-5.5f, 1f);
        public Vec2 proneSize = new Vec2(20f, 9f);
        public Vec2 proneOffset = new Vec2(-10f, 6f);
        
        public Vec2 standingHeadOffset = new Vec2(0f, -4f);
        public Vec2 crouchingHeadOffset = new Vec2(0f, 3f);
        public Vec2 proneHeadOffset = new Vec2(7f, 7f);

        public string bodyLocation = "Sprites/Operators/jager.png";
        public string headLocation = "Sprites/Operators/jagerHat.png";
        public string handLocation = "Sprites/Operators/SpetsnazHand.png";
        
        //movement
        public bool moveLeft;
        public bool moveRight;
        public bool moveUp;
        public bool moveDown;
        public bool jump;
        public bool crouching;
        public bool repelling;
        public bool upsideDown;
        public bool sprinting;
        public float reppelCount;
        public int slideFrames;

        public int falling;
        public int framesSinceUngrounded;

        public bool canPlant;
        public bool canDropDefuser;
        public bool canPickUpDefuser;
        public bool canDefuse;

        /// <summary>
        /// Team name: Def or Att for defenders and attackers
        /// </summary>
        public string team = "";
        public string name = "";
        public Operators lastDamageFrom;
        public bool killCamAdded;
        public string lastLocation = "";
        public bool identified;

        public float _idleSpeed;
        public int skipFrames;
        public int jumpFrames;
        public bool crouchLock;
        public bool strafe;

        public int changeStateFrames;

        /// <summary>
        /// 'normal' - stand, 'crouch' - crouch, 'slide' - prone, 'injured' - injured, 'repell' - repelled, 'upsidedown' - up side down repell
        /// </summary>
        public string mode = "normal";
        public float _maxSpeed;
        public float _accelerationSpeed;

        public bool silentStep;
        public bool observing;
        public int observFrames;

        //state part
        public int fireFrames;
        public int burnFrames;
        public int elecFrames;
        public int poisonFrames;
        public int concussionFrames;
        public int deafenFrames;
        public int cutFrames;
        public int flashImmuneFrames;
        public int seeTSmokeFrames; 
        public int invisibleForCams;
        public int undetectable;
        public int painShockFrames;

        public int gunFirstIndex;
        public int gunSecondIndex;
        public int DeviceIndex;

        public List<GunDev> Primary = new List<GunDev>(3);
        public List<GunDev> Secondary = new List<GunDev>(3);
        public List<Device> Devices = new List<Device>(3);

        //inventory part
        public bool HasDefuser;
        public List<Device> inventory = new List<Device>(6);
        public int holdIndex;
        public int lastGunIndex;
        public int [] lockedTakeOut = new int[9];
        public int nextItemIndex = -1;
        public int delay;

        public GunDev MainGun;
        public GunDev SecondGun;
        public Device MainDevice;
        public Device SecondDevice;
        public Device Phone;
        public Knife Knife;
        public Drone drone;
        public Defuser d;
        public AntiDefuser ad;

        //public int cantSelectAnyThanMainDevice;

        public Vec2 headPosition;
        public Vec2 handPosition;
        public Vec2 hand2Position;
        public float holdAngle;
        public int lockWeaponChange;

        //Controller binds and e.t.c.
        public bool controller = false;
        public bool openInventory;
        public bool inventorySelect;

        //Needed for some sounds
        public bool female;

        //public Holdable holdObject;

        public bool init;
        public bool immobilized;
        public int unableToJump;
        public int unableToMove;
        public int unableToSprint;
        public int unableToChangeState;
        public int unableToProne;
        public int unableToCrouch;
        public int unableToStand;

        public SpriteMap _aim;
        public Vec2 prevMousePosition;
        public Vec2 aim;

        public bool isADS;
        public float ADS;
        public float xScope = 1;
        public float inADS;

        public int runFrames;

        public Device holdObject2;

        public GamemodeScripter gm;
        public GenericController genericController;

        public bool local;
        public ulong netIndex;
        public int operatorID;

        public PointLight light;

        public List<Effect> effects = new List<Effect>();

        private float headAnimation;
        private float gunAnimation;

        private SinWave _concussion = 0.1f;
        private SinWave _concussionAlt = 0.089f;

        public Color c = Color.White;

        public int bulletImmuneFrames;

        /// <summary>
        /// Than higher priority is taken, than lower priority movements can't be done
        /// </summary>
        public float priorityTaken; 
        //1 - PickUp
        //2 - Take RookArmor
        //2.1 - Controll Tachanka turret
        //3 - PlantDefuser
        //4 - Take out G.U. mine

        public bool spectate;
        public int spectating;
        //public List<Operators> spec;

        public BitmapFont _font;

        public string injureScream = "SFX/Characters/ScreamMute.wav";
        public string deviceTakeoutPhrase = "";
        public string killPhrase = "";
        public string roundStartPhrase = "";

        float headLevelOffset = 6;

        public HandAnimation currentHandAnimation;

        float pingCooldown;

        SpriteMap _cd = new SpriteMap(Mod.GetPath<R6S>("Sprites/whiteDot.png"), 1, 1);
        SpriteMap _widebutton = new SpriteMap(Mod.GetPath<R6S>("Sprites/Keys.png"), 34, 17);
        SpriteMap _button = new SpriteMap(Mod.GetPath<R6S>("Sprites/Keys.png"), 17, 17);
        SpriteMap _statusToxic = new SpriteMap(Mod.GetPath<R6S>("Sprites/Toxicated.png"), 32, 32, false);
        SpriteMap _statusShock = new SpriteMap(Mod.GetPath<R6S>("Sprites/PainShocked.png"), 64, 36, false);
        SpriteMap _statusConcussion = new SpriteMap(Mod.GetPath<R6S>("Sprites/Concussioned.png"), 32, 32, false);
        SpriteMap _status = new SpriteMap(Mod.GetPath<R6S>("Sprites/Statuses.png"), 17, 17, false);

        Sprite _callBG = new Sprite(Mod.GetPath<R6S>("Sprites/DokkaebiCall.png"));
        SpriteMap _dokiBG = new SpriteMap(Mod.GetPath<R6S>("Sprites/DokkaebiICON.png"), 21, 24);

        public Operators(float xpos, float ypos) : base(xpos, ypos)
        {
            _widebutton.CenterOrigin();
            _button.CenterOrigin();
            _status.CenterOrigin();

            _aim = new SpriteMap(Mod.GetPath<R6S>("Sprites/Aim.png"), 17, 17);
            _aim.center = new Vec2(8.5f, 8.5f);
            _aim.frame = 6;
          
            _font = new BitmapFont("biosFont", 8, -1);
            _sprite = new SpriteMap(GetPath(bodyLocation), 32, 32, false);
            _hand = new SpriteMap(GetPath(handLocation), 8, 8, false);
            _head = new SpriteMap(GetPath(headLocation), 32, 32, false);
            _dbno = new SpriteMap(GetPath("Sprites/DBNO.png"), 32, 32, false);
            _dbno.center = new Vec2(16, 16);
            _head.CenterOrigin();
            _hand.CenterOrigin();
            _sprite.depth = 0.4f;
            _head.depth = 0.45f;
            _hand.depth = 0.5f;
            weight = 0f;
            flammable = 0f;
            center = new Vec2(16, 16);
            collisionSize = new Vec2(20, 14f);
            collisionOffset = new Vec2(-10f, -7f);
            SetSprites();

            _sprite.color = new Vec3(0, 0, 0).ToColor();
        }

        public virtual void SetSprites()
        {
            _sprite = new SpriteMap(GetPath(bodyLocation), 32, 32, false);
            _hand = new SpriteMap(GetPath(handLocation), 8, 8, false);
            _head = new SpriteMap(GetPath(headLocation), 32, 32, false);

            _head.CenterOrigin();
            _hand.center = new Vec2(6, 4);
            _sprite.AddAnimation("run", 0.2f, true, new int[] {
                1,
                2,
                3,
                4,
                5,
                6
            });
            _sprite.AddAnimation("idle", 1f, true, new int[] {
                0
            });
            _sprite.AddAnimation("jump", 1f, false, new int[] {
                7, 8
            });
            _sprite.AddAnimation("crouch", 1f, true, new int[] {
                11
            });
            _sprite.AddAnimation("slide", 1f, true, new int[] {
                12
            });
            _sprite.AddAnimation("injured", 0.04f, true, new int[] {
                17,
                18
            });
            _sprite.AddAnimation("planting", 0.2f, true, new int[] {
                13,
                14
            });
            _sprite.SetAnimation("idle");
            graphic = _sprite;
            base.graphic = _sprite;
        }

        public virtual void InitializeInv()
        {
            if (!(Level.current is Editor) && MainGun == null)
            {
                MainGun = Primary[gunFirstIndex];
                SecondGun = Secondary[gunSecondIndex];
                SecondDevice = Devices[DeviceIndex];
            }
            if (!(Level.current is Editor) && MainGun != null)
            {
                if(head == null)
                {
                    head = new OperatorHead(position.x, position.y);
                    head.oper = this;
                    //head._sprite = _head;
                    Level.Add(head);
                }

                inventory.Add(Knife); //0 V
                inventory.Add(MainGun); //1
                inventory.Add(SecondGun); //2
                inventory.Add(MainDevice); //3 MMB
                inventory.Add(SecondDevice); //4 G
                inventory.Add(Phone); //5
                inventory.Add(drone); //6

                if(team == "Att")
                {
                    d = new Defuser(0, 0);
                    inventory.Add(d); //Defuser
                }
                if(team == "Def")
                {
                    ad = new AntiDefuser(0, 0);
                    inventory.Add(ad); //AntiDefuser

                    WoodenBarricade w = new WoodenBarricade(position.x, position.y);

                    inventory.Add(w);
                }
                
                if(MainGun is BallisticShield)
                {
                    if((MainGun as BallisticShield).shieldKnife != null)
                    {
                        Level.Add((MainGun as BallisticShield).shieldKnife);
                        (MainGun as BallisticShield).shieldKnife.team = team;
                    }
                }                

                for (int i = 0; i < inventory.Count; i++)
                {
                    //DevConsole.Log(inventory[i].editorName);
                    inventory[i].team = team;
                    ChangeWeapon(0, i, true);
                    inventory[i].takenSlot = i;
                }
                ChangeWeapon(0, 1);

                foreach (GamemodeScripter game in Level.current.things[typeof(GamemodeScripter)])
                {
                    if (gm == null)
                    {
                        gm = game;
                    }
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            _maxSpeed = (Speed * 0.2f + 1.9f);
            _accelerationSpeed = Speed * 0.1f + 0.6f;

            UpdateLayerID();
        }

        public virtual void UpdateLayerID()
        {
            foreach (MapLayer mapLayer in Level.current.things[typeof(MapLayer)])
            {
                if (position.x > mapLayer.position.x && position.y > mapLayer.position.y &&
                    position.x < mapLayer.position.x + mapLayer.xTiles * 16 && position.y < mapLayer.position.y + mapLayer.yTiles * 16)
                {
                    layerID = mapLayer.LayerID;
                }
            }
        }

        public virtual void GetDamageBullet(int Damage, ulong from = 255)
        {
            if (local && bulletImmuneFrames <= 0)
            {
                float mod = 1f;
                /*if (hitPos.y > 26)
                {
                    mod = 1000;
                }
                if (hitPos.y < 4)
                {
                    mod = 0.5f;
                }*/
                if (HasEffect("SpawnArmor"))
                {
                    mod *= GetEffect("SpawnArmor").timer / GetEffect("SpawnArmor").maxTimer;
                }
                Health -= (int)(Damage * mod * (0.9f - 0.1f * Armor));

                if(from != netIndex && from < 200)
                {
                    DuckNetwork.SendToEveryone(new NMHitScan());
                }
            }
        }

        public virtual void GetDamage(float Damage)
        {
            if (local)
            {
                float mod = 1f;
                if (HasEffect("SpawnArmor"))
                {
                    mod *= GetEffect("SpawnArmor").timer / GetEffect("SpawnArmor").maxTimer;
                }
                Health -= (int)(Damage);
            }
        }

        public virtual void GoStand()
        {
            if (Level.CheckRect<Block>(new Vec2(position.x, bottom) - new Vec2(standingSize.x * 0.5f, standingSize.y), new Vec2(position.x, bottom) + new Vec2(standingSize.x * 0.5f, -1)) == null)
            {
                Level.Add(new SoundSource(position.x, position.y, 200, "SFX/player_change_state.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 200, "SFX/player_change_state.wav", "J"));

                mode = "normal";
                DuckNetwork.SendToEveryone(new NMChangeState("stand", this));
                changeStateFrames = 20;
            }
        }
        public virtual void GoCrouch()
        {
            if (Level.CheckRect<Block>(new Vec2(position.x, bottom) - new Vec2(crouchSize.x * 0.5f, crouchSize.y), new Vec2(position.x, bottom) + new Vec2(crouchSize.x * 0.5f, -1)) == null)
            {
                Level.Add(new SoundSource(position.x, position.y, 200, "SFX/player_change_state.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 200, "SFX/player_change_state.wav", "J"));
                hSpeed = 0;
                mode = "crouch";
                DuckNetwork.SendToEveryone(new NMChangeState("crouch", this));
                //_sprite.frame = 13;
                changeStateFrames = 20;
            }
        }
        public virtual void GoProne()
        {
            if (Level.CheckRect<Block>(new Vec2(position.x, bottom) - new Vec2(proneSize.x * 0.5f, proneSize.y), new Vec2(position.x, bottom) + new Vec2(proneSize.x * 0.5f, -1)) == null)
            {
                Level.Add(new SoundSource(position.x, position.y, 200, "SFX/player_change_state.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 200, "SFX/player_change_state.wav", "J"));
                hSpeed = 0;
                mode = "slide";
                DuckNetwork.SendToEveryone(new NMChangeState("slide", this));
                //_sprite.frame = 12;
                changeStateFrames = 20;
            }
        }
        public virtual void UpdateMode()
        {
            float speed = 0.5f;

            if(changeStateFrames > 0)
            {
                changeStateFrames--;
            }

            if (mode == "normal")
            {
                if (collisionSize.y < crouchSize.y - 0.5f)
                {
                    collisionSize += new Vec2((crouchSize.x - proneSize.x) / (crouchSize.y - proneSize.y), (crouchSize.y - proneSize.y) / (crouchSize.y - proneSize.y)) * speed;
                    collisionOffset += new Vec2((crouchOffset.x - proneOffset.x) / (crouchSize.y - proneSize.y), (crouchOffset.y - proneOffset.y) / (crouchSize.y - proneSize.y)) * speed;
                    hSpeed = 0;
                }
                if (collisionSize.y < standingSize.y)
                {
                    collisionSize += new Vec2((standingSize.x - crouchSize.x) / (standingSize.y - crouchSize.y), (standingSize.y - crouchSize.y) / (standingSize.y - crouchSize.y)) * speed;
                    collisionOffset += new Vec2((standingOffset.x - crouchOffset.x) / (standingSize.y - crouchSize.y), (standingOffset.y - crouchOffset.y) / (standingSize.y - crouchSize.y)) * speed;
                    hSpeed = 0;
                }
                else
                {
                    collisionSize = standingSize;
                    collisionOffset = standingOffset;
                }
                //collisionSize = new Vec2(8f, 22f);
                //collisionOffset = new Vec2(-4f, -7f);
                handPosition = new Vec2(3f * offDir, 7f);
                hand2Position = new Vec2(8f * offDir, 7f);

                //headPosition = (standingHeadOffset - crouchingHeadOffset * (1 - (standingSize.y - collisionSize.y) / (standingSize.y - crouchSize.y))) * new Vec2(offDir, 1);
                headPosition = standingHeadOffset;

                headLevelOffset = 6;
            }
            if (mode == "crouch")
            {
                if (collisionSize.y < crouchSize.y - 0.5f)
                {
                    collisionSize += new Vec2((proneSize.x - crouchSize.x) / (proneSize.y - crouchSize.y), (proneSize.y - crouchSize.y) / (proneSize.y - crouchSize.y)) * speed;
                    collisionOffset += new Vec2((proneOffset.x - crouchOffset.x) / (proneSize.y - crouchSize.y), (proneOffset.y - crouchOffset.y) / (proneSize.y - crouchSize.y)) * speed;
                    hSpeed = 0;
                }
                else if(collisionSize.y > crouchSize.y + 0.5f)
                {
                    collisionSize -= new Vec2((standingSize.x - crouchSize.x) / (standingSize.y - crouchSize.y), (standingSize.y - crouchSize.y) / (standingSize.y - crouchSize.y)) * speed;
                    collisionOffset -= new Vec2((standingOffset.x - crouchOffset.x) / (standingSize.y - crouchSize.y), (standingOffset.y - crouchOffset.y) / (standingSize.y - crouchSize.y)) * speed;
                    hSpeed = 0;
                }
                else
                {
                    collisionSize = crouchSize;
                    collisionOffset = crouchOffset;
                }
                headLevelOffset = 3;
                //collisionSize = new Vec2(10f, 16f);
                //collisionOffset = new Vec2(-5f, -1f);
                //handPosition = new Vec2(3f * offDir, 10f);
                //hand2Position = new Vec2(8f * offDir, 10f);
                //headPosition = new Vec2(0f, 0f);
            }
            if (mode == "slide")
            {
                if (collisionSize.y > crouchSize.y + 0.5f)
                {
                    collisionSize -= new Vec2((standingSize.x - crouchSize.x) / (standingSize.y - crouchSize.y), (standingSize.y - crouchSize.y) / (standingSize.y - crouchSize.y)) * speed;
                    collisionOffset -= new Vec2((standingOffset.x - crouchOffset.x) / (standingSize.y - crouchSize.y), (standingOffset.y - crouchOffset.y) / (standingSize.y - crouchSize.y)) * speed;

                    hSpeed = 0;
                }
                if (collisionSize.y > proneSize.y)
                {
                    collisionSize -= new Vec2((crouchSize.x - proneSize.x) / (crouchSize.y - proneSize.y), (crouchSize.y - proneSize.y) / (crouchSize.y - proneSize.y)) * speed;
                    collisionOffset -= new Vec2((crouchOffset.x - proneOffset.x) / (crouchSize.y - proneSize.y), (crouchOffset.y - proneOffset.y) / (crouchSize.y - proneSize.y)) * speed;

                    hSpeed = 0;
                }
                else
                {
                    collisionSize = proneSize;
                    collisionOffset = proneOffset;
                }
                headLevelOffset = 1;

                //collisionSize = new Vec2(14f, 11f);
                //collisionOffset = new Vec2(-7f, 4f);
                //handPosition = new Vec2(5f * offDir, 14f);
                //hand2Position = new Vec2(10f * offDir, 14f);
                //headPosition = new Vec2(2f * offDir, 6f);
            }
            if (mode == "injured" && !isDead)
            {
                collisionSize = new Vec2(14f, 11f);
                collisionOffset = new Vec2(-7f, 4f);
                handPosition = new Vec2(3f * offDir, 14f);
                hand2Position = new Vec2(12f * offDir, 14f);
                headPosition = new Vec2(-5f * offDir, 6f - (_sprite.frame % 2));
            }
            if (mode == "reppel")
            {
                collisionSize = new Vec2(8f, 22f);
                collisionOffset = new Vec2(-4f, -7f);
                handPosition = new Vec2(3f * offDir, 6f);
                hand2Position = new Vec2(12f * offDir, 6f);
                headPosition = new Vec2(0f, -4f);
            }
            if (mode == "upside")
            {
                collisionSize = new Vec2(8f, 22f);
                collisionOffset = new Vec2(-4f, -7f);
                handPosition = new Vec2(3f * offDir, 4f);
                hand2Position = new Vec2(12f * offDir, 4f);
                headPosition = new Vec2(0f, -4f);
                _head.flipV = true;
                _sprite.flipV = true;
                _hand.flipV = true;
            }
            else
            {
                _head.flipV = false;
                _sprite.flipV = false;
                _hand.flipV = false;
            }


            if (local && !(holdObject is Phone) && changeStateFrames <= 0)
            {
                if (controller && genericController != null)
                {
                    if (repelling && !upsideDown)
                    {
                        mode = "reppel";
                    }
                    else if (repelling && upsideDown)
                    {
                        mode = "upsidedown";
                    }
                    else if (grounded && mode != "slide" && duckOwner.inputProfile.genericController.MapPressed(256, false))
                    {
                        if (unableToProne <= 0)
                        {
                            GoProne();
                        }
                    }
                    else if (grounded && mode != "crouch" && crouching)
                    {
                        if (unableToCrouch <= 0)
                        {
                            GoCrouch();
                        }
                    }
                    else if (grounded && ((mode == "crouch" && crouching) || (mode == "slide" &&  duckOwner.inputProfile.genericController.MapPressed(256, false))))
                    {
                        if (unableToStand <= 0)
                        {
                            GoStand();
                        }
                    }
                }
                else
                {
                    if (repelling && !upsideDown)
                    {
                        mode = "reppel";
                    }
                    else if (repelling && upsideDown)
                    {
                        mode = "upsidedown";
                    }
                    else if (grounded && mode != "slide" && (Keyboard.Pressed(PlayerStats.keyBindings[6]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[6])))
                    {
                        if (unableToProne <= 0)
                        {
                            GoProne();
                        }
                    }
                    else if (grounded && mode != "crouch" && crouching)
                    {
                        if (unableToCrouch <= 0)
                        {
                            GoCrouch();
                        }
                    }
                    else if (grounded && ((mode == "crouch" && crouching) || (mode == "slide" && (Keyboard.Pressed(PlayerStats.keyBindings[6]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[6])))))
                    {
                        if (unableToStand <= 0)
                        {
                            GoStand();
                        }
                    }
                }
            }

            if(unableToChangeState > 0)
            {
                unableToChangeState--;
            }
            if(unableToCrouch > 0)
            {
                unableToCrouch--;
            }
            if (unableToProne > 0)
            {
                unableToProne--;
            }
            if(unableToStand > 0)
            {
                unableToStand--;
            }

            if(DBNO && !isDead)
            {
                mode = "injured";          
            }

            //_sprite.speed = 0.1f;
            //DevConsole.Log(Convert.ToString(_sprite.frame), Color.White);
            if (grounded && _sprite.currentAnimation == "jump" || mode == "normal")
            {
                if (Math.Abs(hSpeed) > 0.1f)
                {
                    //DevConsole.Log("Running", Color.White);
                    if (_sprite.currentAnimation != "run")
                    {
                        _sprite.SetAnimation("run");
                    }
                    /*if (runFrames <= 0)
                    {
                        //DevConsole.Log("Running", Color.White);
                        if (_sprite.frame < 6)
                        {
                            _sprite.frame += 1;
                        }
                        else
                        {
                            _sprite.frame = 1;
                        }
                        runFrames = 10;
                    }
                    else
                        runFrames--;*/
                }
                else if (Math.Abs(hSpeed) <= 0.1f)
                {

                    if (_sprite.currentAnimation != "idle")
                    {
                        _sprite.SetAnimation("idle");
                    }
                    //_sprite.frame = 0;
                }
            }
            
            if (mode == "normal" && Math.Abs(vSpeed) > 0.1f)
            {
                if (_sprite.currentAnimation != "jump")
                {
                    _sprite.SetAnimation("jump");
                }
                /*if (grounded)
                {
                    _sprite.frame = 7;
                }
                else
                {
                    _sprite.frame = 8;
                }*/
            }
            if ((mode == "crouch" || mode == "normal" || mode == "slide") && collisionSize.y < (standingSize.y + crouchSize.y) * 0.5f && collisionSize.y > (proneSize.y + crouchSize.y) * 0.5f)
            {
                if (_sprite.currentAnimation != "crouh")
                {
                    _sprite.SetAnimation("crouch");
                }
                handPosition = new Vec2(3f * offDir, 10f);
                hand2Position = new Vec2(12f * offDir, 10f);
                headPosition = crouchingHeadOffset * new Vec2(offDir, 1);
                /*if(collisionSize.y < crouchSize.y)
                {
                    headPosition = (crouchingHeadOffset - proneHeadOffset * (1 - (crouchSize.y - collisionSize.y) / (crouchSize.y - proneSize.y))) * new Vec2(offDir, 1);
                }
                if (collisionSize.y > crouchSize.y)
                {
                    headPosition = (standingHeadOffset - crouchingHeadOffset  * ((collisionSize.y - crouchSize.y) / (standingSize.y - crouchSize.y))) * new Vec2(offDir, 1);
                }*/
            }
            if ((mode == "crouch" || mode == "normal" || mode == "slide") && collisionSize.y < (proneSize.y + crouchSize.y) * 0.5f)
            {
                if (_sprite.currentAnimation != "slide")
                {
                    _sprite.SetAnimation("slide");
                }
                handPosition = new Vec2(5f * offDir, 14f);
                hand2Position = new Vec2(14f * offDir, 14f);
                headPosition = proneHeadOffset * new Vec2(offDir, 1);
            }
            if (mode == "injured")
            {
                if (_sprite.currentAnimation != "injured")
                {
                    _sprite.SetAnimation("injured");
                }              
            }           
        }

        public virtual void KnifeStab(int prevItemIndex = 1)
        {
            //holdIndex = prevItemIndex;
            _hand.SetAnimation("knifeStab");
            if (Knife != null && !DBNO)
            {
                if (MainGun is BallisticShield && holdIndex == 1)
                {
                    if ((MainGun as BallisticShield).shieldKnife != null)
                    {
                        (MainGun as BallisticShield).shieldKnife.team = team;
                        (MainGun as BallisticShield).shieldKnife.stabTime = 80;
                    }
                    else
                    {
                        Knife.team = team;
                        Knife.stabTime = 80;
                    }
                }
                else
                {
                    Knife.team = team;
                    Knife.stabTime = 80;
                }
            }

            ChangeWeapon(60, 0);
            lockWeaponChange = 60;
        }

        public virtual void BackToWeapon(int time)
        {
            if (inventory[lastGunIndex] != null)
            {
                if (lastGunIndex == 1)
                {
                    if (inventory[1] is GunDev)
                    {
                        if ((inventory[1] as GunDev).magazine + (inventory[1] as GunDev).ammo <= 0)
                        {
                            lastGunIndex = 2;
                        }
                    }
                }
                if (!Level.current.things.Contains(inventory[holdIndex]))
                {
                    Level.Add(inventory[holdIndex]);
                }

                lockWeaponChange = time;
                holdIndex = lastGunIndex;
                holdObject = inventory[holdIndex];
            }
        }

        public virtual void ChangeWeapon(int time, int index, bool fakeUse = false)
        {
            if (index == -1)
            {
                holdIndex = 0;
                holdObject = null;
                holdObject2 = null;
            }
            else if (inventory[index] != null)
            {
                /*if (index == 1)
                {
                    if (inventory[1] is GunDev)
                    {
                        if ((inventory[1] as GunDev).magazine + (inventory[1] as GunDev).ammo <= 0 && (inventory[2] as GunDev).magazine + (inventory[2] as GunDev).ammo > 0)
                        {
                            index = 2;
                        }
                    }
                }
                if (index == 2)
                {
                    if (inventory[2] is GunDev)
                    {
                        if ((inventory[2] as GunDev).magazine + (inventory[2] as GunDev).ammo <= 0 && (inventory[1] as GunDev).magazine + (inventory[1] as GunDev).ammo > 0)
                        {
                            index = 1;
                        }
                    }
                }*/

                if (!Level.current.things.Contains(inventory[index]))
                {
                    Level.Add(inventory[index]);
                }

                
                if (index == 0)
                {
                    if (MainGun is BallisticShield)
                    {
                        if ((MainGun as BallisticShield).shieldKnife != null && holdIndex == 1)
                        {
                            holdObject = (MainGun as BallisticShield).shieldKnife;
                            holdIndex = 0;
                            lockWeaponChange = time;
                            return;
                        }
                    }
                }

                if (inventory[index].requiresTakeOut)
                {
                    ADS = 0;
                    lockWeaponChange = time;
                    holdIndex = index;
                    holdObject = inventory[holdIndex];
                }
                else
                {
                    inventory[index].oper = this;
                    inventory[index].opeq = opeq;
                    inventory[index].own = duckOwner;
                    inventory[index].user = this;
                    if (!fakeUse)
                    {
                        inventory[index].PocketActivation();
                    }
                }
            }
        }
        
        public virtual void Jump(float power = 1)
        {
            vSpeed -= 4f * power;
            unableToJump = 6;
        }

        public virtual void UpdateMovement()
        {
            if (duckOwner != null)
            {
                if (duckOwner.inputProfile != null)
                {
                    //Removing movement from character while in cams
                    if (immobilized || observing)
                    {
                        moveLeft = false;
                        moveRight = false;
                        jump = false;
                        moveUp = false;
                        moveDown = false;
                        crouching = false;
                    }

                    if(unableToMove > 0)
                    {
                        unableToMove--;
                        moveRight = false;
                        moveLeft = false;
                    }
                    if(unableToJump > 0 && grounded)
                    {
                        unableToJump--;
                        jump = false;
                    }
                    if (unableToSprint > 0)
                    {
                        sprinting = false;
                        unableToSprint--;
                    }

                    if(!grounded && vSpeed > 0.1f)
                    {
                        falling += 1;
                    }

                    //High fall
                    if (grounded)
                    {
                        framesSinceUngrounded = -1;
                        if (falling > 23)
                        {
                            Level.Add(new SoundSource(position.x, position.y, 320 * (float)Math.Sqrt(Armor), "SFX/operDrop.wav", "J") {});
                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 320 * (float)Math.Sqrt(Armor), "SFX/operDrop.wav", "J"));

                            unableToMove = 20;
                            unableToJump = 20;
                            unableToSprint = 60;
                                                        
                            SmallSmoke s = SmallSmoke.New(position.x, bottom, 0.8f, 0.55f);
                            s.hSpeed = -2;
                            s.alpha = alpha; //So smoke won't be visible through walls
                            Level.Add(s);

                            s = SmallSmoke.New(position.x, bottom, 0.8f, 0.55f);
                            s.hSpeed = 2;
                            s.alpha = alpha;
                            Level.Add(s);

                             s = SmallSmoke.New(position.x, bottom, 0.8f, 0.4f);
                            s.hSpeed = -1;
                            s.alpha = alpha;
                            Level.Add(s);

                            s = SmallSmoke.New(position.x, bottom, 0.8f, 0.4f);
                            s.hSpeed = 1;
                            s.alpha = alpha;
                            Level.Add(s);
                        }
                        falling = 0;
                    }
                    else
                    {
                        framesSinceUngrounded++;
                    }

                    skipFrames = Maths.CountDown(skipFrames, 1, 0);
                    _skipPlatforms = false;

                    if (skipFrames > 0 && crouching == true)
                    {
                        _skipPlatforms = true;
                    }

                    bool sprintjump = false;

                    //Jumping and making coyote time
                    if (jump && (grounded || framesSinceUngrounded < 10))
                    {
                        float xMod = 0.85f;
                        float yMod = 1;
                        if (mode == "crouch")
                        {
                            yMod = 0.6f;
                        }
                        if (mode == "slide" || mode == "injured")
                        {
                            yMod = 0f;
                        }

                        float speedMaxMod = 1;
                        if(holdObject != null && holdObject is GunDev)
                        {
                            if (isADS)
                            {
                                speedMaxMod *= (holdObject as GunDev).gunADSMobility;
                            }
                            else
                            {
                                speedMaxMod *= (holdObject as GunDev).gunMobility;
                            }
                        }

                        //xMod *= Math.Abs(hSpeed) * speedMaxMod / _maxSpeed;
                        if (sprinting && Math.Abs(hSpeed) > _maxSpeed * 1.05f * speedMaxMod)
                        {
                            xMod *= 1.2f;
                            yMod *= 0.85f;
                            sprintjump = true; 
                            hSpeed += xMod * Math.Sign(hSpeed) * 0.7f;
                        }
                        if (isADS)
                        {
                            xMod *= 0.5f;
                        }
                        
                        Jump(yMod);
                        
                        grounded = false;
                        framesSinceUngrounded = 12;
                    }

                    //Applying animation speed, speed of charctr and sound mod for all stances
                    float modi = 1f;
                    float soundmodi = 1f;
                    
                    if (mode == "normal")
                    {
                        _sprite.speed = 0.45f;
                        modi = 0.9f;
                        soundmodi = 0.7f;
                        if (!grounded)
                        {
                            modi = 0.7f;
                            soundmodi = 0.1f;
                        }
                    }
                    if (mode == "crouch")
                    {
                        _sprite.speed = 0.45f;
                        modi = 0.6f;
                        soundmodi = 0.5f;
                        if (!grounded)
                        {
                            modi = 0.4f;
                            soundmodi = 0.1f;
                        }
                    }
                    if (mode == "slide")
                    {
                        _sprite.speed = 0.45f;
                        modi = 0.3f;
                        soundmodi = 1f;
                        if (!grounded)
                        {
                            modi = 0.2f;
                            soundmodi = 0.5f;
                        }
                    }
                    if (mode == "injured")
                    {
                        _sprite.speed = 0.2f + 0.7f * ((Math.Abs(hSpeed) + 1) / 2);
                        modi = 0.4f;
                        soundmodi = 1f;
                        if (!grounded)
                        {
                            modi = 0.1f;
                        }
                    }

                    //Applying and cancelling sprint in net
                    if (holdObject != null && local && Math.Abs(hSpeed) > _maxSpeed * 0.5f)
                    {
                        if (!(holdObject as Device).lockSprint && mode == "normal" && grounded)
                        {
                            if (controller && genericController != null)
                            {
                                if (duckOwner.inputProfile.genericController.MapDown(512))
                                {
                                    sprinting = true;
                                    DuckNetwork.SendToEveryone(new NMChangeState("sprint", this));
                                }
                                if (duckOwner.inputProfile.genericController.MapReleased(512) || unableToSprint > 0)
                                {
                                    sprinting = false;
                                    DuckNetwork.SendToEveryone(new NMChangeState("stopSprint", this));
                                }
                            }
                            else
                            {
                                if (Keyboard.Down(PlayerStats.keyBindings[19]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[19]))
                                {
                                    sprinting = true;
                                    DuckNetwork.SendToEveryone(new NMChangeState("sprint", this));
                                }
                                if (Keyboard.Released(PlayerStats.keyBindings[19]) || unableToSprint > 0)
                                {
                                    sprinting = false;
                                    DuckNetwork.SendToEveryone(new NMChangeState("stopSprint", this));
                                }
                            }
                        }
                        else
                        {
                            if (sprinting)
                            {
                                DuckNetwork.SendToEveryone(new NMChangeState("stopSprint", this));
                                sprinting = false;
                            }
                        }
                    }
                    else
                    {
                        if (sprinting)
                        {
                            DuckNetwork.SendToEveryone(new NMChangeState("stopSprint", this));
                            sprinting = false;
                        }
                    }

                    if (holdObject != null && local)
                    {
                        if (sprinting)
                        {
                            if (!sprinting)
                            {
                                DuckNetwork.SendToEveryone(new NMChangeState("sprint", this));
                            }
                            sprinting = true;
                            if (holdObject is Device && !(holdObject as Device).lockSprint)
                            {
                                modi = 1.2f;
                                soundmodi = 1.2f;
                                if (holdObject is GunDev)
                                {
                                    GunDev g = holdObject as GunDev;
                                    if (!g.holdback)
                                    {
                                        g.shotFrames = 0.1f;
                                        holdObject.angle = 0.7f * offDir;
                                    }
                                }
                            }

                            if (holdObject is Knife)
                            {
                                modi = 1.2f;
                            }
                            _sprite.speed = 0.6f;
                        }

                        if (holdObject is GunDev)
                        {
                            if (isADS)
                            {
                                modi *= ((holdObject as GunDev).gunADSMobility);
                                soundmodi += ((holdObject as GunDev).gunADSMobility) * 0.1f;
                            }
                            else
                            {
                                modi *= ((holdObject as GunDev).gunMobility);
                                soundmodi += ((holdObject as GunDev).gunMobility) * 0.1f;
                            }
                        }
                    }

                    float minSpeed = 0.15f;
                    if (modi * _accelerationSpeed * 0.5f < minSpeed)
                    {
                        modi = minSpeed / (_accelerationSpeed * 0.5f);
                    }

                    if (silentStep)
                    {
                        soundmodi *= _accelerationSpeed;
                    }

                    //DevConsole.Log("Initialized sprint", Color.White);

                    if (runFrames > 0)
                    {
                        runFrames--;
                    }

                    if (!repelling)
                    {
                        float midair = 0.7f;
                        if (sprintjump)
                        {
                            modi = 1.2f;
                            midair = 1;
                        }

                        if (moveLeft)
                        {
                            if (grounded)
                            {
                                hSpeed -= modi * _accelerationSpeed * 0.5f;

                                if (hSpeed < -_maxSpeed * modi)
                                {
                                    hSpeed = -_maxSpeed * modi;
                                }
                                if (sprinting)
                                {
                                    offDir = -1;
                                }
                                _idleSpeed += 1f;
                            }
                            else
                            {
                                if (hSpeed > -_maxSpeed * modi)
                                {
                                    if (!sprintjump)
                                    {
                                        hSpeed -= modi * _accelerationSpeed * 0.5f * midair;
                                        if(hSpeed < -_maxSpeed * modi)
                                        {
                                            hSpeed = -_maxSpeed * modi;
                                        }
                                    }
                                }
                            }

                        }
                        if (moveRight)
                        {
                            if (grounded)
                            {
                                hSpeed += modi * _accelerationSpeed * 0.5f;
                                if (hSpeed > _maxSpeed * modi)
                                {
                                    hSpeed = _maxSpeed * modi;
                                }
                                if (sprinting)
                                {
                                    offDir = 1;
                                }
                                _idleSpeed += 1f;
                            }
                            else
                            {
                                if (hSpeed < _maxSpeed * modi)
                                {
                                    if (!sprintjump)
                                    {
                                        hSpeed += modi * _accelerationSpeed * 0.5f * midair; 
                                        if (hSpeed > _maxSpeed * modi)
                                        {
                                            hSpeed = _maxSpeed * modi;
                                        }
                                    }
                                }
                            }
                        }

                        //Step sounds
                        if(grounded && Math.Abs(hSpeed) > 0.75f && (mode == "normal" || mode == "crouch"))
                        {
                            if (runFrames <= 0)
                            {
                                runFrames = (int)(30 * (2 - modi));
                                if (local)
                                {
                                    string floorMaterial = "Wood";
                                    float floorMuffling = 1;
                                    if(Level.CheckLine<Block>(new Vec2(position.x, bottom), new Vec2(position.x, bottom) + new Vec2(0, -1)) != null)
                                    {
                                        Block b = Level.CheckLine<Block>(new Vec2(position.x, bottom), new Vec2(position.x, bottom) + new Vec2(0, -1));
                                        if(b.physicsMaterial == PhysicsMaterial.Metal)
                                        {
                                            floorMaterial = "Metal";                                            
                                        }
                                        if (b.physicsMaterial == PhysicsMaterial.Crust)
                                        {
                                            floorMaterial = "Grass";
                                        }
                                        if (b.physicsMaterial == PhysicsMaterial.Plastic)
                                        {
                                            floorMaterial = "Tile";
                                            floorMuffling = 0.8f;
                                        }
                                        if (b.physicsMaterial == PhysicsMaterial.Paper)
                                        {
                                            floorMaterial = "Cardboard";
                                        } 
                                        if(b.physicsMaterial == PhysicsMaterial.Rubber || b is SnowTileset || b is SnowIceTileset)
                                        {
                                            floorMaterial = "Snow";
                                        }
                                    }
                                    else if (Level.CheckLine<Platform>(new Vec2(position.x, bottom), new Vec2(position.x, bottom) + new Vec2(0, -1)) != null)
                                    {
                                        Platform b = Level.CheckLine<Platform>(new Vec2(position.x, bottom), new Vec2(position.x, bottom) + new Vec2(0, -1));
                                        if (b.physicsMaterial == PhysicsMaterial.Metal)
                                        {
                                            floorMaterial = "Metal";
                                        }
                                        if (b.physicsMaterial == PhysicsMaterial.Crust)
                                        {
                                            floorMaterial = "Grass";
                                        }
                                        if (b.physicsMaterial == PhysicsMaterial.Plastic)
                                        {
                                            floorMaterial = "Tile";
                                            floorMuffling = 0.8f;
                                        }
                                        if (b.physicsMaterial == PhysicsMaterial.Paper)
                                        {
                                            floorMaterial = "Cardboard";
                                            floorMuffling = 0.9f;
                                        }
                                        if (b.physicsMaterial == PhysicsMaterial.Rubber)
                                        {
                                            floorMaterial = "Snow";
                                        }
                                    }
                                    else if (Level.CheckLine<Nubber>(new Vec2(position.x, bottom), new Vec2(position.x, bottom) + new Vec2(0, -1)) != null)
                                    {
                                        Nubber b = Level.CheckLine<Nubber>(new Vec2(position.x, bottom), new Vec2(position.x, bottom) + new Vec2(0, -1));
                                        if (b.physicsMaterial == PhysicsMaterial.Metal)
                                        {
                                            floorMaterial = "Metal";
                                        }
                                        if (b.physicsMaterial == PhysicsMaterial.Crust)
                                        {
                                            floorMaterial = "Grass";
                                        }
                                        if (b.physicsMaterial == PhysicsMaterial.Plastic)
                                        {
                                            floorMaterial = "Tile";
                                            floorMuffling = 0.8f;
                                        }
                                        if (b.physicsMaterial == PhysicsMaterial.Paper)
                                        {
                                            floorMaterial = "Cardboard";
                                            floorMuffling = 0.9f;
                                        }
                                        if (b.physicsMaterial == PhysicsMaterial.Rubber)
                                        {
                                            floorMaterial = "Snow";
                                        }
                                    }

                                    Level.Add(new SoundSource(position.x, position.y, 280 * (soundmodi + 0.1f) * (float)Math.Sqrt(Armor), 
                                        "SFX/Steps/Step" + floorMaterial + Convert.ToString(Rando.Int(1, 6) + ".wav"), "J") { ShowRange = false, volumeMod = floorMuffling });
                                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 280 * (soundmodi + 0.1f) * (float)Math.Sqrt(Armor),
                                        "SFX/Steps/Step" + floorMaterial + Convert.ToString(Rando.Int(1, 6) + ".wav"), "J", floorMuffling));
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                    //DevConsole.Log("Initialized repell setting", Color.White);

                    if (!moveLeft && !moveRight)
                    {
                        if (grounded)
                        {
                            _idleSpeed -= 1f;
                            float mod = 0.7f;

                            //Airjab knockoff effect
                            if (slideFrames > 0)
                            {
                                mod = 1;
                            }

                            if (Math.Abs(hSpeed) > 0.01f)
                            {
                                hSpeed *= mod;
                            }
                        }
                        else
                        {
                        }
                    }
                    if (grounded)
                    {
                        friction = 0.1f;
                    }
                    else
                    {
                        friction = 0;
                    }


                    //Plant defuser in plant zone or disabling it
                    
                    //DevConsole.Log("Initialized DBNO", Color.White);

                    //Must be syncronized
                    /*if (repelling)
                    {
                        ReppelLabel r = Level.CheckLine<ReppelLabel>(position + new Vec2(0f, 16f), position + new Vec2(0f, -480f));
                        if (r != null)
                        {
                            if (moveUp && position.y > (r.position.y + 4f))
                            {
                                vSpeed -= 0.1f;
                            }
                            if (moveDown && r.length > (position - r.position).length)
                            {
                                vSpeed += 0.1f;
                            }
                            if (!moveDown && !moveUp)
                            {
                                vSpeed *= 0.88f;
                            }
                            if (position.y < (r.position.y + 4f))
                            {
                                vSpeed += 0.1f;
                            }
                            if (vSpeed > 1f)
                            {
                                vSpeed = 1f;
                            }
                            if (vSpeed < -1f)
                            {
                                vSpeed = -1f;
                            }
                        }
                    }*/

                    //DevConsole.Log("Initialized reppel movement", Color.White);

                    if (_idleSpeed > 1f)
                    {
                        _idleSpeed = 1f;
                    }
                    if (_idleSpeed < 0f)
                    {
                        _idleSpeed = 0f;
                    }

                    //DevConsole.Log("Initialized speed bounds", Color.White);


                    //Must be synced
                    /*
                    if (local)
                    {
                        if ((mode == "normal" || mode == "reppel" || mode == "upsidedown") && Level.CheckLine<ReppelLabel>(position, position + new Vec2(0f, -320f)) != null)
                        {
                            ReppelLabel r = Level.CheckLine<ReppelLabel>(position, position + new Vec2(0f, -480f));
                            if (r != null)
                            {
                                if (Level.CheckLine<Block>(r.position, position) == null && local)
                                {

                                    if (Keyboard.Down(Keys.Space) || duckOwner.inputProfile.genericController.MapDown(4096, false))
                                    {
                                        if (!repelling && Math.Abs(hSpeed) < 0.5f)
                                        {
                                            if (reppelCount < 1 && grounded)
                                            {
                                                reppelCount += 0.02f;
                                                hSpeed = -(position.x - r.position.x + 9 * r.offDir) / 7;
                                            }
                                            else
                                            {
                                                DuckNetwork.SendToEveryone(new NMChangeState("reppel", this));
                                                repelling = true;
                                                reppelCount = 0;
                                                gravMultiplier = 0;
                                            }
                                        }
                                    }
                                    if (Keyboard.Pressed(Keys.Space))
                                    {
                                        if (repelling)
                                        {
                                            hSpeed = 4f * r.offDir;
                                            vSpeed = -5f;
                                            repelling = false;
                                            reppelCount = 0;
                                            gravMultiplier = 1;
                                            mode = "normal";
                                            DuckNetwork.SendToEveryone(new NMChangeState("stand", this));
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            reppelCount = 0;
                            if (mode == "reppel" || mode == "upsidedown")
                            {
                                mode = "normal";
                                gravMultiplier = 1;
                                repelling = false;
                            }
                        }
                    }*/

                    //DevConsole.Log("Initialized unreppel and reppel", Color.White);
                }
            }
        }

        public virtual void UpdateHoldable()
        {
            if (duckOwner != null)
            {
                /*if (local && Level.current.camera != null)
                {
                    Level.current.camera.position = position - (position - Mouse.positionScreen) * 0.01f - new Vec2(Level.current.camera.width / 2, Level.current.camera.height / 2);
                }*/
                if (local && !isDead)
                {
                    Vec2 vec2 = aim - position;
                    if(holdObject is GunDev)
                    {
                        if (!controller)
                        {
                            GunDev g = holdObject as GunDev;
                            vec2 = aim - g.position;
                        }
                        else
                        {
                            //vec2 = aim;
                        }
                    }
                    Vec2 vec3 = new Vec2(vec2.x, vec2.y * -1f);
                    float num2 = (float)Math.Atan((vec3.y / vec3.x));

                    if (holdObject is Phone)
                    {
                        num2 = 0.2f - observFrames * 0.02f;
                        num2 *= offDir;
                    }

                    float ang = (float)Math.PI*2 - num2;


                    if (!(holdObject is Phone))
                    {
                        if (!controller)
                        {
                            if (Mouse.positionScreen.x > position.x && offDir < 0)
                            {
                                offDir = 1;
                            }
                            if (Mouse.positionScreen.x < position.x && offDir > 0)
                            {
                                offDir = -1;
                            }
                        }
                        else
                        {
                            if (duckOwner.inputProfile.rightStick.x >= 0.15)
                            {
                                offDir = 1;
                            }
                            if (duckOwner.inputProfile.rightStick.x < -0.15)
                            {
                                offDir = -1;
                            }
                        }
                    }

                    if (sprinting)
                    {
                        if (hSpeed >= 0)
                        {
                            offDir = 1;
                        }
                        if (hSpeed < 0)
                        {
                            offDir = -1;
                        }
                    }
                    /*if (offDir > 0)
                    {
                        dir = 360 - Maths.RadToDeg(ang);
                    }
                    else
                    {
                        dir = 180f - Maths.RadToDeg(ang);
                    }*/
                    holdAngle = ang;

                    //holdAngle = dir;
                    if (gm != null)
                    {
                        if (gm.currentPhase > 2 && !observing)
                        {
                            Level.current.camera.position = position - new Vec2(Level.current.camera.width, Level.current.camera.height) / 2;
                            if (!observing)
                            {
                                Level.current.camera.position = position - new Vec2(Level.current.camera.width, Level.current.camera.height) / 2;

                                if (holdObject is GunDev)
                                {
                                    GunDev g = holdObject as GunDev;
                                    Vec2 recoil = g.recoil;

                                    if (!isDead)
                                    {
                                        Vec2 diff = (position - aim);
                                       
                                        if (local && isADS)
                                        {
                                            Level.current.camera.position = position + (recoil / 10) - diff * (ADS  + 0.05f) - new Vec2(Level.current.camera.width, Level.current.camera.height) / 2;
                                        }
                                        if (local && !isADS)
                                        {
                                            Level.current.camera.position = position + (recoil / 10) - diff * (ADS) - new Vec2(Level.current.camera.width, Level.current.camera.height) / 2;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!observing)
                        {
                            Level.current.camera.position = position - new Vec2(Level.current.camera.width, Level.current.camera.height) / 2;
                            if (holdObject is GunDev)
                            {
                                GunDev g = holdObject as GunDev;
                                Vec2 recoil = g.recoil;

                                if (!isDead)
                                {
                                    if (local && isADS)
                                    {
                                        Level.current.camera.position = position + (recoil / 10) - (position - aim) * (ADS + 0.05f) - new Vec2(Level.current.camera.width, Level.current.camera.height) / 2;
                                    }
                                    if (local && !isDead)
                                    {
                                        Level.current.camera.position = position + (recoil / 10) - (position - aim) * ADS - new Vec2(Level.current.camera.width, Level.current.camera.height) / 2;
                                    }
                                }
                            }
                        }
                    }
                }
                if (isDead)
                {
                    holdObject = null;
                    holdObject2 = null;
                }

                if (holdObject != null)
                {
                    holdObject.owner = this;
                    holdObject.position = position + handPosition + new Vec2(holdObject._holdOffset.x * offDir, holdObject._holdOffset.y - inADS * headLevelOffset);
                    holdObject.offDir = offDir;
                    holdObject._offDir = _offDir;
                    holdObject.flipHorizontal = offDir == -1;

                    if (holdObject is BallisticShield)
                    {
                        //holdAngle += 1.57f * inADS * offDir;
                        if ((holdObject as BallisticShield).opened)
                        {
                            holdAngle = 3.14f - 1.57f * offDir;
                        }
                    }
                    holdObject.depth = 0.6f;
                    if (!(holdObject is Device) || (holdObject is Device && !(holdObject as Device).dontRotate))
                    {
                        holdObject.angle = holdAngle;
                        if (sprinting)
                        {
                            holdObject.angle = 0.5f * offDir;
                        }
                        if(nextItemIndex >= 0 && delay > 0)
                        {
                            holdObject.angle = (-1.8f + delay * 0.15f) * offDir;
                        }
                        if(lockWeaponChange > 0)
                        {
                            holdObject.angle = (1.8f + lockWeaponChange * 0.15f) * offDir;
                        }
                    }

                    if (local)
                    {
                        holdObject.alpha = alpha;
                    }
                    else
                    {
                        holdObject.alpha = alpha;
                    }
                    if (Mouse.positionScreen != prevMousePosition)
                    {
                        prevMousePosition = Mouse.positionScreen;

                        DuckNetwork.SendToEveryone(new NMHoldAngle(holdAngle, offDir, this, holdObject, holdObject2));
                    }
                    if (holdObject is Device)
                    {
                        Device h = holdObject as Device;
                        if (h.oneHand && h.mainHand && SecondGun != null)
                        {
                            holdObject2 = SecondGun;
                            if (holdObject2 != null)
                            {
                                SecondGun.owner = this;
                                SecondGun.position = position + hand2Position + new Vec2(holdObject._holdOffset.x * offDir, holdObject._holdOffset.y - inADS * headLevelOffset);
                                SecondGun.offDir = offDir;
                                SecondGun._offDir = _offDir;
                                SecondGun.flipHorizontal = offDir == -1;

                                if (!(holdObject2 is Device) || (holdObject2 is Device && !(holdObject2 as Device).dontRotate))
                                {
                                    holdObject2.angle = holdAngle;
                                    if (sprinting)
                                    {
                                        holdObject2.angle = 0.5f * offDir;
                                    }
                                    if (nextItemIndex >= 0 && delay > 0)
                                    {
                                        holdObject2.angle = (-1.8f + delay * 0.15f) * offDir;
                                    }
                                }

                                holdObject2.depth = 0.55f;
                                if (local)
                                {
                                    holdObject2.alpha = alpha;
                                }
                                else
                                {
                                    holdObject2.alpha = alpha;
                                }
                            }
                        }
                        else
                        {
                            holdObject2 = null;
                        }
                    }
                }
            }

            if(MainGun is BallisticShield)
            {
                if((MainGun as BallisticShield).shieldKnife != null)
                {
                    if(holdObject != (MainGun as BallisticShield).shieldKnife)
                    {
                        (MainGun as BallisticShield).shieldKnife.position = new Vec2(99999999f, -99999999f);
                        (MainGun as BallisticShield).shieldKnife.owner = null;
                        (MainGun as BallisticShield).shieldKnife.own = null;
                        (MainGun as BallisticShield).shieldKnife.oper = null;
                        (MainGun as BallisticShield).shieldKnife.opeq = null;
                    }
                }
            }

            foreach (Holdable_plus h in inventory)
            {
                if (h != holdObject && h != holdObject2)
                {
                    h.position = new Vec2(99999999f, -99999999f);
                    h.owner = null;
                    Holdable_plus h_ = h;
                    h_.own = null;
                    h_.oper = null;
                    h_.opeq = null;

                    if (h is BallisticShield && mode != "injured")
                    {
                        bool canUse = true;
                        if(holdObject is Knife)
                        {
                            if(!(holdObject as Knife).defaultKnife)
                            {
                                canUse = false;
                            }
                        }
                        if (canUse)
                        {
                            Vec2 offset = new Vec2(0, 0);
                            if (mode == "normal")
                            {
                                offset = new Vec2(-1 * offDir, 2);
                                h.angle = 0;

                                h.collisionSize = new Vec2(12f, 18f);
                                h.collisionOffset = new Vec2(-6f, -9f);
                            }
                            if (mode == "crouch")
                            {
                                offset = new Vec2(-3 * offDir, 4);
                                h.angle = 0;

                                h.collisionSize = new Vec2(12f, 18f);
                                h.collisionOffset = new Vec2(-6f, -9f);
                            }
                            if (mode == "slide")
                            {
                                offset = new Vec2(-4 * offDir, 6);
                                h.angle = (float)Math.PI * 0.5f * offDir;

                                h.collisionSize = new Vec2(18f, 12f);
                                h.collisionOffset = new Vec2(-9f, -6f);
                            }

                            h.position = position - h._holdOffset * new Vec2(offDir, 0) + offset;
                            h.angle += (float)Math.PI;
                        }
                    }
                }
            }
            //_head.angle = dir/2;

            canPlant = false;
            canDropDefuser = false;
            canPickUpDefuser = false;
            canDefuse = false;

            //Defuser interaction
            if (gm != null)
            {
                int defuserSlot = 7;
                if (local && inventory[defuserSlot] != null && lockedTakeOut[defuserSlot] <= 0)
                {
                    //Planting
                    if (team == "Att" && !DBNO && inventory[defuserSlot] is Defuser && d != null && (mode == "normal" || mode == "crouch"))
                    {
                        if (priorityTaken <= 3.1f && d.UsageCount > 0 && HasDefuser && !gm.planted && Level.CheckRect<PlantZone>(topLeft, bottomRight) != null)
                        {
                            canPlant = true;
                            if (!controller && (Keyboard.Pressed(PlayerStats.keyBindings[5]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[5])))
                            {
                                unableToJump = 3;
                                unableToMove = 3;
                                GoCrouch();
                                priorityTaken = 3;
                                ChangeWeapon(30, defuserSlot);
                                DuckNetwork.SendToEveryone(new NMChangeInventoryItem(30, defuserSlot, this));
                            }
                            if (controller && genericController != null)
                            {
                                if (duckOwner.inputProfile.genericController.MapPressed(2))
                                {
                                    unableToJump = 3;
                                    unableToMove = 3;
                                    GoCrouch();
                                    priorityTaken = 3;
                                    ChangeWeapon(30, defuserSlot);
                                    DuckNetwork.SendToEveryone(new NMChangeInventoryItem(30, defuserSlot, this));
                                }
                            }
                        }
                    }
                    //Defusing
                    if (team == "Def" && !DBNO && inventory[defuserSlot] is AntiDefuser && gm.planted && !gm.defused && ad != null && (mode == "normal" || mode == "crouch"))
                    {
                        canDefuse = false;
                        if (priorityTaken <= 3.1f && Level.CheckCircle<DefuserAP>(position, 40) != null)
                        {
                            if (!controller && (Keyboard.Down(PlayerStats.keyBindings[5]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[5])))
                            {
                                unableToJump = 3;
                                unableToMove = 3;
                                GoCrouch();
                                priorityTaken = 3;
                                ChangeWeapon(30, defuserSlot);
                                DuckNetwork.SendToEveryone(new NMChangeInventoryItem(30, defuserSlot, this));
                            }
                            if (controller && genericController != null)
                            {
                                if (duckOwner.inputProfile.genericController.MapPressed(2))
                                {
                                    unableToJump = 3;
                                    unableToMove = 3;
                                    GoCrouch();
                                    priorityTaken = 3;
                                    ChangeWeapon(30, defuserSlot);
                                    DuckNetwork.SendToEveryone(new NMChangeInventoryItem(30, defuserSlot, this));
                                }
                            }
                        }
                    }
                }
            }

            //Barricading
            if (inventory.Count >= 9)
            {
                if (local && inventory[8] != null && lockedTakeOut[8] <= 0)
                {
                    if (priorityTaken <= 1.1f && team == "Def" && !DBNO && Level.CheckCircle<DoorFrame>(position, 16) != null && mode == "normal")
                    {
                        /*&& Level.CheckRect<DoorFrame>(topLeft + new Vec2(2, 2), bottomRight - new Vec2(2, 2)) == null*/
                        if ((Keyboard.Pressed(PlayerStats.keyBindings[5]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[5])) && !controller)
                        {
                            
                                DoorFrame d = Level.CheckCircle<DoorFrame>(position, 16);
                                if (Level.CheckLine<Block>(position, d.position) == null)
                                {
                                    unableToJump = 3;
                                    unableToMove = 3;
                                    priorityTaken = 1;
                                    ChangeWeapon(30, 8);
                                    DuckNetwork.SendToEveryone(new NMChangeInventoryItem(30, 8, this));
                                }
                            
                            if (holdIndex == 8 && Keyboard.Released(PlayerStats.keyBindings[5]))
                            {
                                ChangeWeapon(30, 1);
                                DuckNetwork.SendToEveryone(new NMChangeInventoryItem(30, 1, this));
                            }
                        }
                        else
                        {
                            if (genericController != null && controller)
                            {
                                if (duckOwner.inputProfile.genericController.MapPressed(2))
                                {
                                    {
                                        DoorFrame d = Level.CheckCircle<DoorFrame>(position, 16);
                                        if (Level.CheckLine<Block>(position, d.position) == null)
                                        {
                                            unableToJump = 3;
                                            unableToMove = 3;
                                            priorityTaken = 1;
                                            ChangeWeapon(30, 8);
                                            DuckNetwork.SendToEveryone(new NMChangeInventoryItem(30, 8, this));
                                        }
                                    }
                                    if (holdIndex == 8 && duckOwner.inputProfile.genericController.MapReleased(2))
                                    {
                                        ChangeWeapon(30, 1);
                                        DuckNetwork.SendToEveryone(new NMChangeInventoryItem(30, 1, this));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (HasDefuser)
            {
                canDropDefuser = true;
            }
            else
            {
                foreach (IDP def in Level.CheckCircleAll<IDP>(position, 24))
                {
                    if (Level.CheckLine<Block>(position, def.position) == null)
                    {
                        canPickUpDefuser = true;
                    }
                }
            }


            if (Keyboard.Pressed(PlayerStats.keyBindings[23]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[23]))
            {
                if (HasDefuser)
                {
                    DropDefuser();
                }
                else
                {
                    PickUpDefuser();
                }
            }
            

            //DevConsole.Log("Initialized decleration", Color.White);

            if (DBNO && !isDead)
            {
                if (local && holdIndex != 0)
                {
                    ChangeWeapon(10, -1);
                    DuckNetwork.SendToEveryone(new NMChangeInventoryItem(10, -1, this));
                }
            }
        }

        public void DropDefuser()
        {
            HasDefuser = false;
            if (isServerForObject)
            {
                Level.Add(new IDP(position.x, position.y + 10));
                DuckNetwork.SendToEveryone(new NMLoserDefuser(this));
            }
        }

        public void PickUpDefuser()
        {
            foreach(IDP def in Level.CheckCircleAll<IDP>(position, 24))
            {
                if(Level.CheckLine<Block>(position, def.position) == null)
                {
                    HasDefuser = true;
                    DuckNetwork.SendToEveryone(new NMPickUpDefuser(position, (byte)netIndex));
                    Level.Remove(def);
                }
            }
        }

        public virtual void UpdateStatusEffects()
        {
            if (bulletImmuneFrames > 0)
            {
                bulletImmuneFrames--;
            }

            if(painShockFrames > 0)
            {
                painShockFrames--;
            }

            if(cutFrames > 0)
            {
                cutFrames--;
                if(cutFrames <= 0)
                {
                    GetDamage(10);
                }
            }

            if (slideFrames > 0)
            {
                inADS = 0;
                isADS = false;
                ADS = 0;
                slideFrames--;
                if (slideFrames == 0)
                {
                    GoProne();
                }
            }

            if(undetectable > 0)
            {
                undetectable--;
            } 

            if(invisibleForCams > 0)
            {
                invisibleForCams--;
            }

            if(flashImmuneFrames > 0)
            {
                flashImmuneFrames--;
            }

            if(seeTSmokeFrames > 0)
            {
                seeTSmokeFrames--;
            }

            //Counters            

            if (stepFrames > 0)
            {
                stepFrames--;
            }

            if (undeadFrames > 0)
            {
                undeadFrames--;
            }

            for (int i = 0; i < 9; i++)
            {
                if (lockedTakeOut[i] > 0)
                {
                    lockedTakeOut[i]--;
                }
            }

            //Poisoned
            if (poisonFrames > 0)
            {
                poisonFrames--;
                if (poisonFrames == 0)
                {
                    if (concussionFrames > 0)
                    {
                        Health -= 18;
                    }
                    else
                    {
                        Health -= 15;
                    }
                }
            }

            //Fired
            if (burnFrames > 0)
            {
                burnFrames--;
                if(unableToSprint > 0)
                {
                    unableToSprint -= 3;
                }
            }
            if (burnFrames <= 0)
            {
                if (Level.CheckRect<LandFire>(topLeft + new Vec2(-8, 0), bottomRight + new Vec2(8, 2)) != null && local)
                {
                    LandFire l = Level.CheckRect<LandFire>(topLeft + new Vec2(-8, 0), bottomRight + new Vec2(8, 2)) as LandFire;
                    if(l.oper != null)
                    {
                        lastDamageFrom = l.oper;
                    }
                    burnFrames = 25;
                    Health -= 12;
                }
            }

            //Electrified
            if (elecFrames > 0)
            {
                elecFrames--;
                hSpeed *= 0.9f;
                unableToSprint = 6;

                if (elecFrames == 0)
                {
                    Health -= 4;
                }

                if(concussionFrames > 0)
                {
                    concussionFrames -= 3;
                }
            }

            if(concussionFrames > 0)
            {
                float mod = 1;
                if(concussionFrames < 120)
                {
                    mod = 0.01666f * concussionFrames * 0.5f;
                }

                concussionFrames--;

                if (!isDead && local)
                {
                    Level.current.camera.position += new Vec2(_concussion * 16f + Level.current.camera.size.x * 0.0f, _concussionAlt * -9f + Level.current.camera.size.y * 0.0f) * mod;
                    Level.current.camera.size += new Vec2(-32, -16) * 2 * mod;
                    Level.current.camera.center += new Vec2(16, 9) * 2 * mod;
                }
            }

            if (!isDead)
            {
                if (Level.CheckRect<SmallFire>(topLeft + new Vec2(-8, -2), bottomRight + new Vec2(8, 0)) != null)
                {
                    fireFrames = 20;
                }
                if (position.y > Level.current.lowestPoint + 499f && duckOwner == null)
                {
                    isDead = true;
                }
            }

            int EMPd = 0;
            foreach(Effect f in effects)
            {
                if(f.name == "EMP'd")
                {
                    EMPd++;
                }
            }
            if(EMPd > 1)
            {
                GetEffect("EMP'd").timer = 0;
            }

            if(deafenFrames > 0)
            {
                deafenFrames--;
            }

            if (HasEffect("Phone called") && HasEffect("Jammed"))
            {
                effects.Remove(GetEffect("Phone called"));
            }
        }

        public virtual void UpdateInteractivities()
        {

        }

        public virtual void UpdateHealth()
        {
            //Extra health
            if (Health > 100)
            {
                extraHealth += Health - 100;
                Health = 100;
            }
            if (extraHealth > 0 && Health < 100)
            {
                extraHealth -= 100 - Health;
                Health = 100;
            }
            if (extraHealth > 0 && Health == 100)
            {
                extraHealth -= 1;
            }
            if (extraHealth > 100)
            {
                extraHealth = 100;
            }

            if(!beenPainShocked && Health < 35 && Health > 0 && !isDead)
            {
                beenPainShocked = true;
                deafenFrames = 240;
                painShockFrames = 240;
            }

            //Death check
            if (duckOwner != null)
            {
                if (Health <= 0 && duckOwner.dead == false && !isDead)
                {
                    Health = 0;
                    if (wasDowned < maxDowns)
                    {
                        if (lastDamageFrom != null)
                        {
                            if (lastDamageFrom.local && lastDamageFrom.team != team)
                            {
                                PlayerStats.renown += 60;
                                PlayerStats.Save();
                                Level.Add(new RenownGained() { description = "Enemy injured", amount = 60 });
                            }
                        }
                        Injure();
                    }
                    else
                    {
                        Kill();
                    }
                }
            }


            if (isDead)
            {
                if (!killCamAdded && local)
                {
                    killCamAdded = true;
                    if (lastDamageFrom != null)
                    {
                        if (lastDamageFrom.duckOwner != null)
                        {
                            Level.Add(new KillCam(position.x, position.y) { killedBy = lastDamageFrom.duckOwner.profile.name, killerHealth = lastDamageFrom.Health, oper = lastDamageFrom });
                        }
                    }
                    else
                    {
                        Level.Add(new KillCam(position.x, position.y) { killedBy = name, killerHealth = Health, oper = this });
                    }
                }
                position = new Vec2(-99999999f, -99999999f);
            }
            
            if (Health < 0)
            {
                Health = 0;
                if (wasDowned < maxDowns)
                {
                    Injure();
                }
            }

            //Dead, but not out
            if (DBNO == true && Health > 0)
            {
                //Checking if team could be revived
                bool canRevive = false;
                foreach(Operators op in Level.current.things[typeof(Operators)])
                {
                    if(op.team == team && (!op.DBNO || op.inventory[3] is Stimulator || op.inventory[3] is BuffFinka) && !op.isDead)
                    {
                        canRevive = true;
                    }
                }
                //_sprite.frame = 12;
                crouch = true;
                sliding = true;
                hSpeed *= 0.5f;
                //itemIndex = 0;
                vSpeed = vSpeed < 0 ? vSpeed * 0.4f : vSpeed;

                
                //Bleeding

                if (timeUntilDeath <= 0)
                {
                    Health = 0;
                    if(lastDamageFrom != null)
                    {
                        if (lastDamageFrom.local && lastDamageFrom.team != team)
                        {
                            PlayerStats.renown += 60;
                            PlayerStats.Save();
                            Level.Add(new RenownGained() { description = "Enemy finished", amount = 60 });
                        }
                    }
                }
                else if (timeUntilDeath > 0)
                {
                    if(Math.Abs(hSpeed) > 0.06f)
                    {
                        timeUntilDeath -= 0.01666666f;
                    }
                    if (timeToRevive > 2.8f)
                    {
                        timeUntilDeath -= 0.01666666f;
                    }
                }


                //Reviving

                bool reviving = false;
                foreach (Operators h in Level.CheckRectAll<Operators>(topLeft, bottomRight))
                {
                    if (timeToRevive > 0)
                    {
                        if (h.team == team && h.mode == "crouch")
                        {                            
                            timeToRevive -= 0.01666666f;
                            reviving = true;


                            if(timeToRevive <= 0 && h.local)
                            {
                                PlayerStats.renown += 50;
                                PlayerStats.Save();
                                Level.Add(new RenownGained() { description = "Teammate recovered", amount = 50 });
                            }
                        }
                    }
                }

                if (!reviving)
                {
                    timeToRevive = ReviveTime;
                }

                //Self-revive
                if (inventory[3] is Stimulator)
                {
                    if ((inventory[3] as Stimulator).UsageCount > 0)
                    {
                        canRevive = true;
                        if (local)
                        {
                            if (timeUntilDeath < DeathTime * 0.9f)
                            {
                                if (!controller && (Keyboard.Pressed(PlayerStats.keyBindings[9]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[9])))
                                {
                                    (inventory[3] as Stimulator).UsageCount--;
                                    unableToJump = 45;
                                    unableToMove = 30;
                                    resetFromDBNO();
                                    Health = 75;
                                }
                                if(controller && genericController != null)
                                {
                                    //duckOwner.inputProfile.genericController.MapPressed(2);
                                }
                            }
                        }
                    }
                }

                if(inventory[3] is BuffFinka)
                {
                    if ((inventory[3] as BuffFinka).UsageCount > 0)
                    {
                        canRevive = true;
                        if (local)
                        {
                            if (timeUntilDeath < DeathTime * 0.9f)
                            {
                                if (!controller && (Keyboard.Pressed(PlayerStats.keyBindings[9]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[9])))
                                {
                                    (inventory[3] as BuffFinka).UsageCount--;
                                    unableToJump = 45;
                                    unableToMove = 30;
                                    resetFromDBNO();
                                }
                                if (controller && genericController != null)
                                {
                                    //duckOwner.inputProfile.genericController.MapPressed(2);
                                }
                            }
                        }
                    }
                }


                if (!canRevive)
                {
                    Kill();
                }
                if (timeToRevive <= 0)
                {
                    resetFromDBNO();
                }
                if(timeUntilDeath < DeathTime/3)
                {
                    _dbno.frame = 2;
                }
                if(timeToRevive < 3)
                {
                    _dbno.frame = 1;
                    if(timeToRevive < ReviveTime/2)
                    {
                        _dbno.frame = 3;
                    }
                }
            }

            //Draw part
            if (duckOwner != null)
            {
                if (duckOwner.dead)
                {
                    isDead = true;
                }
                else if (position.y > Level.current.lowestPoint + 499f)
                {
                    position = duckOwner.position;
                }
                /*else if (isDead)
                {
                    duckOwner.Kill(new DTImpact(this));
                }*/
            }

            if (isDead)
            {
                position = new Vec2(-9999999f, -9999999f);
                if (local && inventory.Count > 5)
                {
                    Phone p = inventory[5] as Phone;
                    if (Keyboard.Pressed(PlayerStats.keyBindings[11]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[11]))
                    {
                        spectate = !spectate;
                    }

                    //Old swap between cameras
                    if (spectate)
                    {
                        if (p.ConnectedCameras() > p.camIndex)
                        {
                            observing = true;
                            ChangeWeapon(0, 5);
                        }
                        List<ObservationThing> spec = new List<ObservationThing>();

                        foreach (ObservationThing oper in Level.current.things[typeof(ObservationThing)])
                        {
                            if (oper.team == team && !oper.jammed && !oper.broken)
                            {
                                spec.Add(oper);
                            }
                        }
                        if (spec.Count > p.camIndex)
                        {
                            if (Keyboard.Pressed(PlayerStats.keyBindings[15]))
                            {
                                p.MovePrev();
                            }
                            if (Keyboard.Pressed(PlayerStats.keyBindings[16]))
                            {
                                p.MoveNext();
                            }
                            Level.current.camera.position = spec[p.camIndex].position - Level.current.camera.size / 2;
                        }
                    }
                    else
                    {
                        List<Operators> spec = new List<Operators>();

                        foreach (Operators oper in Level.current.things[typeof(Operators)])
                        {
                            if (oper != this && oper.team == team && !oper.isDead)
                            {
                                spec.Add(oper);
                            }
                        }
                        while(spectating >= spec.Count)
                        {
                            spectating--;
                        }
                        if (spec.Count > spectating && spec.Count > 0)
                        {
                            if (Keyboard.Pressed(PlayerStats.keyBindings[17]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[17]))
                            {
                                spectating -= 1;
                                if (spectating < 0)
                                {
                                    spectating = spec.Count - 1;
                                }
                            }
                            if (Keyboard.Pressed(PlayerStats.keyBindings[18]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[18]))
                            {
                                spectating += 1;
                                if (spectating >= spec.Count)
                                {
                                    spectating = 0;
                                }
                            }

                            if(spectating < 0)
                            {
                                spectating = 0;
                            }

                            Level.current.camera.position = spec[spectating].position - Level.current.camera.size / 2;
                        }
                    }
                }
            }

            if (prevHealth != Health && local)
            {
                if (prevHealth < Health)
                {
                    Healed = true;
                }
                else { Healed = false; }
                
                prevHealth = Health;
                showFrames = 45;
                if (local)
                {
                    DuckNetwork.SendToEveryone(new NMUpdateHealth(netIndex, Health));
                }
            }
            //DuckNetwork.SendToEveryone(new NMUpdateHealth(this, Health));
        }
        
        public virtual void UpdateSkins()
        {
            if (operatorID >= 0)
            {
                foreach (SkinElement skin in SkinsCollectionManager.allCollection)
                {
                    string location = "";
                    if (skin.name == PlayerStats.operPreferences[operatorID * 20 + 13])
                    {
                        location = "Sprites/Operators/skins/" + skin.location + ".png";
                    }
                    if (location != "")
                    {
                        _head = new SpriteMap(GetPath(location), 32, 32);
                        _head.CenterOrigin();
                        if (skin.animated)
                        {
                            headAnimation += skin.animationSpeed;
                            if (headAnimation >= 1 * skin.frames)
                            {
                                headAnimation = 0;
                            }

                            _head.frame = (int)(headAnimation);
                        }
                    }
                }
                if(holdObject is GunDev)
                {
                    if (inventory.Count > 3)
                    {
                        int slot = 0;

                        if (holdIndex == 1)
                        {
                            if (inventory[1].editorName == Primary[0].editorName)
                            {
                                slot = 5;
                            }
                            if (Primary.Count >= 2)
                            {
                                if (inventory[1].editorName == Primary[1].editorName)
                                {
                                    slot = 16;
                                }
                            }
                            if (Primary.Count >= 3)
                            {
                                if (inventory[1].editorName == Primary[2].editorName)
                                {
                                    slot = 17;
                                }
                            }
                        }
                        if (holdIndex == 2)
                        {
                            if (inventory[2].editorName == Secondary[0].editorName)
                            {
                                slot = 11;

                            }
                            if (Secondary.Count >= 2)
                            {
                                if (inventory[2].editorName == Secondary[1].editorName)
                                {
                                    slot = 18;
                                }
                            }
                            if (Secondary.Count >= 3)
                            {
                                if (inventory[2].editorName == Secondary[2].editorName)
                                {
                                    slot = 19;
                                }
                            }
                        }

                        if (PlayerStats.openedCustoms.Contains(PlayerStats.operPreferences[operatorID * 20 + slot]))
                        {
                            string location = "";
                            foreach (SkinElement s in SkinsCollectionManager.allCollection)
                            {
                                if (s.name == PlayerStats.operPreferences[operatorID * 20 + slot])
                                {
                                    location = "Sprites/Guns/skins/" + s.location + ".png";
                                }
                                if (location != "" && s.mainThing == holdObject.editorName)
                                {
                                    (holdObject as GunDev).trackType = s.trackType;
                                    (holdObject as GunDev)._sprite = new SpriteMap(GetPath(location), 32, 32);
                                    if(holdObject is RP41 || holdObject is G8A1 || holdObject is OTs11 || holdObject is DP27)
                                    {
                                        (holdObject as GunDev)._sprite = new SpriteMap(GetPath(location), 48, 32);
                                    }
                                    (holdObject as GunDev).graphic = (holdObject as GunDev)._sprite;
                                    (holdObject as GunDev).CorrectSprite();
                                    if (s.animated)
                                    {
                                        gunAnimation += s.animationSpeed;
                                        if (gunAnimation >= s.frames)
                                        {
                                            gunAnimation = 0;
                                        }

                                        (holdObject as GunDev)._sprite.frame = (int)(gunAnimation);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public virtual void GetSomeHelpPlease()
        {
            DuckNetwork.SendToEveryone(new NMSuggestInfo(this));
        }

        public virtual void InfoSuggested()
        {
            if (local && duckOwner != null)
            {
                DuckNetwork.SendToEveryone(new NMSendInfo(this, name, duckOwner.profile.networkIndex, duckOwner));
            }
        }

        public override void Update()
        {
            //Checking if it's local
            if(opeq == null || duckOwner == null || (duckOwner != null && !duckOwner.profile.localPlayer))
            {
                local = false;
            }
            //Requesting basic info
            if (!local && isInitialized && opeq == null)
            {
                GetSomeHelpPlease();
            }

            if(holdIndex == 1)
            {
                lastGunIndex = 1;                
            }
            if(holdIndex == 2)
            {
                lastGunIndex = 2;
            }

            if (local)
            {
                if (priorityTaken > 0)
                {
                    priorityTaken -= 0.01f;
                }
                if (!(holdObject is GunDev))
                {
                    if (controller)
                    {
                        if (!observing)
                        {
                            if ((Math.Abs(duckOwner.inputProfile.rightStick.x) > 0.2f) || (Math.Abs(duckOwner.inputProfile.rightStick.y) > 0.2f))
                            {
                                float mod = 1;

                                if(holdObject is GunDev)
                                {
                                    mod = ((holdObject as GunDev).xScope - 1) * ADS + 1;
                                }

                                aim = position + new Vec2(duckOwner.inputProfile.rightStick.x, -duckOwner.inputProfile.rightStick.y) * 80f * mod * (Level.current.camera.size / new Vec2(320, 180));
                            }
                            else
                            {
                                aim = position + new Vec2(80f * offDir, 0f);
                            }
                        }
                        else
                        {
                            aim = Level.current.camera.position + Level.current.camera.size/2 + new Vec2(duckOwner.inputProfile.rightStick.x, -duckOwner.inputProfile.rightStick.y) * 80f * (Level.current.camera.size / new Vec2(320, 180));
                        }
                    }
                    else
                    {
                        aim = Mouse.positionScreen;
                    }
                }
            }
            if(duckOwner != null && (name == "" || name == null))
            {
                name = duckOwner.profile.name;
                DuckNetwork.SendToEveryone(new NMName(this, name));
            }
            
            UpdateHoldable();
            UpdateMovement();
            UpdateStatusEffects();
            UpdateInteractivities();
            UpdateHealth();
            UpdateSkins();

            if (nextItemIndex >= 0)
            {
                if (delay > 0)
                {
                    delay--;
                }
                else
                {
                    ChangeWeapon(20, nextItemIndex);
                    nextItemIndex = -1;
                }
            }

            if (operatorID % 2 == 0)
            {
                team = "Def";
            }
            else
            {
                team = "Att";
            }

            if (!init)
            {
                init = true;
                InitializeInv();
            }

            if (!(holdObject is GunDev))
            {
                xScope = 1;
            }

            //Light bulb to demonstrate what player sees and what obstructs his vision
            if (local)
            {
                if(light != null)
                {
                    Level.Remove(light);
                }

                Vec2 lightPos = Level.current.camera.position + Level.current.camera.size/2;
                if(!isDead)
                {
                    lightPos = head.position;
                }

                light = new PointLight(lightPos.x, lightPos.y, Color.White, 320);
                Level.Add(light);
            }
            base.Update();

            UpdateMode();

            if(ui == null && local)
            {
                ui = new LocalOperUI(position.x, position.y);
                ui.oper = this;
                Level.Add(ui);
            }
            if (duckOwner != null)
            {
                if (local)
                {
                    //DevConsole.Log(duckOwner.inputProfile.lastActiveDevice.name, Color.White);

                    if (duckOwner.inputProfile.lastActiveDevice.name == "keyboard")
                    {
                        controller = false;
                    }
                    else
                    {
                        controller = true;
                    }
                    /*if (local && (Keyboard.Pressed(Keys.F)))
                        {
                            HasDefuser = false;
                            d.position = this.position;
                            d = null;
                    }*/
                    if (holdObject is Phone)
                    {
                        observing = true;
                    }
                    else
                    {
                        observing = false;
                    }
                    if (lockWeaponChange > 0)
                    {
                        lockWeaponChange--;
                    }
                    if (lockWeaponChange <= 0 && duckOwner.localDuck && !isDead)
                    {
                        if (!DBNO)
                        {
                            if (controller && genericController != null)
                            {
                                if (duckOwner.inputProfile.genericController.MapPressed(1, false) && holdIndex != 5)
                                {
                                    openInventory = true;
                                }
                                if (!duckOwner.inputProfile.genericController.MapDown(1))
                                {
                                    openInventory = false;
                                }
                                GamepadInput();
                            }
                            else
                            {
                                KeyboardInput();
                            }
                        }
                    }
                }
            }
        }
        public Phone GetPhone()
        {
            if(inventory[5] is Phone)
            {
                return inventory[5] as Phone;
            }
            return null;
        }
        public void GamepadInput()
        {
            if (duckOwner != null && genericController != null)
            {
                if (genericController.MapPressed(128, false) && holdIndex != 0 && holdIndex != 5 && lockedTakeOut[0] <= 0)
                {
                    KnifeStab(holdIndex);
                    holdIndex = 0;
                    DuckNetwork.SendToEveryone(new NMChangeInventoryItem(0, 0, this));
                }
                if (genericController.MapPressed(8192, false) && lockedTakeOut[5] <= 0)
                {
                    if ((inventory[5] as Phone).ConnectedCameras() > 0 || HasEffect("Phone called"))
                    {
                        if (holdIndex != 5)
                        {
                            if ((inventory[5] as Phone).ConnectedCameras() > 0)
                            {
                                while ((inventory[5] as Phone).camIndex >= (inventory[5] as Phone).ConnectedCameras())
                                {
                                    (inventory[5] as Phone).camIndex--;
                                }

                                (inventory[5] as Phone).GetAvailibleCameras();
                                (inventory[5] as Phone).GetCurrentObservable().Connect();
                            }
                            holdIndex = 5;
                            observFrames = 20;
                            ChangeWeapon(50, holdIndex);
                            DuckNetwork.SendToEveryone(new NMChangeInventoryItem(50, 5, this));

                            Level.Add(new SoundSource(position.x, position.y, 320, "SFX/OpenCam.wav", "J"));
                        }
                        else
                        {
                            Level.Add(new SoundSource(position.x, position.y, 160, "SFX/player_selected_weapon.wav", "J"));
                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/player_selected_weapon.wav", "J"));

                            observFrames = 20;

                            (inventory[5] as Phone).GetCurrentObservable().Disconnect();

                            holdIndex = 1;
                            ChangeWeapon(20, holdIndex);
                            DuckNetwork.SendToEveryone(new NMChangeInventoryItem(20, 1, this));
                        }
                    }
                }
                else if (genericController.MapPressed(32768, false) && holdIndex != 6 && holdIndex != 5 && inventory[6].UsageCount > 0 && lockedTakeOut[6] <= 0)
                {
                    Level.Add(new SoundSource(position.x, position.y, 160, "SFX/player_throw.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/player_throw.wav", "J"));

                    if (holdIndex == 5)
                    {
                        observFrames = 20;
                    }
                    holdIndex = 6;
                    ChangeWeapon(30, holdIndex);
                    DuckNetwork.SendToEveryone(new NMChangeInventoryItem(30, 6, this));
                }

                if (openInventory && lockWeaponChange <= 0)
                {
                    if (duckOwner.inputProfile.rightStick.x < -0.75f && holdIndex != 1 && holdIndex != 5 && lockedTakeOut[1] <= 0)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 160, "SFX/player_selected_weapon.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/player_selected_weapon.wav", "J"));

                        if (holdIndex == 5)
                        {
                            observFrames = 20;
                        }
                        holdIndex = 1;
                        ChangeWeapon(20, holdIndex);
                        DuckNetwork.SendToEveryone(new NMChangeInventoryItem(20, 1, this));
                    }
                    else if (duckOwner.inputProfile.rightStick.y > 0.75f && holdIndex != 2 && holdIndex != 5 && lockedTakeOut[2] <= 0)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 160, "SFX/player_selected_weapon.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/player_selected_weapon.wav", "J"));

                        if (holdIndex == 5)
                        {
                            observFrames = 20;
                        }
                        holdIndex = 2;
                        DuckNetwork.SendToEveryone(new NMChangeInventoryItem(20, 2, this));
                        ChangeWeapon(20, holdIndex);
                    }
                    else if (duckOwner.inputProfile.rightStick.y < -0.75f && holdIndex != 4 && inventory[4].UsageCount > 0 && holdIndex != 5 && lockedTakeOut[4] <= 0)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 160, "SFX/player_throw.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/player_throw.wav", "J"));

                        if (holdIndex == 5)
                        {
                            observFrames = 20;
                        }
                        holdIndex = 4;
                        ChangeWeapon(30, holdIndex);
                        DuckNetwork.SendToEveryone(new NMChangeInventoryItem(30, 4, this));
                    }
                    else if (duckOwner.inputProfile.rightStick.x > 0.75f && holdIndex != 3 && inventory[3].UsageCount > 0 && holdIndex != 5 && lockedTakeOut[3] <= 0)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 160, "SFX/player_throw.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/player_throw.wav", "J"));

                        if (holdIndex == 5)
                        {
                            observFrames = 20;
                        }
                        holdIndex = 3;
                        ChangeWeapon(30, holdIndex);
                        DuckNetwork.SendToEveryone(new NMChangeInventoryItem(30, 3, this));
                    }
                }
            }
        }

        public void KeyboardInput()
        {
            if (Keyboard.Pressed(PlayerStats.keyBindings[21]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[21]))
            {
                KnifeStab(holdIndex);
                TakeOutInventoryItem(0);
            }
            if (Keyboard.Pressed(PlayerStats.keyBindings[7]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[12]))
            {
                Level.Add(new SoundSource(position.x, position.y, 160, "SFX/player_selected_weapon.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/player_selected_weapon.wav", "J"));

                TakeOutInventoryItem(1);
            }
            else if ((Keyboard.Pressed(PlayerStats.keyBindings[8]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[12])) && holdIndex != 2 && holdIndex != 5 && lockedTakeOut[2] <= 0)
            {
                Level.Add(new SoundSource(position.x, position.y, 160, "SFX/player_selected_weapon.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/player_selected_weapon.wav", "J"));

                TakeOutInventoryItem(2);
            }
            else if ((Keyboard.Pressed(PlayerStats.keyBindingsAlternate[9]) || Keyboard.Pressed(PlayerStats.keyBindings[9])) && inventory[3].UsageCount > 0)
            {
                Level.Add(new SoundSource(position.x, position.y, 160, "SFX/player_throw.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/player_throw.wav", "J"));

                TakeOutInventoryItem(3);
            }
            else if ((Keyboard.Pressed(PlayerStats.keyBindings[10]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[10])) && inventory[4].UsageCount > 0)
            {
                Level.Add(new SoundSource(position.x, position.y, 160, "SFX/player_throw.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/player_throw.wav", "J"));

                TakeOutInventoryItem(4);
            }
            else if (Keyboard.Pressed(PlayerStats.keyBindings[11]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[11]))
            {
                TakeOutPhone();
            }
            else if ((Keyboard.Pressed(PlayerStats.keyBindings[12]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[12])) && inventory[6].UsageCount > 0)
            {
                Level.Add(new SoundSource(position.x, position.y, 160, "SFX/player_throw.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/player_throw.wav", "J"));

                TakeOutInventoryItem(6);
            }

            if(pingCooldown > 0)
            {
                pingCooldown -= 0.02f;
            }

            if ((Keyboard.Pressed(PlayerStats.keyBindings[25]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[25])) && pingCooldown <= 0)
            {
                pingCooldown = 2f; 

                ATTracer tracer = new ATTracer();
                tracer.range = (headPosition + position - aim).length; 
                tracer.penetration = 0.4f;

                float a = Maths.PointDirection(position + headPosition, aim);

                Vec2 pos = headPosition + position;

                Bullet b = new Bullet(pos.x, pos.y, tracer, a, owner, false, -1f, true, true);

                if (!observing)
                {
                    Level.Add(new Ping(b.end.x, b.end.y) { Team = team, color = Color.Yellow });
                }
                else
                {
                    Level.Add(new Ping(b.end.x, b.end.y) { Team = team, color = Color.Yellow });
                }
            }
        }

        public void TakeOutInventoryItem(int index)
        {
            if(holdIndex != 5 && holdIndex != index && index < inventory.Count && lockedTakeOut[index] <= 0)
            {
                int delayed = inventory[holdIndex].switchOffTime;
                
                //holdIndex = index;
                if (delayed <= 0)
                {
                    ChangeWeapon(20, index);
                    DuckNetwork.SendToEveryone(new NMChangeInventoryItem(20, index, this));
                }
                else
                {
                    DelayTakeOutInventoryItem(index, delayed);
                    if(holdObject is GunDev)
                    {
                        (holdObject as GunDev).shotFrames = 0.01666666f * (delayed + 2);
                    }
                }
            }
        }
        public void DelayTakeOutInventoryItem(int index, int delayed)
        {
            nextItemIndex = index;
            delay = delayed;
        }

        public void TakeOutPhone()
        {
            int phoneSlot = 5;
            if (inventory[phoneSlot] is Phone && lockedTakeOut[phoneSlot] <= 0)
            {
                Phone phone = inventory[phoneSlot] as Phone;
                observFrames = 20;
                if (holdIndex == phoneSlot)
                {
                    Level.Add(new SoundSource(position.x, position.y, 160, "SFX/player_selected_weapon.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/player_selected_weapon.wav", "J"));

                    observFrames = 20;

                    if (phone.ConnectedCameras() > phone.camIndex)
                    {
                        phone.GetCurrentObservable().Disconnect();
                    }

                    BackToWeapon(20);
                    DuckNetwork.SendToEveryone(new NMChangeInventoryItem(20, lastGunIndex, this));
                }
                else
                {
                    if (phone.ConnectedCameras() > 0)
                    {
                        while (phone.camIndex >= phone.ConnectedCameras())
                        {
                            (inventory[phoneSlot] as Phone).camIndex--;
                        }

                        phone.GetAvailibleCameras();
                        if (phone.ConnectedCameras() > phone.camIndex)
                        {
                            phone.GetCurrentObservable().Connect();
                        }
                    }

                    observFrames = 20;
                    ChangeWeapon(50, phoneSlot);
                    DuckNetwork.SendToEveryone(new NMChangeInventoryItem(50, phoneSlot, this));

                    Level.Add(new SoundSource(position.x, position.y, 320, "SFX/OpenCam.wav", "J"));
                }
            }
        }
        
        public virtual void Kill()
        {
            Health = 0;
            isDead = true;
            if (local)
            {
                Level.Add(new DeadBody(position.x, position.y) { _sprite = _sprite, _head = _head, _hand = _hand, offDir = offDir, team = team });

                if (bodyLocation != null && headLocation != null && handLocation != null && team != null)
                {
                    //DuckNetwork.SendToEveryone(new NMSpawnDeadBody(bodyLocation, headLocation, handLocation, team, position, offDir));
                }
            }
        }

        //[NetworkAction]
        public virtual void resetFromDBNO(int _health = 20)
        {
            DBNO = false;
            Health = _health;
            mode = "crouch";
            ChangeWeapon(20, 1);
            if (local)
            {
                DuckNetwork.SendToEveryone(new NMChangeState("resetFrom", this));
                DuckNetwork.SendToEveryone(new NMChangeInventoryItem(20, 1, this));
            }
        }

        public virtual void Injure()
        {
            Level.Add(new SoundSource(position.x, position.y, 320, injureScream, "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, injureScream, "J"));

            if (local)
            {
                DuckNetwork.SendToEveryone(new NMChangeState("injure", this));
            }
            
            wasDowned += 1;
            Health = 20;
            DBNO = true;
            undeadFrames = 60;
            bulletImmuneFrames = 60;
            mode = "injured";
        }

        public virtual void RecievedDamage(Operators op)
        {
            if(op != null && local)
            {
                //DuckNetwork.SendToEveryone(new NMHitScan(op, this, 1));
            }
        }

        public virtual void HitScan()
        {
            if (local)
            {
                Level.Add(new HitScan(position.x, position.y));
            }
        }

        public virtual void DrawBody()
        {
            if (_head != null && _hand != null)
            {
                _head.angle = holdAngle / 2;

                if (holdObject != null)
                {
                    if (holdObject is Phone)
                    {
                        _head.angle = -0.2f + 0.02f * observFrames + (float)Math.PI;
                        _head.angle *= -offDir;
                    }
                    if (holdObject is BallisticShield)
                    {
                        Vec2 mPos = new Vec2(Mouse.positionScreen.x, Mouse.positionScreen.y);
                        Vec2 cPos = Level.current.camera.position + Level.current.camera.size * 0.5f;
                        if (offDir > 0)
                        {
                            _head.angleDegrees = -Maths.PointDirection(Vec2.Zero, (mPos - cPos) * new Vec2(0.5f, 1f)) + 180;
                        }
                        else
                        {
                            _head.angleDegrees = -Maths.PointDirection(Vec2.Zero, (mPos - cPos) * new Vec2(0.5f, 1f));       
                        }                        
                    }
                }

                _head.alpha = alpha;

                if (offDir > 0)
                {
                    _head.scale = new Vec2(1, 1);
                    _head.angle -= (float)Math.PI;
                    //_head.flipH = true;
                    if(sprinting && local && Mouse.positionScreen.x < position.x && holdObject != null && holdObject is BallisticShield)
                    {
                        _head.scale = new Vec2(-1, -1);
                    }
                }
                if (offDir < 0)
                {
                    _head.scale = new Vec2(1, -1); 
                    if (sprinting && local && Mouse.positionScreen.x > position.x && holdObject != null && holdObject is BallisticShield)
                    {
                        _head.scale = new Vec2(-1, 1);
                    }
                    //_head.angle -= (float)Math.PI;
                    //_head.flipH = true;
                }
                //_head.depth = 0.45f;

                _hand.angle = holdAngle;
                _hand.flipH = offDir != 1;

                _hand.alpha = alpha;

                Vec2 hPos = new Vec2(0 * 4 * (float)Math.Sin(holdAngle), 0 * 4 * (float)Math.Cos(holdAngle));
                Vec2 move = new Vec2(0, 0);

                if (holdObject != null)
                {
                    if (holdObject is GunDev)
                    {
                        move = new Vec2(2, 3);

                        if ((holdObject as GunDev).shield)
                        {
                            move = new Vec2(6, -2);
                        }
                    }
                }

                if (handAnimFrames > 0)
                {
                    handAnimFrames--;
                }
                else
                {
                    handAnimation = "Idle";
                }

                if (handAnimation == "Throw") // 20 frames
                {
                    _hand.angle = (-0.5f + 0.02f * (20 - handAnimFrames)) * offDir;
                    hPos.x += 3 * offDir;
                    hPos.y += -4;

                    _hand.frame = 6;
                }
                if (handAnimation == "" || handAnimation == "Idle")
                {
                    _hand.frame = 0;
                }


                Vec2 holdObjOffset = new Vec2();
                Vec2 animationHand1Offset = new Vec2();
                Vec2 animationHand2Offset = new Vec2();
                float hand2Angle = _hand.angle;

                if (currentHandAnimation != null)
                {
                    _hand.angleDegrees = currentHandAnimation.GetHand1Angle();
                    hand2Angle = currentHandAnimation.GetHand2Angle();
                    animationHand1Offset = currentHandAnimation.GetHand1Position();
                    animationHand2Offset = currentHandAnimation.GetHand2Position();
                    currentHandAnimation.Update();
                }
                if (holdObject != null)
                {
                    holdObjOffset = new Vec2(holdObject._holdOffset.x * offDir, holdObject._holdOffset.y - inADS * headLevelOffset);
                }
                Graphics.Draw(_hand, position.x + handPosition.x - (1 + move.x) * offDir + hPos.x + holdObjOffset.x, position.y + handPosition.y + hPos.y + move.y + holdObjOffset.y, 1f);

                if (holdObject2 != null)
                {
                    holdObjOffset = new Vec2(holdObject2._holdOffset.x * offDir, holdObject2._holdOffset.y - inADS * headLevelOffset);
                }
                _hand.angle = hand2Angle;
                Graphics.Draw(_hand, handPosition.x - 6 * offDir + holdObjOffset.x, hand2Position.y + 3 + holdObjOffset.y, 1f);

                /*if (repelling)
                {
                    ReppelLabel r = Level.CheckLine<ReppelLabel>(position, position + new Vec2(0, -480));
                    if (r != null)
                    {
                        Graphics.DrawLine(position, r.position + new Vec2(-9 * r.offDir, 0), Color.White, 1.7f, 0.1f);
                        SpriteMap _hook = new SpriteMap(GetPath("Sprites/GrapplingHook.png"), 16, 16);
                        _hook.center = new Vec2(8f, 8f);
                        _hook.scale = new Vec2(0.5f);
                        _hook.angle = 0.5f * r.offDir;
                        Graphics.Draw(_hook, r.position.x - 9 * r.offDir, r.position.y + 1);
                    }
                }*/

                Graphics.Draw(_head, position.x + headPosition.x, position.y + headPosition.y, 0.45f);
            }
        }      

        public override void Draw()
        {
            if (local)
            {
                alpha = 1;
                if(_head != null)
                {
                    _head.alpha = 1;
                }
            }
            base.Draw();
            if (_sprite != null && _dbno != null)
            {
                _sprite.depth = 0.8f;
                //layer = Layer.Game;
                graphic.flipH = flipHorizontal;
                if (_aim != null)
                {
                    _aim.scale = new Vec2(Level.current.camera.height / 192, Level.current.camera.height / 192);
                    if(!isADS && holdObject is GunDev)
                    {
                        GunDev gun = (holdObject as GunDev);
                        double arc = (holdObject as GunDev)._arc * 0.5f;
                        if(arc >= 90)
                        {
                            arc = 89;
                        }
                        if(arc < 0)
                        {
                            arc = 0;
                        }
                        float heightOfCross = (float)Math.Tan(Maths.DegToRad((float)arc));
                        float dist = (aim - gun.barrelOffsetPos).length;
                        float localScaleX = dist * heightOfCross;
                        _aim.scale *= localScaleX / 5.5f;
                        /*Vec2 opAim = position - aim;
                        Graphics.DrawLine(gun.barrelOffsetPos, new Vec2(opAim.x * (float)Math.Cos(arc * 0.5f) - opAim.y * (float)Math.Sin(arc * 0.5f),
                           opAim.y * (float)Math.Cos(arc * 0.5f) + opAim.x * (float)Math.Sin(arc * 0.5f)) + position, Color.White); 
                        Graphics.DrawLine(gun.barrelOffsetPos, new Vec2(opAim.x * (float)Math.Cos(-arc * 0.5f) - opAim.y * (float)Math.Sin(-arc * 0.5f),
                            opAim.y * (float)Math.Cos(-arc * 0.5f) + opAim.x * (float)Math.Sin(-arc * 0.5f)) + position, Color.White);
                        Graphics.DrawLine(gun.barrelOffsetPos, new Vec2(aim.x, aim.y), Color.White);*/
                    }
                }
                if (!local && timeToRevive < ReviveTime)
                {
                    Graphics.DrawLine(position + new Vec2(-8, -6), position + new Vec2(8, -6), Color.Gray * 0.5f * alpha, 2f, 0.1f);
                    Graphics.DrawLine(position + new Vec2(-8, -6), position + new Vec2(-8 + 16 * (Health / 100), -6), Color.Green * 0.5f * alpha, 2f, 0.2f);
                }
                if (!isDead)
                {
                    if (DBNO == true)
                    {
                        _dbno.scale = new Vec2(1f + _pulse * 0.05f, 1f + _pulse * 0.05f);
                        Graphics.Draw(_dbno, position.x, position.y - 16f, 1f);
                    }

                    DrawBody();
                }
                else
                {
                    mode = "injured";
                    DrawBody();
                }
                if (!local)
                {
                    _sprite.color = c;
                }
                else
                {
                    //_sprite.color = new Vec3(255, 255, 255).ToColor();
                }
                if (PlayerStats.shownickname && opeq != null && duckOwner != null)
                {
                    string tag = duckOwner.profile.name;
                    Graphics.DrawStringOutline(tag, position + headPosition + new Vec2(tag.Length * -4 * 0.5f, -12), c * _head.alpha, Color.Black * _head.alpha, 0.2f, null, 0.5f);
                }
            }
        }

        public virtual bool HasEffect(string effect)
        {
            foreach (Effect e in effects)
            {
                if(e.name == effect)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual Effect GetEffect(string name)
        {
            foreach (Effect e in effects)
            {
                if (e.name == name)
                {
                    return e;
                }
            }
            return null;
        }

        public virtual void IntoxicatedInteraction(Effect f)
        {
            if (f != null)
            {
                Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);
                Vec2 pos = Level.current.camera.position;
                Vec2 cameraSize = Level.current.camera.size;

                if (Keyboard.Down(PlayerStats.keyBindings[4]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[4]))
                {
                    f.charge -= 0.02f;
                }

                string text = "Hold   to remove needle";
                Graphics.DrawString(text, pos + new Vec2(cameraSize.x / 320 * 160 - text.Length / 2 * 9, cameraSize.y / 180 * 70), Color.White, 1f, null);

                _widebutton.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);
                _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[4]))
                {
                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[4]);
                    Graphics.Draw(_widebutton, pos.x + (160 - text.Length / 2 * 9 + 44) * Unit.x, pos.y + 73 * Unit.x, 1);
                }
                else
                {
                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[4]);
                    Graphics.Draw(_button, pos.x + (160 - text.Length / 2 * 9 + 44) * Unit.x, pos.y + 73 * Unit.x, 1);
                }

                if (f.charge < 1)
                {
                    Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200 * (1 - f.charge), cameraSize.y / 180 * 120), Color.White, 1f, 1f);
                    Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200, cameraSize.y / 180 * 120), Color.Gray, 2f, 0.98f);
                }


                _statusToxic.scale = new Vec2(Level.current.camera.size.x / 31, Level.current.camera.size.y / 31);
                _statusToxic.alpha = 0.75f + f.timer * 0.1f;

                Graphics.Draw(_status, pos.x + cameraSize.x / 2, pos.y + cameraSize.y / 2);
            }
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer != null)
            {
                if (pLayer == Layer.Foreground)
                {
                    if (concussionFrames > 0 && !observing && local)
                    {
                        Vec2 Pos = Level.current.camera.position;
                        Vec2 Size = Level.current.camera.size;

                        _statusConcussion.CenterOrigin();
                        _statusConcussion.scale = new Vec2(Level.current.camera.size.x / 24, Level.current.camera.size.y / 24);
                        _statusConcussion.alpha = 0.6f;

                        if(concussionFrames < 120)
                        {
                            _statusConcussion.alpha = 0.005f * concussionFrames;
                        }

                        Graphics.Draw(_statusConcussion, Pos.x + Size.x / 2 + _concussion * 32, Pos.y + -_concussionAlt * 16 + Size.y / 2);
                        Graphics.Draw(_statusConcussion, Pos.x + Size.x / 2 - _concussionAlt * 32, Pos.y + _concussion * 16 + Size.y / 2);
                    }

                    if(painShockFrames > 0 && !observing && local)
                    {
                        Vec2 Pos = Level.current.camera.position;
                        Vec2 Size = Level.current.camera.size;


                        _statusShock.CenterOrigin();
                        _statusShock.scale = new Vec2(Level.current.camera.size.x / 63, Level.current.camera.size.y / 35);

                        if (painShockFrames < 40)
                        {
                            _statusShock.alpha = 0.025f * painShockFrames;
                        }
                        _statusShock.alpha *= 0.3f;

                        Graphics.Draw(_statusShock, Pos.x + Size.x * 0.5f, Pos.y + Size.y * 0.5f);
                        Graphics.Draw(_statusShock, Pos.x + Size.x * 0.5f, Pos.y + Size.y * 0.5f);
                    }


                    if (!(Level.current is Editor) && local)
                    {
                        foreach (GamemodeScripter g in Level.current.things[typeof(GamemodeScripter)])
                        {
                            if (g.debugMode && opeq != null)
                            {
                                string text = name + " : " + opeq.name;
                                Graphics.DrawStringOutline(text, position + new Vec2(-4 * text.Length, -8), Color.White, Color.Black, 1f, null, 1f);
                            }
                        }

                        int i = 0;
                        foreach (Effect f in effects)
                        {
                            if (f != null)
                            {
                                Vec2 Pos = Level.current.camera.position;
                                Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);

                                _status.CenterOrigin();
                                _status.scale = new Vec2(Unit.x, Unit.x);

                                f.owner = this;
                                f.Update();

                                if (f.name == "Extra armor")
                                {
                                    _status.frame = 0;
                                    f.team = team;
                                }
                                if (f.name == "Jammed")
                                {
                                    _status.frame = 1;
                                }
                                if (f.name == "EMP'd")
                                {
                                    _status.frame = 2;
                                }
                                if (f.name == "Proximity alarm")
                                {
                                    _status.frame = 3;
                                }
                                if (f.name == "Intoxicated")
                                {
                                    _status.frame = 4;
                                    IntoxicatedInteraction(f);
                                }
                                if (f.name == "Exposed")
                                {
                                    _status.frame = 5;
                                }
                                if (f.name == "Phone called")
                                {
                                    _status.frame = 6;
                                    
                                    if(holdIndex != 5)
                                    {
                                        f.charge = 0;
                                    }
                                    else
                                    {
                                        f.charge += 0.01666666f;
                                    }

                                    Vec2 cameraSize = Level.current.camera.size;
                                    Vec2 pos = Level.current.camera.position;
                                    string text = "Hold   to reset phone";

                                    Graphics.DrawStringOutline(text, pos + new Vec2(cameraSize.x / 320 * 160 - text.Length / 2 * 9, cameraSize.y / 180 * 70), Color.White, Color.Black, 1f, null);

                                    _widebutton.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);
                                    _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                                    if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[15]))
                                    {
                                        _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[11]);
                                        Graphics.Draw(_widebutton, pos.x + (160 - text.Length / 2 * 9 + 42) * Unit.x, pos.y + 73 * Unit.x, 1);
                                    }
                                    else
                                    {
                                        _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[11]);
                                        Graphics.Draw(_button, pos.x + (160 - text.Length / 2 * 9 + 42) * Unit.x, pos.y + 73 * Unit.x, 1);
                                    }

                                    Vec2 camPos = Level.current.camera.position;
                                    Vec2 camSize = Level.current.camera.size;

                                    if (holdIndex == 5)
                                    {
                                        if (!(Keyboard.Down(PlayerStats.keyBindings[11]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[11])))
                                        {
                                            observFrames = 20;
                                            BackToWeapon(30);
                                        }

                                        if (f.charge > 0)
                                        {
                                            Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200 * (f.charge / 5), cameraSize.y / 180 * 120), Color.White, 1f, 1f);
                                            Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200, cameraSize.y / 180 * 120), Color.Gray, 2f, 0.98f);
                                        }

                                        _callBG.CenterOrigin();
                                        _dokiBG.CenterOrigin();

                                        //_callBG.scale *= new Vec2(0.25f, 0.25f);
                                        //_dokiBG.scale *= new Vec2(0.25f, 0.25f);

                                        _dokiBG.frame = (int)((_pulse + 1) / 2);

                                        //Graphics.Draw(_callBG, camPos.x + camSize.x * 0.5f, camPos.y + camSize.y * 0.5f, 1.02f);
                                        Graphics.Draw(_dokiBG, camPos.x + camSize.x * 0.5f, camPos.y + camSize.y * (0.505f + _pulse * 0.02f), 0.9999f);
                                    }
                                }
                                if (f.name == "Overhealed")
                                {
                                    _status.frame = 7;
                                }
                                if(f.name == "PingedByAlibi")
                                {
                                    _status.frame = 8;
                                }
                                if(f.name == "SpawnArmor")
                                {
                                    _status.frame = 9;
                                    f.team = team;
                                }
                                if (f.name == "ArmorCooldown")
                                {
                                    _status.frame = 9;
                                }


                                if (f.team == team)
                                {
                                    _status.color = Color.Blue;
                                }
                                else
                                {
                                    _status.color = Color.DarkRed;
                                }


                                if (local)
                                {
                                    _status.depth = 0.5f;

                                    Graphics.Draw(_status, Pos.x + 128 * Unit.x - 20 * Unit.x * i, Pos.y + 168 * Unit.y, 1.2f);

                                    _status.frame = 35;

                                    Graphics.Draw(_status, Pos.x + 128 * Unit.x - 20 * Unit.x * i, Pos.y + 168 * Unit.y, 1.2f);
                                }

                                if (f.type > 0)
                                {
                                    f.timer -= 0.01666666f;
                                    
                                    if ((f.type == 1 || f.type == 3) && local)
                                    {
                                        _cd.scale = new Vec2(1.6f, 0.6f) * Unit;
                                        _cd.CenterOrigin();
                                        for (int k = 0; k < (f.timer / f.maxTimer) * 200; k++)
                                        {
                                            float numr = 1.8f * k - 90;
                                            float angl = Maths.DegToRad(numr);
                                            _cd.angleDegrees = numr;
                                            _cd.color = _status.color;
                                            Graphics.Draw(_cd, Level.current.camera.position.x + (128f + 0.5f + 8f * (float)Math.Cos(angl)) * Unit.x - i * 20 * Unit.x,
                                                Level.current.camera.position.y + (168f + 0.5f + 8f * (float)Math.Sin(angl)) * Unit.x, 1.196f);

                                            Vec3 clr = _status.color.ToVector3();
                                            _cd.color = new Vec3(clr.x - 20, clr.y - 20, clr.z - 20).ToColor();

                                            Graphics.Draw(_cd, Level.current.camera.position.x + (128f + 0.5f + 7f * (float)Math.Cos(angl)) * Unit.x - i * 20 * Unit.x,
                                                Level.current.camera.position.y + (168f + 0.5f + 7f * (float)Math.Sin(angl)) * Unit.x, 1.195f);
                                        }
                                    }
                                }

                                if (f.type == 1)
                                {
                                    _status.depth = 0.6f;
                                    _status.frame = 34;
                                    _status.scale += new Vec2(_pulse * 0.3f + 0.4f, _pulse * 0.3f + 0.4f);

                                    //Graphics.Draw(_status, Pos.x + 128 * Unit.x - 20 * Unit.x * i, Pos.y + 168 * Unit.y, 1.2f);
                                }

                                if (f.timer <= 0)
                                {
                                    f.TimerOut();
                                    if (f.removeOnEnd)
                                    {
                                        effects.Remove(f);
                                        return;
                                    }
                                }

                                i++;
                            }
                        }
                    }
                }
            }
        }
    }
}
