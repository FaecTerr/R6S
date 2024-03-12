using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class R6SMainUpdate : IAutoUpdate
    {
        public int AquaPoints;
        public int MagmaPoints;

        public List<Profile> teamAqua = new List<Profile>();
        public List<Profile> teamMagma = new List<Profile>();

        public bool givePoints = false;
        public Level prevLevel;

        public List<User> teammates = new List<User>();

        public Level mainMenu;

        public bool AquaWonLastRound;


        public int playedMatches;

        public R6SMainUpdate()
        {
            AutoUpdatables.Add(this);
        }


        public void Update()
        {
            while (PlayerStats.operPreferences.Count < 20 * PlayerStats.totalOperators || PlayerStats.operPreferences.Count < PlayerStats.totalOperators * 20)
            {
                PlayerStats.Data.operPreferences.Add("");
                PlayerStats.operPreferences.Add("");
            }

            if (Network.connections.Count > 0)
            {

            }

            if (Level.current != null)
            {
                if (Level.current != prevLevel)
                {
                    if(prevLevel is GameLevel && (AquaPoints + MagmaPoints > 0 || Level.current is GameLevel))
                    {
                        givePoints = true;
                    }
                    prevLevel = Level.current;

                }
                if (Level.current is GameLevel)
                {
                    //DevConsole.Log(Level.current._level);
                    foreach(GunDev g in Level.current.things[typeof(GunDev)])
                    {
                        foreach(SkinElement skin in SkinsCollectionManager.allCollection)
                        {
                            if (skin.mainThing == g.editorName && g.oper != null)
                            {
                                {
                                    if (PlayerStats.operPreferences[g.oper.operatorID * 20 + 5] == skin.name || PlayerStats.operPreferences[g.oper.operatorID * 20 + 16] == skin.name || 
                                        PlayerStats.operPreferences[g.oper.operatorID * 20 + 17] == skin.name || PlayerStats.operPreferences[g.oper.operatorID * 20 + 11] == skin.name || 
                                        PlayerStats.operPreferences[g.oper.operatorID * 20 + 18] == skin.name || PlayerStats.operPreferences[g.oper.operatorID * 20 + 19] == skin.name)
                                    {
                                        int sizey = g._sprite.width;
                                        g._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/skins/" + skin.location + ".png"), 32, sizey);
                                        g._sprite.CenterOrigin();
                                    }
                                }
                            }
                        }
                    }


                    if (givePoints)
                    {
                        if (AquaWonLastRound)
                        {
                            foreach (Profile profile in teamAqua)
                            {
                                GameMode.lastWinners.Add(profile);
                            }
                            AquaPoints += 1;

                        }
                        else
                        {
                            foreach (Profile profile in teamMagma)
                            {
                                GameMode.lastWinners.Add(profile);
                            }
                            MagmaPoints += 1;
                        }
                        givePoints = false;
                    }
                }
                
                if(GameMode.numMatchesPlayed != playedMatches)
                {
                    playedMatches = GameMode.numMatchesPlayed;
                    teamAqua.Clear();
                    teamMagma.Clear();
                }

                if (Level.current is TitleScreen || Level.current is Editor || Level.current is TeamSelect2)
                {
                    playedMatches = GameMode.numMatchesPlayed;
                    teamAqua.Clear();
                    teamMagma.Clear();
                    AquaPoints = 0;
                    MagmaPoints = 0;
                }

                if (Level.current is TitleScreen)
                {
                    if (Keyboard.Pressed(Keys.F2))
                    {
                        if (mainMenu == null)
                        {
                            mainMenu = new MainMenu();
                            Level.current = mainMenu;
                        }
                        else
                        {
                            Level.current = mainMenu;
                        }
                    }
                    int k = 0;
                    foreach (MenuSign m in Level.current.things[typeof(MenuSign)])
                    {
                        k++;
                    }
                    if (k == 0)
                    {
                        Level.Add(new MenuSign());
                    }
                }
            }
            /*
            if (GameMode.playedGame)
            {
                teamAqua = null;
                teamMagma = null;

                MagmaPoints = 0;
                AquaPoints = 0;
            }*/
        }
    }
}
