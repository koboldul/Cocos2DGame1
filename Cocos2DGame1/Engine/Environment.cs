#region [ Directives ]
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Football.Sprites;
using System.Linq;
#endregion

namespace Football.Engine
{
    public class GEnvironment
    {
        #region [ .ctrs ]
        public GEnvironment()
        {
            _players = new List<Player>();
        }
        #endregion
        
        #region [ Public Properties ]
        public int TerrainWidth { get { return 450; } }
        public int TerrainHeight { get { return (int)(TerrainWidth / 0.7); } }
        public int XOffset { get { return 5; } }
        public int YOffset { get { return 30; } }
        public int MarginWidth { get { return 5; } }

        public Vector2 TerrainCenter
        {
            get
            {
                Vector2 center = new Vector2(
                    XOffset + MarginWidth + TerrainWidth / 2,
                    YOffset + MarginWidth + TerrainHeight / 2);
                return center;
            }
        }

        public void Update(GameTime time) 
        {
            _ball.Update(time);
            HandleCollisionWithMargins(_ball);
            
            foreach (var player in _players) 
            {
                //HandleCollisionWithPlayer(_ball, player);
            }
        }
        #endregion

        #region [ Public Methods ]
        public void RegisterEntity(Entity entity) 
        {
            if (entity is Ball) 
            {
                _ball = entity as Ball;
            }
            if (entity is Player)
            {
                _players.Add(entity as Player);
            }
        }

        
        #endregion

        #region [ Collision ]
        public void HandleCollisionWithMargins(Ball ball) 
        {
            Vector2 v = ball.Velocity;

            if (ball.Position.X <= MarginWidth + XOffset) v = new Vector2(-ball.Velocity.X, ball.Velocity.Y);

            if (ball.Position.X + ball.Size.X >= MarginWidth + XOffset + TerrainWidth) v = new Vector2(-ball.Velocity.X, ball.Velocity.Y);

            if (ball.Position.Y <= MarginWidth + YOffset) v = new Vector2(ball.Velocity.X, -ball.Velocity.Y);

            if (ball.Position.Y + ball.Size.Y > MarginWidth + YOffset + TerrainHeight) v = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
            
            ball.Velocity = v;           
        }

        public void HandleCollisionWithPlayer(Ball ball, Player player)
        {
            Vector2 v = ball.Velocity;

            if (ball.Position.X + ball.Size.X >= player.Position.X && ball.Position.Y < player.Position.Y) 
                v = new Vector2(-ball.Velocity.X, ball.Velocity.Y);

            ball.Velocity = v;
        }
        #endregion

        #region [ Private Members ]
        private IList<Player> _players;
        private Ball _ball;
        #endregion
    }
}
