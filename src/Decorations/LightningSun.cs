using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Furniture")]
    public class LightningSun : Thing
    {
        EditorProperty<float> Range;
        EditorProperty<float> Alpha;
        EditorProperty<int> Red;
        EditorProperty<int> Green;
        EditorProperty<int> Blue;
        EditorProperty<bool> Disableable;

        PointLight _point;



        public LightningSun(float xval, float yval) : base(xval, yval)
        {
            center = new Vec2(2f, 2f);
            collisionOffset = new Vec2(-2f, -2f);
            collisionSize = new Vec2(4f, 4f);

            depth = -0.6f;

            Alpha = new EditorProperty<float>(1, null, 0, 1, 0.1f);
            Range = new EditorProperty<float>(640, null, 80, 3200, 80);
            Red = new EditorProperty<int>(255, null, 0, 255, 1);
            Green = new EditorProperty<int>(255, null, 0, 255, 1);
            Blue = new EditorProperty<int>(255, null, 0, 255, 1);
            Disableable = new EditorProperty<bool>(false);  
        }

        public override void Initialize()
        {
            base.Initialize();
            if(!(Level.current is Editor))
            {
                Color c = new Vec3(Red, Green, Blue).ToColor();
                _point = new PointLight(position.x, position.y, c * Alpha, Range);
                Level.Add(_point);
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public void TurnOff()
        {
            if(_point != null && Disableable)
            {
                Level.Remove(_point);
                _point = null;
            }
        }

        public void TurnOn()
        {
            if (Disableable)
            {
                if (_point != null)
                {
                    Level.Remove(_point);
                    _point = null;
                }
                Color c = new Vec3(Red, Green, Blue).ToColor();
                _point = new PointLight(position.x, position.y, c * Alpha, Range);
                Level.Add(_point);
            }
        }

        public override void Draw()
        {
            if(Level.current is Editor)
            {
                Sprite s = new Sprite(GetPath("Sprites/PointLight.png"));
                s.CenterOrigin();
                s.scale = new Vec2(0.4f, 0.4f);
                Color c = new Vec3(Red, Green, Blue).ToColor();
                s.color = c;
                Graphics.Draw(s, position.x, position.y);

            }
            base.Draw();
        }
    }
}
