using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaEncrypter.Models
{
    public class EncryptionWheel
    {

        public string[] AbcArray = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public Dictionary<int, int> WheelConverter { get; private set; }


        private int _Counter;

        public int Counter
        {
            get { return _Counter; }
            set { _Counter = value; }

        }



        public EncryptionWheel(Dictionary<int, int> abcIntDict, int wheelStartPosition = 0)
        {
            WheelConverter = abcIntDict;

            Counter = wheelStartPosition;
        }




        public void Rotate()
        {
            Counter++;
        }


        public int FromKeyToValue(int value)
        {
            var key = (value + Counter) % (WheelConverter.Count());

            int encryptedValue;

            bool hasValue = WheelConverter.TryGetValue(key, out encryptedValue);

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


        public void Reset()
        {
            _Counter = 0;

        }


    }
}
