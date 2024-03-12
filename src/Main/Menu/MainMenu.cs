using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace DuckGame.R6S
{
    public class MainMenu : Level
    {
        public List<User> users = new List<User>();

        public bool hosted;

        public Lobby _hostedLobby;

        private int pages = 5;
        public int page = 0;
        public float nextPage;

        public int updateFriends;

        int ingame = 0;
        int online = 0;
        int offline = 0;

        int lvl = 0;

        public int partysize;

        public override void Initialize()
        {
            Load();
            base.Initialize();
        }

        public virtual void Load()
        {
            foreach (Thing t in current.things[typeof(Thing)])
            {
                Remove(t);
            }

            if (!Music.currentSong.Contains("SFX/Music/CupcakeandDill.ogg"))
            {
                Music.Load(Mod.GetPath<R6S>("SFX/Music/CupcakeandDill.ogg"), true);
                Music.PlayLoaded();
            }
            DevConsole.Log(Music.currentSong.ToString());

            Add(new CategoryButton(20, 10) { screen = 1 });
            Add(new CategoryButton(20 + 30, 10) { screen = 2 });
            Add(new CategoryButton(20 + 60, 10) { screen = 3 });

            Add(new SettingsButton(320 - 8, 8));

            for (int i = 0; i < pages; i++)
            {
                Add(new ChangePage(250 - 10 * (pages - 1) / 2 + 10 * i, 168) { page = i });
            }

            Add(new NewsPageButton(250, 128));

            int exp = PlayerStats.exp;
            if (exp > 100)
            {
                lvl = 1;
                if (exp > 200)
                {
                    lvl = 2;
                    if (exp > 300)
                    {
                        lvl = 3;
                        if (exp > 400)
                        {
                            lvl = 4;
                            if (exp > 500)
                            {
                                lvl = 5;
                                if (exp > 750)
                                {
                                    lvl = 6;
                                    if (exp > 1000)
                                    {
                                        lvl = 7;
                                        if (exp > 1250)
                                        {
                                            lvl = 8;
                                            if (exp > 1500)
                                            {
                                                lvl = 9;
                                                if (exp > 1750)
                                                {
                                                    lvl = 10;
                                                    if (exp > 2000)
                                                    {
                                                        lvl = 11;
                                                        if (exp > 2500)
                                                        {
                                                            lvl = 12;
                                                            if (exp > 3000)
                                                            {
                                                                lvl = 13;
                                                                if (exp > 3500)
                                                                {
                                                                    lvl = 14;
                                                                    if (exp > 4000)
                                                                    {
                                                                        lvl = 15;
                                                                        if (exp > 4500)
                                                                        {
                                                                            lvl = 16;
                                                                            if (exp > 5000)
                                                                            {
                                                                                lvl = 17;
                                                                                if (exp > 5500)
                                                                                {
                                                                                    lvl = 18;
                                                                                    if (exp > 6000)
                                                                                    {
                                                                                        lvl = 19;
                                                                                        if (exp > 6500)
                                                                                        {
                                                                                            lvl = 20;
                                                                                            if (exp > 7000)
                                                                                            {
                                                                                                lvl = 21;
                                                                                                if (exp > 7500)
                                                                                                {
                                                                                                    lvl = 22;
                                                                                                    if (exp > 8000)
                                                                                                    {
                                                                                                        lvl = 23;
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if(PlayerStats.exp > 9999)
            {
                PlayerStats.exp = 9999;
            }
            //Add(new ChangePage(246, 168) { page = 0 });
            //Add(new ChangePage(254, 168) { page = 1 });
            DateTime lp = Convert.ToDateTime(PlayerStats.lastPlayed);
            DateTime now = DateTime.Now;
            if (lp.Month != now.Month || lp.Year != now.Year)
            {
                PlayerStats.packs += lvl;
                PlayerStats.exp = 0;
            }
            if (lp.Day != now.Day || lp.Month != now.Month || lp.Year != now.Year)
            {
                PlayerStats.exp += 50;
            }
            PlayerStats.lastPlayed = now.ToString();
            PlayerStats.Save();
        }

        public MainMenu() : base()
        {
            Layer.Game.fade = 0f;
            Layer.Foreground.fade = 0f;

            Load();

            /*User local = null;
            foreach(Profile p in Profiles.allCustomProfiles)
            {
                if (local == null)
                {
                    local = User.GetUser(p.steamID);
                }
            }
            if (local != null)
            {
                users.Add(local);
                //DevConsole.Log(local.name);
            }*/
        }


        public override void Update()
        {
            foreach(Duck d in current.things[typeof(Duck)])
            {
                d.position = new Vec2(-9999999f, -9999999f);
            }
            foreach (Thing t in current.things[typeof(Thing)])
            {
                if (!(t is Button || t is SettingsButton || t is NewsPageButton || t is CategoryButton || t is ChangePage))
                {
                    Remove(t);
                }
            }
            

            bool load = true;
            foreach(SettingsButton s in current.things[typeof(SettingsButton)])
            {
                load = false;
            }
            if (load)
            {
                Load();
            }

            if (nextPage < 3)
            {
                nextPage += 0.01f;
            }
            else
            {
                page++;
                nextPage = 0;
                if(page >= pages)
                {
                    page = 0;
                }
            }

            if (Keyboard.Pressed(Keys.F3))
            {
                //current = new TutorialLevel(Mod.GetPath<R6S>("Levels/[R6S]Tutorial.lev"));
                current = new TestArea(new Editor(), Mod.GetPath<R6S>("Levels/[R6S]Tutorial.lev"));
                current.AddThing(new EditorTestLevel(new Editor()));
            }

            /*if (_hostedLobby == null)
            {
                DuckNetwork.Host(4, NetworkLobbyType.Public);
                //_hostedLobby.SetLobbyModsData("");
                
                //_hostedLobby = Network.activeNetwork.core.lobby;
                //DuckNetwork.Host(4, NetworkLobbyType.Public);
            }
            else
            {
                if (updateFriends % 60 == 0)
                {
                    if (partysize != _hostedLobby.users.Count)
                    {
                        if(partysize > _hostedLobby.users.Count)
                        {
                            // !TODO! connect sound
                        }
                        else
                        {
                            // !TODO! disconnect sound
                        }
                        partysize = _hostedLobby.users.Count;
                    }
                    foreach (User user in _hostedLobby.users)
                    {
                        if (!users.Contains(user))
                        {
                            users.Add(user);
                        }
                    }
                }
            }*/
          

            /*if (!hosted)
            {
                Network.HostServer(NetworkLobbyType.Public, 4, "R6S mod Server", 1337);
                //Network.activeNetwork.core.lobby.SetLobbyModsData("R6S MOD");
                Network.activeNetwork.core.lobby.SetLobbyData("customLevels", "100");

                GhostManager.context.SetGhostIndex(32);
                int index = 0;
                List<ProfileBox2> _profiles = new List<ProfileBox2>();
                foreach (ProfileBox2 profileBox in _profiles)
                {
                    profileBox.ChangeProfile(DuckNetwork.profiles[index]);
                    index++;
                }

                things.RefreshState();
                foreach (Thing t in things)
                {
                    t.DoNetworkInitialize();
                    if (Network.isServer && t.isStateObject)
                    {
                        GhostManager.context.MakeGhost(t, -1, false);
                    }
                }
            }*/

            if (Keyboard.Pressed(Keys.Escape))
            {
                current = new TitleScreen();
            }

            camera.position = new Vec2(0, 0);

            base.Update();


            /*foreach (Duck d in current.things[typeof(Duck)])
            {
                d.position = new Vec2(-9999999f, -9999999f);
            }
            foreach (Thing t in current.things[typeof(Thing)])
            {
                if (!(t is Button || t is SettingsButton || t is NewsPageButton || t is CategoryButton))
                {
                    Remove(t);
                }
            }*/
        }

        public override void PostDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Foreground)
            {
                Vec2 camPos = current.camera.position;
                SpriteMap _renown = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/Renown.png"), 32, 32);
                _renown.CenterOrigin();
                _renown.scale = new Vec2(0.2f, 0.2f);

                Graphics.Draw(_renown, current.camera.position.x + 272, current.camera.position.y + 5, 1);
                _renown.frame = 1;
                Graphics.Draw(_renown, current.camera.position.x + 272, current.camera.position.y + 12, 1);

                SpriteMap _key = new SpriteMap(Mod.GetPath<R6S>("Sprites/keys.png"), 17, 17);
                _key.CenterOrigin();
                _key.frame = PlayerStats.GetFrameOfButton(Keys.Escape);


                Graphics.DrawRect(camPos, camPos + new Vec2(320, 16), Color.Black * 0.2f,  -0.2f);

                Graphics.Draw(_key, 16 + camPos.x, 169 + camPos.y, 1f);
                Graphics.DrawStringOutline("EXIT", camPos + new Vec2(30, 166), Color.White, Color.Black, 1f, null, 1f);

                Graphics.DrawStringOutline(Convert.ToString(PlayerStats.renown), new Vec2(current.camera.position.x + 280, current.camera.position.y + 4), Color.Yellow, Color.Black, 1f, null, 0.25f);
                Graphics.DrawStringOutline(Convert.ToString(PlayerStats.materials), new Vec2(current.camera.position.x + 280, current.camera.position.y + 12), Color.Yellow, Color.Black, 1f, null, 0.25f);




                SpriteMap _elo = new SpriteMap(Mod.GetPath<R6S>("Sprites/RankIcons.png"), 23, 23);
                _elo.CenterOrigin();
                _elo.scale = new Vec2(0.4f, 0.4f);

                int elo = PlayerStats.ELO;

                /*if (PlayerStats.rankedHistory.Count >= 10)
                {
                    lvl = 1;
                    if (elo >= 1000 && elo < 2600)
                    {
                        lvl = (elo - 1000) / 100 + 1;
                    }
                    if (elo >= 2600 && elo < 3800)
                    {
                        lvl = (elo - 1000) / 200 + 8;
                    }
                    if (elo >= 3800 && elo < 4200)
                    {
                        lvl = 22;
                    }
                    if (elo >= 4200)
                    {
                        lvl = 23;
                    }
                }*/
                _elo.frame = lvl;

                if (Keyboard.Pressed(Keys.Add))
                {
                    PlayerStats.ELO += 50;
                }
                if (Keyboard.Pressed(Keys.Subtract))
                {
                    PlayerStats.ELO -= 50;
                }

                Graphics.Draw(_elo, current.camera.position.x + 254, current.camera.position.y + 6, 1);
                Graphics.DrawString(Convert.ToString(PlayerStats.exp), new Vec2(248, 12), Color.White, 0.4f, null, 0.4f);

                int k = 0;
                foreach (User u in users)
                {

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

                    if (avatarMedium != null && avatarMedium.Length == 16384 && avatar == null)
                    {
                        Texture2D texture2D = new Texture2D(Graphics.device, 64, 64);
                        texture2D.SetData(avatarMedium);
                        avatar = new Sprite(texture2D, 0.0f, 0.0f);
                        avatar.CenterOrigin();
                    }

                    Graphics.Draw(avatar.texture, current.camera.position + new Vec2(236f - 14 * k - 11.5f * 0.25f, 9f - 11.5f * 0.45f), null, Color.White, 0, new Vec2(), new Vec2(0.359375f, 0.359375f) * 0.45f, SpriteEffects.None, 0.3f);
                    k++;
                }
                while (k < 4)
                {
                    SpriteMap _openSlot = new SpriteMap(Mod.GetPath<R6S>("Sprites/ModMenu/PlayersIcon.png"), 23, 23);
                    _openSlot.depth = 0.3f;
                    _openSlot.CenterOrigin();
                    _openSlot.scale *= 0.45f;
                    Graphics.Draw(_openSlot, camera.position.x + 236 - 14 * k, camera.position.y + 9);
                    k++;
                }


                SpriteMap _cursor = new SpriteMap(Mod.GetPath<R6S>("Sprites/Aim.png"), 17, 17);
                _cursor.CenterOrigin();

                _cursor.position = Mouse.positionScreen;
                _cursor.frame = 31;
                Graphics.Draw(_cursor, _cursor.position.x, _cursor.position.y, 1);



                SpriteMap _border = new SpriteMap(Mod.GetPath<R6S>("Sprites/ModMenu/NewsBlock.png"), 128, 72);
                _border.frame = 0;
                _border.CenterOrigin();
                _border.depth = -0.4f;
                Graphics.Draw(_border, 0, 250, 128);
                _border.frame = page + 1;
                _border.depth = -0.5f;
                Graphics.Draw(_border, page + 1, 250, 128);
                Graphics.DrawLine(new Vec2(250 - 63, 128 + 34.7f), new Vec2(250 - 64 + 126 * (nextPage / 3), 128 + 34.7f), Color.White, 0.5f, -0.3f);




                Vec2 friendsRelativePosition = new Vec2(20, 130);

                SpriteMap _friend = new SpriteMap(Mod.GetPath<R6S>("Sprites/ModMenu/FriendIcon.png"), 23, 23);
                _friend.CenterOrigin();

                
                Graphics.Draw(_friend, friendsRelativePosition.x, friendsRelativePosition.y);




                string text = "* ";

                if (updateFriends > 0)
                {
                    updateFriends--;
                }
                else
                {
                    ingame = 0;
                    online = 0;
                    offline = 0;

                    updateFriends = 60;

                    foreach (User user in Steam.friends)
                    {
                        if (user.inCurrentGame)
                        {
                            ingame += 1;
                        }
                        if (user.state == SteamUserState.Online)
                        {
                            online += 1;
                        }
                        if (user.state == SteamUserState.Offline)
                        {
                            offline += 1;
                        }
                    }
                }

                Color c = Color.White;

                if(ingame > 0)
                {
                    text += Convert.ToString(ingame) + " (in game)";
                    c = Color.LightBlue;
                }
                else if (online > 0)
                {
                    text += Convert.ToString(online) + " (online)";
                    c = Color.LightGreen;
                }
                else
                {
                    text += Convert.ToString(offline) + " (offline)";
                    c = Color.LightGray;
                }

                Graphics.DrawStringOutline(text, friendsRelativePosition + new Vec2(16, -4), c, Color.Black, 0.1f);

            }

            if(pLayer == Layer.Background)
            {
                SpriteMap _background = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/Background.png"), 320, 240);
                _background.depth = 0.5f;
                _background.position = camera.position;

                Graphics.Draw(_background, camera.position.x, camera.position.y);
            }
            base.PostDrawLayer(pLayer);
        }
    }
}
