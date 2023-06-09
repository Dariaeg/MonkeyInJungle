using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TiledSharp;

namespace MonkeyInJungle.project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static float screenWidth;
        public static float screenHeight;

        #region TileMap
        private TmxMap map;
        private TilemapManager tilemapManager;
        private Texture2D tileset;
        private List<Rectangle> collisions;
        private Rectangle start;
        private Rectangle end;
        #endregion

        #region Creators
        private GameCreator _gameCreator;
        #endregion

        #region Player
        private Player player;
        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 540;
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.ApplyChanges();
            screenHeight = _graphics.PreferredBackBufferHeight;
            screenWidth = _graphics.PreferredBackBufferWidth;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            #region TileMap
            map = new TmxMap("Content\\level1.tmx");
            tileset = Content.Load<Texture2D>("JungleTileset\\" + map.Tilesets[0].Name.ToString());
            int tileWidth = map.Tilesets[0].TileWidth;
            int tileHeight = map.Tilesets[0].TileHeight;
            int tilesetWidth = tileset.Width/ tileWidth;

            tilemapManager = new TilemapManager(map, tileset, tilesetWidth, tileWidth, tileHeight);
            #endregion

            #region Collision
            collisions = new List<Rectangle>();

            foreach (var o in map.ObjectGroups["Collisions"].Objects)
            {
                if (o.Name == "")
                {
                    collisions.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
                }
                if (o.Name == "start")
                {
                    start = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
                }
                if (o.Name == "end")
                {
                    end = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);

                }
            }

            #endregion

            _gameCreator = new GameCreator(end);


            #region Player
            player = new Player(
                new Vector2(start.X, start.Y),
                Content.Load<Texture2D>("sprite2"),
                Content.Load<Texture2D>("sprites1"),
                Content.Load<Texture2D>("sprites2")
            );
            #endregion
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
         
            var initPosition = player.position;
            player.UpDate();
            
            #region Player Collisions
            foreach (var rect in collisions)
            {
                player.isFalling = true;
                if (rect.Intersects(player.hitbox))
                {
                    player.isFalling = false;
                    break;
                }
            }

            foreach(var rect in collisions)
            {
                if (rect.Intersects(player.hitbox))
                {
                    player.position.X = initPosition.X;
                    player.velocity.X = initPosition.X;
                    break;
                }
            }
            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            tilemapManager.Draw(_spriteBatch);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            player.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}