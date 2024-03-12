using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public enum EffectType 
    { 
        Constant,
        Temporary,
        Conditional,
        Chargable
    }

    public class Effect
    {
        public string name = "";
        public string team = "Def";
        public EffectType effectType;

        public int type { get { return (int)effectType; } set { effectType = (EffectType)value; } }

        public float charge;

        public float timer = 0.1f;
        public float maxTimer = 0.1f;

        public bool removeOnEnd = true;

        public Operators owner;

        public virtual void TimerOut()
        {

        }

        public virtual void Update()
        {

        }
    }
}
