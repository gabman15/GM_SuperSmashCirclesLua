﻿using NLua;
using System;

namespace GM_SuperSmashCircles
{
    /// <summary>
    /// input module based in lua code
    /// </summary>
    public class LuaInputModule : InputModule
    {
        /// <summary>
        /// a private reference to the game
        /// </summary>
        private SSCGame game;
        /// <summary>
        /// the lua state
        /// </summary>
        public Lua State { get; set; }
        /// <summary>
        /// lua function to be tirggered when the game updates
        /// </summary>
        public LuaFunction OnUpdate { get; set; }
        /// <summary>
        /// creates a new lua-based input module
        /// </summary>
        /// <param name="game">a reference to the game</param>
        /// <param name="filename">the lua file's name to load the lua input module from</param>
        public LuaInputModule(SSCGame game, string filename)
        {
            State = new Lua();
            State.LoadCLRPackage();
            State.DoFile(filename);
            State.GetFunction("OnCreation")?.Call();
            OnUpdate = State.GetFunction("OnUpdate");
            game.Events.On("update", () =>
            {
                OnUpdate?.Call();
            });
        }
        /// <summary>
        /// checks from the lua state if the given input is pressed
        /// </summary>
        /// <param name="name">name of the input to check</param>
        /// <returns>if the given input is pressed</returns>
        public override bool Get(string name)
        {
            bool result;
            try
            {
                result = (bool)State.GetFunction("Get")?.Call(name)[0];
            }
            catch (InvalidCastException)
            {
                return false;
            }
            return result;
        }
    }
}