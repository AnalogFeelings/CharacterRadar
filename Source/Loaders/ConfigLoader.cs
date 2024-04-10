#region License Information (MIT)
// MIT License
// 
// Copyright (c) 2021-2024 Analog Feelings
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterRadar.Loaders
{
    /// <summary>
    /// Class that handles loading plugin settings.
    /// </summary>
    internal class ConfigLoader
    {
        public readonly ConfigEntry<bool> UseHashColors;

        public readonly Dictionary<Character, Color> CharacterColors;

        /// <summary>
        /// Creates a new config loader.
        /// </summary>
        /// <param name="file">The plugin's config file.</param>
        public ConfigLoader(ConfigFile file)
        {
            if(_instance != null)
                throw new InvalidOperationException("An instance has already been created.");
            
            _instance = this;

            UseHashColors = file.Bind("Settings", "UseHashColors", true,
                "Use a hashing algorithm for unknown NPC colors. Recommended.");
            
            ConfigEntry<Color> baldiColor = file.Bind("Colors", "BaldiColor",
                new Color(0, 1, 0, 1), "The color of the Baldi arrow.");

            ConfigEntry<Color> beansColor = file.Bind("Colors", "BeansColor",
                new Color(1, 0, 1, 1), "The color of the Beans arrow.");

            ConfigEntry<Color> bullyColor = file.Bind("Colors", "BullyColor",
                new Color(0.8f, 0.4f, 0.01f, 1), "The color of the It's a Bully arrow.");

            ConfigEntry<Color> chalklesColor = file.Bind("Colors", "ChalklesColor",
                new Color(0.58f, 0.2f, 0, 1), "The color of the Chalkles arrow.");

            ConfigEntry<Color> craftersColor = file.Bind("Colors", "CraftersColor",
                new Color(0.76f, 0.76f, 0.76f, 1), "The color of the Arts and Crafters arrow.");

            ConfigEntry<Color> cumuloColor = file.Bind("Colors", "CumuloColor",
                new Color(0.4f, 0.3f, 0.4f, 1), "The color of the Cloudy Copter arrow.");

            ConfigEntry<Color> testColor = file.Bind("Colors", "TestColor",
                new Color(0.2f, 0.2f, 0.18f, 1), "The color of The Test's arrow.");

            ConfigEntry<Color> playtimeColor = file.Bind("Colors", "PlaytimeColor",
                new Color(1, 0, 0, 1), "The color of the Playtime arrow.");

            ConfigEntry<Color> pompColor = file.Bind("Colors", "PompColor",
                new Color(0.97f, 1, 0.4f, 1), "The color of Mrs. Pomp's arrow.");

            ConfigEntry<Color> principalColor = file.Bind("Colors", "PrincipalColor",
                new Color(0.5f, 0.5f, 0.5f, 1), "The color of the Principal's arrow.");

            ConfigEntry<Color> prizeColor = file.Bind("Colors", "PrizeColor",
                new Color(0.05f, 0.66f, 0.58f, 1), "The color of First Prize's arrow.");

            ConfigEntry<Color> sweepColor = file.Bind("Colors", "SweepColor",
                new Color(0, 0.56f, 0.15f, 1), "The color of Gotta Sweep's arrow.");
            
            ConfigEntry<Color> reflexColor = file.Bind("Colors", "RelexColor",
                new Color(0, 0.29f, 0.5f, 1), "The color of Dr. Reflex's arrow.");

            ConfigEntry<Color> unknownColor = file.Bind("Colors", "UnknownColor",
                new Color(0, 0, 1, 1), "The arrow color of an unknown character.");

            CharacterColors = new Dictionary<Character, Color>()
            {
                [Character.Null] = unknownColor.Value,
                [Character.Baldi] = baldiColor.Value,
                [Character.Beans] = beansColor.Value,
                [Character.Bully] = bullyColor.Value,
                [Character.Chalkles] = chalklesColor.Value,
                [Character.Crafters] = craftersColor.Value,
                [Character.Cumulo] = cumuloColor.Value,
                [Character.LookAt] = testColor.Value,
                [Character.Playtime] = playtimeColor.Value,
                [Character.Pomp] = pompColor.Value,
                [Character.Principal] = principalColor.Value,
                [Character.Prize] = prizeColor.Value,
                [Character.Sweep] = sweepColor.Value,
                [Character.DrReflex] = reflexColor.Value
            };
        }
        
        private static ConfigLoader _instance;
        
        /// <summary>
        /// The current instance of the <see cref="ConfigLoader"/>.
        /// </summary>
        public static ConfigLoader Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("No instance has been created.");
                
                return _instance;
            }
        }
    }
}
