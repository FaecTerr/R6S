using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Furniture")]
    public class Sign : Thing, IDrawToDifferentLayers
    {
        public float beep;
        public SpriteMap _sprite;

        public GamemodeScripter script;

        public Sign(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Bomb.png"), 32, 32);
            center = new Vec2(16f, 16f);
            _collisionSize = new Vec2(32f, 32f);
            _collisionOffset = new Vec2(-16f, -16f);
            graphic = _sprite;
            _sprite.frame = 0;
            _canFlip = true;
            depth = -0.6f;
            _editorName = "Bomb";
            hugWalls = WallHug.Floor;
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Foreground)
            {
                bool leftest = false;
                foreach(Sign s in Level.current.things[typeof(Sign)])
                {
                    if(s != this)
                    {
                        if(s.position.x > position.x)
                        {
                            leftest = true;
                        }
                    }
                }
                SpriteMap _letter = new SpriteMap(GetPath("Sprites/Decorations/Sites.png"), 32, 32);
                _letter.scale = new Vec2(0.5f, 0.5f);
                _letter.CenterOrigin();
                if (!leftest)
                {
                    _letter.frame = 1;
                }

                Vec2 pos = position;
                if (pos.x < Level.current.camera.position.x + Level.current.camera.size.x * 0.05f)
                {
                    pos.x = Level.current.camera.position.x + Level.current.camera.size.x * 0.05f;
                }
                if (pos.x > Level.current.camera.position.x + Level.current.camera.size.x * 0.95f)
                {
                    pos.x = Level.current.camera.position.x + Level.current.camera.size.x * 0.95f;
                }
                if (pos.y < Level.current.camera.position.y + Level.current.camera.size.y * 0.05f)
                {
                    pos.y = Level.current.camera.position.y + Level.current.camera.size.y * 0.05f;
                }
                if (pos.y > Level.current.camera.position.y + Level.current.camera.size.y * 0.95f)
                {
                    pos.y = Level.current.camera.position.y + Level.current.camera.size.y * 0.95f;
                }
                _letter.depth = -0.7f;
                Graphics.Draw(_letter, pos.x, pos.y);
            }
        }

        public override void Update()
        {
            if (_sprite.frame == 0)
            {
                foreach (GamemodeScripter g in Level.current.things[typeof(GamemodeScripter)])
                {
                    if(script == null)
                    {
                        script = g; 
                    }
                    if (g.planted)
                    {
                        _sprite.frame = 1;
                    }
                }
            }

            if(_sprite.frame == 1)
            {
                if(beep <= 0)
                {
                    beep = 1.5f;

                    Level.Add(new SoundSource(position.x, position.y, 240, "SFX/bombbeep.wav", "J"));
                }
                else
                {
                    beep -= 0.01666666f;
                }
            }
            base.Update();

            if (!(Level.current is Editor))
            {
                if (Level.CheckPoint<PlantZone>(position.x, position.y) == null)
                {
                    if (script != null)
                    {
                        if (script.currentPhase == 3)
                        {
                            Level.Remove(this);
                        }
                    }
                }
            }
        }

    }
}
