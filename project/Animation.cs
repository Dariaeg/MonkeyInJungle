using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonkeyInJungle.project
{
    public class Animation
    {
        Texture2D spritesheet;
        int frames;
        int columns = 0;
        int rows = 0;

        double timeBetweenFrames = 0.0;
        int width;
        int height;
        public Animation(Texture2D spritesheet, int width = 25, int height = 32)
        {
            this.spritesheet = spritesheet;
            frames = (int)(spritesheet.Width / width);
            this.width = width;
            this.height = height;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime, float milisecondsperframes = 100, SpriteEffects effect = SpriteEffects.None)
        {
            if (columns < frames)
            {
                var rect = new Rectangle((int)width * columns, rows, (int)width, (int)width);
                spriteBatch.Draw(spritesheet, position, rect, Color.White, 0f, new Vector2(), 1f, effect, 1);
                timeBetweenFrames += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timeBetweenFrames > milisecondsperframes)
                {
                    timeBetweenFrames -= milisecondsperframes;
                    columns++;

                    if (columns == frames)
                    {
                        columns = 0;
                    }
                }

            }

        }
    }
}