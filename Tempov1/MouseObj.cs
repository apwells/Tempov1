using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tempov1
{
    
    class MouseObj
    {
        MouseState currentMouse;
        MouseState lastMouse;


        public bool HasClicked()
        {
            currentMouse = Mouse.GetState();

            lastMouse = currentMouse;
            return false;
        }

        public int whichClick()
        {
            currentMouse = Mouse.GetState();
            if (currentMouse.RightButton == ButtonState.Pressed &&
                lastMouse.RightButton == ButtonState.Released)
            {
                lastMouse = currentMouse;
                return 2;
            }
            if (currentMouse.LeftButton == ButtonState.Pressed &&
                lastMouse.LeftButton == ButtonState.Released)
            {
                lastMouse = currentMouse;
                return 1;
            }
            lastMouse = currentMouse;
            return 0;
        }
    }
}
