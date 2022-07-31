using EnigmaEncrypter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaEncrypter.Services
{
    public class EncryptionService
    {
        //private EncryptionWheel

        private EncryptionWheel _Reflector;

        private char[] _firstWheel = new[] {'W', 'B', 'F', 'R', 'E', 'C', 'G', 'J', 'P', 'H', 'X', 'L',
                 'M', 'T', 'O', 'I', 'Q', 'D',  'S', 'N', 'U', 'V', 'A', 'K',
                 'Y', 'Z' };

        private char[] _secondWheel = new[] { 'T', 'D', 'U', 'U', 'W', 'I', 'G', 'P', 'M', 'Q', 'R', 'L',
                 'F', 'O', 'N', 'H', 'J', 'K',  'S', 'A', 'C', 'V', 'E', 'X',
                 'Y', 'Z' };

        private char[] _thirdWheel = new[] { 'B', 'A', 'S', 'D', 'X', 'V', 'G', 'H', 'W', 'J', 'K', 'M',
                 'L', 'N', 'O', 'P', 'U', 'R',  'C', 'T', 'Q', 'F', 'I', 'E',
                 'Y', 'Z' };


        private char[] _reflector = new[] { 'S', 'U', 'C', 'P', 'M', 'F', 'G', 'H', 'I', 'W', 'X', 'L',
                 'E', 'N', 'O', 'D', 'Q', 'R',  'A', 'T', 'B', 'V', 'J', 'K',
                 'Y', 'Z' };


        private List<EncryptionWheel> _encryptionWheels = new List<EncryptionWheel>();
        public Dictionary<int, EncryptionWheel> Wheels = new Dictionary<int, EncryptionWheel>();



        public EncryptionService()
        {

            _encryptionWheels.Add(new EncryptionWheel(_firstWheel));

            _encryptionWheels.Add(new EncryptionWheel(_secondWheel));

            _encryptionWheels.Add(new EncryptionWheel(_thirdWheel));

            for (int i = 0; i < _encryptionWheels.Count; i++)
            {
                Wheels[i] = _encryptionWheels[i];
            }

            _Reflector = new EncryptionWheel(_reflector);

        }


        //public void SetPosition(int wheelStartPosition, int wheelNumber)
        //{

        //    for (var i = 0; i < _encryptionWheels.Count(); i++)
        //    {
        //        if (i == wheelNumber)
        //        {
        //            _encryptionWheels[i].SetInitialPosition(wheelStartPosition);
        //        }
        //    }
        //}


        public string EncryptLetter(char input)
        {
            var intChr = input - 65;

            foreach (var wheel in _encryptionWheels)
            {
                intChr = wheel.FromKeyToValue(intChr);
            }

            intChr = _Reflector.FromKeyToValue(intChr);

            for (var i = _encryptionWheels.Count() - 1; i >= 0; i--)
            {
                intChr = _encryptionWheels[i].FromValueToKey(intChr);
            }

            RotateWheels();

            return ((char)(intChr + 65)).ToString();


        }

        void RotateWheels()
        {
            var updateCount = true;

            for (var i = 0; i < _encryptionWheels.Count(); i++)
            {
                if (updateCount)
                {
                    _encryptionWheels[i].Rotate();
                    updateCount = false;
                }

                if (_encryptionWheels[i].Counter == _encryptionWheels[i].RotatePosition)
                {
                    updateCount = true;
                }

                if (_encryptionWheels[i].Counter == _encryptionWheels[i].WheelConverter.Count())
                {
                    _encryptionWheels[i].Reset();
                }
            }

        }

        public void Reset()
        {
            foreach (var wheel in _encryptionWheels)
                wheel.Reset();

        }

    }
}
