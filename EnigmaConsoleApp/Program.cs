﻿
using EnigmaLib;

var enigma = new EnigmaMachine();

enigma.SetRotorPositions(new[] { 1, 0, 0 });

var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

var encrypedLetters = letters.Select(l => enigma.Apply(l));

enigma.SetRotorPositions(new[] { 1, 0, 0 });

var letters1 = encrypedLetters.Select(l => enigma.Apply(l));
Console.WriteLine(letters);
Console.WriteLine(letters1.ToArray());

Console.WriteLine("Hello, Enigma!");
Console.ReadLine();
