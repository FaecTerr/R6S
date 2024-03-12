using System;
using System.Collections.Generic;
using System.Linq;
using DuckGame;

namespace DuckGame.R6S
{    
    public class IDP : PhysicsObject
    {
        public SpriteMap _sprite;
        public float radius = 32;
        public float takeTime = 0.3f;

        public float taking = 0;

        public IDP(float xval, float yval) : base(xval, yval)
        {
            center = new Vec2(8f, 12f);
            weight = 0f;
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(6f, 8f);
            _sprite = new SpriteMap(GetPath("Sprites/Defuser"), 16, 16, false);
            base.graphic = _sprite;
        }
    }
}
