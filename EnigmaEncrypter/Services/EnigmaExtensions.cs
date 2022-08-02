using EnigmaEncrypter.Models;
using System;

namespace EnigmaEncrypter.Services
{
    public static class EnigmaExtensions
    {
        static int searchLevel = 0;

        public static void SetInitialPosition(this EncryptionWheel wheel, int level, int value)
        {
            if (wheel == null)
            {
                throw new ArgumentNullException(nameof(wheel));
            }
            if (searchLevel++ == level)
            {
                searchLevel = 0;
                wheel.SetInitialPosition(value);
            }
            else
            {
                SetInitialPosition(wheel.Next!, searchLevel, value);
            }
        }
    }
}
