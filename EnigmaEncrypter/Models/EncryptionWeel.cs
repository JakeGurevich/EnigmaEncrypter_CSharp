using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaEncrypter.Models
{
    public class EncryptionWheel
    {
        public Dictionary<int, int> WheelConverter { get; private set; }


        public int Counter { get; private set; }


        public int RotatePosition { get; private set; }

        public void SetInitialPosition(int position)
        {
            Counter = position;
        }

        public EncryptionWheel(Dictionary<int, int> abcIntDict)
        {
            WheelConverter = abcIntDict;

            RotatePosition = abcIntDict.Count - 1;
        }


        /// <summary>
        /// EncryptionWheel => what does this do??
        /// </summary>
        /// <param name="letters">what does the payload means</param>


        public EncryptionWheel(char[] letters)
        {
            //Defines when next wheel makes rotation
            RotatePosition = letters.Length - 1;

            WheelConverter = new Dictionary<int, int>();
            for (int i = 0; i < letters.Length; i++)
            {
                WheelConverter[i] = letters[i] - 65;
            }
        }

        public void Rotate()
        {
            Counter++;
        }

        public int FromKeyToValue(int value)
        {
            var key = (value + Counter) % (WheelConverter.Count());

            bool hasValue = WheelConverter.TryGetValue(key, out var encryptedValue);

            if (hasValue)
            {

                return encryptedValue;
            }
            return -1;
        }


        public int FromValueToKey(int valueToFind)
        {

            var myKey = WheelConverter.FirstOrDefault(x => x.Value == valueToFind).Key;

            var updatedKey = (WheelConverter.Count() + (myKey - Counter)) % (WheelConverter.Count());

            return updatedKey;
        }

        internal void InitWheelPoisition(int value)
        {
            Counter = value;
        }

        public void Reset()
        {
            Counter = 0;

        }


    }
}
