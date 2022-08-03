namespace EnigmaLib;

public class Rotor
{
    readonly int[] _WireMap = new int[EnigmaMachine.NumChars];
    readonly int[] _InverseWireMap = new int[EnigmaMachine.NumChars];

    public int Position { get; set; }

    public Rotor(int[] wiring)
    {
        for (var i = 0; i < wiring.Length; i++)
        {
            _WireMap[i] = wiring[i];
            _InverseWireMap[wiring[i]] = i;
        }
    }

    internal int ApplyForward(int c) =>
        _WireMap[(c + Position) % EnigmaMachine.NumChars];

    internal int ApplyBackward(int c)
    {
        var ci = _InverseWireMap[c];

        return (ci - Position + EnigmaMachine.NumChars) % EnigmaMachine.NumChars;
    }

    internal void Reset()
    {
        Position = 0;
    }
}