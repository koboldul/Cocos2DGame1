#region [ Directives ]
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Football.Engine;
using System.Diagnostics; 
#endregion

namespace Football.Sprites
{
    public class Ball : Entity
    {
        #region [ .ctrs ]
        public Ball(GEnvironment environment)
        {
            _environment = environment;
        } 
        #endregion

        #region [ Entity Abstract ]
        protected override string AssetName
        {
            get
            {
                return ASSET_NAME;
            }
        }

        public override void Update(GameTime gameTime)
        {
            var elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Position == default(Vector2))
            {
                Position = new Vector2(
                    _environment.TerrainCenter.X - 0.35f * _texture.Bounds.Width / 2,
                    _environment.TerrainCenter.Y - 0.35f * _texture.Bounds.Height / 2);
            }

            if (Velocity != Vector2.Zero)
            {
                

                Position += Velocity * elapsedSeconds;

                float newMagnitude = Velocity.Length() - _decelerate * elapsedSeconds;
                _velocity.Normalize();

                Velocity *= Math.Max(0, newMagnitude);
            }
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch theSpriteBatch, float elapsedSeconds)
        {
            SpriteEffects ef = SpriteEffects.None;

            if (Velocity != Vector2.Zero)
            {
                ef = invert ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                invert = !invert;
            }
            
            theSpriteBatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero, 0.35f, ef, 0f);
        } 
        #endregion

        #region [ Public Properties ]
        public Vector2 Velocity
        {
            get { return _velocity;}
            set 
            {
                _velocity = value;
                _decelerate = 60f;
            }
        }
        #endregion

        #region [ Public Methods ]
        public void ResetBall()
        {
            Position = new Vector2(_environment.TerrainCenter.X + Size.X / 2, _environment.TerrainCenter.Y - Size.Y /2);
            Velocity = Vector2.Zero;
        }
        #endregion

        #region [ Private Members ]
        private const string ASSET_NAME = "ball";
        GEnvironment _environment;
        float _decelerate;
        Vector2 _velocity;
        bool invert = false;
        #endregion
    }
}
