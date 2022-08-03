using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EnigmaLib.Config;
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


            List<int[]> rotorWires = new();

            foreach (var rotor in conf.Rotors)
            {
                rotorWires.Add(rotor.Select(c => _ForwardConverter[c]).ToArray());
            }

            _RotorSet.SetRotorWiring(rotorWires);

            var refWires = conf.Reflector.Select(c => _ForwardConverter[c]).ToArray();
            _Reflector = new Reflector(refWires);
        }

        public char Apply(char c)
        {
            var ci = _ForwardConverter[c];

            ci = _RotorSet.ApplyForward(ci);

            ci = _Reflector.Apply(ci);

            ci = _RotorSet.ApplyBackward(ci);
            
            //_RotorSet.Step();

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
        
        
        Reflector _Reflector;
        readonly RotorSet _RotorSet = new();

        Dictionary<char, int> _ForwardConverter = new();
        Dictionary<int, char> _BackwardConverter = new();

    }
}
