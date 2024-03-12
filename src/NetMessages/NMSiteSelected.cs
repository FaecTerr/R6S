using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMSiteSelected : NMEvent
    {
        public string site;
        public NMSiteSelected()
        {
        }

        public NMSiteSelected(string _site)
        {
            site = _site;
        }

        public override void Activate()
        {
            if (site != "")
            {
                foreach (SiteSelector s in Level.current.things[typeof(SiteSelector)])
                {
                    s.CreateSite(site);
                }
                foreach(GamemodeScripter gm in Level.current.things[typeof(GamemodeScripter)])
                {
                    gm.GetSpawnPosition();
                }
            }
        }
    }
}