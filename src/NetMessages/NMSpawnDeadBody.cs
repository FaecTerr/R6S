using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMSpawnDeadBody : NMEvent
    {
        public SpriteMap _head;
        public SpriteMap _sprite;
        public SpriteMap _hand;
        public string team;
        public sbyte offDir;
        public Vec2 position;

        public NMSpawnDeadBody()
        {
        }

        public NMSpawnDeadBody(string b, string H, string h, string t, Vec2 pos, sbyte dir)
        {
            if (b != null && H != null && h != null)
            {
                _sprite = new SpriteMap(Mod.GetPath<R6S>(b), 32, 32, false);
                _hand = new SpriteMap(Mod.GetPath<R6S>(h), 16, 16, false);
                _head = new SpriteMap(Mod.GetPath<R6S>(H), 32, 32, false);
            }
            offDir = dir;
            position = pos;
            if (team != null)
            {
                team = t;
            }
        }

        public override void Activate()
        {
            if(_sprite != null && _hand != null && _head != null && team != null)
            {
                Level.Add(new DeadBody(position.x, position.y) { _sprite = _sprite, _head = _head, _hand = _hand, offDir = offDir, team = team });
            }
        }
    }
}