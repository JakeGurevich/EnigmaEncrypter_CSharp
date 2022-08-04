using EnigmaLib.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaLib.Utils
{
    public class WireGenerator
    {
       
        public int[] RefWires { get; private set; } = new int[EnigmaMachine.NumChars];
        
         public List<int[]> RotorWires { get; } = new();
        
        public void GenerateWiring(Dictionary<char, int> converter,ConfigModel config)
        {
            foreach (var rotor in config.Rotors)
            {
                RotorWires.Add(rotor.Select(c => converter[c]).ToArray());
            };
            RefWires = config.Reflector.Select(c => converter[c]).ToArray();
        }
    }
}
