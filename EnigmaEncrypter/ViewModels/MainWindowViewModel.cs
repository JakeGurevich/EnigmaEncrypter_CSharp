using Base;
using EnigmaEncrypter.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaEncrypter.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {

        private readonly EncryptionService _Enigma;

        private const int MAX_WHEEL_VALUE = 25;

        private const int MIN_WHEEL_VALUE = 0;

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
                if (value > MAX_WHEEL_VALUE)
                {
                    value = MAX_WHEEL_VALUE;
                }
                if (value < MIN_WHEEL_VALUE)
                {
                    value = MIN_WHEEL_VALUE;
                }

                if (SetProperty(ref _FirstWheelStartPosition, value))
                {
                    _Enigma.Root.SetInitialPosition(0, value);
                }
            }
        }

        private int _SecondWheelStartPosition = 0;

        public int SecondWheelStartPosition
        {
            get { return _SecondWheelStartPosition; }
            set
            {
                if (value > MAX_WHEEL_VALUE)
                {
                    value = MAX_WHEEL_VALUE;
                }
                if (value < MIN_WHEEL_VALUE)
                {
                    value = MIN_WHEEL_VALUE;
                }
                SetProperty(ref _SecondWheelStartPosition, value);

                _Enigma.Root.SetInitialPosition(1, value);
            }
        }


        private int _ThirdWheelStartPosition = 0;

        public int ThirdWheelStartPosition
        {
            get { return _ThirdWheelStartPosition; }
            set
            {
                if (value > MAX_WHEEL_VALUE)
                {
                    value = MAX_WHEEL_VALUE;
                }
                if (value < MIN_WHEEL_VALUE)
                {
                    value = MIN_WHEEL_VALUE;
                }
                SetProperty(ref _ThirdWheelStartPosition, value);

                _Enigma.Root.SetInitialPosition(2, value);
            }
        }

        public ObservableCollection<string> List { get; } = new();

        public Command ClearListCmd { get; }
        public Command AddToListCmd { get; }
        public Command PressCmd { get; }
        public Command ResetCmd { get; }
        public Command AddValueCmd { get; }
        public Command DecreaseValueCmd { get; }


        public MainWindowViewModel()
        {
            PressCmd = new Command(HandlePress);
            ResetCmd = new Command(HandleReset);
            AddValueCmd = new Command(IncreasePosition);
            DecreaseValueCmd = new Command(DecreasePosition);
            AddToListCmd = new Command(AddToItemList);
            ClearListCmd = new Command(ClearList);

            _Enigma = new EncryptionService();
        }

        private void ClearList()
        {
            List.Clear();
        }

        private void AddToItemList()
        {
            var item = $"Input: {_Input} - Output :{_Output}";
            List.Add(item);
        }

        private void DecreasePosition(object obj)
        {

            switch (obj.ToString())
            {
                case "1":
                    FirstWheelStartPosition--;
                    break;
                case "2":
                    SecondWheelStartPosition--;
                    break;
                case "3":
                    ThirdWheelStartPosition--;
                    break;
            }
        }

        private void IncreasePosition(object obj)
        {
            switch (obj.ToString())
            {

                case "1":
                    FirstWheelStartPosition++;
                    break;
                case "2":
                    SecondWheelStartPosition++;
                    break;
                case "3":
                    ThirdWheelStartPosition++;
                    break;
            }
        }

        private void HandleReset()
        {

            _Enigma.Reset();
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
                Output += _Enigma.Encrypt(chr);
            }

        }
    }
}
