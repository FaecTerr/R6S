using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace DuckGame.R6S
{
    internal class update : IUpdateable
    {
        private R6SMainUpdate upd = new R6SMainUpdate();

        public bool Enabled
        {
            get
            {
                return true;
            }
        }

        public int UpdateOrder
        {
            get
            {
                return 1;
            }
        }

        public event EventHandler<EventArgs> EnabledChanged;

        public event EventHandler<EventArgs> UpdateOrderChanged;

        public void Update(GameTime gameTime)
        {
            upd.Update();
        }
    }
}
