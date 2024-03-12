using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Scripting")]
    public class MapLayer : Thing, IDrawToDifferentLayers
    {
        public EditorProperty<int> xTiles = new EditorProperty<int>(16, null, 4, 320, 1);
        public EditorProperty<int> yTiles = new EditorProperty<int>(16, null, 4, 180, 1);

        public EditorProperty<int> LayerID = new EditorProperty<int>(0, null, 0, 3, 1);
        Color[] colors = new Color[] {Color.Orange, Color.CornflowerBlue, Color.SeaGreen, Color.LightCyan};
        Color[] darkColors = new Color[] { Color.DarkOrange, Color.Blue, Color.Green, Color.Cyan };
        Color[] interColors = new Color[] { Color.Red, Color.SeaGreen, Color.Blue, Color.Blue };

        Sprite map;
        bool init;
        bool roundStarted;

        public override void Initialize()
        {
            base.Initialize();
        }

        public void Init()
        {
            Tex2D texture = new Tex2D(xTiles, yTiles);
            Color[] texArray = new Color[xTiles * yTiles];

            for (int i = 0; i < yTiles; i++)
            {
                for (int j = 0; j < xTiles; j++)
                {
                    Block b = Level.CheckPoint<Block>(position.x + j * 16, position.y + i * 16);
                    if (b != null && !(b is InvisBlock) && !(b is LocalOutsideBlock))
                    {
                        if (b is BreakableSurface)
                        {
                            texArray[i * xTiles + j] = interColors[LayerID];
                        }
                        else
                        {
                            texArray[i * xTiles + j] = darkColors[LayerID];
                        }
                    }
                    else
                    {
                        texArray[i * xTiles + j] = colors[LayerID];
                    }
                }
            }

            texture.SetData(texArray);
            map = new Sprite(texture);
            //map.CenterOrigin();
            init = true;
        }

        public override void Draw()
        {
            base.Draw();

        }

        public void OnDrawLayer(Layer pLayer)
        {
            if(pLayer == Layer.Foreground)
            {
                if (Level.current is Editor)
                {
                    Graphics.DrawRect(position, position + new Vec2(xTiles * 16, yTiles * 16), colors[LayerID] * 0.4f);
                    Graphics.DrawRect(position, position + new Vec2(xTiles * 16, yTiles * 16), colors[LayerID] * 0.8f, depth, false);
                }
                else
                {
                    if (!init)
                    {
                        Init();
                    }
                    if (map != null && !roundStarted)
                    {
                        if (Level.current.things[typeof(GamemodeScripter)].Count() > 0) 
                        {
                            GamemodeScripter gm = Level.current.things[typeof(GamemodeScripter)].First() as GamemodeScripter;
                            if (gm.currentPhase < 2 && gm.screen == 0)
                            {
                                Graphics.Draw(map, Level.current.camera.position.x + 220, Level.current.camera.position.y + 62 + LayerID.value * 40);
                            }
                            else
                            {
                                roundStarted = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
