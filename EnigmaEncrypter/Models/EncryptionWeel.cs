using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaEncrypter.Models
{
    public class EncryptionWheel
    {
        private Dictionary<int, int> _WheelConverter;

        private int _Counter;

        private int _RotatePosition;

        public void SetInitialPosition(int position)
        {
            _Counter = position;
        }

        public EncryptionWheel? Next { get; set; } = null;

        public EncryptionWheel? Previous { get; set; } = null;

        public EncryptionWheel(Dictionary<int, int> abcIntDict)
        {
            _WheelConverter = abcIntDict;

            _RotatePosition = abcIntDict.Count;
        }

        public EncryptionWheel(char[] letters)
        {
            //Defines when next wheel makes rotation
            _RotatePosition = letters.Length;

            _WheelConverter = new Dictionary<int, int>();
            for (int i = 0; i < letters.Length; i++)
            {
                _WheelConverter[i] = letters[i] - 65;
            }
        }

        public int Encrypt(int input, bool isFromKeyToValue = true)
        {
            var intChr = input;

            if (Next == null)
            {
                intChr = FromKeyToValue(intChr);
                intChr = Previous!.Encrypt(intChr, false);
            }
            else
            {
                if (isFromKeyToValue)
                {
                    intChr = FromKeyToValue(intChr);
                    intChr = Next.Encrypt(intChr);
                }
                else
                {
                    intChr = FromValueToKey(intChr);
                    if (Previous == null)
                    {
                        Rotate();
                        return intChr;
                    }
                    intChr = Previous.Encrypt(intChr, false);
                }
            }

            return intChr;
        }

        private void Rotate()
        {
            _Counter++;

            if (_Counter == _RotatePosition && Next?.Next != null)
            {
                Next.Rotate();
            }

            if (_Counter == _WheelConverter.Count)
            {
                Reset();
            }
        }

        private int FromKeyToValue(int value)
        {
            var key = (value + _Counter) % (_WheelConverter.Count());

            bool hasValue = _WheelConverter.TryGetValue(key, out var encryptedValue);

            if (hasValue)
            {
                return encryptedValue;
            }
            return -1;
        }

        private int FromValueToKey(int valueToFind)
        {

            var myKey = _WheelConverter.FirstOrDefault(x => x.Value == valueToFind).Key;

            var updatedKey = (_WheelConverter.Count() + (myKey - _Counter)) % (_WheelConverter.Count());

            return updatedKey;
        }

        internal void InitWheelPoisition(int value)
        {
            _Counter = value;
        }

        private void Reset()
        {
            // reset
            _Counter = 0;
        }

        public void ResetTree()
        {
            // reset
            Reset();
            // return if you are the last node
            if (Next == null) return;
            // call next
            Next.ResetTree();
        }
    }
}
