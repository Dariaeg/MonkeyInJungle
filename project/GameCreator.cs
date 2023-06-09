using Microsoft.Xna.Framework;

namespace MonkeyInJungle.project
{
    public class GameCreator
    {
        private Rectangle end;
        public GameCreator(Rectangle end)
        {
            this.end = end;
        }

        public bool hasGameEnded(Rectangle playerHitbox)
        {
            return end.Intersects(playerHitbox);
        }
    }
}
