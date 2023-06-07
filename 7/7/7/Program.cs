using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;

string message = File.ReadAllText("d:\\1POIT\\3\\Crypto\\labs\\passed\\7\\text.txt");
string origMessage = File.ReadAllText("d:\\1POIT\\3\\Crypto\\labs\\passed\\7\\text.txt");
message = string.Join("", Encoding.ASCII.GetBytes(message).Select(c => c.ToString("X2")));
string key1 = "2B7E151628AED2A6ABF7158809CF4F3C";
string key2 = "3A5C9F1E6271BA2C43ED9D8B9CF6C7A4";
string key3 = "F0F1F2F3F4F5F6F7F8F9FAFBFCFDFEFF";
var des = new DesEncryption();

// Шифруем с помощью алгоритма des-ede3 (шифр+расш+шифр)
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
var encrypted = des.Encrypt(message, key1);
encrypted = des.Decrypt(encrypted, key2);
encrypted = des.Encrypt(encrypted, key3);
stopwatch.Stop();
Console.WriteLine("\nШифрование 3DES {0} символов заняло {1} мс ", message.Length, stopwatch.ElapsedMilliseconds);
File.WriteAllText("d:\\1POIT\\3\\Crypto\\labs\\passed\\7\\encrypted.txt", encrypted);

// Расшифруем с помощью алгоритма des-ede3 (расш+шифр+расш)
stopwatch.Reset();
stopwatch.Start();
var decrypted = des.Decrypt(encrypted, key3);
decrypted = des.Encrypt(decrypted, key2);
decrypted = des.Decrypt(decrypted, key1);
stopwatch.Stop();
Console.WriteLine("Расшифрование 3DES {0} символов заняло {1} мс ", decrypted.Length, stopwatch.ElapsedMilliseconds);
decrypted = Regex.Replace(new string(Encoding.ASCII.GetChars(BitArrayExtensions.FromHex(decrypted))).Trim(), @"[^\u0020-\u007E]", string.Empty);
File.WriteAllText("d:\\1POIT\\3\\Crypto\\labs\\passed\\7\\decrypted.txt", decrypted);

// Анализ лавинного эффекта
Console.WriteLine("\nАнализ лавинного эффекта");
encrypted = des.Encrypt(des.Decrypt(des.Encrypt(message, key1), key3), key3);

// Меняем один символ в исходном сообщении
// лавинный эффект
string changedMessage = "x" + message.Substring(1);
changedMessage = string.Join("", Encoding.ASCII.GetBytes(changedMessage).Select(c => c.ToString("X2")));

string encryptedChangedText = des.Encrypt(des.Decrypt(des.Encrypt(changedMessage, key1), key3), key3);

// считаем количество измененных символов в зашифрованных сообщениях
int diffCount = 0;
for (int i = 0; i < encrypted.Length; i++)
{
    if (encrypted[i] != encryptedChangedText[i])
    {
        diffCount++;
    }
}
Console.WriteLine("Количество изменённых символов: {0}", diffCount);
Console.WriteLine("\n");

// Анализ влияния слабых и полуслабых ключей
// слабые ключи
key1 = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
key2 = "11111111111111111111111111111111";
key3 = "22222222222222222222222222222222";

Console.WriteLine($"Слабые ключи: {key1}, {key2}, {key3}");

encrypted = des.Encrypt(message, key1);
encrypted = des.Decrypt(encrypted, key2);
encrypted = des.Encrypt(encrypted, key3);

stopwatch.Reset();
stopwatch.Start();
decrypted = des.Decrypt(encrypted, key3);
decrypted = des.Encrypt(decrypted, key2);
decrypted = des.Decrypt(decrypted, key1);
stopwatch.Stop();
Console.WriteLine("Расшифрование 3DES {0} символов заняло {1} мс ", decrypted.Length, stopwatch.ElapsedMilliseconds);

// Сравнение с исходным сообщением и подсчет измененных символов
diffCount = 0;
int length = Math.Min(origMessage.Length, encrypted.Length); // Определяем минимальную длину для сравнения символов
for (int i = 0; i < length; i++)
{
    if (origMessage[i] != encrypted[i])
    {
        diffCount++;
    }
}

// Вывод результатов
Console.WriteLine("Количество измененных символов при использовании слабых ключей: {0}", diffCount);
// лавинный эффект
changedMessage = "x" + message.Substring(1);
changedMessage = string.Join("", Encoding.ASCII.GetBytes(changedMessage).Select(c => c.ToString("X2")));

encryptedChangedText = des.Encrypt(des.Decrypt(des.Encrypt(changedMessage, key1), key3), key3);

// считаем количество измененных символов в зашифрованных сообщениях
diffCount = 0;
for (int i = 0; i < encrypted.Length; i++)
{
    if (encrypted[i] != encryptedChangedText[i])
    {
        diffCount++;
    }
}
Console.WriteLine("Лавинный эффект, количество изменённых символов: {0}", diffCount);
Console.WriteLine("\n");

// полуслабые ключи
key1 = "01010101010101010101010101010101";
key2 = "44444444444444444444444444444444";
key3 = "55555555555555555555555555555555";

Console.WriteLine($"Полуслабые ключи: {key1}, {key2}, {key3}");

stopwatch.Reset();
stopwatch.Start();
encrypted = des.Encrypt(message, key1);
encrypted = des.Decrypt(encrypted, key2);
encrypted = des.Encrypt(encrypted, key3);
decrypted = des.Decrypt(encrypted, key3);
decrypted = des.Encrypt(decrypted, key2);
decrypted = des.Decrypt(decrypted, key1);
stopwatch.Stop();
Console.WriteLine("Расшифрование 3DES {0} символов заняло {1} мс ", decrypted.Length, stopwatch.ElapsedMilliseconds);

// Сравнение с исходным сообщением и подсчет измененных символов
diffCount = 0;
length = Math.Min(origMessage.Length, encrypted.Length); // Определяем минимальную длину для сравнения символов
for (int i = 0; i < length; i++)
{
    if (origMessage[i] != encrypted[i])
    {
        diffCount++;
    }
}

// Вывод результатов
Console.WriteLine("Количество измененных символов при использовании полуслабых ключей: {0}", diffCount);
// лавинный эффект
changedMessage = "x" + message.Substring(1);
changedMessage = string.Join("", Encoding.ASCII.GetBytes(changedMessage).Select(c => c.ToString("X2")));

encryptedChangedText = des.Encrypt(des.Decrypt(des.Encrypt(changedMessage, key1), key3), key3);

// считаем количество измененных символов в зашифрованных сообщениях
diffCount = 0;
for (int i = 0; i < encrypted.Length; i++)
{
    if (encrypted[i] != encryptedChangedText[i])
    {
        diffCount++;
    }
}
Console.WriteLine("Лавинный эффект, количество изменённых символов: {0}", diffCount);
