using EnigmaEncrypter.Models;
using EnigmaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class EncryptionService
    {

        private char[] _firstWheel = new[] {'W', 'N', 'F', 'R', 'E', 'C', 'A', 'B', 'P', 'G', 'X', 'L',
                 'S', 'T', 'O', 'Q', 'D', 'I',  'M', 'J', 'H', 'V', 'U', 'K','Y', 'Z' };

        private char[] _secondWheel = new[] { 'T', 'Y', 'U', 'B', 'W', 'I', 'G', 'P', 'M', 'Q', 'R', 'E',
                 'F', 'O', 'Z', 'H', 'J', 'K',  'S', 'A', 'V', 'C', 'L', 'X','D', 'N' };

        private char[] _thirdWheel = new[] { 'S', 'A', 'B', 'I', 'X', 'V', 'G', 'H', 'W', 'J', 'K', 'M',
                 'L', 'N', 'U', 'Z', 'O', 'R',  'C', 'T', 'Q', 'F', 'D', 'E','Y', 'P' };

        private char[] _reflector = new[] { 'R', 'U', 'S', 'M', 'J', 'Z', 'O', 'W', 'Y', 'E', 'X', 'Q','D',
                                            'T', 'G', 'V', 'L', 'A',  'C', 'N', 'B', 'P', 'H', 'K','I', 'F' };

        private EncryptionWheel _root;

        public EncryptionWheel Root => _root;


        public EncryptionService()
        {
            _root = new EncryptionWheel(_firstWheel);
            var secondWheel = new EncryptionWheel(_secondWheel);
            _root.Next = secondWheel;
            secondWheel.Previous = _root;

            var thirdWheel = new EncryptionWheel(_thirdWheel);
            secondWheel.Next = thirdWheel;
            thirdWheel.Previous = secondWheel;

            var reflector = new EncryptionWheel(_reflector);
            thirdWheel.Next = reflector;
            reflector.Previous = thirdWheel;

        }

        public string Encrypt(char input)
        {
            var intChar = input - 65;

            intChar = _root.Encrypt(intChar);

            return ((char)(intChar + 65)).ToString();


        }



        public void Reset() => _root.ResetTree();

    }
}
