using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.R6S
{
    public class Defuser : Placeable
    {
        public bool picked;

        public SoundSource sfx;
        public Defuser(float xval, float yval) : base(xval, yval)
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

            isSecondary = true;
            index = 22;

            CheckRect = new Vec2(8, 2);

            DeviceCost = 100;
            descriptionPoints = "Defuser planted";

            mainDevice = this;

            sfx = new SoundSource(position.x, position.y, 320, "SFX/DefuserPlacing.wav", "J");
        }

        public override void SetAfterPlace()
        {
            afterPlace = new DefuserAP(position.x, position.y);
            base.SetAfterPlace();
        }

        public override void Set()
        {
            if (oper != null)
            {
                DuckNetwork.SendToEveryone(new NMLoserDefuser(oper));
                oper.HasDefuser = false;
                Level.Add(new InfoFeedTab("", oper.name + " planted defuser") { args = new string[1]{ "news" } });
                //Level.Add(new InfoFeedTab(oper.name, 2));
            }
            base.Set();
        }

        public override void Update()
        {
            PlantZone pz = Level.CheckRect<PlantZone>(topLeft, bottomRight);
            if (pz == null)
            {
                canPlace = false;
            }
            else
            {
                canPlace = true;
            }
            base.Update();
            if(oper != null)
            {
                if (oper.holdObject == this && Keyboard.Released(Keys.F))
                {
                    setting = 0;
                    oper.BackToWeapon(20);
                }
                if(oper.mode != "crouch" || !canPlace || !oper.HasDefuser)
                {
                    setting = 0;
                    oper.BackToWeapon(20);
                }
            }
            if(setting > 0 && setting < 0.017f)
            {
                sfx.position = position;
                if(sfx == null)
                {
                    sfx = new SoundSource(position.x, position.y, 320, "SFX/DefuserPlacing.wav", "JL");
                }
                else
                {
                    sfx.Play();
                }
                Level.Add(sfx);                
            }
            if(setting <= 0)
            {
                if(sfx != null)
                {
                    sfx.Cancel();
                }
            }
        }
    }


    public class DefuserAP : Placeable
    {
        public float timer = 45f;
        public float defuseTime = 7;
        public float defuse = 0f;
        public bool defused = false;
        public bool started;

        public GamemodeScripter g;

        public DefuserAP(float xval, float yval) : base(xval, yval)
        {
            center = new Vec2(8f, 12f);
            weight = 0f;
            collisionOffset = new Vec2(-8f, -5f);
            collisionSize = new Vec2(14f, 8f);
            zeroSpeed = true;
            scannable = true;
            placeable = true;
            _sprite = new SpriteMap(GetPath("Sprites/Defuser"), 16, 16, false);
            base.graphic = _sprite;
            canPick = false;
            canPickUp = false;
            Set();

            mainDevice = this;
            isSecondary = true;
            index = 22;

            DeviceCost = 100;
            descriptionPoints = "Defuser planted";
        }


        public override void Set()
        {
            base.Set();
            foreach (GamemodeScripter gm in Level.current.things[typeof(GamemodeScripter)])
            {
                if (!gm.planted && gm.currentPhase == 4)
                {
                    gm.planted = true;
                    gm.time = timer;
                    g = gm;
                    if(prevOwner != null && prevOwner is Operators)
                    {
                        oper.HasDefuser = false;
                        DuckNetwork.SendToEveryone(new NMLoserDefuser(prevOwner as Operators));
                    }

                    DuckNetwork.SendToEveryone(new NMGamemodeEvent(gm, "plant", timer));
                }
            }
        }

        public override void Update()
        {
            if(g == null)
            {
                foreach (GamemodeScripter gm in Level.current.things[typeof(GamemodeScripter)])
                {
                    if (!gm.planted && gm.currentPhase == 4)
                    {
                        g = gm;
                    }
                }
            }

            canPick = false;

            if (setted)
            {
                _sprite.frame = 0;
                sleeping = false;
            }
            else
            {
                _sprite.frame = 1;
            }
            if (setted == true && !defused && g != null)
            {
                PlantZone pz = Level.CheckRect<PlantZone>(this.topLeft, this.bottomRight);
                if (pz == null)
                {
                    defused = true;
                    SFX.Play(GetPath("bombdefused.wav"), 1f, 0f, 0f, false);
                    defuse = 0f;
                }

                if (defuse > defuseTime)
                {
                    g.defused = true;
                    defused = true;
                    DuckNetwork.SendToEveryone(new NMGamemodeEvent(g, "defuse", timer));

                    defuse = 0;
                    

                    PlayerStats.renown += DeviceCost;
                    PlayerStats.Save();
                    Level.Add(new RenownGained() { description = "Defuser deactivated", amount = DeviceCost });
                }

            }
            base.Update();
        }        
    }
}
