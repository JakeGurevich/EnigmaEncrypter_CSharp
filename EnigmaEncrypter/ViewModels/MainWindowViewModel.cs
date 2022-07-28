using Base;
using EnigmaEncrypter.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaEncrypter.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {

        private readonly EncryptionService _Enigma;
        private const int MAX_WHELL_VALUE = 26;
        private const int MIN_WHELL_VALUE = 0;
        //private string _Result;

        //public string Result
        //{
        //    get { return _Result; }
        //    set
        //    {
        //        SetProperty(ref _Result, value);
        //    }
        //}

        private string _Input;

        public string Input
        {
            get { return _Input; }
            set
            {
                SetProperty(ref _Input, value);
            }
        }


        private string _Output;

        public string Output
        {
            get { return _Output; }
            set
            {

                SetProperty(ref _Output, value);
            }
        }


        private int _FirstWheelStartPosition = 0;

        public int FirstWheelStartPosition
        {
            get { return _FirstWheelStartPosition; }
            set
            {
                if (value > MAX_WHELL_VALUE)
                {
                    value = MAX_WHELL_VALUE;
                }
                if (value < MIN_WHELL_VALUE)
                {
                    value = MIN_WHELL_VALUE;
                }

                if (SetProperty(ref _FirstWheelStartPosition, value))
                {
                    _Enigma?.SetPosition(_FirstWheelStartPosition, _SecondWheelStartPosition, _ThirdWheelStartPosition);
                }
            }
        }

        private int _SecondWheelStartPosition = 0;

        public int SecondWheelStartPosition
        {
            get { return _SecondWheelStartPosition; }
            set
            {
                if (value > MAX_WHELL_VALUE)
                {
                    value = MAX_WHELL_VALUE;
                }
                if (value < MIN_WHELL_VALUE)
                {
                    value = MIN_WHELL_VALUE;
                }
                SetProperty(ref _SecondWheelStartPosition, value);

                _Enigma?.SetPosition(_FirstWheelStartPosition, _SecondWheelStartPosition, _ThirdWheelStartPosition);
            }
        }


        private int _ThirdWheelStartPosition = 0;

        public int ThirdWheelStartPosition
        {
            get { return _ThirdWheelStartPosition; }
            set
            {
                if (value > MAX_WHELL_VALUE)
                {
                    value = MAX_WHELL_VALUE;
                }
                if (value < MIN_WHELL_VALUE)
                {
                    value = MIN_WHELL_VALUE;
                }
                SetProperty(ref _ThirdWheelStartPosition, value);

                _Enigma?.SetPosition(_FirstWheelStartPosition, _SecondWheelStartPosition, _ThirdWheelStartPosition);
            }
        }

        public Command PressCmd { get; }
        public Command ResetCmd { get; }


        public MainWindowViewModel()
        {
            PressCmd = new Command(HandlePress);
            ResetCmd = new Command(HandleReset);


            _Enigma = new EncryptionService();
        }

        private void HandleReset()
        {

            _Enigma.Reset();
            //Result = "";
            Input = "";
            Output = "";
            FirstWheelStartPosition = 0;
            SecondWheelStartPosition = 0;
            ThirdWheelStartPosition = 0;

        }

        private void HandlePress(object obj)
        {
            if (obj is string str)
            {
                var chr = str[0];
                Input += chr.ToString();
                Output += _Enigma.EncryptLetter(chr);
            }
            //var letter = Enum.Parse<Models.LetterEnum>(obj.ToString());
            //Input += obj.ToString();
            //Result += _Enigma.EncryptLetter(letter);
            //Output = Result;
        }
    }
}
