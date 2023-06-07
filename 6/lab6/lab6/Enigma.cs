using System.Text;

public class Enigma
{
    public int posL;
    public int posM;
    public int posR;
    public Rotor rotorL;
    public Rotor rotorM;
    public Rotor rotorR;
    private Reflector reflector;
    private string keyboard;

    public Enigma(Reflector reflector, int posL, int posM, int posR, string keyboard)
    {
        this.posL= posL;
        this.posM = posM;
        this.posR = posR;
        this.reflector = reflector;
        this.keyboard = keyboard;
    }
    public string Crypt(string text)
    {
        var rotorL = new RotorIII(posL);
        var rotorM = new RotorVII(posM);
        var rotorR = new RotorI(posR);

        var result = new StringBuilder(text.Length);
        char symbol;

        foreach (var ch in text)
        {
            if (keyboard.Contains(ch))
            {
                symbol = rotorR[keyboard.IndexOf(ch)];
            }
            else
            {
                result.Append(ch);
                continue;
            }

            symbol = rotorM[keyboard.IndexOf(symbol)];
            symbol = rotorL[keyboard.IndexOf(symbol)];

            symbol = reflector.Reflect(symbol);

            symbol = keyboard[rotorL.IndexOf(symbol)];
            symbol = keyboard[rotorM.IndexOf(symbol)];
            symbol = keyboard[rotorR.IndexOf(symbol)];
            result.Append(symbol);

            //if (posL == 0)
            //{
            //    if (rotorM.isFullyRotated)
            //    {
            //        rotorL.MoveRotor(1);
            //    }
            //    rotorR.MoveRotor(posR);
            //    rotorM.MoveRotor(posM);
            //}
            //else if (posM == 0)
            //{
            //    if (rotorR.isFullyRotated)
            //    {
            //        rotorM.MoveRotor(1);
            //    }
            //    rotorR.MoveRotor(posL);
            //    rotorL.MoveRotor(posL);
            //}
        }
        return result.ToString();
    }
}