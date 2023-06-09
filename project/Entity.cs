using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyInJungle.project
{
    public abstract class Entity
    {
        public enum currentAnimation
        {
            IdleRight,
            RunRight,
            RunLeft
        }

        public Vector2 position;
        public Rectangle hitbox;

        public abstract void UpDate();

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
