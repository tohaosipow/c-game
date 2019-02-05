using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyGame
{
    enum GameObjectType
    {
        Floor,
        Wall,
        Player
    }
    abstract class GameObject : Transformable, Drawable
    {

        public GameObjectType Type { set; get; }
        public double mass { get; set; } = 1.0;

        public GameObject()
        {

        }

        public GameObject(Vector2f position)
        {
            Position = position;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(Sprite, states);
        }

        abstract public GameObjectType type { get; set; }
        abstract public Sprite Sprite { set; get; }
        public Vector2f Velocity { set; get; }
  
    }
}