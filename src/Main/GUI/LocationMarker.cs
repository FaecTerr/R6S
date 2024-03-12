using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class LocationMarker : Thing, IDrawToDifferentLayers
    {
        Sprite marker;
        Color color;

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Foreground)
            {
                if(marker != null)
                {
                    marker.color = color;
                    Graphics.Draw(marker, position.x, position.y);
                }
            }
        }
    }
}
