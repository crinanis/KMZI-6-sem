using System.Diagnostics;
using System.Numerics;

bool yes = true;
while (yes)
{
    Console.WriteLine("\n1. Задание 1.1");
    Console.WriteLine("2. Задание 1.2");
    Console.WriteLine("3. Задание 2");
    Console.WriteLine("4. Выход");

    string input = Console.ReadLine();
    int choice;
    int.TryParse(input, out choice);

    int xmin = 71, xmax = 105, a = -1, b = 1, p = 751, k = 7, l = 8;
    switch (choice)
    {
        case 1:
            Task1_1(xmin, xmax, b, p);
            break;
        case 2:
            Task1_2(a, b, p, k, l);
            break;
        case 3:
            Task2(a, p);
            break;
        case 4:
            yes = false;
            break;
        default:
            break;
    }
}

void Task1_1(int xmin, int xmax, int b, int p)
{
    DateTime startTime = DateTime.Now;
    Console.WriteLine("Полученные точки:");
    for (int x = xmin; x <= xmax; x++)
    {
        int y = (int)Math.Sqrt((Math.Pow(x, 3) - x + b) % p);
        Console.Write($"({x}, {y})");
    }
    DateTime endTime = DateTime.Now;
    Console.WriteLine($"\nВремя вычисления ординат: {(endTime - startTime).TotalMilliseconds} мс");
}

void Task1_2(BigInteger a, BigInteger b, BigInteger p, BigInteger k, BigInteger l)
{
    BigInteger[] P = { 62, 372 }, Q = { 70, 195 }, R = { 67, 84 };
    Console.WriteLine($"P({P[0]}, {P[1]}), Q({Q[0]}, {Q[1]}), R({R[0]}, {R[1]})");
    BigInteger[] kP = EllipticCurve.MultiplyPoint(k, P, a, p);
    BigInteger[] lQ = EllipticCurve.MultiplyPoint(l, Q, a, p);
    Console.WriteLine($"{k}P = {kP.Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
    Console.WriteLine($"P + Q = {EllipticCurve.CalculateSum(P, Q, p).Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
    Console.WriteLine($"{k}P + {l}Q - R = {EllipticCurve.CalculateSum(EllipticCurve.CalculateSum(kP, lQ, p), EllipticCurve.InversePoint(R), p).Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
    Console.WriteLine($"P - Q + R = {EllipticCurve.CalculateSum(EllipticCurve.CalculateSum(P, EllipticCurve.InversePoint(Q), p), R, p).Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
}
void Task2(BigInteger a, BigInteger p)
{
    Console.WriteLine("Введите текст для шифрования:");
    string text = Console.ReadLine();
    BigInteger[] G = { BigInteger.Zero, BigInteger.One };
    BigInteger[,] encryptedText = EllipticCurve.Encrypt(text, G, a, p, 25);
    Console.WriteLine($"Расшифрованный текст: {EllipticCurve.Decrypt(encryptedText, a, p, 25)}");
}
