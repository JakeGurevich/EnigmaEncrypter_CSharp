
using EnigmaLib;

var enigma = new EnigmaMachine();

enigma.SetRotorPositions(new []{1,2,3});

var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

var encrypedLetters = letters.Select(l => enigma.Apply(l));

enigma.SetRotorPositions(new[] { 1, 2, 3 });

var letters1 = encrypedLetters.Select(l => enigma.Apply(l));


Console.WriteLine("Hello, Enigma!");
