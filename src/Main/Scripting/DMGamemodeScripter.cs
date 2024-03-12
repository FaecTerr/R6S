using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Gamemode")]
    public class DMGamemodeScripter : Thing
    {
        public OPEQ selected;
        public Duck localDuck;
        //public Operators oper;
        public Vec2 camPos; //size = 480
        public int reticle;

        public int defenders;
        public int attackers;

        public int selectedId;

        public Vec2 mousePos;
        public bool showGrid;

        public float Unit = 1;

        public int selectedPr;
        public int selectedSc;
        public int selectedDv;

        public bool endRound;

        public bool debugMode;

        public int[] fpsHistory = new int[100];
        public int checkFrames;
        public DateTime lastCheck;
        public DateTime lastFPS = DateTime.Now;

        public bool winnerDeterminated;

        public bool controller;

        public int GridX;
        public int GridY;
        public Vec2 padMousePosition = new Vec2(160, 90);
        public Vec2 padMousePositionScreen;

        public int reinforcementPool = 3;

        //public EditorProperty<int> team; //Selected by player
        public int team; //Selected by player

        public List<OPEQ> selectedOperators = new List<OPEQ>();

        public int currentPhase;
        public float time;

        public float selectionPhase = 15f;
        public float loadPhase = 5f;
        public float actionPhase = 150f;
        public float winPhase = 5f;
        public float endPhase = 15f;

        public int loaded;
        public int screen = 1;
        public int confirmed;

        //public Vec2 mousePos;

        public bool spawned;

        public bool addSelection = false;

        public SpriteMap _aim;
        public Vec2 aim;

        public SpriteMap _sprite;

        public List<float> phaseTime = new List<float>(); // 0 - Selection, 1 - Loading, 2 - Active, 3 - End

        public string stateOfRound = "Draw";

        public int attKills;
        public int defKills;

        public int[] banned = new int[PlayerStats.totalOperatorsPlanned];

        public EditorProperty<string> mapname;

        public DMGamemodeScripter() : base()
        {
            _sprite = new SpriteMap(GetPath("Sprites/Script.png"), 16, 16);
            _sprite.center = new Vec2(8, 8);
            center = new Vec2(8, 8);
            graphic = _sprite;
            _aim = new SpriteMap(Mod.GetPath<R6S>("Sprites/Aim.png"), 17, 17);
            _aim.center = new Vec2(8.5f, 8.5f);
            _aim.frame = 31;

            collisionSize = new Vec2(14, 14);
            _collisionOffset = new Vec2(-7, -7);

            //team = new EditorProperty<int>(0, this, 0, 1, 1);

            mapname = new EditorProperty<string>("Backrooms");

            layer = Layer.Foreground;
        }

        public override void Initialize()
        {
            base.Initialize();
            phaseTime.Add(selectionPhase);
            phaseTime.Add(loadPhase);
            phaseTime.Add(actionPhase);
            phaseTime.Add(winPhase);
            phaseTime.Add(endPhase);

            for (int i = 0; i < banned.Length; i++)
            {
                banned[i] = -4;
            }

            DevConsole.AddCommand(new CMD("showgrid", delegate ()
            {
                foreach (GamemodeScripter sc in Level.current.things[typeof(GamemodeScripter)])
                {
                    sc.showGrid = !sc.showGrid;
                    if (sc.showGrid)
                    {
                        DevConsole.Log("|GREEN|Show grid applied!");
                    }
                    else
                    {
                        DevConsole.Log("|RED|Show grid was toggled off!");
                    }
                }
            }));
            DevConsole.AddCommand(new CMD("r6debug", delegate ()
            {
                foreach (GamemodeScripter sc in Level.current.things[typeof(GamemodeScripter)])
                {
                    sc.debugMode = !sc.debugMode;
                    if (sc.debugMode)
                    {
                        DevConsole.Log("|GREEN|This round now is in debug mode!");
                    }
                    else
                    {
                        DevConsole.Log("|RED|Debug mode was toggled off!");
                    }
                }
            }));
        }

        public virtual void ClearFromDucks()
        {
            foreach (Duck d in Level.current.things[typeof(Duck)])
            {
                d.quack = 999;
                if (Level.current is DuckGameTestArea)
                {
                    //DevConsole.Log(d.profile.name, Color.White);
                    if (d.profile.name == "Player2" || d.profile.name == "Player3" || d.profile.name == "Player4")
                    {
                        d.Kill(new DTImpale(this));
                    }
                }
                if (d.profile.localPlayer && !(d.profile.name == "Player2" || d.profile.name == "Player3" || d.profile.name == "Player4"))
                {
                    localDuck = d;
                }
                d.grounded = false;
                d.alpha = 0;
                d.position = new Vec2(-9999999f, -9999999f);

                if (d.ragdoll != null)
                {
                    d.ragdoll.position = d.position;
                    if (d.ragdoll.part1 != null)
                    {
                        d.ragdoll.part1.alpha = 0;
                        d.ragdoll.part1.position = d.position;
                    }
                    if (d.ragdoll.part2 != null)
                    {
                        d.ragdoll.part2.alpha = 0;
                        d.ragdoll.part2.position = d.position;
                    }
                    if (d.ragdoll.part3 != null)
                    {
                        d.ragdoll.part3.alpha = 0;
                        d.ragdoll.part3.position = d.position;
                    }

                    d.ragdoll.alpha = 0;
                    d.ragdoll.Unragdoll();
                }

                d.enablePhysics = false;
            }
            foreach (OPEQ o in Level.current.things[typeof(OPEQ)])
            {
                o.position = new Vec2(-9999999f, -9999999f);
            }
            foreach (Window w in Level.current.things[typeof(Window)])
            {
                w._ruined = true;
                w._didImpactSound = true;
                w.Destroy();
            }
        }

        public override void Update()
        {
            ClearFromDucks();

            if (/*(mousePos - Mouse.position).length > 2 * Unit ||*/ Mouse.left == InputState.Pressed || Mouse.middle == InputState.Pressed || Mouse.right == InputState.Pressed)
            {
                if (localDuck != null)
                {
                    localDuck.inputProfile.lastActiveDevice.name = "keyboard";
                }
            }
            if (localDuck != null)
            {
                if (localDuck.profile.inputProfile.genericController != null)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        if (localDuck.profile.inputProfile.genericController.MapPressed((int)Math.Pow(2, i)))
                        {
                            localDuck.inputProfile.lastActiveDevice.name = "";
                        }
                    }
                }

                if (localDuck.inputProfile.lastActiveDevice.name == "keyboard")
                {
                    controller = false;
                }
                else
                {
                    controller = true;
                }
            }

            mousePos = Mouse.position;

            if (localDuck != null)
            {
                padMousePosition += localDuck.inputProfile.genericController.leftStick * new Vec2(1, -1) * 2;

                if (localDuck.inputProfile.lastActiveDevice.name == "keyboard")
                {
                    padMousePosition = Mouse.position;
                }
                else
                {
                    Mouse.position = padMousePosition;
                }


                if (padMousePosition.x < 0)
                {
                    padMousePosition.x = 0;
                }
                if (padMousePosition.y < 0)
                {
                    padMousePosition.y = 0;
                }
                if (padMousePosition.x > 320)
                {
                    padMousePosition.x = 320;
                }
                if (padMousePosition.y > 180)
                {
                    padMousePosition.y = 180;
                }

                padMousePositionScreen = Level.current.camera.position + padMousePosition;
            }

            //Hiding opeq from players
            if (currentPhase > 2)
            {
                foreach (OPEQ d in Level.current.things[typeof(OPEQ)])
                {
                    d.alpha = 0;
                    d._sprite.alpha = 0;
                }
            }

            /*foreach (Bullet bullet in Level.current.things[typeof(Bullet)])
            {
                if(bullet.ammo.penetration > 1)
                {
                    bullet.ammo.penetration = 1;
                }
            }*/

            Level.current.camera.size = new Vec2(320, 180);
            Unit = Level.current.camera.size.x / 320;

            if (selected != null)
            {
                if (selected.oper != null)
                {
                    if (selected.oper.holdIndex == 1 || selected.oper.holdIndex == 2)
                    {
                        if (selected.oper.ADS > 0 && selected.oper.holdObject is GunDev)
                        {
                            GunDev g = selected.oper.holdObject as GunDev;
                            Level.current.camera.size = new Vec2(320, 180) - new Vec2(320, 180) * selected.oper.ADS / (6 - g.xScope);
                            Unit = Level.current.camera.size.x / 320;
                        }
                    }
                    if (selected.oper.holdObject is ElectronicsDetector)
                    {
                        Level.current.camera.size = new Vec2(320, 180) * 1.5f;
                        Unit = Level.current.camera.size.x / 320;
                    }
                }
            }

            if (!(Level.current is Editor))
            {
                _sprite.frame = 1;
            }

            camPos = Level.current.camera.position;

            aim = Mouse.positionScreen;
            //DevConsole.Log("Mouse position:" + Convert.ToString(Mouse.position), Color.White);
            //DevConsole.Log("MScreen position:" + Convert.ToString(Mouse.positionScreen), Color.White);
            base.Update();


            if (time <= 0 && currentPhase > 0 && currentPhase < 4)
            {
                if (currentPhase == 2)
                {
                    spawned = false;
                }
                time = phaseTime[currentPhase];
                currentPhase++;
                if (currentPhase == 1)
                {
                    SFX.Play(GetPath("SFX/Announcer/AnnounceMap.wav"), 1);
                }
                if (currentPhase == 2)
                {
                    SFX.Play(GetPath("SFX/Announcer/AnnounceLoadout.wav"), 1);
                    if (selected == null)
                    {
                        selected = RandomOp();
                        selectedOperators.Add(selected);
                    }
                }
                if (currentPhase == 3)
                {

                }
                if (currentPhase == 4)
                {
                }
            }
            else
            {
                time -= 0.01666666f;
            }

            if (currentPhase == 0)
            {
                SideSelectPhase();
            }
            if (currentPhase == 1)
            {
                LoadoutPhase();
            }
            if (currentPhase == 2)
            {
                ActionPhase();
            }
            if (currentPhase == 3)
            {
                EndPhase();
            }
            if (currentPhase == 4)
            {
                SkipPhase();
            }
        }

        public virtual void SideSelectPhase()
        {
            camPos = new Vec2(-9999f, -9999f);
            Level.current.camera.position = camPos;

            if (!addSelection)
            {
                addSelection = true;
                SideSelect();
            }

            int t = 0;
            foreach (Duck d in Level.current.things[typeof(Duck)])
            {
                if (!d.dead)
                {
                    t += 1;
                }
            }

            if (loaded >= t)
            {
                time = selectionPhase;
                currentPhase = 1;
                SideSelectRemove();
                addSelection = false;
                //SetPlantRoom();
            }
        }

        public virtual void LoadoutPhase()
        {
            int t = 0;
            foreach (Duck d in Level.current.things[typeof(Duck)])
            {
                if (!d.dead)
                {
                    t += 1;
                }
            }
            if (confirmed >= t)
            {
                //DevConsole.Log("SKIP SKIP SKIP", Color.White);
                if (time > 3)
                {
                    selectionPhase = 3f;
                    time = 3f;
                }
            }
            camPos = new Vec2(-9999f, -9999f);
            Level.current.camera.position = camPos;
            if (screen == 1)
            {
                if (!addSelection /*&& time < selectionPhase - 1*/)
                {
                    addSelection = true;
                    OperatorsSelect(banned);
                }
                SideSelectRemove();
            }
            if (selected != null && screen == 2)
            {
                SideSelectRemove();
                RemoveOperatorSelect();

                if (!addSelection)
                {
                    addSelection = true;
                    LoadoutSelect();
                }
                else
                {
                    foreach (EquipmentSelection eq in Level.current.things[typeof(EquipmentSelection)])
                    {
                        if (eq.itemID == 1)
                        {
                            if (eq.slotID == selectedPr)
                            {
                                eq._select.frame = 2;
                            }
                        }
                        if (eq.itemID == 2)
                        {
                            if (eq.slotID == selectedSc)
                            {
                                eq._select.frame = 2;
                            }
                        }
                        if (eq.itemID == 3)
                        {
                            if (eq.slotID == selectedDv)
                            {
                                eq._select.frame = 2;
                            }
                        }
                    }
                }
            }
            if (screen == 3)
            {
                if (!addSelection)
                {
                    addSelection = true;
                    LoadoutRemove();
                }
            }
        }

        public virtual void ActionPhase()
        {
            if (selected != null)
            {
                if(selected.oper != null && selected.oper.isDead)
                {
                    Level.Remove(selected.oper);
                    spawned = false;
                }
                if (!spawned)
                {
                    spawned = true;
                    //Level.Add(selected.oper);

                    Level.Add(selected);

                    foreach (Duck d in Level.current.things[typeof(Duck)])
                    {
                        if (d.profile.localPlayer && !d.dead)
                        {
                            selected.oper.MainGun = null;

                            d.Equip(selected);
                            selected.oper.duckOwner = d;
                            selected.duckOwner = d;
                            selected.oper.InitializeInv();
                        }
                    }

                    foreach (Spawn s in Level.current.things[typeof(Spawn)])
                    {
                        s.alpha = 0;
                    }
                }
                else
                {
                    UpdateVision();
                }

            }
            foreach (Operators op in Level.current.things[typeof(Operators)])
            {
                if (op.team == "Att" && Level.current is GameLevel)
                {
                    op.unableToMove = 5;
                    op.unableToJump = 5;
                }
            }
            foreach (OPEQ op in Level.current.things[typeof(OPEQ)])
            {
                op.position = new Vec2(-99999999, -99999999);
            }
        }

        public virtual void EndPhase()
        {
            if (!endRound)
            {
                endRound = true;

            }
        }

        public virtual void SkipPhase()
        {
            if (!(Level.current is Editor))
            {
                //Level.current.skipCurrentLevelReset = true;
                //Level.skipInitialize = true;
                //Level.current = Level.core.nextLevel;
                //Level.current.waitingOnNewData = true;

                GameMode.Skip();
            }
        }

        public virtual void TeamWon(string t = "Draw")
        {
            if (!winnerDeterminated)
            {
                endRound = true;
                winnerDeterminated = true;
                if (t == "Draw")
                {
                    SFX.Play(GetPath("SFX/Announcer/stinger_lose.wav"));
                    if (localDuck != null)
                    {
                        PlayerStats.renown += 120;
                        PlayerStats.Save();
                        Level.Add(new RenownGained() { description = "No winners", amount = 120 });
                    }
                    stateOfRound = "Draw";
                }
                if (t == "Def")
                {
                    if (team == 0)
                    {
                        SFX.Play(GetPath("SFX/Announcer/stinger_win.wav"));
                        stateOfRound = "Won";
                    }
                    if (team == 1)
                    {
                        SFX.Play(GetPath("SFX/Announcer/stinger_lose.wav"));
                        stateOfRound = "Lost";
                    }
                    R6S.upd.givePoints = true;
                    R6S.upd.AquaWonLastRound = true;

                    if (localDuck != null && team == 0)
                    {
                        PlayerStats.renown += 200;
                        PlayerStats.Save();
                        Level.Add(new RenownGained() { description = "Round won", amount = 200 });
                    }

                    if (localDuck != null && team == 1)
                    {
                        PlayerStats.renown += 50;
                        PlayerStats.Save();
                        Level.Add(new RenownGained() { description = "Nice try", amount = 50 });
                    }
                }
                if (t == "Att")
                {
                    if (team == 1)
                    {
                        SFX.Play(GetPath("SFX/Announcer/stinger_win.wav"));
                        stateOfRound = "Won";
                    }
                    if (team == 0)
                    {
                        SFX.Play(GetPath("SFX/Announcer/stinger_lose.wav"));
                        stateOfRound = "Lost";
                    }
                    R6S.upd.givePoints = true;
                    R6S.upd.AquaWonLastRound = false;


                    if (localDuck != null && team == 1)
                    {
                        PlayerStats.renown += 200;
                        PlayerStats.Save();
                        Level.Add(new RenownGained() { description = "Round won", amount = 200 });
                    }
                    if (localDuck != null && team == 0)
                    {
                        PlayerStats.renown += 50;
                        PlayerStats.Save();
                        Level.Add(new RenownGained() { description = "Nice try", amount = 50 });
                    }
                }
            }
        }

        public virtual void UpdateVision()
        {
            if (selected != null)
            {
                int k = 0;
                if (selected.oper.head != null && selected.oper.local)
                {
                    Vec2 pov = selected.oper.head.position;
                    Vec2 povTL = selected.oper.head.topLeft;
                    Vec2 povBR = selected.oper.head.bottomRight;

                    if (selected.oper.isDead)
                    {
                        pov = Level.current.camera.position + Level.current.camera.size / 2;
                        povTL = pov;
                        povBR = pov;
                    }

                    foreach (PhysicsObject m in Level.CheckRectAll<PhysicsObject>(camPos - new Vec2(20, 20) * Unit, camPos + Level.current.camera.size + new Vec2(20, 20) * Unit))
                    {
                        k++;
                        float alp = 1;
                        if (Level.CheckLine<Block>(m.position, povTL) != null)
                        {
                            alp *= 0.7f;
                        }
                        if (Level.CheckLine<Block>(m.position, povBR) != null)
                        {
                            alp *= 0.7f;
                        }
                        if (Level.CheckLine<Block>(m.position, new Vec2(povTL.x, povBR.y)) != null)
                        {
                            alp *= 0.7f;
                        }
                        if (Level.CheckLine<Block>(m.position, new Vec2(povBR.x, povTL.y)) != null)
                        {
                            alp *= 0.7f;
                        }
                        if (Level.CheckLine<Block>(m.position, pov) != null)
                        {
                            alp *= 0.7f;
                        }
                        else
                        {
                            alp = 1;
                        }
                        if (selected.oper.seeTSmokeFrames <= 0)
                        {
                            if (Level.CheckLine<SmokeGR>(m.position, povTL) != null)
                            {
                                alp *= 0.7f;
                            }
                            if (Level.CheckLine<SmokeGR>(m.position, povBR) != null)
                            {
                                alp *= 0.7f;
                            }
                            if (Level.CheckLine<SmokeGR>(m.position, new Vec2(povTL.x, povBR.y)) != null)
                            {
                                alp *= 0.7f;
                            }
                            if (Level.CheckLine<SmokeGR>(m.position, new Vec2(povBR.x, povTL.y)) != null)
                            {
                                alp *= 0.7f;
                            }
                            if (Level.CheckLine<SmokeGR>(m.position, pov) != null)
                            {
                                alp *= 0.7f;
                            }
                            foreach (SmokeGR s in Level.CheckRectAll<SmokeGR>(camPos - new Vec2(20, 20) * Unit, camPos + Level.current.camera.size + new Vec2(20, 20) * Unit))
                            {
                                s.alpha = 1f;
                            }
                        }
                        else
                        {
                            foreach (SmokeGR s in Level.CheckRectAll<SmokeGR>(camPos - new Vec2(20, 20) * Unit, camPos + Level.current.camera.size + new Vec2(20, 20) * Unit))
                            {
                                s.alpha = 0.4f * s.order;
                            }
                        }
                        if (alp < 0.2f)
                        {
                            alp = 0;
                        }

                        if (m is Operators)
                        {
                            Operators o = m as Operators;

                            if (o.team != selected.oper.team)
                            {
                                if (o.HasEffect("Exposed"))
                                {
                                    alp += 0.4f;
                                }
                            }
                            else
                            {
                                alp = 1f;
                            }
                            if (!o.identified && alp > 0.2f)
                            {
                                o.identified = true;
                            }
                        }
                        m.alpha = alp;


                        if (alp == 1 && m is Operators)
                        {
                            if ((m as Operators).holdObject != null)
                            {
                                (m as Operators).holdObject.alpha = alp;
                            }
                        }
                    }
                }
            }
        }

        public virtual void RemoveBack()
        {
            foreach (BackButton o in Level.current.things[typeof(BackButton)])
            {
                Level.Remove(o);
            }
        }

        public virtual void LoadoutRemove()
        {
            foreach (EquipmentSelection o in Level.current.things[typeof(EquipmentSelection)])
            {
                Level.Remove(o);
            }
            foreach (Confirm o in Level.current.things[typeof(Confirm)])
            {
                Level.Remove(o);
            }
            foreach (BackButton o in Level.current.things[typeof(BackButton)])
            {
                Level.Remove(o);
            }
        }

        public virtual void RemoveOperatorSelect()
        {
            foreach (OperatorSelection o in Level.current.things[typeof(OperatorSelection)])
            {
                Level.Remove(o);
            }
        }

        public virtual void SideSelect()
        {
            Level.Add(new SideSelectionButton(camPos.x + 60 * Unit, camPos.y + 144 * Unit) { team = "Def" });
            Level.Add(new SideSelectionButton(camPos.x + 260 * Unit, camPos.y + 144 * Unit) { team = "Att" });

            foreach (Profile p in R6S.upd.teamAqua)
            {
                defenders++;
                loaded++;
                if (p.localPlayer)
                {
                    team = 0;
                    SideSelectRemove();
                }
            }
            foreach (Profile p in R6S.upd.teamMagma)
            {
                attackers++;
                loaded++;
                if (p.localPlayer)
                {
                    team = 1;
                    SideSelectRemove();
                }
            }
        }

        public virtual void SideSelectRemove()
        {
            foreach (SideSelectionButton s in Level.current.things[typeof(SideSelectionButton)])
            {
                Level.Remove(s);
            }
        }

        public virtual void OperatorsSelect(int[] ban)
        {
            if (team == 0)
            {
                int k = 0;
                for (int i = 0; i < PlayerStats.totalDefenders; i++)
                {
                    int m = k / 6;
                    int l = k % 6;
                    OPEQ op = PlayerStats.GetDefenderByCallID(i, position);
                    if (op != null && op.oper != null && !ban.Contains(op.oper.operatorID))
                    {
                        Level.Add(new OperatorSelection(camPos.x + 14 * Unit + 26 * l * Unit, camPos.y + 40 * Unit + 26 * m * Unit) { operatorID = op.oper.operatorID });
                        k++;
                    }
                }
                /*for (int i = 0; i < PlayerStats.totalOperators; i += 2)
                {
                    int m = (i / 12);
                    int l = (i % 12) / 2;
                    Level.Add(new OperatorSelection(camPos.x + 18 * Unit + 26 * l * Unit, camPos.y + 40 * Unit + 26 * m * Unit) { operatorID = i });
                }*/
                //Level.Add(new OperatorSelection(camPos.x + 14 * Unit + 26 * 0 * Unit, camPos.y + 40 * Unit + 26 * 4 * Unit) { operatorID = -2 });
            }
            if (team == 1)
            {
                int k = 0;
                for (int i = 0; i < PlayerStats.totalAttackers; i++)
                {
                    int m = k / 6;
                    int l = k % 6;
                    OPEQ op = PlayerStats.GetAttackerByCallID(i, position);
                    if (op != null && op.oper != null && !ban.Contains(op.oper.operatorID))
                    {
                        Level.Add(new OperatorSelection(camPos.x + 14 * Unit + 26 * l * Unit, camPos.y + 40 * Unit + 26 * m * Unit) { operatorID = op.oper.operatorID });
                        k++;
                    }
                }
                /*for (int i = 0; i < PlayerStats.totalOperators; i += 2)
                {
                    int m = (i / 12);
                    int l = (i % 12) / 2;
                    Level.Add(new OperatorSelection(camPos.x + 18 * Unit + 26 * l * Unit, camPos.y + 40 * Unit + 26 * m * Unit) { operatorID = i + 1 });
                }*/
                //Level.Add(new OperatorSelection(camPos.x + 14 * Unit + 26 * 0 * Unit, camPos.y + 40 * Unit + 26 * 4 * Unit) { operatorID = -1 });
            }
        }

        public Vec2 GetSpawnPosition()
        {
            Vec2 pos = new Vec2();

            string point = "";

            foreach (SiteSelector s in Level.current.things[typeof(SiteSelector)])
            {
                if (s.FirstSite.Count > 0)
                {
                    if (s.selectedSite < s.FirstSite.Count)
                    {
                        point = Rando.Float(1) >= 0.5f ? s.FirstSite[s.selectedSite] : s.SecondSite[s.selectedSite];
                    }
                }
            }
            List<Spawn> AttSpawns = new List<Spawn>();
            if (selected != null)
            {
                if (selected.oper != null)
                {
                    foreach (Spawn s in Level.current.things[typeof(Spawn)])
                    {
                        s.alpha = 0;
                        if (s._team == selected.oper.team)
                        {
                            if (point != "" && s.location == point && team == 0)
                            {
                                s.oper = selected.oper;
                                selected.oper.position = s.position;
                                return s.position;
                            }
                            if (point == "" || s.team == "Att")
                            {
                                AttSpawns.Add(s);
                            }
                        }
                    }

                    if (team == 1)
                    {
                        Spawn s = AttSpawns[Rando.Int(AttSpawns.Count - 1)];
                        s.oper = selected.oper;
                        selected.oper.position = s.position;
                        return s.position;
                    }

                    if (pos == null)
                    {
                        foreach (Spawn s in Level.current.things[typeof(Spawn)])
                        {
                            if (s._team == selected.oper.team)
                            {
                                s.oper = selected.oper;
                                selected.oper.position = s.position;
                                return s.position;
                            }
                        }
                    }
                }
            }
            return pos;
        }

        public virtual OPEQ RandomOp()
        {
            OPEQ op = new OPEQ(position.x, position.y);

            int operatorID = Rando.Int(0, 11) * 2 + team;

            op = PlayerStats.GetOpeqByID(operatorID, position);

            op.netIndex = localDuck.profile.networkIndex;
            DuckNetwork.SendToEveryone(new NMSelectedOper(operatorID, localDuck.profile.networkIndex, localDuck.profile.name));

            return op;
        }

        public virtual void LoadoutSelect()
        {
            if (selected != null)
            {
                if (selected.oper != null)
                {
                    try
                    {
                        while (PlayerStats.operPreferences.Count < 520)
                        {
                            PlayerStats.operPreferences.Add("");
                            PlayerStats.Save();
                        }
                        if (selected.oper.operatorID >= 0)
                        {
                            selectedPr = Convert.ToInt32(PlayerStats.operPreferences[selected.oper.operatorID * 20]);
                            selectedSc = Convert.ToInt32(PlayerStats.operPreferences[selected.oper.operatorID * 20 + 6]);
                            selectedDv = Convert.ToInt32(PlayerStats.operPreferences[selected.oper.operatorID * 20 + 12]);
                        }
                    }
                    catch
                    {
                        if (selected.oper.operatorID >= 0)
                        {
                            PlayerStats.operPreferences[selected.oper.operatorID * 20] = "0";
                            PlayerStats.operPreferences[selected.oper.operatorID * 20 + 6] = "0";
                            PlayerStats.operPreferences[selected.oper.operatorID * 20 + 12] = "0";
                            PlayerStats.Save();
                        }
                    }

                    while (selectedPr >= selected.oper.Primary.Count && selectedPr > 0)
                    {
                        selectedPr--;
                    }
                    while (selectedSc >= selected.oper.Secondary.Count && selectedSc > 0)
                    {
                        selectedSc--;
                    }
                    while (selectedDv >= selected.oper.Devices.Count && selectedDv > 0)
                    {
                        selectedDv--;
                    }
                    for (int i = 0; i < selected.oper.Primary.Count; i++)
                    {
                        Level.Add(new EquipmentSelection(camPos.x + 40 * Unit + 64 * i * Unit, camPos.y + 60 * Unit) { itemID = 1, slotID = i, oper = selected.oper });
                    }
                    for (int i = 0; i < selected.oper.Secondary.Count; i++)
                    {
                        Level.Add(new EquipmentSelection(camPos.x + 40 * Unit + 64 * i * Unit, camPos.y + 100 * Unit) { itemID = 2, slotID = i, oper = selected.oper });
                    }
                    for (int i = 0; i < selected.oper.Devices.Count; i++)
                    {
                        Level.Add(new EquipmentSelection(camPos.x + 40 * Unit + 64 * i * Unit, camPos.y + 140 * Unit) { itemID = 3, slotID = i, oper = selected.oper });
                    }
                    Level.Add(new Confirm(camPos.x + 54 * Unit, camPos.y + 166 * Unit));

                    Level.Add(new BackButton(camPos.x + 120 * Unit, camPos.y + 166 * Unit));
                }
            }
        }

        public virtual void DrawLoadingPhase()
        {
            int att = 0;
            int def = 0;

            if (selectedOperators.Count > 0)
            {
                foreach (OPEQ opeq in selectedOperators)
                {
                    if (opeq.name != null && opeq._sprite != null)
                    {
                        if (opeq.oper.team == "Att" && team == 1)
                        {
                            SpriteMap _banner = new SpriteMap(GetPath("Sprites/GUI/banner.png"), 56, 134);
                            if (PlayerStats.globalPreferences.Count > 0 && PlayerStats.globalPreferences[0] != null)
                            {
                                SkinElement skin = SkinsCollectionManager.allCollection.Find(s => s.name == PlayerStats.globalPreferences[0]);
                                if (skin != null)
                                    _banner = new SpriteMap(GetPath("Sprites/GUI/" + skin.location + ".png"), 56, 134);
                            }
                            _banner.depth = 0.1f;
                            Graphics.Draw(_banner, Level.current.camera.position.x + (17 + 70 * att) * Unit, Level.current.camera.position.y + 39 * Unit);

                            //user profile avatar
                            if (att == 0 && localDuck != null)
                            {
                                User u = User.GetUser(localDuck.profile.steamID);
                                byte[] avatarMedium = null;
                                if (u != null && u.avatarMedium != null)
                                {
                                    avatarMedium = u.avatarMedium;

                                }
                                else
                                {
                                    if (Steam.user != null && Steam.user.avatarMedium != null)
                                    {
                                        avatarMedium = Steam.user.avatarMedium;

                                    }

                                }
                                Sprite avatar = null;
                                //Sprite sprite = (Sprite)null;
                                if (avatarMedium != null && avatarMedium.Length == 16384 && avatar == null)
                                {
                                    Texture2D texture2D = new Texture2D(Graphics.device, 64, 64);
                                    texture2D.SetData(avatarMedium);
                                    avatar = new Sprite(texture2D, 0.0f, 0.0f);
                                    avatar.CenterOrigin();
                                    //sprite.alpha = 0.7f + (0.3f * _pulse);
                                }
                                //Graphics.Draw(avatar.texture, Level.current.camera.position + new Vec2((25f + 70 * att) * Unit, 116f * Unit), null, Color.White, 0, new Vec2(), new Vec2(0.68f, 0.68f), SpriteEffects.None, 2f + 0.1f);
                            }


                            /*
                            Graphics.DrawRect(Level.current.camera.position + new Vec2(18 + 70 * att, 40) * Unit, Level.current.camera.position + new Vec2(72 + 70 * att, 172) * Unit, Color.Orange * 0.7f, 0.1f, false, 3f);
                            Graphics.DrawRect(Level.current.camera.position + new Vec2(19 + 70 * att, 41) * Unit, Level.current.camera.position + new Vec2(71 + 70 * att, 171) * Unit, Color.OrangeRed * 0.7f, 0.1f, true, 1f);
                            */

                            Graphics.Draw(opeq.oper.Primary[selectedPr]._sprite, Level.current.camera.position.x + (42 + 70 * att) * Unit, Level.current.camera.position.y + 56 * Unit, 0.6f);
                            Graphics.Draw(opeq.oper.Secondary[selectedSc]._sprite, Level.current.camera.position.x + (42 + 70 * att) * Unit, Level.current.camera.position.y + 80 * Unit, 0.6f);
                            Graphics.Draw(opeq.oper.Devices[selectedDv]._sprite, Level.current.camera.position.x + (42 + 70 * att) * Unit, Level.current.camera.position.y + 104 * Unit, 0.6f);
                            Graphics.DrawStringOutline(opeq.name, Level.current.camera.position + new Vec2(56 * Unit - opeq.name.Length / 2 * 8 * Unit + 70 * att * Unit, 160 * Unit), Color.White, Color.Black, 0.6f, null, 0.5f);

                            if (opeq.duckOwner != null)
                            {
                                //DevConsole.Log(opeq.duckOwner.profile.name);
                                Graphics.DrawStringOutline(opeq.duckOwner.profile.name, Level.current.camera.position + new Vec2(24 * Unit /*- opeq.oper.name.Length / 2 * 8 * Unit*/ + 70 * att * Unit, 47 * Unit), Color.White, Color.Black, 0.6f, null, 0.5f);
                                //Graphics.DrawStringOutline(opeq.duckOwner.profile.name, Level.current.camera.position + new Vec2(56 * Unit - opeq.duckOwner.profile.name.Length / 2 * 8 * Unit + 70 * att * Unit, 46 * Unit), Color.White, Color.Black, 0.6f, null, 0.5f);                    
                            }

                            SpriteMap icon = opeq._sprite.CloneMap();
                            icon.scale = new Vec2(2f, 2f) * Unit;
                            icon.CenterOrigin();
                            Graphics.Draw(icon, Level.current.camera.position.x + (47 + 70 * att) * Unit, Level.current.camera.position.y + 132 * Unit, 0.6f);
                            icon.scale = new Vec2(1f, 1f) * Unit;
                            att++;
                        }
                        if (opeq.oper.team == "Def" && team == 0)
                        {
                            SpriteMap _banner = new SpriteMap(GetPath("Sprites/GUI/banner.png"), 56, 134);
                            if (PlayerStats.globalPreferences.Count > 0 && PlayerStats.globalPreferences[0] != null)
                            {
                                SkinElement skin = SkinsCollectionManager.allCollection.Find(s => s.name == PlayerStats.globalPreferences[0]);
                                if (skin != null)
                                {
                                    _banner = new SpriteMap(GetPath("Sprites/GUI/" + skin.location + ".png"), 56, 134);
                                }
                            }
                            _banner.depth = 0.1f;
                            Graphics.Draw(_banner, Level.current.camera.position.x + (17 + 70 * def) * Unit, Level.current.camera.position.y + 39 * Unit);


                            //user profile avatar
                            if (def == 0 && localDuck != null)
                            {
                                User u = User.GetUser(localDuck.profile.steamID);
                                byte[] avatarMedium = null;
                                if (u != null && u.avatarMedium != null)
                                {
                                    avatarMedium = u.avatarMedium;

                                }
                                else
                                {
                                    if (Steam.user != null && Steam.user.avatarMedium != null)
                                    {
                                        avatarMedium = Steam.user.avatarMedium;

                                    }

                                }
                                Sprite avatar = null;
                                //Sprite sprite = (Sprite)null;
                                if (avatarMedium != null && avatarMedium.Length == 16384 && avatar == null)
                                {
                                    Texture2D texture2D = new Texture2D(Graphics.device, 64, 64);
                                    texture2D.SetData(avatarMedium);
                                    avatar = new Sprite((Tex2D)texture2D, 0.0f, 0.0f);
                                    avatar.CenterOrigin();
                                    //sprite.alpha = 0.7f + (0.3f * _pulse);
                                }
                                //Graphics.Draw(avatar.texture, Level.current.camera.position + new Vec2((25f + 70 * att) * Unit, 116f * Unit), null, Color.White, 0, new Vec2(), new Vec2(0.68f, 0.68f), SpriteEffects.None, 1 + 0.1f);
                            }

                            /*
                            Graphics.DrawRect(Level.current.camera.position + new Vec2(18 + 70 * def, 40) * Unit, Level.current.camera.position + new Vec2(72 + 70 * def, 172) * Unit, Color.Blue * 0.7f, 0.1f, false, 3f);
                            Graphics.DrawRect(Level.current.camera.position + new Vec2(19 + 70 * def, 41) * Unit, Level.current.camera.position + new Vec2(71 + 70 * def, 171) * Unit, Color.Turquoise * 0.7f, 0.1f, true, 1f);
                            */

                            Graphics.Draw(opeq.oper.Primary[selectedPr]._sprite, Level.current.camera.position.x + (42 + 70 * def) * Unit, Level.current.camera.position.y + 56 * Unit, 0.6f);
                            Graphics.Draw(opeq.oper.Secondary[selectedSc]._sprite, Level.current.camera.position.x + (42 + 70 * def) * Unit, Level.current.camera.position.y + 80 * Unit, 0.6f);
                            Graphics.Draw(opeq.oper.Devices[selectedDv]._sprite, Level.current.camera.position.x + (42 + 70 * def) * Unit, Level.current.camera.position.y + 104 * Unit, 0.6f);
                            Graphics.DrawStringOutline(opeq.name, Level.current.camera.position + new Vec2(56 * Unit - opeq.name.Length / 2 * 8 * Unit + 70 * def * Unit, 160 * Unit), Color.White, Color.Black, 0.6f, null, 0.5f);
                            if (opeq.duckOwner != null)
                            {
                                //DevConsole.Log(opeq.duckOwner.profile.name);
                                Graphics.DrawStringOutline(opeq.duckOwner.profile.name, Level.current.camera.position + new Vec2(24 * Unit/* - opeq.oper.name.Length / 2 * 8 * Unit*/ + 70 * def * Unit, 47 * Unit), Color.White, Color.Black, 0.6f, null, 0.5f);
                                //Graphics.DrawStringOutline(opeq.duckOwner.profile.name, Level.current.camera.position + new Vec2(56 * Unit - opeq.duckOwner.profile.name.Length / 2 * 8 * Unit + 70 * att * Unit, 46 * Unit), Color.White, Color.Black, 0.6f, null, 0.5f);
                            }
                            SpriteMap icon = opeq._sprite.CloneMap();

                            icon.scale = new Vec2(2f, 2f) * Unit;
                            icon.CenterOrigin();
                            Graphics.Draw(icon, Level.current.camera.position.x + (47 + 70 * def) * Unit, Level.current.camera.position.y + 132 * Unit, 0.6f);
                            icon.scale = new Vec2(1f, 1f) * Unit;
                            def++;
                        }
                    }
                }
            }
        }

        public virtual void DrawOperatorsSelect() //Everytime, except changemap-phase
        {
            if (currentPhase < 3 && !(Level.current is Editor))
            {
                SpriteMap _gui = new SpriteMap(Mod.GetPath<R6S>("Sprites/Cameras/GUIFilters.png"), 64, 36, false);

                _gui.scale = new Vec2(Level.current.camera.size.x / 64, Level.current.camera.size.y / 36) * Unit;

                Graphics.Draw(_gui, camPos.x, camPos.y, -0.1f);

                if (team == 0 && currentPhase > 0)
                {
                    string site = "";
                    foreach (SiteSelector s in Level.current.things[typeof(SiteSelector)])
                    {
                        if (s.selectedSite < s.FirstSite.Count)
                        {
                            site = s.FirstSite[s.selectedSite] + " : " + s.SecondSite[s.selectedSite];
                        }
                    }
                    Graphics.DrawString(site, camPos + new Vec2(160 - site.Length * 3 * 0.5f, 175), Color.White, 1f, null, 0.5f);
                }
            }

            if (currentPhase == 0 && !(Level.current is Editor))
            {
                string text = "Join in team";
                Graphics.DrawStringOutline(text, Level.current.camera.position + new Vec2(160 - 4 * text.Length * Unit, 40) * Unit, Color.White, Color.Black, 0.6f, null, 1f);


                string text2 = Convert.ToString(defenders) + "    VS    " + Convert.ToString(attackers);
                Graphics.DrawStringOutline(text2, Level.current.camera.position + new Vec2(160 - 4 * text.Length * Unit, 130) * Unit, Color.White, Color.Black, 0.6f, null, 1f);
            }

            if (currentPhase > 0)
            {
                if (currentPhase == 1 && screen == 2)
                {
                    Graphics.DrawStringOutline("Primary gun", Level.current.camera.position + new Vec2(20, 35) * Unit, Color.White, Color.Black, 0.6f, null, 1f);

                    Graphics.DrawStringOutline("Secondary Gun", Level.current.camera.position + new Vec2(20, 75) * Unit, Color.White, Color.Black, 0.6f, null, 1f);

                    Graphics.DrawStringOutline("Secondary device", Level.current.camera.position + new Vec2(20, 115) * Unit, Color.White, Color.Black, 0.6f, null, 1f);
                }

                float UIScale = 1;

                if (time > 0)
                {
                    int min = 0;
                    int timer = (int)time;
                    while (timer >= 60)
                    {
                        min += 1;
                        timer -= 60;
                    }

                    string text = Convert.ToString(min) + ":" + Convert.ToString(timer);
                    if (timer < 10)
                    {
                        text = Convert.ToString(min) + ":0" + Convert.ToString(timer);
                    }

                    Graphics.DrawStringOutline(text, Level.current.camera.position - new Vec2(160f + 2 * text.Length * UIScale, 180 - 2 * UIScale) * Unit + new Vec2(320, 180) * Unit, Color.White, Color.Black, 0.5f, null, 0.5f * Unit);
                }

                Graphics.DrawStringOutline(Convert.ToString(R6S.upd.AquaPoints), new Vec2(Level.current.camera.position.x + 30 * Unit, Level.current.camera.position.y + 6 * Unit), Color.White, Color.Black);
                Graphics.DrawStringOutline(Convert.ToString(R6S.upd.MagmaPoints), new Vec2(Level.current.camera.position.x + 285 * Unit, Level.current.camera.position.y + 6 * Unit), Color.White, Color.Black);


                SpriteMap _timer = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUITimer.png"), 296, 28, false);
                _timer.alpha = 0f;
                _timer.center = new Vec2(148f, 14f);

                _timer.scale = new Vec2(1, 1) * Unit;

                Graphics.Draw(_timer, Level.current.camera.position.x + 160 * Unit, Level.current.camera.position.y + 11 * Unit, 0.3f);

                int def = 0;
                int att = 0;

                if (selectedOperators.Count > 0)
                {
                    foreach (OPEQ opeq in selectedOperators)
                    {
                        SpriteMap _icon = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
                        _icon.frame = opeq._sprite.frame;
                        _icon.CenterOrigin();
                        offDir = 1;
                        _icon.scale = new Vec2(0.5f, 0.5f) * Unit;
                        _icon.flipH = false;

                        if (opeq.oper.team == "Def")
                        {
                            Vec2 pos = new Vec2(Level.current.camera.position.x + (160 + (-32 - 16 * def) * UIScale) * Unit, Level.current.camera.position.y + 4f * Unit * UIScale);
                            if (team == 0)
                            {
                                Graphics.DrawLine(new Vec2(pos.x - 4f * Unit * UIScale, pos.y + 6 * Unit * UIScale),
                                    new Vec2(pos.x + (-4 + 7) * Unit * UIScale, pos.y + 6 * Unit * UIScale), Color.Black, 1f * Unit * UIScale, 0.45f);
                                Graphics.DrawLine(new Vec2(pos.x - 4f * Unit * UIScale, pos.y + 6 * Unit * UIScale),
                                    new Vec2(pos.x + (-4 + 7 * (opeq.oper.Health / 100f)) * Unit * UIScale, pos.y + 6 * Unit * UIScale), Color.White, 1f * Unit * UIScale, 0.65f);
                                Graphics.Draw(_icon, pos.x, pos.y + 0.5f * Unit * UIScale, 0.6f);
                                def++;
                            }
                            if (team == 1)
                            {
                                if (!opeq.oper.identified)
                                {
                                    _icon.frame = 74;
                                }
                                Graphics.Draw(_icon, pos.x, pos.y + 0.5f * Unit * UIScale, 0.6f);
                                def++;
                            }
                        }

                        if (opeq.oper.team == "Att")
                        {
                            Vec2 pos = new Vec2(Level.current.camera.position.x + (160 + (32 + 16 * att) * UIScale) * Unit, Level.current.camera.position.y + 4f * Unit * UIScale);
                            if (team == 0)
                            {
                                if (!opeq.oper.identified)
                                {
                                    _icon.frame = 74;
                                }
                                Graphics.Draw(_icon, pos.x, pos.y + 0.5f * Unit * UIScale, 0.6f);
                                att++;
                            }
                            if (team == 1)
                            {
                                Graphics.DrawLine(new Vec2(pos.x - 4f * Unit * UIScale, pos.y + 6 * Unit * UIScale),
                                    new Vec2(pos.x + (-4 + 7) * Unit * UIScale, pos.y + 6 * Unit * UIScale), Color.Black, 1f * Unit * UIScale, 0.45f);
                                Graphics.DrawLine(new Vec2(pos.x - 4f * Unit * UIScale, pos.y + 6 * Unit * UIScale),
                                    new Vec2(pos.x + (-4 + 7 * (opeq.oper.Health / 100f)) * Unit * UIScale, pos.y + 6 * Unit * UIScale), Color.White, 1f * Unit * UIScale, 0.65f);

                                Graphics.Draw(_icon, pos.x, pos.y + 0.5f * Unit * UIScale, 0.6f);
                                att++;
                            }
                        }
                    }
                }
            }
        }

        public virtual void DrawMatchInfo()
        {
            /*if (team == 0 && currentPhase < 4)
            {
                Graphics.DrawRect(camPos + new Vec2(-1, 14) * Unit, camPos + new Vec2(40, 30) * Unit, Color.Black * 0.8f, 1f);
                Graphics.DrawString(Convert.ToString(reinforcementPool), camPos + new Vec2(29, 18), Color.White, 1.01f, null, Unit);
                Sprite _s = new Sprite(GetPath("Sprites/Reinforcements.png"));
                _s.CenterOrigin();
                _s.scale = new Vec2(Unit * 0.75f, Unit * 0.75f);
                Graphics.Draw(_s, camPos.x + 17.5f, camPos.y + 22.5f, 1.02f);
            }*/
        }

        public override void Draw()
        {
            base.Draw();

            Graphics.DrawString("R6:S MOD PUBLIC BETA", Level.current.camera.position + new Vec2(Unit * 10, Unit * 20), Color.White * 0.2f, 1f, null, Unit);
            Graphics.DrawString("ver. " + R6S.version + " (by FaecTerr)", Level.current.camera.position + new Vec2(Unit * 10, Unit * 30), Color.White * 0.2f, 1f, null, Unit * 0.5f);


            if (currentPhase == 1)
            {
                if (phaseTime[currentPhase - 1] - time < 5)
                {
                    //DevConsole.Log(Convert.ToString(currentPhase) + ": " + Convert.ToString(phaseTime[currentPhase - 1]));
                    Sprite mapPreview = new Sprite(GetPath("Sprites/Maps/Noname.png"));

                    if (mapname == "Kanal")
                    {
                        mapPreview = new Sprite(GetPath("Sprites/Maps/KanalPrev.png"));
                    }
                    if (mapname == "Sailboat")
                    {
                        mapPreview = new Sprite(GetPath("Sprites/Maps/SailboatPrev.png"));
                    }
                    if (mapname == "Insection")
                    {
                        mapPreview = new Sprite(GetPath("Sprites/Maps/InsectionPrev.png"));
                    }
                    if (mapname == "Border")
                    {
                        mapPreview = new Sprite(GetPath("Sprites/Maps/BorderPrev.png"));
                    }
                    if (mapname == "Abandoned")
                    {
                        mapPreview = new Sprite(GetPath("Sprites/Maps/AbandonedPrev.png"));
                    }
                    Vec2 pos = Level.current.camera.position;
                    mapPreview.CenterOrigin();
                    mapPreview.scale = new Vec2(1f + (phaseTime[currentPhase - 1] - time) / 5 * 0.05f, 1f + (phaseTime[currentPhase - 1] - time) / 5 * 0.05f);
                    mapPreview.depth = -0.05f;
                    mapPreview.alpha = 1 - (phaseTime[currentPhase - 1] - time) / 5;


                    Graphics.Draw(mapPreview, pos.x + 160 * Unit, pos.y + 90 * Unit);
                }
            }

            if (currentPhase != 5)
            {
                DrawOperatorsSelect();
            }
            if (currentPhase == 2 || (currentPhase == 1 && screen == 3))
            {
                DrawLoadingPhase();
            }
            if (currentPhase == 3 && team == 1)
            {
                //Graphics.DrawStringOutline("Use drones, to find enemies position", );
            }
            if (currentPhase < 3)
            {
                if (localDuck != null)
                {
                    if (localDuck.inputProfile.lastActiveDevice.name == "keyboard")
                    {
                        aim = Mouse.positionScreen;
                        Graphics.Draw(_aim, aim.x, aim.y, 1f);
                        //DevConsole.Log(localDuck.inputProfile.lastActiveDevice.name + localDuck.profile.name);
                    }
                    else
                    {
                        aim = padMousePositionScreen;
                        Graphics.Draw(_aim, aim.x, aim.y, 1f);
                        //DevConsole.Log(localDuck.inputProfile.lastActiveDevice.name + localDuck.profile.name);
                    }
                }
            }
            else
            {
                DrawMatchInfo();
            }

            if (localDuck != null && (Keyboard.Down(PlayerStats.keyBindings[24]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[24])))
            {
                float xSize = 1f * PlayerStats.TabScale;
                float ySize = 1f * PlayerStats.TabScale;

                Vec2 camPos = Level.current.camera.position;
                Vec2 camSize = Level.current.camera.size;
                Graphics.DrawRect(camPos + new Vec2(160f - 130f * xSize, 90f - (78f) * ySize) * Unit, camPos + new Vec2(160f + 130f * xSize, 90f + (78f) * ySize) * Unit, Color.Black * 0.3f, 1.2f);
                Graphics.DrawRect(camPos + new Vec2(160f - 130f * xSize, 90f - (8f) * ySize) * Unit, camPos + new Vec2(160f + 130f * xSize, 90f + (8f) * ySize) * Unit, Color.Black * 0.3f, 1.2f);

                int att = 0;
                int def = 0;


                foreach (OPEQ op in selectedOperators)
                {
                    if (op.oper != null && op.duckOwner != null)
                    {
                        if (op.oper.team == "Att")
                        {
                            Graphics.DrawRect(camPos + new Vec2(160f + (-130f + 26f) * xSize, 90f + (-8f - 18f * att) * ySize) * Unit, camPos + new Vec2(160f + 130f * xSize, 90f + (-8f - 16f - 18f * att) * ySize) * Unit, Color.Wheat * 0.3f, 1.25f);
                            Graphics.DrawStringOutline(op.duckOwner.profile.name, camPos + new Vec2(160f + (-130f + 32f) * xSize, 90f + (-8f - 13f - 18f * att) * ySize) * Unit, Color.White, Color.Black, 1.3f, null, 1f * Unit * ySize);

                            att++;
                        }
                        if (op.oper.team == "Def")
                        {
                            Graphics.DrawRect(camPos + new Vec2(160f + (-130f + 26f) * xSize, 90f + (8f + 18f * def) * ySize) * Unit, camPos + new Vec2(160f + 130f * xSize, 90f + (8f + 16f + 18f * def) * ySize) * Unit, Color.Wheat * 0.3f, 1.25f);
                            Graphics.DrawStringOutline(op.duckOwner.profile.name, camPos + new Vec2(160f + (-130f + 32f) * xSize, 90f + (8f + 5f + 18f * def) * ySize) * Unit, Color.White, Color.Black, 1.3f, null, 1f * Unit * ySize);

                            def++;
                        }
                    }
                }

                Graphics.DrawRect(camPos + new Vec2(160f - 130f * xSize, 90f - 78f * ySize) * Unit, camPos + new Vec2(160f + (-130f + 26f) * xSize, 90f - 8f * ySize) * Unit, Color.Orange * 0.3f, 1.2f);
                Graphics.DrawRect(camPos + new Vec2(160f - 130f * xSize, 90f + 8f * ySize) * Unit, camPos + new Vec2(160f + (-130f + 26f) * xSize, 90f + 78f * ySize) * Unit, Color.Blue * 0.3f, 1.2f);


                Graphics.DrawStringOutline(Convert.ToString(R6S.upd.MagmaPoints), camPos + new Vec2(160f + (-130f + 5f) * xSize, 90f + (-40f - 6f) * ySize) * Unit, Color.White, Color.Black, 1.3f, null, 2f * Unit * ySize);
                Graphics.DrawStringOutline(Convert.ToString(R6S.upd.AquaPoints), camPos + new Vec2(160f + (-130f + 5f) * xSize, 90f + (40f - 6f) * ySize) * Unit, Color.White, Color.Black, 1.3f, null, 2f * Unit * ySize);
            }

            if (currentPhase == 5)
            {
                DrawEnd();
            }


            if (selected != null && PlayerStats.DebugSpeed)
            {
                Vec2 pos = Level.current.camera.position;
                String text = "horizontal velocity: ";

                text += Convert.ToString(Math.Round(selected.oper.hSpeed, 3));
                if (selected.oper.sprinting)
                {
                    //text += " (S)";
                }
                //text += " " + Convert.ToString(Math.Round(selected.oper.friction, 3));

                Graphics.DrawString(text, pos + new Vec2(2 * Unit, 80 * Unit), Color.White * 0.5f, 1f, null, Unit * 0.3f);

                text = "vertical velocity: ";
                text += Convert.ToString(Math.Round(selected.oper.vSpeed, 3));
                Graphics.DrawString(text, pos + new Vec2(2 * Unit, 85 * Unit), Color.White * 0.5f, 1f, null, Unit * 0.3f);
            }

            if (debugMode)
            {
                DebugingDraw();
            }

            if (showGrid)
            {
                Vec2 pos = Level.current.camera.position;
                for (int r = 0; r < 180; r++)
                {
                    Graphics.DrawLine(pos + new Vec2(0, r) * Unit, pos + new Vec2(320, r) * Unit, Color.Wheat, 0.25f, 1f);
                }
                for (int c = 0; c < 320; c++)
                {
                    Graphics.DrawLine(pos + new Vec2(c, 0) * Unit, pos + new Vec2(c, 180) * Unit, Color.Wheat, 0.25f, 1f);
                }
            }
        }

        public virtual void DebugingDraw()
        {
            Vec2 pos = Level.current.camera.position;

            /*for (int r = 0; r < 180; r++)
            {
                Graphics.DrawLine(pos + new Vec2(0, r) * Unit, pos + new Vec2(320, r) * Unit, Color.Wheat, 0.25f, 1f);
            }
            for (int c = 0; c < 320; c++)
            {
                Graphics.DrawLine(pos + new Vec2(c, 0) * Unit, pos + new Vec2(c, 180) * Unit, Color.Wheat, 0.25f, 1f);
            }*/

            if (selected != null)
            {
                String text = "horizontal velocity: ";

                text += Convert.ToString(Math.Round(selected.oper.hSpeed, 3));


                Graphics.DrawString(text, pos + new Vec2(2 * Unit, 80 * Unit), Color.White * 0.5f, 1f, null, Unit * 0.3f);

                text = "vertical velocity: ";
                text += Convert.ToString(Math.Round(selected.oper.vSpeed, 3));
                Graphics.DrawString(text, pos + new Vec2(2 * Unit, 85 * Unit), Color.White * 0.5f, 1f, null, Unit * 0.3f);

                Graphics.DrawString("Selected oper: " + selected.name, Level.current.camera.position + new Vec2(Unit * 160, Unit * 20), Color.White * 0.5f, 0.5f, null, Unit);
            }
            else
            {
                Graphics.DrawString("Operator not selected", Level.current.camera.position + new Vec2(Unit * 160, Unit * 20), Color.White * 0.5f, 0.5f, null, Unit);
            }

            string netIndex = "";
            string name = "";

            if ((DateTime.Now - lastFPS).Milliseconds >= 500)
            {
                if (fpsHistory.Length > 2)
                {
                    for (int j = 99; j > 0; j--)
                    {
                        fpsHistory[j] = fpsHistory[j - 1];
                        if (j >= 100)
                        {
                            fpsHistory[j] = 0;
                        }
                    }
                }
                fpsHistory[0] = (int)(1000f / (DateTime.Now - lastCheck).Milliseconds);
                lastFPS = DateTime.Now;
            }
            for (int j = 0; j < 100; j++)
            {
                Graphics.DrawLine(pos + new Vec2(10 + 1 * j, 170) * Unit, pos + new Vec2(10 + 1 * j, 170 - 1 * fpsHistory[j]) * Unit, Color.White * 0.4f, Unit, 1f);
            }
            Graphics.DrawRect(pos + new Vec2(10, 170) * Unit, pos + new Vec2(10 + 99, 170 - 60) * Unit, Color.White * 0.2f, Unit);

            lastCheck = DateTime.Now;

            foreach (Duck d in Level.current.things[typeof(Duck)])
            {
                if (d.profile != null)
                {
                    if (d.profile.localPlayer)
                    {
                        netIndex = Convert.ToString(d.profile.networkIndex);
                        name = d.profile.name;
                    }
                }
                if (selected != null)
                {
                    if (selected.duckOwner == d)
                    {
                        netIndex = Convert.ToString(d.profile.networkIndex);
                        name = d.profile.name;
                    }
                }
            }

            Graphics.DrawString("netIndex: " + netIndex, Level.current.camera.position + new Vec2(Unit * 200, Unit * 30), Color.White * 0.5f, 1f, null, Unit * 0.5f);
            Graphics.DrawString("name: " + netIndex, Level.current.camera.position + new Vec2(Unit * 200, Unit * 40), Color.White * 0.5f, 1f, null, Unit * 0.5f);

            if (localDuck != null)
            {
                if (localDuck.profile.name == name)
                {
                    Graphics.DrawString("localDuck variable == to found local duck", Level.current.camera.position + new Vec2(Unit * 200, Unit * 50), Color.White * 0.5f, 0.5f, null, Unit * 0.5f);
                }
                else
                {
                    Graphics.DrawString("localDuck variable != to found local duck", Level.current.camera.position + new Vec2(Unit * 200, Unit * 50), Color.White * 0.5f, 0.5f, null, Unit * 0.5f);
                }
            }

            int i = 0;

            foreach (Operators o in Level.current.things[typeof(Operators)])
            {
                string n = o.name;
                string ind = Convert.ToString(o.netIndex);

                if (o.opeq != null)
                {
                    ind += " Q: " + o.opeq.netIndex;
                }
                else
                {
                    ind += " Q: -1";
                }
                if (o.duckOwner != null)
                {
                    ind += " D: " + o.duckOwner.profile.networkIndex;
                }
                else
                {
                    ind += " D: -1";
                }

                Graphics.DrawString(n + ": " + ind, Level.current.camera.position + new Vec2(Unit * 200, Unit * (60 + i * 10)), Color.White * 0.5f * o.alpha, 1f, null, Unit * 0.5f);

                i++;
            }

            Graphics.DrawString("'P' to close", Level.current.camera.position + new Vec2(Unit * 200, Unit * 160), Color.White * 0.5f, 1f, null, Unit * 0.5f);
        }

        public virtual void DrawEnd()
        {
            Vec2 camPos = Level.current.camera.position;
            Vec2 camSize = Level.current.camera.size;
            Vec2 Unit = camSize / new Vec2(320, 180);
            Vec2 camCenter = camPos + camSize * 0.5f;


            float move = time - 2.5f;
            if (move < 0)
            {
                move = 0;
            }

            Color c = Color.CornflowerBlue;
            string text = "Round ";


            if (stateOfRound == "Won")
            {
                c = Color.Yellow;
                text += "won";
            }
            if (stateOfRound == "Lost")
            {
                c = Color.OrangeRed;
                text += "lost";
            }

            Graphics.DrawStringOutline(text, camCenter + new Vec2(-3 * text.Length, -4) * Unit, Color.White, Color.Black, 1f, null, Unit.x);

            for (int i = 0; i < 3; i++)
            {
                move = time - (2.5f + 0.1f * i);
                if (move < 0)
                {
                    move = 0;
                }
                Sprite _endRound = new Sprite(GetPath("Sprites/RoundEndSign.png"));
                _endRound.CenterOrigin();
                _endRound.angle = -0.4f + 0.4f * i;
                _endRound.scale = new Vec2(0.9f - move * move * 0.05f, 0.9f - move * move * 0.05f) * Unit * 5 * new Vec2(1.3f - 0.3f * Math.Abs(i - 1), 1.3f - 0.3f * Math.Abs(i - 1));
                _endRound.alpha = 0.35f - 0.2f * Math.Abs(i - 1);
                _endRound.color = c;
                Graphics.Draw(_endRound, camCenter.x, camCenter.y, 0.995f - 0.001f * Math.Abs(i - 1));
            }
        }
    }
}
