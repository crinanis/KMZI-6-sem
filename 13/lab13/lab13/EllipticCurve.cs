using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

public class EllipticCurve
{
    private static readonly string alphabet = "abcdefghijklmnopqrstuvwxyzабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
    private static readonly BigInteger[,] pointKit =
    {
        { 15, 7  }, { 15, 18 }, { 17, 2  }, { 17, 21 }, 
        { 19, 4  }, { 19, 17 }, { 21, 11 }, { 21, 14 },
        { 22, 5  }, { 22, 16 }, { 24, 2  }, { 24, 21 }, 
        { 27, 9  }, { 27, 14 }, { 28, 12 }, { 28, 13 },
        { 29, 6  }, { 29, 17 }, { 32, 4  }, { 32, 19 }, 
        { 33, 2  }, { 33, 21 }, { 35, 8  }, { 35, 13 },
        { 36, 5  }, { 36, 14 }, { 15, 9  }, { 9, 22  }, 
        { 4, 11  }, { 18, 3  }, { 21, 16 }, { 27, 16 },
        { 13, 20 }, { 8, 5   }, { 12, 18 }, { 25, 14 }, 
        { 2, 4   }, { 19, 56 }, { 31, 8  }, { 6, 2   },
        { 10, 6  }, { 14, 21 }, { 30, 15 }, { 16, 1  }, 
        { 3, 19  }, { 20, 23 }, { 26, 10 }, { 17, 25 },
        { 7, 27  }, { 24, 24 }, { 1, 30  }, { 5, 26  }, 
        { 11, 29 }, { 22, 32 }, { 29, 31 }, { 32, 28 },
        { 23, 13 }, { 28, 35 }
    };

    public static BigInteger Mod(BigInteger x, BigInteger m)
    {
        BigInteger remainder = x % m;
        if (remainder < 0)
        {
            remainder += m;
        }
        return remainder;
    }

    public static BigInteger ModInverse(BigInteger a, BigInteger m)
    {
        a = Mod(a, m);
        for (BigInteger x = 1; x < m; x++)
        {
            if (Mod(a * x, m) == 1)
            {
                return x;
            }
        }
        return a; // Возвращаем исходное значение `a`, если обратный элемент не найден.
    }

    private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();

    public static BigInteger GenerateRandomNumber(BigInteger minValue, BigInteger maxValue)
    {
        byte[] bytes = new byte[maxValue.ToByteArray().Length];
        rng.GetBytes(bytes);
        BigInteger randomValue = new BigInteger(bytes);
        return BigInteger.Remainder(randomValue, maxValue - minValue) + minValue;
    }
    private static BigInteger GetLambda(BigInteger[] P, BigInteger a, BigInteger p)
    {
        return Mod(Mod(3 * BigInteger.Pow(P[0], 2) + a, p) * ModInverse(2 * P[1], p), p);
    }

    private static BigInteger GetLambda(BigInteger[] P, BigInteger[] Q, BigInteger p)
    {
        return Mod(Mod(Q[1] - P[1], p) * Mod(ModInverse(Q[0] + Mod(-P[0], p), p), p), p);
    }

    public static BigInteger[] MultiplyPoint(BigInteger k, BigInteger[] P, BigInteger a, BigInteger p)
    {
        BigInteger[] kP = P;

        int logValue = (int)BigInteger.Log(k, 2);
        for (int i = 0; i < logValue; i++)
        {
            kP = CalculateSum(kP, a, p);
        }

        logValue = (int)BigInteger.Log(k, 2);
        while (k > 1)
        {
            logValue = (int)BigInteger.Log(k, 2);
            for (int i = 0; i < logValue; i++)
            {
                kP = CalculateSum(kP, CalculateSum(P, a, p), p);
            }

            k = k - BigInteger.Pow(2, logValue);
        }


        if (k == 1)
        {
            kP = CalculateSum(kP, P, p);
        }

        return kP;
    }

    public static BigInteger[] InversePoint(BigInteger[] P)
    {
        return new BigInteger[2] { P[0], (-1) * P[1] };
    }

    public static BigInteger[] CalculateSum(BigInteger[] P, BigInteger[] Q, BigInteger p)
    {
        BigInteger lambda = GetLambda(P, Q, p);
        BigInteger x = Mod(lambda * lambda - P[0] - Q[0], p);
        BigInteger y = Mod(lambda * (P[0] - x) - P[1], p);
        return new BigInteger[] { x, y };
    }

    public static BigInteger[] CalculateSum(BigInteger[] P, BigInteger a, BigInteger p)
    {
        BigInteger lambda = GetLambda(P, a, p);
        BigInteger x = Mod(lambda * lambda - P[0] - P[0], p);
        BigInteger y = Mod(lambda * (P[0] - x) - P[1], p);
        return new BigInteger[] { x, y };
    }

    public static BigInteger[,] Encrypt(string text, BigInteger[] G, BigInteger a, BigInteger p, BigInteger d)
    {
        DateTime startTime = DateTime.Now;
        BigInteger[] Q = MultiplyPoint(d, G, a, p), P;
        BigInteger[,] encryptedText = new BigInteger[text.Length, 4];
        BigInteger k;
        Console.WriteLine($"G = ({G[0]}, {G[1]}), d = {d}, Q = ({Q[0]}, {Q[1]})");

        for (int i = 0; i < text.Length; i++)
        {
            k = GenerateRandomNumber(2, d);
            P = Enumerable.Range(0, pointKit.GetLength(1))
            .Select(x => BigInteger.Parse(pointKit[alphabet.IndexOf(text[i]), x].ToString())).ToArray();

            BigInteger[] C1 = MultiplyPoint(k, G, a, p);
            BigInteger[] kQ = MultiplyPoint(k, Q, a, p);
            BigInteger[] C2 = CalculateSum(P, kQ, p);
            encryptedText[i, 0] = C1[0];
            encryptedText[i, 1] = C1[1];
            encryptedText[i, 2] = C2[0];
            encryptedText[i, 3] = C2[1];
        }

        DateTime endTime = DateTime.Now;
        Console.WriteLine($"Зашифрованный текст: {string.Join(" ", encryptedText.Cast<BigInteger>())}");
        Console.WriteLine($"Время зашифрования: {(endTime - startTime).TotalMilliseconds} мс");
        return encryptedText;
    }

    public static string Decrypt(BigInteger[,] encryptedText, BigInteger a, BigInteger p, BigInteger d)
    {
        DateTime startTime = DateTime.Now;
        string decryptedText = "";
        for (int i = 0; i < encryptedText.GetUpperBound(0) + 1; i++)
        {
            BigInteger[] C1 = MultiplyPoint(d, new BigInteger[] { encryptedText[i, 0], encryptedText[i, 1] }, a, p);
            BigInteger[] C2 = { encryptedText[i, 2], encryptedText[i, 3] };
            BigInteger[] P = CalculateSum(C2, InversePoint(C1), p);

            for (int k = 0; k < pointKit.GetUpperBound(0) + 1; k++)
            {
                if (pointKit[k, 0] == P[0] && pointKit[k, 1] == P[1])
                {
                    decryptedText += alphabet[k];
                    break;
                }
            }
        }

        DateTime endTime = DateTime.Now;
        Console.WriteLine($"Время расшифрования: {(endTime - startTime).TotalMilliseconds} мс");
        return decryptedText;
    }
}