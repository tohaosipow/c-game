﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace MyGame
{
    class Floor : GameObject
    {
        public override GameObjectType type { get => GameObjectType.Floor; set => type = value; }
        public override Sprite Sprite
        {
            get;
            set;
        }
        public Floor()
        {
            if (Sprite == null) Sprite = new Sprite(new Texture(@"sprites/wall.jpg"));
        }

        public Floor(Vector2f position) : base(position)
        {
            if (Sprite == null) Sprite = new Sprite(new Texture(@"sprites/wall.jpg"));
        }
    }
}
