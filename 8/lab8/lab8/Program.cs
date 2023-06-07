using System.Numerics;
using System.Text;

BigInteger p = 11;
BigInteger q = 23;
BigInteger x = 2;

BBSGenerator bbs = new BBSGenerator(p, q, x);
Console.WriteLine("BBS:");
for (int i = 0; i < 10; i++)
{
    BigInteger randomNumber = bbs.GenerateRandomNumber(1); // Генерация одного случайного бита
    Console.WriteLine("x: " + bbs.x + ", случайный бит " + (i + 1) + ": " + randomNumber);
}

Console.WriteLine("RC4:");
int[] ikey = { 43, 45, 100, 21, 1 };
byte[] key = new byte[ikey.Length];

for (int i = 0; i < ikey.Length; i++)
{
    key[i] = Convert.ToByte(ikey[i]);
}

RC4 rc = new RC4(key);
RC4 rc2 = new RC4(key);
byte[] testBytes = Encoding.ASCII.GetBytes("Budanowa Ksenya Andreevna");

DateTime startTime = DateTime.Now;
byte[] encrypted = rc.Encode(testBytes);
DateTime endTime = DateTime.Now;

Console.WriteLine($"Зашифрованная строка: {Encoding.ASCII.GetString(encrypted)}");
Console.WriteLine("Шифрование RC4 {0} символов заняло {1} мс", testBytes.Length, (endTime - startTime).TotalMilliseconds);

startTime = DateTime.Now;
byte[] decrypted = rc2.Encode(encrypted);
endTime = DateTime.Now;

Console.WriteLine($"Расшифрованная строка: {Encoding.ASCII.GetString(decrypted)}");
Console.WriteLine("Расшифрование RC4 {0} символов заняло {1} мс", testBytes.Length, (endTime - startTime).TotalMilliseconds);