using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFallGame2D.Clases;
using Statics;
using System;

namespace MonoFallGame2D
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch sBatch;

        private Loader loader;

        private Player player;
        private Level level;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new(new Vector2(100f, 100f));
            level = new();
            GameStatics.RectanglesList = level.CreateLevel();
            loader = new(this.Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sBatch = new SpriteBatch(GraphicsDevice);

            loader.Load();
            player.LoadSprite();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(delta);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            sBatch.Begin();
            player.Draw(sBatch);
            sBatch.End();

            base.Draw(gameTime);
        }
    }
}
