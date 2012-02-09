using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BallGameWin
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BallGame : Microsoft.Xna.Framework.Game
    {
        public static BallGame Instance = null;

        //Gjort disse public for senere bruk.
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;

        List<Ball> Baller = new List<Ball>();   //Antagelig det samme som en Array??
        
        //Holder informasjon om hvor musepekeren var i forrige frame.
        MouseState PreviousMouse = Mouse.GetState();

        public BallGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Graphics.PreferredBackBufferWidth = 800;        //Beskriver Størrelsen på vinduet
            Graphics.PreferredBackBufferHeight = 480;       //Beskriver Størrelsen på vinduet

            //Gjør musepekeren synlig
            IsMouseVisible = true;

            //Dette gjør at telefonen fungerer i begge landskapsmodusene.
            Graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            Instance = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use Content to load your game content here
            Texture2D balltexture = Content.Load<Texture2D>("smiley");

            //Ballen = new Ball(balltexture, new Vector2(400f,240f));   lager en ny ball
            //Texture = Content.Load<Texture2D>("Ball");
            //HalfTexture = Texture.Size() * 0.5f;

            Random r = new Random((int)DateTime.Now.Ticks / 98246);

            for (int i = 0; i < 5; i++)
            {
                Baller.Add(new Ball(balltexture, new Vector2(50 + 700 * (float)r.NextDouble(), 50 + 380 * (float)r.NextDouble())));
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your update logic here

            //Henter inn mouseState på starten av Update, så den ikke kan endre seg underveis
            MouseState mouseState = Mouse.GetState();

            foreach (Ball ball in Baller)
            {
                ball.Update(deltaTime, mouseState, PreviousMouse);
            }
            
            //Ball.Update(deltaTime, mouseState, PreviousMouse);
            

            //oppdaterer PreviousMouse
            PreviousMouse = mouseState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

            // TODO: Add your drawing code here

            SpriteBatch.Begin();
            //Baller.Draw(SpriteBatch);
            foreach (Ball ball in Baller)
            {
                ball.Draw(SpriteBatch);
            }
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
