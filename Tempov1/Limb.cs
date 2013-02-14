using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Controllers; // What does this do?

namespace Tempov1
{
    class Limb
    {
        private int xOffset;
        private int yOffset;
        private float scale;
        private Character parent;
        private Color limbColour = Color.Red;   // CHANGE

        public Vector2 position;

        public Texture2D texture;

        // Physics
        public Body limbBody;

        public Limb(int x, int y, float scale, Texture2D texture, Body body, World world, Character parent, Color limbColour)
        {
            this.texture = texture;
            xOffset = x;
            //xOffset = x;
            yOffset = y;
            this.scale = scale;
            this.parent = parent;
            this.limbColour = limbColour;

            position = new Vector2(xOffset, yOffset);

            limbBody = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits((texture.Width * scale)), ConvertUnits.ToSimUnits((texture.Height * scale)), 1);
            limbBody.BodyType = BodyType.Dynamic;
            limbBody.Position = ConvertUnits.ToSimUnits(parent.position + position + parent.circleOrigin);
            limbBody.Restitution = 0.1f;
            limbBody.Friction = 0.5f;
            JointFactory.CreateWeldJoint(world, limbBody, body, Vector2.Zero);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, ConvertUnits.ToDisplayUnits(limbBody.Position), null, limbColour, limbBody.Rotation, new Vector2(texture.Width/2, texture.Height / 2), scale, SpriteEffects.None, 0f);
        }

    }
}
