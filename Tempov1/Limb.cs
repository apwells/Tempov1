using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tempov1
{
    class Limb
    {
        private int xOffset;
        private int yOffset;
        private float scale;

        public Vector2 position;

        public Texture2D texture;

        public Limb(int x, int y, float scale, Texture2D texture)
        {
            this.texture = texture;
            xOffset = (int)(x - (scale*(texture.Width / 2)));
            yOffset = y;
            this.scale = scale;

            position = new Vector2(xOffset, yOffset);

            
        }

    }
}
