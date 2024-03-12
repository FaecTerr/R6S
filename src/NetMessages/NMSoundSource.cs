using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMSoundSource : NMEvent
    {
        public float range;
        /// <summary>
        /// G - global (doesn't jam from walls)
        /// J - jamming of walls
        /// S - Can't go through walls
        /// </summary>
        public string sType = "";
        public string sound = "";
        public Vec2 pos = new Vec2(-9999999f, -9999999f);
        public float muffling;

        public NMSoundSource()
        {

        }

        public NMSoundSource(Vec2 p, float r, string source = "", string type = "J", float muffle = 1f)
        {
            pos = p;
            range = r;

            sType = type;
            sound = source;

            muffling = muffle;
        }

        public override void Activate()
        {
            if (sound != null && sType != null && pos.x + pos.y > (-9999999 * 2))
            {
                Level.Add(new SoundSource(pos.x, pos.y, range, sound, sType) { volumeMod = muffling});
            }
        }
    }
}