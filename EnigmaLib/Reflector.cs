namespace EnigmaLib;

public class Reflector
{
    readonly int[] _WireMap = new int[EnigmaMachine.NumChars];

    public Reflector(int[] wiring)
    {
        for (var i = 0; i < wiring.Length; i++)
        {
            _WireMap[i] = wiring[i];
        }
    }

    internal int Apply(int c) => _WireMap[(c)];

}