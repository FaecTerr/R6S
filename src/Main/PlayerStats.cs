using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace DuckGame.R6S
{
    public class PlayerStats
    {
        public static StatsData _data;

        public static bool _SetLan;

        //Options
        public static bool firstLaunch;

        //Stats
        public static int renown;
        public static int materials;
        public static List<int> openedOperators = new List<int>();
        public static List<string> openedCustoms = new List<string>();
        public static List<string> usedCodes = new List<string>();
        public static List<int> rankedHistory = new List<int>();

        public static float screenshake;
        public static bool shownickname = true;
        public static float volume;
        public static bool hideKeyBindIcons;
        public static bool aimAssist = true;
        public static bool DebugSpeed;
        public static bool CycleCameras = true;
        public static float TabScale = 1f;

        public static int packs;
        public static int exp;
        public static string lastPlayed;

        public static int totalAttackers = 33; 
        public static int totalDefenders = 33; 

        //Bindings
        //[0] - Jump; [1] - Crouch; [2] - Left; [3] - Right; [4] - Interact; [5] - Plant; [6] - Prone; [7] - Primary; [8] - Secondary; [9] - MainDevice;
        //[10] - Secondary device; [11] - Phone; [12] - Drone; [13] - Shoot; [14] - Aim down side; [15] - Prev cam; [16] - Next cam; 
        //[17] - Prev category; [18] - Next category; [19] - Sprint; [20] - Reload; [21] - Melee, [22] - Change fire mode, [23] - Drop defuser, [24] - Tab info
        //[25] - Ping
        public static List<Keys> keyBindings = new List<Keys>(30);
        public static List<Keys> keyBindingsAlternate = new List<Keys>(30);

        public static List<String> SkeyBindings = new List<String>(30);
        public static List<String> SkeyBindingsAlternate = new List<String>(30);

        //Saves
        //public static List<int> operIDpreferences = new List<int>(5); 
        //[0] - Num of prefered gun, [1] - Prefered scope/reticle, [2] - Prefered muzzle, [3] - Prefered grip, [4] - Prefered undergrip, [5] - Prefered skin??
        //[6] - Num of prefered secondary, [7] - Prefered secondary scope/reticle, [8] - Prefered secondary muzzle, [9] - Prefered secondary grip, [10] - Prefered secondary undergrip
        //[11] - Prefered secondary skin??, [12] - Prefered device, [13] - Prefered head gear, [14] - Prefered body gear, [15] - Prefered device skin??, [16] - prefered primary 2 skin, 
        //[17] - prefered primary 3 skin, [18] - prefered secondary 2 skin, [19] - prefered primary skin
        public static List<string> operPreferences = new List<string>(20 * 34); //20 strings to save for each of 34 operators - 0.39kb if every string have 1 symbol 
        public static List<string> globalPreferences = new List<string>(20); //[0] - Banner, [1] - Accessory

        public static int ELO = 2500;

        public static int totalOperators = 65; 
        public static int totalOperatorsPlanned = 66;

        static PlayerStats()
        {
            _data = new StatsData();
            //Load();
            firstLaunch = true;
        }

        public PlayerStats()
        {
        }

        public static StatsData Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public static void Save()
        {
            DuckXML duckXML = new DuckXML();
            DXMLNode data = new DXMLNode("Data");
            data.Add(_data.Serialize());
            duckXML.Add(data);
            string path = DuckFile.optionsDirectory + "/R7Stats.dat";
            SaveDuckXML(duckXML, path);
        }

        public static void SaveDuckXML(DuckXML doc, string path)
        {
            DevConsole.Log("Stats saved to " + path, Color.White);
            DuckFile.CreatePath(Path.GetDirectoryName(path));
            try
            {
                if (File.Exists(path))
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                }
            }
            catch (Exception)
            {

            }
            try
            {
                if (File.Exists(path))
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                }
            }
            catch (Exception)
            {
            }
            string docString = doc.ToString();
            if (string.IsNullOrWhiteSpace(docString))
            {
                throw new Exception("Blank XML (" + path + ")");
            }
            File.WriteAllText(path, docString);
        }

        public static DuckXML LoadDuckXML(string path)
        {
            DevConsole.Log("Stats loaded from " + path, Color.White);
            DuckFile.CreatePath(Path.GetDirectoryName(path));
            //DuckFile.PrepareToLoadCloudFile(path);
            if (!File.Exists(path))
            {
                return null;
            }
            DuckXML result;
            try
            {
                result = DuckXML.Load(path);
            }
            catch
            {
                result = null;
            }
            return result;
        }
        
        public static void Load()
        {
            if (File.Exists(DuckFile.optionsDirectory + "R7Stats.dat"))
            {
                DuckXML xdocument = LoadDuckXML(DuckFile.optionsDirectory + "/R7Stats.dat");
                if (xdocument != null)
                {
                    new Profile("", null, null, null, false, null);

                    IEnumerable<DXMLNode> enumerable = xdocument.Elements("Data");
                    if (enumerable != null)
                    {
                        foreach (DXMLNode xelement in enumerable.Elements())
                        {
                            if (xelement.Name == "R7Stats")
                            {
                                _data.Deserialize(xelement);
                                break;
                            }
                        }
                    }
                }
            }

        }

        public static int LastPressedButton()
        {
            int i = -1;

            for (int j = 0; j < 255; j++)
            {
                if (Keyboard.Pressed((Keys)j))
                {
                    i = j;
                }
            }
            if (Keyboard.Pressed(Keys.MouseLeft))
            {
                i = 999990;
            }
            if (Keyboard.Pressed(Keys.MouseRight))
            {
                i = 999992;
            }
            if (Keyboard.Pressed(Keys.MouseMiddle))
            {
                i = 999991;
            }
            if (Keyboard.Pressed(Keys.MouseKeys))
            {
                i = 999989;
            }

            return i;
        }

        public static int GetFrameOfButtonGP(string key)
        {
            int i = 1;

            if(key == "CrossUP")
            {
                i =  232;
            }
            if (key == "CrossLEFT")
            {
                i = 233;
            }
            if (key == "CrossRIGHT")
            {
                i = 234;
            }
            if (key == "CrossDOWN")
            {
                i = 235;
            }


            if (key == "RT")
            {
                i = 236;
            }
            if (key == "RB")
            {
                i = 237;
            }
            if (key == "LT")
            {
                i = 238;
            }
            if (key == "LB")
            {
                i = 239;
            }


            if (key == "LStickUP")
            {
                i = 240;
            }
            if (key == "LStickDOWN")
            {
                i = 241;
            }
            if (key == "LStickRIGHT")
            {
                i = 242;
            }
            if (key == "LStickLEFT")
            {
                i = 243;
            }

            if (key == "RStickUP")
            {
                i = 244;
            }
            if (key == "RStickDOWN")
            {
                i = 245;
            }
            if (key == "RStickRIGHT")
            {
                i = 246;
            }
            if (key == "RStickLEFT")
            {
                i = 247;
            }


            if (key == "Select")
            {
                i = 248;
            }
            if (key == "Start")
            {
                i = 249;
            }
            if (key == "RButton")
            {
                i = 250;
            }
            if (key == "LButton")
            {
                i = 251;
            }

            if (key == "Y")
            {
                i = 252;
            }
            if (key == "X")
            {
                i = 253;
            }
            if (key == "A")
            {
                i = 254;
            }
            if (key == "B")
            {
                i = 255;
            }

            return i;
        }

        public static int GetFrameOfButton(Keys key = Keys.None)
        {
            int index = 0;
            int k = (int)key;

            if(k < 140)
            {
                index = k;
            }

            if(key == Keys.LeftShift)
            {
                index = 2;
            }
            if (key == Keys.RightShift)
            {
                index = 3;
            }
            if (key == Keys.Back)
            {
                index = 5;
            }
            if (key == Keys.Enter)
            {
                index = 6;
            }
            if (key == Keys.CapsLock)
            {
                index = 7;
            }
            if (key == Keys.Space)
            {
                index = 11;
            }
            if (key == Keys.MouseLeft)
            {
                index = 136;
            }
            if (key == Keys.MouseRight)
            {
                index = 137;
            }
            if (key == Keys.MouseMiddle)
            {
                index = 138;
            }
            if (key == Keys.MouseKeys)
            {
                index = 141;
            }
            if(key == Keys.LeftControl || key == Keys.RightControl)
            {
                index = 21;
            }
            if (key == Keys.LeftAlt || key == Keys.RightAlt)
            {
                index = 20;
            }
            if(key == Keys.None)
            {
                index = 1;
            }
            if(key == Keys.Tab)
            {
                index = 144;
            }


            return index;
        }

        public static bool GetSizeOfButton(Keys key = Keys.None)
        {
            bool wide = false;
            int k = (int)key;

            if (key == Keys.LeftShift)
            {
                wide = true;
            }
            if (key == Keys.RightShift)
            {
                wide = true;
            }
            if (key == Keys.Back)
            {
                wide = true;
            }
            if (key == Keys.Enter)
            {
                wide = true;
            }
            if (key == Keys.CapsLock)
            {
                wide = true;
            }
            if (key == Keys.Space)
            {
                wide = true;
            }

            return wide;
        }
        
        public static void ResetKeybindings()
        {
            keyBindings = new List<Keys>()
            {
                Keys.W,
                Keys.S,
                Keys.A,
                Keys.D,
                Keys.E,
                Keys.F,
                Keys.LeftControl,
                Keys.D1,
                Keys.D2,
                Keys.MouseMiddle,
                Keys.G,
                Keys.D5,
                Keys.D6,
                Keys.MouseLeft,
                Keys.MouseRight,
                Keys.Q,
                Keys.E,
                Keys.D1,
                Keys.D3,
                Keys.LeftShift,
                Keys.R,
                Keys.V,
                Keys.B,
                Keys.X,
                Keys.Tab,
                Keys.Z,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None
            };

            keyBindingsAlternate = new List<Keys>()
            {
                Keys.None,
                Keys.C,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.D3,
                Keys.D4,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.D0,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None,
                Keys.None
            };
            ConvertKeysFrom();
            Save();
        }
        
        public static void ConvertKeys()
        {
            keyBindings.Clear();
            keyBindingsAlternate.Clear();
            foreach (String s in SkeyBindings)
            {
                keyBindings.Add((Keys)Convert.ToInt32(s));
            }
            foreach (String s in SkeyBindingsAlternate)
            {
                keyBindingsAlternate.Add((Keys)Convert.ToInt32(s));
            }
        }

        public static void ConvertKeysFrom()
        {
            SkeyBindings.Clear();
            SkeyBindingsAlternate.Clear();
            foreach (Keys k in keyBindings)
            {
                SkeyBindings.Add(Convert.ToString((int)k));
            }
            foreach (Keys k in keyBindingsAlternate)
            {
                SkeyBindingsAlternate.Add(Convert.ToString((int)k));
            }
        }
        
        public static OPEQ GetOpeqByID(int operatorID, Vec2 position)
        {
            OPEQ op = new OPEQ(position.x, position.y);

            if (operatorID == -2)
            {
                op = new RecruitDefOPEQ(position.x, position.y);
            }
            if (operatorID == -1)
            {
                op = new RecruitAttOPEQ(position.x, position.y);
            }

            if (operatorID == 0)
            {
                op = new JagerOPEQ(position.x, position.y);
            }
            if (operatorID == 1)
            {
                op = new BlitzOPEQ(position.x, position.y);
            }
            if (operatorID == 2)
            {
                op = new BanditOPEQ(position.x, position.y);
            }
            if (operatorID == 3)
            {
                op = new IQOPEQ(position.x, position.y);
            }

            if (operatorID == 4)
            {
                op = new TachankinOPEQ(position.x, position.y);
            }
            if (operatorID == 5)
            {
                op = new GlazOPEQ(position.x, position.y);
            }
            if (operatorID == 6)
            {
                op = new KapkanOPEQ(position.x, position.y);
            }
            if (operatorID == 7)
            {
                op = new FuzeOPEQ(position.x, position.y);
            }

            if (operatorID == 8)
            {
                op = new SmokeOPEQ(position.x, position.y);
            }
            if (operatorID == 9)
            {
                op = new ThatcherOPEQ(position.x, position.y);
            }
            if (operatorID == 10)
            {
                op = new MuteOPEQ(position.x, position.y);
            }
            if (operatorID == 11)
            {
                op = new SledgeOPEQ(position.x, position.y);
            }

            if (operatorID == 12)
            {
                op = new RookOPEQ(position.x, position.y);
            }
            if (operatorID == 13)
            {
                op = new MontagneOPEQ(position.x, position.y);
            }
            if (operatorID == 14)
            {
                op = new DocOPEQ(position.x, position.y);
            }
            if (operatorID == 15)
            {
                op = new TwitchOPEQ(position.x, position.y);
            }

            if (operatorID == 16)
            {
                op = new PulseOPEQ(position.x, position.y);
            }
            if (operatorID == 17)
            {
                op = new AshOPEQ(position.x, position.y);
            }
            if (operatorID == 18)
            {
                op = new CastleOPEQ(position.x, position.y);
            }
            if (operatorID == 19)
            {
                op = new ThermiteOPEQ(position.x, position.y);
            }

            if (operatorID == 20)
            {
                op = new ValkyrieOPEQ(position.x, position.y);
            }
            if (operatorID == 21)
            {
                op = new MaverickOPEQ(position.x, position.y);
            }

            if (operatorID == 22)
            {
                op = new EchoOPEQ(position.x, position.y);
            }
            if (operatorID == 23)
            {
                op = new CapitaoOPEQ(position.x, position.y);
            }

            if (operatorID == 24)
            {
                op = new LesionOPEQ(position.x, position.y);
            }
            if (operatorID == 25)
            {
                op = new JackalOPEQ(position.x, position.y);
            }

            if (operatorID == 26)
            {
                op = new ElaOPEQ(position.x, position.y);
            }
            if (operatorID == 27)
            {
                op = new ZofiaOPEQ(position.x, position.y);
            }

            if (operatorID == 28)
            {
                op = new VigilOPEQ(position.x, position.y);
            }
            if (operatorID == 29)
            {
                op = new DokkaebiOPEQ(position.x, position.y);
            }

            if(operatorID == 30)
            {
                op = new AlibiOPEQ(position.x, position.y);
            }
            if (operatorID == 31)
            {
                op = new FinkaOPEQ(position.x, position.y);
            }

            if (operatorID == 32)
            {
                op = new KaidOPEQ(position.x, position.y);
            }
            if (operatorID == 33)
            {
                op = new NomadOPEQ(position.x, position.y);
            }

            if (operatorID == 34)
            {
                op = new MozzieOPEQ(position.x, position.y);
            }
            if (operatorID == 35)
            {
                op = new GridlockOPEQ(position.x, position.y);
            }

            if (operatorID == 36)
            {
                op = new GoyoOPEQ(position.x, position.y);
            }
            if (operatorID == 37)
            {
                op = new YingOPEQ(position.x, position.y);
            }

            if (operatorID == 38)
            {
                op = new WardenOPEQ(position.x, position.y);
            }
            if (operatorID == 39)
            {
                op = new NokkOPEQ(position.x, position.y);
            }

            if (operatorID == 40)
            {
                op = new FrostOPEQ(position.x, position.y);
            }
            if (operatorID == 41)
            {
                op = new BuckOPEQ(position.x, position.y);
            }

            if (operatorID == 42)
            {
                op = new CaveiraOPEQ(position.x, position.y);
            }
            if (operatorID == 43)
            {
                op = new BlackbeardOPEQ(position.x, position.y);
            }

            if (operatorID == 44)
            {
                op = new MiraOPEQ(position.x, position.y);
            }
            if (operatorID == 45)
            {
                op = new HibanaOPEQ(position.x, position.y);
            }

            if (operatorID == 46)
            {
                op = new MaestroOPEQ(position.x, position.y);
            }

            if (operatorID == 47)
            {
                op = new LionOPEQ(position.x, position.y);
            }
            if (operatorID == 48)
            {
                op = new ClashOPEQ(position.x, position.y);
            }
            if (operatorID == 49)
            {
                op = new AmaruOPEQ(position.x, position.y);
            }
            if (operatorID == 50)
            {
                op = new MelusiOPEQ(position.x, position.y);
            }
            if (operatorID == 51)
            {
                op = new ZeroOPEQ(position.x, position.y);
            }
            if (operatorID == 53)
            {
                op = new AceOPEQ(position.x, position.y);
            }
            if (operatorID == 54)
            {
                op = new ThunderbirdOPEQ(position.x, position.y);
            }
            if (operatorID == 55)
            {
                op = new FloresOPEQ(position.x, position.y);
            }
            if (operatorID == 64)
            {
                op = new SolisOPEQ(position.x, position.y);
            }

            return op;
        }
        public static OPEQ GetOpeqByCallID(int operatorID, Vec2 position)
        {
            OPEQ op = new OPEQ(position.x, position.y);

            if (operatorID == -2)
            {
                op = new RecruitDefOPEQ(position.x, position.y);
            }
            if (operatorID == -1)
            {
                op = new RecruitAttOPEQ(position.x, position.y);
            }

            if (operatorID == 0)
            {
                op = new JagerOPEQ(position.x, position.y);
            }
            if (operatorID == 1)
            {
                op = new BlitzOPEQ(position.x, position.y);
            }
            if (operatorID == 2)
            {
                op = new BanditOPEQ(position.x, position.y);
            }
            if (operatorID == 3)
            {
                op = new IQOPEQ(position.x, position.y);
            }

            if (operatorID == 4)
            {
                op = new TachankinOPEQ(position.x, position.y);
            }
            if (operatorID == 5)
            {
                op = new GlazOPEQ(position.x, position.y);
            }
            if (operatorID == 6)
            {
                op = new KapkanOPEQ(position.x, position.y);
            }
            if (operatorID == 7)
            {
                op = new FuzeOPEQ(position.x, position.y);
            }

            if (operatorID == 8)
            {
                op = new SmokeOPEQ(position.x, position.y);
            }
            if (operatorID == 9)
            {
                op = new ThatcherOPEQ(position.x, position.y);
            }
            if (operatorID == 10)
            {
                op = new MuteOPEQ(position.x, position.y);
            }
            if (operatorID == 11)
            {
                op = new SledgeOPEQ(position.x, position.y);
            }

            if (operatorID == 12)
            {
                op = new RookOPEQ(position.x, position.y);
            }
            if (operatorID == 13)
            {
                op = new MontagneOPEQ(position.x, position.y);
            }
            if (operatorID == 14)
            {
                op = new DocOPEQ(position.x, position.y);
            }
            if (operatorID == 15)
            {
                op = new TwitchOPEQ(position.x, position.y);
            }

            if (operatorID == 16)
            {
                op = new PulseOPEQ(position.x, position.y);
            }
            if (operatorID == 17)
            {
                op = new AshOPEQ(position.x, position.y);
            }
            if (operatorID == 18)
            {
                op = new CastleOPEQ(position.x, position.y);
            }
            if (operatorID == 19)
            {
                op = new ThermiteOPEQ(position.x, position.y);
            }

            if (operatorID == 20)
            {
                op = new FrostOPEQ(position.x, position.y);
            }
            if (operatorID == 21)
            {
                op = new BuckOPEQ(position.x, position.y);
            }

            if (operatorID == 22)
            {
                op = new ValkyrieOPEQ(position.x, position.y);
            }
            if (operatorID == 23)
            {
                op = new BlackbeardOPEQ(position.x, position.y);
            }

            if (operatorID == 24)
            {
                op = new CaveiraOPEQ(position.x, position.y);
            }
            if (operatorID == 25)
            {
                op = new CapitaoOPEQ(position.x, position.y);
            }

            if (operatorID == 26)
            {
                op = new EchoOPEQ(position.x, position.y);
            }
            if (operatorID == 27)
            {
                op = new HibanaOPEQ(position.x, position.y);
            }

            if (operatorID == 28)
            {
                op = new MiraOPEQ(position.x, position.y);
            }
            if (operatorID == 29)
            {
                op = new JackalOPEQ(position.x, position.y);
            }

            if (operatorID == 30)
            {
                op = new LesionOPEQ(position.x, position.y);
            }
            if (operatorID == 31)
            {
                op = new YingOPEQ(position.x, position.y);
            }

            if (operatorID == 32)
            {
                op = new ElaOPEQ(position.x, position.y);
            }
            if (operatorID == 33)
            {
                op = new ZofiaOPEQ(position.x, position.y);
            }

            if (operatorID == 34)
            {
                op = new VigilOPEQ(position.x, position.y);
            }
            if (operatorID == 35)
            {
                op = new DokkaebiOPEQ(position.x, position.y);
            }

            if (operatorID == 36)
            {
                op = new FinkaOPEQ(position.x, position.y);
            }
            if (operatorID == 37)
            {
                op = new LionOPEQ(position.x, position.y);
            }

            if (operatorID == 38)
            {
                op = new AlibiOPEQ(position.x, position.y);
            }
            if (operatorID == 39)
            {
                op = new MaestroOPEQ(position.x, position.y);
            }

            if (operatorID == 40)
            {
                op = new ClashOPEQ(position.x, position.y);
            }
            if (operatorID == 41)
            {
                op = new MaverickOPEQ(position.x, position.y);
            }

            if (operatorID == 42)
            {
                op = new KaidOPEQ(position.x, position.y);
            }
            if (operatorID == 43)
            {
                op = new NomadOPEQ(position.x, position.y);
            }
            if (operatorID == 44)
            {
                op = new GridlockOPEQ(position.x, position.y);
            }
            if (operatorID == 45)
            {
                op = new MozzieOPEQ(position.x, position.y);
            }

            if (operatorID == 46)
            {
                op = new WardenOPEQ(position.x, position.y);
            }
            if (operatorID == 47)
            {
                op = new NokkOPEQ(position.x, position.y);
            }

            if (operatorID == 48)
            {
                op = new GoyoOPEQ(position.x, position.y);
            }
            if (operatorID == 49)
            {
                op = new AmaruOPEQ(position.x, position.y);
            }

            /*if (operatorID == 50)
            {
                op = new OryxOPEQ(position.x, position.y);
            }
            if (operatorID == 51)
            {
                op = new IanaOPEQ(position.x, position.y);
            }*/

            if (operatorID == 52)
            {
                op = new MelusiOPEQ(position.x, position.y);
            }
            if (operatorID == 53)
            {
                op = new AceOPEQ(position.x, position.y);
            }

            if (operatorID == 54)
            {
                op = new ZeroOPEQ(position.x, position.y);
            }
            /*if (operatorID == 55)
            {
                op = new AruniOPEQ(position.x, position.y);
            }*/

            if (operatorID == 56)
            {
                op = new FloresOPEQ(position.x, position.y);
            }
            if (operatorID == 57)
            {
                op = new ThunderbirdOPEQ(position.x, position.y);
            }

            /*if (operatorID == 58)
            {
                op = new OsaOPEQ(position.x, position.y);
            }
            if (operatorID == 59)
            {
                op = new ThornOPEQ(position.x, position.y);
            }*/

            /*if (operatorID == 60)
            {
                op = new AzamiOPEQ(position.x, position.y);
            }
            if (operatorID == 61)
            {
                op = new SensOPEQ(position.x, position.y);
            }*/
            if (operatorID == 64)
            {
                op = new SolisOPEQ(position.x, position.y);
            }

            return op;
        }
        public static OPEQ GetAttackerByCallID(int operatorID, Vec2 position)
        {
            OPEQ op = new OPEQ(position.x, position.y);

            if (operatorID == -1)
            {
                op = new RecruitAttOPEQ(position.x, position.y);
            }


            if (operatorID == 0)
            {
                op = new BlitzOPEQ(position.x, position.y);
            }
            if (operatorID == 1)
            {
                op = new IQOPEQ(position.x, position.y);
            }
            if (operatorID == 2)
            {
                op = new GlazOPEQ(position.x, position.y);
            }
            if (operatorID == 3)
            {
                op = new FuzeOPEQ(position.x, position.y);
            }


            if (operatorID == 4)
            {
                op = new ThatcherOPEQ(position.x, position.y);
            }
            if (operatorID == 5)
            {
                op = new SledgeOPEQ(position.x, position.y);
            }
            if (operatorID == 6)
            {
                op = new MontagneOPEQ(position.x, position.y);
            }
            if (operatorID == 7)
            {
                op = new TwitchOPEQ(position.x, position.y);
            }

            if (operatorID == 8)
            {
                op = new AshOPEQ(position.x, position.y);
            }
            if (operatorID == 9)
            {
                op = new ThermiteOPEQ(position.x, position.y);
            }

            if (operatorID == 10)
            {
                op = new BuckOPEQ(position.x, position.y);
            }
            if (operatorID == 11)
            {
                op = new BlackbeardOPEQ(position.x, position.y);
            }


            if (operatorID == 12) 
            {
                op = new CapitaoOPEQ(position.x, position.y);
            }
            if (operatorID == 13)
            {
                op = new HibanaOPEQ(position.x, position.y);
            }
            if (operatorID == 14) 
            {
                op = new JackalOPEQ(position.x, position.y);
            }
            if (operatorID == 15)
            {
                op = new YingOPEQ(position.x, position.y);
            }


            if (operatorID == 16)
            {
                op = new ZofiaOPEQ(position.x, position.y);
            }
            if (operatorID == 17)
            {
                op = new DokkaebiOPEQ(position.x, position.y);
            }
            if (operatorID == 18)
            {
                op = new FinkaOPEQ(position.x, position.y);
            }
            if (operatorID == 19)
            {
                op = new LionOPEQ(position.x, position.y);
            }


            if (operatorID == 20) 
            {
                op = new MaverickOPEQ(position.x, position.y);
            }
            if (operatorID == 21) 
            {
                op = new NomadOPEQ(position.x, position.y);
            }
            if (operatorID == 22)
            {
                op = new GridlockOPEQ(position.x, position.y);
            }
            if (operatorID == 23) 
            {
                op = new NokkOPEQ(position.x, position.y);
            }


            if (operatorID == 24)
            {
                op = new AmaruOPEQ(position.x, position.y);
            }

            /*if (operatorID == 25)
            {
                op = new IanaOPEQ(position.x, position.y);
            }*/
            if (operatorID == 26)
            {
                op = new AceOPEQ(position.x, position.y);
            }
            if (operatorID == 27)
            {
                op = new ZeroOPEQ(position.x, position.y);
            }

            if (operatorID == 28)
            {
                op = new FloresOPEQ(position.x, position.y);
            }
            /*if (operatorID == 29)
            {
                op = new OsaOPEQ(position.x, position.y);
            }*/

            /*if (operatorID == 30)
            {
                op = new SensOPEQ(position.x, position.y);
            }*/

            return op;
        }
        public static OPEQ GetDefenderByCallID(int operatorID, Vec2 position)
        {
            OPEQ op = new OPEQ(position.x, position.y);

            if (operatorID == -1)
            {
                op = new RecruitDefOPEQ(position.x, position.y);
            }

            if (operatorID == 0)
            {
                op = new JagerOPEQ(position.x, position.y);
            }
            if (operatorID == 1)
            {
                op = new BanditOPEQ(position.x, position.y);
            }
            if (operatorID == 2)
            {
                op = new TachankinOPEQ(position.x, position.y);
            }
            if (operatorID == 3)
            {
                op = new KapkanOPEQ(position.x, position.y);
            }

            if (operatorID == 4)
            {
                op = new SmokeOPEQ(position.x, position.y);
            }
            if (operatorID == 5)
            {
                op = new MuteOPEQ(position.x, position.y);
            }
            if (operatorID == 6)
            {
                op = new RookOPEQ(position.x, position.y);
            }
            if (operatorID == 7)
            {
                op = new DocOPEQ(position.x, position.y);
            }

            if (operatorID == 8)
            {
                op = new PulseOPEQ(position.x, position.y);
            }
            if (operatorID == 9)
            {
                op = new CastleOPEQ(position.x, position.y);
            }
            if (operatorID == 10)
            {
                op = new FrostOPEQ(position.x, position.y);
            }
            if (operatorID == 11)
            {
                op = new ValkyrieOPEQ(position.x, position.y);
            }


            if (operatorID == 12)
            {
                op = new CaveiraOPEQ(position.x, position.y);
            }
            if (operatorID == 13)
            {
                op = new EchoOPEQ(position.x, position.y);
            }
            if (operatorID == 14)
            {
                op = new MiraOPEQ(position.x, position.y);
            }
            if (operatorID == 15) 
            {
                op = new LesionOPEQ(position.x, position.y);
            }


            if (operatorID == 16) 
            {
                op = new ElaOPEQ(position.x, position.y);
            }
            if (operatorID == 17) 
            {
                op = new VigilOPEQ(position.x, position.y);
            }
            if (operatorID == 18) 
            {
                op = new AlibiOPEQ(position.x, position.y);
            }
            if (operatorID == 19)
            {
                op = new MaestroOPEQ(position.x, position.y);
            }

            if (operatorID == 20)
            {
                op = new ClashOPEQ(position.x, position.y);
            }
            if (operatorID == 21)
            {
                op = new KaidOPEQ(position.x, position.y);
            }
            if (operatorID == 22)
            {
                op = new MozzieOPEQ(position.x, position.y);
            }
            if (operatorID == 23)
            {
                op = new WardenOPEQ(position.x, position.y);
            }

            if (operatorID == 24)
            {
                op = new GoyoOPEQ(position.x, position.y);
            }
            /*if (operatorID == 25)
            {
                op = new OryxOPEQ(position.x, position.y);
            }*/
            if (operatorID == 26)
            {
                op = new MelusiOPEQ(position.x, position.y);
            }
            /*if (operatorID == 27)
            {
                op = new AruniOPEQ(position.x, position.y);
            }*/

            if (operatorID == 28)
            {
                op = new ThunderbirdOPEQ(position.x, position.y);
            }
            /*if (operatorID == 29)
            {
                op = new ThornOPEQ(position.x, position.y);
            }*/
            /*if (operatorID == 30)
            {
                op = new AzamiOPEQ(position.x, position.y);
            }*/
            if (operatorID == 31)
            {
                op = new SolisOPEQ(position.x, position.y);
            }

            return op;
        }

        public static void Update()
        {          
            firstLaunch = Data.firstLaunch;
            renown = Data.renown;
            ELO = Data.ELO;
            openedOperators = Data.openedOperators;
            openedCustoms = Data.openedCustoms;
            operPreferences = Data.operPreferences;
            usedCodes = Data.usedCodes;
            SkeyBindings = Data.SkeyBindings;
            SkeyBindingsAlternate = Data.SkeyBindingsAlternate;

            if(globalPreferences == null || (globalPreferences != null && globalPreferences.Count <= 0))
            {
                globalPreferences = new List<string>(20);
                globalPreferences.Clear();
                for (int i = 0; i < 20; i++)
                {
                    globalPreferences.Add("");
                    //globalPreferences[i] = "";
                }
            }

            if(SkeyBindings.Count < 24)
            {
                ResetKeybindings();
                ConvertKeysFrom();
                Save();
            }
            if(keyBindings.Count < 24)
            {
                ConvertKeys();
            }
        }
    }
}
