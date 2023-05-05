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
        private string Language;
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
        public string language
        {
            get
            {
                return Language;
            }
            set
            {
                Language = value;
            }
        }
        public Settings()
        {

        }
    }
}
