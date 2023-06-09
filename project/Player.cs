using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyInJungle.project
{
    public class Player: Entity
    {
        public Vector2 velocity;
        public Rectangle playerFallRectangle;
        public SpriteEffects spriteEffects;

        public int playerSpeed = 2;
        public int fallSpeed = 2;
        public int jumpSpeed = -5;
        public float startY;

        public bool isFalling = true;
        public bool isJumping;

        public Animation[] playerAnimation;
        public currentAnimation playerAnimationController;
        public Player(Vector2 position, Texture2D idleRightSprite, Texture2D runLeftSprites,
            Texture2D runRightSprites)
        {
            playerAnimation = new Animation[3];
            this.position = position;
            velocity = new Vector2();
            spriteEffects = SpriteEffects.None;

            playerAnimation[0] = new Animation(idleRightSprite);
            playerAnimation[1] = new Animation(runRightSprites);
            playerAnimation[2] = new Animation(runLeftSprites);

            playerFallRectangle = new Rectangle((int)position.X + 3, (int)position.Y + 32, 25, fallSpeed);
            hitbox = new Rectangle((int)position.X, (int)position.Y, 25, 32);

        }

        public override void UpDate()
        {
       
            KeyboardState keyboard = Keyboard.GetState();

            playerAnimationController = currentAnimation.IdleRight;
            startY = position.Y;
            position = velocity;

            Move(keyboard);

            if (isFalling)
            {
                velocity.Y += fallSpeed;
            }

            position = velocity;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            playerFallRectangle.X = (int)position.X;
            playerFallRectangle.Y = (int)(velocity.Y + 32);
        }

        private void Move(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.A))
            {
                velocity.X -= playerSpeed;
                playerAnimationController = currentAnimation.RunRight;
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                velocity.X += playerSpeed;
                playerAnimationController = currentAnimation.RunLeft;
                spriteEffects = SpriteEffects.None;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)       
        {
            switch (playerAnimationController)
            {
                case currentAnimation.IdleRight:
                    playerAnimation[0].Draw(spriteBatch, position, gameTime);
                    break; 
                case currentAnimation.RunRight:
                    playerAnimation[1].Draw(spriteBatch, position, gameTime);
                    break;
                case currentAnimation.RunLeft:
                    playerAnimation[2].Draw(spriteBatch, position, gameTime);
                    break;
            }
        }
    }
}
