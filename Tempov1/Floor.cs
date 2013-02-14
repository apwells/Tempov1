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

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Tempov1
{
    class Floor
    {
        private Texture2D texture;
        private int width;
        private int height;

        public Vector2 boundingBox;
        public Vector2 position;

        private World world;
        private Body body;

        public Floor(Texture2D texture, int width, int height, int x, int y, World world)
        {
            this.texture = texture;  //Content.Load<Texture2D>("floor.png");   // 1280x147
            this.width = width;
            this.height = height;
            this.boundingBox = new Vector2(width, height);
            position = new Vector2(x, y);
            this.world = world;

            float widthSim = ConvertUnits.ToSimUnits(width),
                heightSim = ConvertUnits.ToSimUnits(height);

            body = BodyFactory.CreateRectangle(world, widthSim, heightSim, 1);
            body.IsStatic = true;
            body.Restitution = 0.3f;
            body.Friction = 0.5f;
            body.Position = ConvertUnits.ToSimUnits(position + new Vector2(width/2, height/2));

            Console.WriteLine("position of floor = " + position.X + "," + position.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //position = body.Position;
            spriteBatch.Draw(texture, ConvertUnits.ToDisplayUnits(body.Position), null, Color.White, 0f, new Vector2((width / 2), (height / 2)), 1f, SpriteEffects.None, 1f);
            //spriteBatch.Draw(texture, ConvertUnits.ToDisplayUnits(body.Position), Color.White);
        }

    }
}
