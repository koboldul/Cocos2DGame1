#region Directives
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Football.Engine;
#endregion

namespace Football.Sprites
{
    class Team
    {
        #region [ .ctors ]
        public Team(PlayerType type, GEnvironment environment)
        {
            _players = new List<Player>();
            _environment = environment;
            for (int i = 0; i < TEAM_SIZE; i++)
            {
                Player player = new Player(type);
                _players.Add(player);
                _environment.RegisterEntity(player);
            }
            _playerType = type;
            
            
        }
        #endregion

        #region [ Loop ]
        public void LoadContent(ContentManager theContentManager)
        {
            foreach (Player player in _players) 
            {
                player.LoadContent(theContentManager);
            }          
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            for (int i=0; i < TEAM_SIZE; i++)
            {
                _players[i].Position = GetPlayerPosition(i);
                _players[i].Draw(theSpriteBatch, 1f);
            }
        }   
        #endregion

        #region [ Private Methods ]
        private Vector2 GetPlayerPosition(int playerNumber) 
        {
            int x = 0;
            int y = 0;

            #region [ Goalkeeper ]
            if (playerNumber == 0)
            {
                x = _environment.XOffset + _environment.MarginWidth + _environment.TerrainWidth / 2;

                if (_playerType == PlayerType.Top)
                {
                    y = _environment.YOffset + _environment.MarginWidth;
                }
                else
                {
                    y = _environment.YOffset + _environment.TerrainHeight + _environment.MarginWidth / 2;
                }
            } 
            #endregion

            #region [ Defenders ]
            if (playerNumber == 1)
            {
                x = _environment.XOffset + _environment.MarginWidth + _environment.TerrainWidth / 3;

                if (_playerType == PlayerType.Top)
                {
                    y = _environment.YOffset + _environment.MarginWidth + _environment.TerrainHeight / 10;
                }
                else
                {
                    y = _environment.YOffset + _environment.MarginWidth + 9 * _environment.TerrainHeight / 10;
                }
            }

            if (playerNumber == 2)
            {
                x = _environment.XOffset + _environment.MarginWidth + 2 * _environment.TerrainWidth / 3;

                if (_playerType == PlayerType.Top)
                {
                    y = _environment.YOffset + _environment.MarginWidth + _environment.TerrainHeight / 10;
                }
                else
                {
                    y = _environment.YOffset + _environment.MarginWidth + 9 * _environment.TerrainHeight / 10;
                }
            } 
            #endregion

            #region [ Middle ]
            if (playerNumber == 3)
            {
                x = _environment.XOffset + _environment.MarginWidth + 2 * _environment.TerrainWidth / 5;

                if (_playerType == PlayerType.Top)
                {
                    y = _environment.YOffset + _environment.MarginWidth + _environment.TerrainHeight / 6;
                }
                else
                {
                    y = _environment.YOffset + _environment.MarginWidth + 5 * _environment.TerrainHeight / 6;
                }
            }

            if (playerNumber == 4)
            {
                x = _environment.XOffset + _environment.MarginWidth + 3 * _environment.TerrainWidth / 5;

                if (_playerType == PlayerType.Top)
                {
                    y = _environment.YOffset + _environment.MarginWidth + _environment.TerrainHeight / 5;
                }
                else
                {
                    y = _environment.YOffset + _environment.MarginWidth + 4 * _environment.TerrainHeight / 5;
                }
            }

            if (playerNumber == 5)
            {
                x = _environment.XOffset + _environment.MarginWidth + 4 * _environment.TerrainWidth / 5;

                if (_playerType == PlayerType.Top)
                {
                    y = _environment.YOffset + _environment.MarginWidth + _environment.TerrainHeight / 6;
                }
                else
                {
                    y = _environment.YOffset + _environment.MarginWidth + 5 * _environment.TerrainHeight / 6;
                }
            } 
            #endregion

            #region [ Attack ]
            if (playerNumber == 6)
            {
                x = _environment.XOffset + _environment.MarginWidth + 2 * _environment.TerrainWidth / 7;

                if (_playerType == PlayerType.Top)
                {
                    y = _environment.YOffset + _environment.MarginWidth + _environment.TerrainHeight / 3;
                }
                else
                {
                    y = _environment.YOffset + _environment.MarginWidth + 2 * _environment.TerrainHeight / 3;
                }
            }

            if (playerNumber == 7)
            {
                x = _environment.XOffset + _environment.MarginWidth + 3*_environment.TerrainWidth / 7;

                if (_playerType == PlayerType.Top)
                {
                    y = _environment.YOffset + _environment.MarginWidth + _environment.TerrainHeight / 3;
                }
                else
                {
                    y = _environment.YOffset + _environment.MarginWidth + 2 * _environment.TerrainHeight / 3;
                }
            }

            if (playerNumber == 8)
            {
                x = _environment.XOffset + _environment.MarginWidth + 4 * _environment.TerrainWidth / 7;

                if (_playerType == PlayerType.Top)
                {
                    y = _environment.YOffset + _environment.MarginWidth + _environment.TerrainHeight / 3;
                }
                else
                {
                    y = _environment.YOffset + _environment.MarginWidth + 2 * _environment.TerrainHeight / 3;
                }
            }

            if (playerNumber == 9)
            {
                x = _environment.XOffset + _environment.MarginWidth + 5 * _environment.TerrainWidth / 7;

                if (_playerType == PlayerType.Top)
                {
                    y = _environment.YOffset + _environment.MarginWidth + _environment.TerrainHeight / 3;
                }
                else
                {
                    y = _environment.YOffset + _environment.MarginWidth + 2 * _environment.TerrainHeight / 3;
                }
            }

            if (playerNumber == 10)
            {
                x = _environment.XOffset + _environment.MarginWidth + 6 * _environment.TerrainWidth / 7;

                if (_playerType == PlayerType.Top)
                {
                    y = _environment.YOffset + _environment.MarginWidth + _environment.TerrainHeight / 3;
                }
                else
                {
                    y = _environment.YOffset + _environment.MarginWidth + 2 * _environment.TerrainHeight / 3;
                }
            } 
            #endregion

            return new Vector2(x, y);
        }
        #endregion

        #region [ Private Members ]
        private IList<Player> _players;
        private PlayerType _playerType;
        private const int TEAM_SIZE = 11;
        private GEnvironment _environment;
        #endregion
    }
}
