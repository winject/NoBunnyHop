using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBunnyHop
{
    class Cooldown
    {
        int _limit;
        int _tempLimit;
        int _counter;
        bool _expired;

        public int Time { set { _tempLimit = value; _limit = CitizenFX.Core.Game.GameTime + _tempLimit; } }
        public int MaximumFrame { get; set;  }
        public int FrameCount { get { return _counter; } }
        public bool HasExpired
        {
            get
            {

                if (CitizenFX.Core.Game.GameTime > _limit)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public Cooldown(int h)
        {

        }

        public void CountThisFrame()
        {
            _counter++;
        }

        public void Reset()
        {
            _limit = 0;
            _tempLimit = 0;
            _expired = false;
            _counter = 0;
        }

        public void ResetFrameCounter()
        {
            _counter = 0;
        }
    }
}
