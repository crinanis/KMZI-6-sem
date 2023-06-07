int[] pos = { 0, 0, 0 };
string keyboard = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

var enigma = new Enigma(new ReflectorBDunn(), pos[0], pos[1], pos[2], keyboard);
var encoded = enigma.Crypt("A");
Console.WriteLine($"Encoded:{encoded}\n");
var decoded = enigma.Crypt(encoded);
Console.WriteLine($"Decoded:{decoded}");