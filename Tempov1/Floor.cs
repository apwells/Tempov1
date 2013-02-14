using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tempov1
{
    class Floor
    {
        private Texture2D texture;
        //private int width;
        //private int height;

        public Vector2 boundingBox;
        public Vector2 position;

        public Floor(Texture2D texture, int width, int height, int x, int y)
        {
            this.texture = texture;  //Content.Load<Texture2D>("floor.png");   // 1280x147
            //this.width = width;
            //this.height = height;
            this.boundingBox = new Vector2(width, height);
            position = new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
