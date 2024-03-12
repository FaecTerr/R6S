using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Scripting")]
    public class Stairs : Thing
    {
        public bool _init = false;
        public SpriteMap _sprite;
        public int stairsnum = 32;
        public EditorProperty<int> style;
        public Stairs(float xval, float yval) : base(xval, yval)
        {
            this._sprite = new SpriteMap(GetPath("Sprites/Stairs.png"), 32, 16, false);
            base.graphic = this._sprite;
            this.collisionOffset = new Vec2(-16f, -8f);
            this.collisionSize = new Vec2(32f, 16f);
            this.center = new Vec2(16f, 8f);
            this.style = new EditorProperty<int>(0, this, 0, 1, 1, null);
        }
        public override void EditorPropertyChanged(object property)
        {
            if (this.style == -1)
            {
                (this.graphic as SpriteMap).frame = Rando.Int(1);
                return;
            }
            (this.graphic as SpriteMap).frame = this.style.value;
        }
        public override void Update()
        {
            base.Update();
            if (_init == false)
            {
                _init = true;
                int j = 1;
                this._sprite.frame = this.style;
                if (this.flipHorizontal == true || this.graphic.flipH == true)
                    j = -1;
                for (int i = 0; i < 17; i++)
                {
                    Level.Add(new Stair(this.x + (16 - i * 2) * j, this.y + 9f - i));
                    Level.Add(new Stair(this.x + j + (16 - i * 2) * j, this.y + 9.5f - i));
                }
            }
        }
        public override void Draw()
        {
            base.Draw();
            this.graphic.flipH = this.flipHorizontal;
        }
    }
}
