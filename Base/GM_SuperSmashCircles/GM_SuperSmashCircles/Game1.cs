﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace GM_SuperSmashCircles
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //DllImport(NativeLibName, CallingConvention= CallingConvention.Cdecl);
        //public static extern int SDL_GameControllerAddMapping(string mappingString);
        public List<Entity> Entities { get; set; }
        //public List<User> Users { get; set; }
        //public List<Platform> Platforms { get; set; }
        public Gamemode CurrentGamemode { get; set; }

        public double Gravity { get; set; }
        
        public Game1()
        {
            GamePad.InitDatabase();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Debug.WriteLine(Joystick.GetState(1).IsConnected);
            
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
            /*
            Entity testentity = Entity.LoadFromFile("testentity.lua");
            testentity.OnCollision.Call();
            testentity.OnUpdate.Call();
            Console.WriteLine("{0} {1} {2} {3} {4}", testentity.X, testentity.Y, testentity.Width, testentity.Height, testentity.RepelAmount);*/
            //Gamemode gamemode = Gamemode.LoadFromFile("testgamemode.lua");

            Gravity = 0;

            Entities = new List<Entity>();
            CurrentGamemode = Gamemode.LoadFromFile("gamemode_default.lua", this);
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

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.Four).Buttons.B == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach(Entity e in Entities)
            {
                e.OnUpdate?.Call();
                e.DY += Gravity; //might want to do gravity differently in the future
                //collision checking here maybe, although it might need to be somewhere else
            }
            CurrentGamemode.OnUpdate?.Call();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void CreateEntity(string filename)
        {
            Entities.Add(Entity.LoadFromFile(filename, this));
        }
    }
}
