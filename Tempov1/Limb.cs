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

        public Vector2 position;

        public Texture2D texture;

        public Limb(int x, int y, float scale, Texture2D texture, Body body, World world, Character parent)
        {
            this.texture = texture;
            xOffset = (int)(x - (scale*(texture.Width / 2)));
            //xOffset = x;
            yOffset = y;
            this.scale = scale;
            this.parent = parent;

            position = new Vector2(xOffset, yOffset);

            Body newbody = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(10), ConvertUnits.ToSimUnits(100), 1);
            newbody.BodyType = BodyType.Dynamic;
            newbody.Position = ConvertUnits.ToSimUnits(parent.position + position + parent.circleOrigin);
            newbody.Restitution = 0.1f;
            newbody.Friction = 0.5f;
            JointFactory.CreateWeldJoint(world, body, newbody, Vector2.Zero);
        }

    }
}
