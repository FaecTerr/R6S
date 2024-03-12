using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Gamemode")]
    public class SiteSelector : Thing
    {
        public SpriteMap _sprite;

        public EditorProperty<string> sites;
        public bool init;
        public List<string> FirstSite = new List<string>();
        public List<string> SecondSite = new List<string>();

        public int selectedSite;
        public SiteSelector() : base()
        {
            _sprite = new SpriteMap(GetPath("Sprites/Script.png"), 16, 16);
            _sprite.center = new Vec2(8, 8);
            center = new Vec2(8, 8);
            graphic = _sprite;

            collisionSize = new Vec2(14, 14);
            _collisionOffset = new Vec2(-7, -7);

            sites = new EditorProperty<string>("1F+2F");
            //team = new EditorProperty<int>(0, this, 0, 1, 1);

            layer = Layer.Foreground;
        }

        public override void Initialize()
        {
            base.Initialize();
            if (!init && !(Level.current is Editor))
            {
                string site1 = "";
                string site2 = "";
                bool swap = false;
                init = true;

                foreach (char c in sites.value)
                {
                    //Splitting one site by rooms
                    if (c == '+')
                    {
                        if (!swap)
                        {
                            swap = !swap;
                            FirstSite.Add(site1);
                            site1 = "";
                        }
                        else
                        {
                            swap = !swap;
                            SecondSite.Add(site2);
                            site2 = "";
                        }
                    }
                    else if (c == ';') //Splitting different sites
                    {
                        if (site1.Length > 0)
                        {
                            FirstSite.Add(site1);
                            site1 = "";
                        }
                        if (site2.Length > 0)
                        {
                            SecondSite.Add(site2);
                            site2 = "";
                        }
                        swap = false;
                    }
                    else
                    {
                        if (!swap)
                        {
                            site1 += c;
                        }
                        else
                        {
                            site2 += c;
                        }
                    }
                }
            }
            selectedSite = Rando.Int(FirstSite.Count);
        }

        public override void Update()
        {
            base.Update();
        }

        public void CreateSite(string location)
        {
            foreach (string s in FirstSite)
            {
                DevConsole.Log(s);
            }
            if (FirstSite.Contains(location) && SecondSite.Count > 0 && FirstSite.Count > 0 && FirstSite.Count == SecondSite.Count)
            {
                int id = FirstSite.IndexOf(location);
                string locationAlt = SecondSite[id];
                selectedSite = id;
                foreach(RoomRect r in Level.current.things[typeof(RoomRect)])
                {
                    if(r.name == location)
                    {
                        Level.Add(new PlantZone(r.position.x + 8, r.position.y + 8) { collisionSize = new Vec2(16 * r.sizex, 16 * r.sizey)});
                    }           
                }
                foreach (RoomRect r in Level.current.things[typeof(RoomRect)])
                {
                    if (r.name == locationAlt)
                    {
                        Level.Add(new PlantZone(r.position.x + 8, r.position.y + 8) { collisionSize = new Vec2(16 * r.sizex, 16 * r.sizey) });
                    }
                }
                //Level.Add(new PlantZone());
            }
        }
    }
}
