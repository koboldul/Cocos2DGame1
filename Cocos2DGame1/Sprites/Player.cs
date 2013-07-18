#region Directives
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
#endregion

namespace Football.Sprites
{
    public enum PlayerType 
    {
        Top = 0,
        Bottom = 1
    }
    
    public class Player : Entity
    {
        #region [ .ctrs ]
        internal Player(PlayerType type) 
        {
            _playerType = type;
        }
        #endregion
        
        #region [ Entity Abstract ]
        protected override string AssetName
        {
            get { return _playerType == PlayerType.Bottom ? BOTTOM_ASSET_NAME : TOP_ASSET_NAME ; }
        }

        public override void Draw(SpriteBatch theSpriteBatch, float elapsedSeconds)
        {
            theSpriteBatch.Draw(_texture, Position, null, Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 0f);
        } 
        #endregion

        #region [ Private members ]
        private PlayerType _playerType = PlayerType.Top;

        private const string TOP_ASSET_NAME = "top-player";
        private const string BOTTOM_ASSET_NAME = "bottom-player";
        #endregion
    }
}
