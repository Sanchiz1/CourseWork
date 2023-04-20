using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiard
{
    public class Settings
    {
        private bool Music = true;
        private bool SoundEffects = true;
        private bool Aim = true;
        public bool music
        {
            get
            {
                return Music;
            }
            set
            {
                Music = value;
            }
        }
        public bool soundEffects
        {
            get
            {
                return SoundEffects;
            }
            set
            {
                SoundEffects = value;
            }
        }
        public bool aim
        {
            get
            {
                return Aim;
            }
            set
            {
                Aim = value;
            }
        }
        public Settings()
        {

        }
    }
}
