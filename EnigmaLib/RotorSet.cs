namespace EnigmaLib;

public class RotorSet
{
    readonly List<Rotor> _Rotors = new();

    internal void SetPosition(int[] positions)
    {
        if (positions.Length != _Rotors.Count)
            throw new Exception("Invalid num. rotors");

        for (var i = 0; i < _Rotors.Count; i++)
        {
            _Rotors[i].Position = positions[i];
        }
        
    }

    internal void Step()
    {
        foreach (var rotor in _Rotors)
        {
            rotor.Position = (++rotor.Position) % EnigmaMachine.NumChars;
            if (rotor.Position != rotor.StepPosition) break;
        }
    }

    internal int ApplyForward(int c)
    {
        var c1 = c;

        foreach (var rotor in _Rotors) 
            c1 = rotor.ApplyForward(c1);

        return c1;
    }

    internal int ApplyBackward(int c)
    {
        var c1 = c;
       
        for(var i = _Rotors.Count-1; i >= 0; i--)
        {
            c1 = _Rotors[i].ApplyBackward(c1);
        }
        
        return c1;
    }

    internal void SetRotorWiring(List<int[]> rotorWiring)
    {
        _Rotors.Clear();

        foreach (var wiring in rotorWiring)
        {
            _Rotors.Add(new Rotor(wiring));
        }
    }

    internal void Reset()
    {
        foreach (var rotor in _Rotors)
            rotor.Reset();
    }
}