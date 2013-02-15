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
using System.Collections;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Controllers;
using FarseerPhysics.Factories;
using Ruminate.GUI.Content;
using Ruminate.GUI.Framework;
using Ruminate.Utils;
using Ruminate.DataStructures;

namespace Tempov1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Character player;
        World world;    // Physics
        MouseObj mouseObj;
        ArrayList plantArray;
        Vector2 backgroundPos = new Vector2(0, 0);

        float alpha = 3f;


        private FarseerPhysics.DebugViews.DebugViewXNA _debugView;

        private Floor floor;

        ArrayList characterList = new ArrayList();

        public Game1()
        {   
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";

            
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            
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

            player = new Character();
            player.isPlayer = true;
            mouseObj = new MouseObj();  // For tracking the mouse
            

            characterList.Add(player);

            for (int x = 0; x < 1; x++)
            {
                characterList.Add(new Character());
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            world = new World(new Vector2(0, 8.2f));   // World with gravity of 1
            

            Content.Load<SpriteFont>("font");

            _debugView = new FarseerPhysics.DebugViews.DebugViewXNA(world);

            // default is shape, controller, joints
            // we just want shapes to display
            _debugView.RemoveFlags(DebugViewFlags.Controllers);
            _debugView.RemoveFlags(DebugViewFlags.Joint);

            _debugView.LoadContent(GraphicsDevice, Content);


           

            //Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            Random rnd = new Random();
            

            floor = new Floor(Content.Load<Texture2D>("floor"), 1280, 147, 0, (GraphicsDevice.Viewport.TitleSafeArea.Height - 148), world);
            GeneratePlants();

            foreach (Character character in characterList)
            {
                System.Threading.Thread.Sleep(50);
            Vector2 playerPosition = new Vector2(rnd.Next(0,1000), rnd.Next(100,100));
            character.Initialize(Content.Load<Texture2D>("Character/head"),
                Content.Load<Texture2D>("Character/face"),
                Content.Load<Texture2D>("Character/leftarm"),
                Content.Load<Texture2D>("Character/rightarm"),
                Content.Load<Texture2D>("Character/leg"),
                Content.Load<Texture2D>("Character/shading"),
                playerPosition,
                world);

            }



            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            HandleInput();

            // TODO: Add your update logic here
            world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            

            /**** PHYSICS 
             * 
            */
            
                     var projection = Matrix.CreateOrthographicOffCenter(
             0f,
             ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Width),
             ConvertUnits.ToSimUnits(graphics.GraphicsDevice.Viewport.Height), 0f, 0f,
             1f);
         _debugView.RenderDebugData(ref projection);



            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            DrawBackground(spriteBatch);
            //player.Draw(spriteBatch);
            floor.Draw(spriteBatch);

            foreach (Character character in characterList)
            {
                character.Draw(spriteBatch);
            }
            
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);
            TitleScreen(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void HandleInput()
        {
            if (mouseObj.HasClicked())
            {
                // Create a new dude
                Console.WriteLine("Mouse pressed");
                NewCharacter();
            }
        }

        public void NewCharacter()
        {
            Random rnd = new Random();
            Character newChar = new Character();
            characterList.Add(newChar);
            Vector2 playerPosition = new Vector2(rnd.Next(0, 1000), rnd.Next(0, 50));
            newChar.Initialize(Content.Load<Texture2D>("Character/head"),
                Content.Load<Texture2D>("Character/face"),
                Content.Load<Texture2D>("Character/leftarm"),
                Content.Load<Texture2D>("Character/rightarm"),
                Content.Load<Texture2D>("Character/leg"),
                Content.Load<Texture2D>("Character/shading"),
                playerPosition,
                world);

        }

        /*
         *  TODO Put this stuff in its own class
         */
        private void DrawBackground(SpriteBatch spriteBatch)
        {

            Texture2D background = Content.Load<Texture2D>("background");
            backgroundPos = backgroundPos - new Vector2((0.1f), 0f);
            spriteBatch.Draw(background, backgroundPos, Color.White);
            spriteBatch.Draw(background, backgroundPos+ new Vector2(background.Width, 0), Color.White);
            if (backgroundPos == new Vector2(-background.Width, 0))
            {
                backgroundPos = Vector2.Zero;
            }

            foreach (Vector4 plant in plantArray)
            {
                String plantTex = "plant" + plant.W;
                Texture2D texture = Content.Load<Texture2D>(plantTex);
                spriteBatch.Draw(texture, new Vector2(plant.X, plant.Y), null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), plant.Z, SpriteEffects.None, 0f);
            }

            Texture2D light = Content.Load<Texture2D>("light");
            spriteBatch.Draw(light, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        private void GeneratePlants()
        {
            Random rnd = new Random();
            int plants = rnd.Next(1, 8);

            plantArray = new ArrayList(5);

            

            for (int x = 0; x < plants; x++)
            {
                // Vector3 is X, Y, Scale, plantSprite
                Vector4 plant = new Vector4(rnd.Next(0, graphics.GraphicsDevice.Viewport.Width), floor.position.Y, (float)Math.Max(rnd.NextDouble(), 0.4), rnd.Next(1, 4));
                plantArray.Add(plant);
                
            }
        }

        private void TitleScreen(GameTime gameTime, SpriteBatch spriteBatch)
        {
            alpha = alpha - (float)(gameTime.ElapsedGameTime.TotalMilliseconds/1000);
            Console.WriteLine(alpha);
            Texture2D texture = Content.Load<Texture2D>("title");
            spriteBatch.Draw(texture, Vector2.Zero, null, new Color(255,255,255, alpha), 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}
