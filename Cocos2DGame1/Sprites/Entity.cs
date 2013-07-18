#region [ Directives ]
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework; 
#endregion

namespace Football.Sprites
{
    public abstract class Entity
    {
        #region [ Public Properties ]
        public Vector2 Position { get; set; }

        public Vector2 Size { get { return _size; } }
        #endregion

        #region [ Public methods ]
        public void LoadContent(ContentManager theContentManager)
        {
            _texture = theContentManager.Load<Texture2D>(AssetName);
            _size = new Vector2(_texture.Width * 0.35f, _texture.Height * 0.35f);
        }
        #endregion

        #region [ Abstract ]
        
        protected abstract string AssetName { get; }

        public abstract void Draw(SpriteBatch theSpriteBatch, float elapsedSeconds);

        public virtual void Update(GameTime gameTime) 
        {
            
        }
       
        #endregion

        #region [ Protected Members ]
        protected Texture2D _texture;

        private Vector2 _size = Vector2.Zero;
        #endregion
    }
}
