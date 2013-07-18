#region Directives
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;
using FotbalPhone; 
#endregion

namespace Football.Engine
{
    class ShootManager
    {
        #region [ .ctrs ]
        public ShootManager(Texture2D lineTexture)
        {
            _lineTexture = lineTexture;
        }
        #endregion
        
        #region [ Public Methods ]
        
        public Vector2 EndShooting(GestureSample gesture)
        {
            Vector2 velocity = ComputeVelocity(gesture);
            
            _startShootingTime = default(TimeSpan);
            _startPosition = default(Vector2);
            
            State = GameState.Idle;

            return velocity;
        }

        public void DrawDirectionVector(SpriteBatch theSpriteBatch) 
        {
            if (State != GameState.Shooting) return;

            Vector2 Origin = new Vector2(0.5f, 0.0f);
            Vector2 diff = - _startPosition + _currentPosition;
            float angle;
            Vector2 Scale = new Vector2(5.0f, Math.Min(diff.Length(), 100) / _lineTexture.Height);

            Debug.WriteLine(string.Format("Lenght : {0}", diff.Length()));

            angle = (float)(Math.Atan2(diff.Y, diff.X)) - MathHelper.PiOver2;

            Color color = diff.Length() > 50 ? Color.Red : Color.Blue;

            theSpriteBatch.Draw(_lineTexture, _startPosition, null, color, angle, Origin, Scale, SpriteEffects.None, 1.0f);
        }

        public void Update(GestureSample gesture) 
        {
            if (State == GameState.Idle)
            {
                _startShootingTime = gesture.Timestamp;
                _startPosition = new Vector2(gesture.Position.X, gesture.Position.Y);
                State = GameState.Shooting;
            }
            
            _currentPosition = new Vector2(gesture.Position.X, gesture.Position.Y);
        }
        #endregion

        #region [ Private Properties ]
        private GameState State
        {
            get;
            set;
        }
        #endregion

        #region [ Private Methods ]
        private Vector2 ComputeVelocity(GestureSample gesture)
        {
            Vector2 diff = _currentPosition - _startPosition;

            float magnitude = Math.Min(diff.Length(), 100) * 5;
            diff.Normalize();
            Vector2 result = diff * magnitude;

            return result; 
        }
        #endregion

        #region [ Private Members ]
        private TimeSpan _startShootingTime;
        private Vector2 _startPosition;
        private Vector2 _currentPosition;

        private Texture2D _lineTexture;
        #endregion
    }
}
