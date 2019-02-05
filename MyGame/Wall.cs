using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace MyGame
{
    class Wall : GameObject
    {
        public override GameObjectType type { get; set; }
        public override Sprite Sprite
        {
            get;
            set;
        }
        public Wall()
        {
            if (Sprite == null) Sprite = new Sprite(new Texture(SeasonController.getInstance().getSeasonTexture()));
            type = GameObjectType.Wall;
        }

        public Wall(Vector2f position) : base(position)
        {
            if (Sprite == null) Sprite = new Sprite(new Texture(SeasonController.getInstance().getSeasonTexture()));
            type = GameObjectType.Wall;
        }
    }
}
