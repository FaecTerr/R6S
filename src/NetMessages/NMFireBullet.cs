using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMFireNewBullet : NMEvent
    {
        public List<MaterialThing> hit = new List<MaterialThing>();
        public bool init;
        public Vec2 _startPos;
        public Vec2 _endPos;
        public float life = 1.1f;
        public int damage = 10;

        public Vec2 pos;
        public Operators oper;
        public float thickness;
        public float range;

        public bool tracer;
        public int rate;

        public float damageDropDistance;
        public float minDamageDrop;
        public float maxDropDistance;

        public float headshotMod;

        public NMFireNewBullet()
        {
        }

        public NMFireNewBullet(float xpos, float ypos, Vec2 startPos, Vec2 endPos, int fireRate, float lifetime, Operators op, int damag, float thick, float rng = 1, bool trace = true, float dDD = 0.5f, float minDam = 0.5f, float maxDrop = 0.8f, float hsMod = 1.5f)
        {
            _startPos = startPos;
            _endPos = endPos;
            life = lifetime;
            pos.x = xpos;
            pos.y = ypos;
            oper = op;
            damage = damag;
            thickness = thick;
            range = rng;
            tracer = trace;
            rate = fireRate;

            damageDropDistance = dDD;
            minDamageDrop = minDam;
            maxDropDistance = maxDrop;

            headshotMod = hsMod;
        }

        public override void Activate()
        {
            if (_startPos != null && _endPos != null && oper != null)
            {
                NewBullet nb = new NewBullet(pos.x, pos.y, pos, _endPos, _endPos, rate, life, tracer, headshotMod) { damage = damage, thickness = thickness, maxDistance = range,
                    shootedBy = oper, damageDropDistance = damageDropDistance, minDamageDrop = minDamageDrop, maxDropDistance = maxDropDistance };

                if (nb != null && oper != null)
                {
                    nb.ignore.Add(oper);
                    if (oper.head != null)
                    {
                        nb.ignore.Add(oper.head);
                    }
                }

                Level.Add(nb);
            }
        }
    }
}