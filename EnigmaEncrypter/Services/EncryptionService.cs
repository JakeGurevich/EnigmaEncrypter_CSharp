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

        private EncryptionWheel _FirstEncryption;

        private EncryptionWheel _SecondEncryption;

        private EncryptionWheel _ThirdEncryption;

        //private EncryptionWheel

        private EncryptionWheel _Reflector;

        private char[] _firstWheel = new[] {'A', 'S', 'D', 'F', 'G', 'H'};
       
        Dictionary<int, int> _FirstAbcIntDict = new Dictionary<int, int> { {0,19}, {2,1}, {1, 2}, {4, 3}, {3, 4},
            {6, 9}, {5, 11}, {8, 16}, {7, 17}, {9, 6},{11, 5},{10, 25}, {12, 24}, {14, 13}, {13, 14}, {15, 23}, {16, 8},
            {18, 20}, {19,0 }, {22,21 }, {17,7 }, {20,18 }, {21,22 }, {25,10 }, {23,15 }, {24,12 } };

        Dictionary<int, int> _SecondAbcIntDict = new Dictionary<int, int> { {3,19}, {1,11}, {0, 14}, {4, 10}, {2, 21},
            {6, 7}, {5, 9}, {8, 20}, {7, 6}, {9, 5},{11, 1},{10, 4}, {12, 23}, {14, 0}, {13, 15}, {17, 22}, {15, 13},
            {16, 24}, {19,3 }, {18,25 }, {25,18 }, {20,8 }, {21,2 }, {22,17 }, {23,12 }, {24,16 } };


        Dictionary<int, int> _ThirdAbcIntDict = new Dictionary<int, int> { {0,11}, {2,6}, {1, 14}, {4, 23}, {3, 21},
            {6, 2}, {5, 9}, {8, 20}, {7, 17}, {9, 5},{11, 0},{10,12 }, {12, 10}, {14, 1}, {13, 16}, {17, 7}, {15, 24},
            {16, 13}, {19,22 }, {18,25 }, {25,18 }, {20,8 }, {21,3 }, {22,19 }, {23,4 }, {24,15 } };


        Dictionary<int, int> _ReflectorAbcIntDict = new Dictionary<int, int>  { {0,19}, {2,1}, {1, 2}, {4, 3}, {3, 4},
            {6, 9}, {5, 11}, {8, 16}, {7, 17}, {9, 6},{11, 5},{10, 25}, {12, 24}, {14, 13}, {13, 14}, {15, 23}, {16, 8},
            {18, 20}, {19,0 }, {22,21 }, {17,7 }, {20,18 }, {21,22 }, {25,10 }, {23,15 }, {24,12 } };



        public EncryptionService()
        {
            _FirstEncryption = new EncryptionWheel(_firstWheel);
            _FirstEncryption = new EncryptionWheel(_FirstAbcIntDict);
            
            _SecondEncryption = new EncryptionWheel(_SecondAbcIntDict);
          
            _ThirdEncryption = new EncryptionWheel(_ThirdAbcIntDict);
          
            _Reflector = new EncryptionWheel(_ReflectorAbcIntDict);
            
        }


        public void SetPosition(int firstWheelStartPosition, int secondWheelStartPosition, int thirdWheelStartPosition)
        {
            if (firstWheelStartPosition != _FirstEncryption.Counter)

                _FirstEncryption.Counter = firstWheelStartPosition;
          

            if (secondWheelStartPosition != _SecondEncryption.Counter)

                _SecondEncryption.Counter = secondWheelStartPosition;
           

            if (thirdWheelStartPosition != _ThirdEncryption.Counter)

                _ThirdEncryption.Counter = thirdWheelStartPosition;
          
        }



        public string EncryptLetter(LetterEnum inputLetter)
        {

            var val= _FirstEncryption.FromKeyToValue((int)inputLetter);

            val = _SecondEncryption.FromKeyToValue(val);

            val = _ThirdEncryption.FromKeyToValue(val);

            val = _Reflector.FromKeyToValue(val);

            val = _ThirdEncryption.FromValueToKey(val);

            val = _SecondEncryption.FromValueToKey(val);

            val = _FirstEncryption.FromValueToKey(val);




            RotateWheels(_FirstEncryption, _SecondEncryption, _ThirdEncryption);

            var outputLetter = _ThirdEncryption.AbcArray[val];

            return outputLetter;
        }

        public string EncryptLetter(char input)
        {
            var intChr = input - 65;

            var val = _FirstEncryption.FromKeyToValue(intChr);

            val = _SecondEncryption.FromKeyToValue(val);

            val = _ThirdEncryption.FromKeyToValue(val);

            val = _Reflector.FromKeyToValue(val);

            val = _ThirdEncryption.FromValueToKey(val);

            val = _SecondEncryption.FromValueToKey(val);

            val = _FirstEncryption.FromValueToKey(val);




            RotateWheels(_FirstEncryption, _SecondEncryption, _ThirdEncryption);

            return ((char)(val + 65)).ToString();

            //var outputLetter = _ThirdEncryption.AbcArray[val];

            //return outputLetter;
        }



        void RotateWheels(EncryptionWheel firstWheel, EncryptionWheel secondWheel, EncryptionWheel thirdWheel)
        {

            firstWheel.Rotate();


            if (firstWheel.Counter > firstWheel.WheelConverter.Count()-1)
            {
                secondWheel.Rotate();

                firstWheel.Counter = 0;

                if (secondWheel.Counter > secondWheel.WheelConverter.Count()-1)
                {
                    thirdWheel.Rotate();

                    secondWheel.Counter = 0;

                }

            }

        }


        void RotateToRight(string[] arr, int times = 1)
        {
            for (int i = 0; i < times; i++)
            {

                string lastElement = arr[(arr.Length - 1)];
                for (int j = (arr.Length - 1); j > 0; j--)
                {
                    arr[j] = arr[j - 1];
                }
                arr[0] = lastElement;
            }
        }

        public void Reset()
        {
            _FirstEncryption.Reset();
            _SecondEncryption.Reset();
            _ThirdEncryption.Reset();

        }

    }
}
