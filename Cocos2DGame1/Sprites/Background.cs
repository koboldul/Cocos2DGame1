#region [ Directives ]
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Football.Engine;   
#endregion
  
namespace Football.Sprites  
{  
    class Background  
    {
        #region [ .ctrs ]
        public Background(Texture2D lineTexture, GEnvironment environment)
        {
            _lineTexture = lineTexture;
            _environment = environment;
        } 
        #endregion

        #region [ Startup ]
        public void LoadContent(ContentManager theContentManager)
        {
            _texture = theContentManager.Load<Texture2D>(ASSET_NAME);
            t2dTiles[0] = theContentManager.Load<Texture2D>(ASSET_NAME);
        }   
        #endregion

        #region [ Loop ]
        public void Draw(SpriteBatch theSpriteBatch)
        {
            float scale = 0.3f;
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 19; y++)
                {
                    Vector2 pos = new Vector2(x * TILE_WIDTH * scale, y * TILE_HEIGHT * scale);
                    theSpriteBatch.Draw(t2dTiles[0], pos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
            }
            
            Color color = Color.Pink;

            int xOffset = _environment.XOffset;
            int yOffset = _environment.YOffset;

            int terrainWidth = _environment.TerrainWidth;
            int terrainHeight = _environment.TerrainHeight;

            int marginWidth = _environment.MarginWidth;

            theSpriteBatch.Draw(_lineTexture, new Rectangle(xOffset, yOffset, terrainWidth + 2*marginWidth, marginWidth), color);
            theSpriteBatch.Draw(_lineTexture, new Rectangle(xOffset, yOffset, marginWidth, terrainHeight + 2*marginWidth), color);
            theSpriteBatch.Draw(_lineTexture, new Rectangle(xOffset, yOffset + terrainHeight + 2*marginWidth, terrainWidth + 2 * marginWidth, marginWidth), color);
            theSpriteBatch.Draw(_lineTexture, new Rectangle(xOffset + terrainWidth + marginWidth, yOffset, marginWidth, terrainHeight + 2 * marginWidth), color);
            theSpriteBatch.Draw(_lineTexture, new Rectangle(xOffset, yOffset + terrainHeight / 2 + marginWidth, terrainWidth+ 2 * marginWidth, marginWidth/2), Color.White);
        } 
        #endregion

        #region [ Private Members ]
        const int TILE_WIDTH = 227;     
        const int TILE_HEIGHT = 145;  
        private Texture2D _texture;
        private Texture2D _lineTexture;
        private GEnvironment _environment;
        public const string ASSET_NAME = "grass-green";  
        
        Texture2D[] t2dTiles = new Texture2D[1];
        #endregion
    }  
 
}  
