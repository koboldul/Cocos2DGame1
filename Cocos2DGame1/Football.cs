#region Directives
using System;
using Football.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Football.Engine;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;
using FotbalPhone;

#endregion

namespace FotbalPhone
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Football : Game
    {
        #region [ .ctrs ]
        public Football()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = true;

            Content.RootDirectory = "Content";

            _environment = new GEnvironment();

            _ball = new Ball(_environment);

            _topTeam = new Team(PlayerType.Top, _environment);
            _bottomTeam = new Team(PlayerType.Bottom, _environment);

            _environment.RegisterEntity(_ball);
            
            // Frame rate is 30 fps by default for Windows Phone.  
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Pre-autoscale settings.  
            _graphics.PreferredBackBufferWidth = 480;
            _graphics.PreferredBackBufferHeight = 800;
        }
        #endregion

        #region [ Bootstrap ]
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            TouchPanel.EnabledGestures =  GestureType.Tap | GestureType.DoubleTap |
                            GestureType.DragComplete | GestureType.FreeDrag;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _topTeam.LoadContent(Content);
            _bottomTeam.LoadContent(Content);
            _ball.LoadContent(Content);

            Texture2D terrainMarkerTexture;

            terrainMarkerTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Int32[] pixel = { 0xFFFFFF }; // White. 0xFF is Red, 0xFF0000 is Blue
            terrainMarkerTexture.SetData<Int32>(pixel, 0, terrainMarkerTexture.Width * terrainMarkerTexture.Height);

            _background = new Background(terrainMarkerTexture, _environment);
            _background.LoadContent(Content);

            _shootingManager = new ShootManager(terrainMarkerTexture);
        }
        #endregion

        #region [ Cleanup ]
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        #region [ Loop ]
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();
            var touchCol = TouchPanel.GetState();

            while (TouchPanel.IsGestureAvailable)
            {
                //throw new Exception();
                GestureSample gesture = TouchPanel.ReadGesture();

                Debug.WriteLine("Touch??" + gesture.GestureType);


                if (gesture.GestureType == GestureType.FreeDrag )
                {
                    Debug.WriteLine(string.Format("Pos X:{0} Y:{1} TimeStamp: {2}", gesture.Position.X, gesture.Position.Y, gesture.Timestamp));
                    _shootingManager.Update(gesture);
                }
                if (gesture.GestureType == GestureType.DragComplete)
                {
                    Vector2 kick = _shootingManager.EndShooting(gesture);
                    _ball.Velocity = kick;

                }
                if (gesture.GestureType == GestureType.Tap)
                {
                    _ball.ResetBall();
                }
            }
            _environment.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            //draw each object
            _background.Draw(_spriteBatch);
            _ball.Draw(_spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);
            _topTeam.Draw(_spriteBatch);
            _bottomTeam.Draw(_spriteBatch);

            _shootingManager.DrawDirectionVector(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion

        #region [ Private members ]
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Background _background;
        private Ball _ball;
        private ShootManager _shootingManager;

        private Team _topTeam;
        private Team _bottomTeam;
        GEnvironment _environment;

        private GameState _state;
        #endregion
    }

    public enum GameState
    {
        Idle = 0,
        Shooting = 1,
        ResolvingMotion = 2,
        CutsceneShowing = 3
    }
}
