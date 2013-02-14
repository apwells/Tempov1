using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;


namespace Tempov1
{
    class Character
    {
        // Animation representing the player
        public Texture2D playerTexture;
        private Texture2D leftArmTexture;
        private Texture2D rightArmTexture;
        private Texture2D legTexture;

        // Position of the Player relative to the upper left side of the screen
        public Vector2 position;

        // State of the player
        public bool active;

        public int health;

        // Changable attributes
        private int legs;
        private float scale;    // Ranges between 0-1
        // private int heads;
        private int arms;
        private Color colour;
        private Color limbColour;

        private ArrayList legArray = new ArrayList();

        public int width
        {
            get { return playerTexture.Width; }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return playerTexture.Height; }
        }

        public void Initialize(Texture2D headTexture, Texture2D leftArmTexture, Texture2D rightArmTexture, Texture2D legTexture, Vector2 position)
        {
            
            playerTexture = headTexture;
            this.leftArmTexture = leftArmTexture;
            this.rightArmTexture = rightArmTexture;
            this.legTexture = legTexture;
            
            this.position = position;
            Generate();
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawLimbs(spriteBatch);
            spriteBatch.Draw(playerTexture, position, null, colour, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            
        }

        private void Generate()
        {
            double minSize = 0.5;

            Random rnd = new Random();
            scale = (float)Math.Max((rnd.NextDouble()), minSize);
            legs = rnd.Next(0, 5);

            arms = rnd.Next(0, 4);
            colour = new Color(rnd.Next(60, 255), 
                               rnd.Next(60, 255), 
                               rnd.Next(60, 255));

            limbColour = new Color(rnd.Next(180, 255),
                                    rnd.Next(180, 255),
                                    rnd.Next(180, 255));



            for (int x = 1; x <= legs; x++)
            {
                float legOffset = scale* ((float)width * ((float)x / ((float)legs + 1f)));

                Console.WriteLine("leg offset was " + legOffset + ". Width is " + width);
                legArray.Add(new Limb((int)legOffset, (int)(150*scale), scale, legTexture));
            }
        }

        private void DrawLimbs(SpriteBatch spriteBatch)
        {
            foreach (Limb limb in legArray)
            {
                Vector2 limbPosition = limb.position;
                Vector2 newPosition = Vector2.Add(limb.position, position);
                spriteBatch.Draw(limb.texture, newPosition,null, limbColour, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }


    }
}
