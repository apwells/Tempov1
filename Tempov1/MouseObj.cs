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
            if (currentMouse.LeftButton == ButtonState.Pressed &&
                lastMouse.LeftButton == ButtonState.Released)
            {
                lastMouse = currentMouse;
                return true;
            }
            lastMouse = currentMouse;
            return false;
        }
    }
}
