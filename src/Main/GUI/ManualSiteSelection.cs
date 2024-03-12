using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class ManualSiteSelection : Button
    {
        public int SiteID;
        public string SiteName;
        public GamemodeScripter gamemode;
        public SiteSelector siteSelector;

        public ManualSiteSelection(float xpos, float ypos) : base(xpos, ypos)
        {
            layer = Layer.Foreground;

            collisionSize = new Vec2(124, 16);
            collisionOffset = new Vec2(-62f, -8f);
        }

        public override void OnActivation()
        {
            base.OnActivation();

        }
        public override void OnReleaseActivation()
        {
            base.OnReleaseActivation(); 
            siteSelector.selectedSite = SiteID;
            if (gamemode != null)
            {
                gamemode.screen = 1;
                gamemode.addSelection = false;
                gamemode.OperatorsSelect(gamemode.banned);
                gamemode.SetPlantRoom();
                gamemode.SiteSelectRemove();
            }
        }

        public override void Draw()
        {
            base.Draw();
            Color drawColor = Color.Gray;
            if (hovered)
            {
                drawColor = Color.Silver;
            }
            if(siteSelector != null && siteSelector.selectedSite == SiteID)
            {
                drawColor = Color.White;
            }

            Graphics.DrawRect(topLeft, bottomRight, drawColor, depth, false);
            Graphics.DrawStringOutline(SiteName, new Vec2(left + 12f, position.y - 4), drawColor, Color.Black);
            
        }
    }
}
