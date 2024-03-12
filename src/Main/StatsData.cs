using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class StatsData : DataClass
    {
        public StatsData()
        {
            _nodeName = "R7Stats";
            firstLaunch = true;
            renown = 6000;
            ELO = 2500;

            openedOperators.Add(-2);
            openedOperators.Add(-1);

            openedOperators.Add(0);
            openedOperators.Add(1);

            openedOperators.Add(6);
            openedOperators.Add(7);

            openedOperators.Add(8);
            openedOperators.Add(9);

            openedOperators.Add(13);
            openedOperators.Add(14);

            openedOperators.Add(17);
            openedOperators.Add(18);

            Volume = 1;
            Screenshake = 0.5f;
            showNicknames = false;
            hideKeysIcons = false;
            AimAssist = true;

            lastPlayed = DateTime.Now.ToString();

            for (int i = 0; i < 520; i++)
            {
                operPreferences.Add("");
            }


            //Bindings
            //[0] - Jump; [1] - Crouch; [2] - Left; [3] - Right; [4] - Interact; [5] - Plant; [6] - Prone; [7] - Primary; [8] - Secondary; [9] - MainDevice;
            //[10] - Secondary device; [11] - Phone; [12] - Drone; [13] - Shoot; [14] - Aim down side; [15] - Prev cam; [16] - Next cam; 
            //[17] - Prev category; [18] - Next category; [19] - Sprint; [20] - Reload; [21] - Melee
        }


        public bool firstLaunch
        {
            get
            {
                return PlayerStats.firstLaunch;
            }
            set
            {
                PlayerStats.firstLaunch = value;
            }
        }

        public List<int> openedOperators
        {
            get
            {
                return PlayerStats.openedOperators;
            }
            set
            {
                PlayerStats.openedOperators = value;
            }
        }

        public int renown
        {
            get
            {
                return PlayerStats.renown;
            }
            set
            {
                PlayerStats.renown = value;
            }
        }

        public string lastPlayed 
        { 
            get
            {
                return PlayerStats.lastPlayed;
            }
            set 
            {
                PlayerStats.lastPlayed = value;
            }
        }

        public int materials
        {
            get
            {
                return PlayerStats.materials;
            }
            set
            {
                PlayerStats.materials = value;
            }
        }

        public List<string> operPreferences
        {
            get
            {
                return PlayerStats.operPreferences;
            }
            set
            {
                PlayerStats.operPreferences = value;
            }
        }
        public List<string> globalPreferences
        {
            get
            {
                return PlayerStats.globalPreferences;
            }
            set
            {
                PlayerStats.globalPreferences = value;
            }
        }
        public int packs
        {
            get 
            {
                return PlayerStats.packs;
            }
            set 
            {
                PlayerStats.packs = value;
            }
        }

        public List<string> openedCustoms
        {
            get
            {
                return PlayerStats.openedCustoms;
            }
            set
            {
                PlayerStats.openedCustoms = value;
            }
        }

        public List<String> SkeyBindings
        {
            get
            {
                return PlayerStats.SkeyBindings;
            }
            set
            {
                PlayerStats.SkeyBindings = value;
            }
        }
        public List<String> SkeyBindingsAlternate
        {
            get
            {
                return PlayerStats.SkeyBindingsAlternate;
            }
            set
            {
                PlayerStats.SkeyBindingsAlternate = value;
            }
        }

        public List<string> usedCodes
        {
            get
            {
                return PlayerStats.usedCodes;
            }
            set
            {
                PlayerStats.usedCodes = value;
            }
        }
        
        public int exp
        {
            get
            {
                return PlayerStats.exp;
            }
            set
            {
                PlayerStats.exp = value;
            }
        }
        public List<int> rankedHistory
        {
            get
            {
                return PlayerStats.rankedHistory;
            }
            set
            {
                PlayerStats.rankedHistory = value;
            }
        }

        public int ELO
        {
            get
            {
                return PlayerStats.ELO;
            }
            set
            {
                if(value < 1000)
                {
                    value = 1000;
                }


                PlayerStats.ELO = value;
            }
        }

        public float Volume
        {
            get
            {
                return PlayerStats.volume;
            }
            set
            {
                if(value < 0)
                {
                    value = 0;
                }
                if(value > 1)
                {
                    value = 1;
                }

                PlayerStats.volume = (float)Math.Round(value, 2);
            }
        }

        public float Screenshake
        {
            get
            {
                return PlayerStats.screenshake;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                if (value > 1)
                {
                    value = 1;
                }

                PlayerStats.screenshake = (float)Math.Round(value, 2);
            }
        }

        public float TabScale
        {
            get
            {
                return PlayerStats.TabScale;
            }
            set
            {
                if (value < 0.25f)
                {
                    value = 0.25f;
                }
                if (value > 1)
                {
                    value = 1;
                }

                PlayerStats.TabScale = (float)Math.Round(value, 2);
            }
        }   

        public bool AimAssist
        {
            get
            {
                return PlayerStats.aimAssist;
            }
            set
            {
                PlayerStats.aimAssist = value;
            }
        }
        public bool DebugSpeed
        {
            get
            {
                return PlayerStats.DebugSpeed;
            }
            set
            {
                PlayerStats.DebugSpeed = value;
            }
        }
        public bool CycleCameras
        {
            get
            {
                return PlayerStats.CycleCameras;
            }
            set
            {
                PlayerStats.CycleCameras = value;
            }
        }
        public bool hideKeysIcons
        {
            get
            {
                return PlayerStats.hideKeyBindIcons;
            }
            set
            {
                PlayerStats.hideKeyBindIcons = value;
            }
        }

        public bool showNicknames
        {
            get
            {
                return PlayerStats.shownickname;
            }
            set
            {
                PlayerStats.shownickname = value;
            }
        }
    }
}
