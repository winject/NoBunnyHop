using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace NoBunnyHop
{
   public static class OnKeyDown
    {
        public static bool Sprint
        {
            get {
                return Game.IsControlPressed(2, Control.Sprint);
            }
        }

        public static bool Jump
        {
            get
            {
                return Game.IsControlJustReleased(2, Control.Jump);
            }
        }

        public static bool Forward
        {
            get
            {
                return Game.IsControlJustReleased(2, Control.MoveUpOnly);
            }
        }
    }
}
