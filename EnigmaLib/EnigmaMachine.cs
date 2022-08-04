using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EnigmaLib.Config;
using EnigmaLib.Utils;
using Newtonsoft.Json;

namespace EnigmaLib
{
    public class EnigmaMachine
    {
        internal const int NumChars = 26;

        public EnigmaMachine()
        {
            Setup();
        }

        void Setup()
        {
            var fullPath = Assembly.GetAssembly(typeof(EnigmaMachine))!.Location;
            var folder = Path.GetDirectoryName(fullPath);
            var configFile = Path.Combine(folder, "Config", "Wiring.json");
            var configText = File.ReadAllText(configFile);

            var conf = JsonConvert.DeserializeObject<ConfigModel>(configText);

            Console.WriteLine($"Ref: {conf.Reflector}");

            foreach (var rot in conf.Rotors)
            {
                Console.WriteLine($"Rot: {rot}");
            }

            
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (var i = 0; i < NumChars; i++)
            {
                _ForwardConverter[letters[i]] = i;
                _BackwardConverter[i] = letters[i];
            }

            _Generator = new WireGenerator();
            
            _Generator.GenerateWiring(_ForwardConverter, conf);
            
            _RotorSet.SetRotorWiring(_Generator.RotorWires);
           
            _Reflector = new Reflector(_Generator.RefWires);


          
        }

        public char Apply(char c)
        {
            var ci = _ForwardConverter[c];

            ci = _RotorSet.ApplyForward(ci);

            ci = _Reflector.Apply(ci);

            ci = _RotorSet.ApplyBackward(ci);
            
            _RotorSet.Step();

            return _BackwardConverter[ci];
        }

        // position of rotors int (0..25)
        public void SetRotorPositions(int[] positions)
        {
            _RotorSet.SetPosition(positions);
        }

        // // (3, 1, 4)
        // public void SetRotors(int[] rotorInx)
        // {
        //
        // }
       
        
        public void Reset()
        {
            _RotorSet.Reset();
        }
        
        
        Reflector _Reflector;
        readonly RotorSet _RotorSet = new();

        Dictionary<char, int> _ForwardConverter = new();
        Dictionary<int, char> _BackwardConverter = new();
        WireGenerator _Generator;

    }
}
