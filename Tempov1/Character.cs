using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Controllers; // What does this do?

namespace Tempov1
{
    class Character
    {
        // Animation representing the player
        public Texture2D playerTexture;
        private Texture2D faceTexture;
        private Texture2D leftArmTexture;
        private Texture2D rightArmTexture;
        private Texture2D legTexture;
        private Texture2D shadowTexture;

        // Position of the Player relative to the upper left side of the screen
        public Vector2 position;

        // State of the player
        public bool isPlayer;

        public int health;

        // Changable attributes
        private int legs;
        private float scale;    // Ranges between 0-1
        // private int heads;
        private int arms;
        private Color colour;
        private Color limbColour;

        public Vector2 circleOrigin;

        // PHYSICS
        public Body body;
        public Fixture fixture;
        public World world;

        public ArrayList legArray = new ArrayList();

        public int width
        {
            get { return playerTexture.Width; }
        }

        // Get the height of the player ship
        public int height
        {
            get { return playerTexture.Height; }
        }

        public void Initialize(Texture2D headTexture, Texture2D faceTexture, Texture2D leftArmTexture, Texture2D rightArmTexture, Texture2D legTexture, Texture2D shadowTexture, Vector2 position, World world)
        {
            
            playerTexture = headTexture;
            this.faceTexture = faceTexture;
            this.leftArmTexture = leftArmTexture;
            this.rightArmTexture = rightArmTexture;
            this.shadowTexture = shadowTexture;
            this.legTexture = legTexture;
            this.position = position;
            this.world = world;

            Generate();
            // Physics setup

            float circleRadius = ConvertUnits.ToSimUnits((( playerTexture.Width /2)*scale));   // 301px radius

            body = BodyFactory.CreateCircle(world, circleRadius, 4f);    // Radius 10, desity 1
            body.BodyType = BodyType.Dynamic;
            body.Position = ConvertUnits.ToSimUnits(position) + ConvertUnits.ToSimUnits(circleOrigin);
            body.Restitution = 0.3f;
            body.Friction = 0.5f;
            //body.OnCollision += MyOnCollision;

            //CircleShape shape = new CircleShape(0.5f);

            //Fixture fixture = body.CreateFixture(shape);
            BuildLegs();
            
            
        }

        //public override bool MyOnCollision(Fixture f1, Fixture f2, Contact contact)

        //{

        //    //We want the collision to happen, so we return true.

        //    return true;

        //} 

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            

            position = body.Position;
            DrawLimbs(spriteBatch);
            spriteBatch.Draw(playerTexture, ConvertUnits.ToDisplayUnits(body.Position), null, colour, body.Rotation, circleOrigin, scale, SpriteEffects.None, 1f);
            spriteBatch.Draw(faceTexture, ConvertUnits.ToDisplayUnits(body.Position), null, colour, body.Rotation, circleOrigin/5, scale, SpriteEffects.None, 1f);
            spriteBatch.Draw(shadowTexture, ConvertUnits.ToDisplayUnits(body.Position), null, colour, 0f, circleOrigin, scale, SpriteEffects.None, 1f);


            // DEBUGGING CODE. TO DELETE
            if (isPlayer)
            {
                // Console.WriteLine("Physics loc = " + body.Position.X + "," + body.Position.Y);
            }
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

            limbColour = new Color(rnd.Next(100, 255),
                                    rnd.Next(100, 255),
                                    rnd.Next(100, 255));






            // circleOrigin = new Vector2((playerTexture.Width /2)*scale, (playerTexture.Height /2)*scale);
            circleOrigin = new Vector2((playerTexture.Width / 2), (playerTexture.Height / 2));

            Console.WriteLine("Circle Origin returning as " + circleOrigin + ". Scale is " + scale + ". Width is " + playerTexture.Width);

            // DELETE ME
            
        }

        private void BuildLegs()
        {
            for (int x = 1; x <= legs; x++)
            {
                float legOffset = scale * ((float)width * ((float)x / ((float)legs + 1f)) - width/2);

                Console.WriteLine("leg offset was " + legOffset + ". Width is " + width);
                Limb limb = new Limb((int)legOffset, (int)(150 * scale), scale, legTexture, body, world, this, limbColour, 1);
                legArray.Add(limb);
            }

            Random rnd = new Random();
            if (rnd.Next(0, 2) == 1)
            {
                legArray.Add(new Limb((int)((-1) * ((playerTexture.Width / 2) * scale)), (int)(90 * scale), scale, leftArmTexture, body, world, this, limbColour, 2));
                legArray.Add(new Limb((int)(((playerTexture.Width / 2) * scale)), (int)(90 * scale), scale, rightArmTexture, body, world, this, limbColour, 3));
            }
        }

        private void DrawLimbs(SpriteBatch spriteBatch)
        {
            foreach (Limb limb in legArray)
            {
                //Vector2 limbPosition = limb.position;
                //Vector2 newPosition = Vector2.Add(ConvertUnits.ToDisplayUnits(limb.body.Position), ConvertUnits.ToDisplayUnits(body.Position));
                limb.Draw(spriteBatch);
            }
        }

        public void DeletePhysics()
        {
            world.RemoveBody(body);
            foreach (Limb limb in legArray)
            {
                world.RemoveBody(limb.limbBody);
            }
        }


    }
}
