using System.Diagnostics;
using System.Text;

namespace lab4
{
    class Program
    {
        private static readonly string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        private static readonly int rows = 5, cols = 5;

        static void Main(string[] args)
        {
            string inputFile = "d:\\1POIT\\3\\Crypto\\labs\\4\\lab4\\text.txt";
            string encryptedFile = "d:\\1POIT\\3\\Crypto\\labs\\4\\lab4\\caesar-encoded.txt";
            string decryptedFile = "d:\\1POIT\\3\\Crypto\\labs\\4\\lab4\\caesar-decoded.txt";
            string key = "Budanowa";

            // Создаем объект Stopwatch для измерения времени выполнения
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            EncryptCaesar(inputFile, encryptedFile, key);
            stopwatch.Stop();
            Console.WriteLine("\nEncryptCaesar занял {0} мс ", stopwatch.ElapsedMilliseconds);

            // Шифр Цезаря с ключом 
            stopwatch.Reset();
            stopwatch.Start();
            DecryptCaesar(encryptedFile, decryptedFile, key);
            stopwatch.Stop();
            Console.WriteLine("\nDecryptCaesar занял {0} мс ", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Частота появления символов в тексте, зашифрованном шифром Цезаря:");
            CountCharacterFrequency(File.ReadAllText(encryptedFile));
            Console.WriteLine("Частота появления символов в расшифрованном тексте:");
            CountCharacterFrequency(File.ReadAllText(decryptedFile));

            // Шифрование с помощью таблицы Трисемуса
            encryptedFile = "d:\\1POIT\\3\\Crypto\\labs\\4\\lab4\\trisemus-encoded.txt";
            decryptedFile = "d:\\1POIT\\3\\Crypto\\labs\\4\\lab4\\trisemus-decoded.txt";
            key = "ksenya";
            var table = TrisemusTable(key);

            stopwatch.Reset();
            stopwatch.Start();
            EncryptTrisemus(inputFile, encryptedFile, table);
            stopwatch.Stop();
            Console.WriteLine("\nEncryptTrisemus занял {0} мс ", stopwatch.ElapsedMilliseconds);

            stopwatch.Reset();
            stopwatch.Start();
            DecryptTrisemus(encryptedFile, decryptedFile, table);
            stopwatch.Stop();
            Console.WriteLine("\nDecryptTrisemus занял {0} мс ", stopwatch.ElapsedMilliseconds);

            Console.WriteLine("Частота появления символов в тексте, зашифрованном шифром Трисемуса:");
            CountCharacterFrequency(File.ReadAllText(encryptedFile));
            Console.WriteLine("Частота появления символов в расшифрованном тексте:");
            CountCharacterFrequency(File.ReadAllText(decryptedFile));
        }


        static void EncryptCaesar(string inputFile, string encryptedFile, string key)
        {
            try
            {
                string inputText = File.ReadAllText(inputFile);
                string encryptedText = "";

                for (int i = 0; i < inputText.Length; i++)
                {
                    int shift = Alphabet.IndexOf(key[i % key.Length]);
                    char c = inputText[i];
                    if (char.IsLetter(c))
                    {
                        char baseChar = char.IsUpper(c) ? 'A' : 'a';
                        c = (char)(((c + shift - baseChar) % 26) + baseChar);
                    }
                    encryptedText += c;
                }
                Console.WriteLine("Зашифрованный текст:\n---------------\n" + encryptedText);
                File.WriteAllText(encryptedFile, encryptedText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        static void DecryptCaesar(string encryptedFile, string decryptedFile, string key)
        {
            try
            {
                string encryptedText = File.ReadAllText(encryptedFile);
                string decryptedText = "";

                for (int i = 0; i < encryptedText.Length; i++)
                {
                    int shift = Alphabet.IndexOf(key[i % key.Length]);
                    char c = encryptedText[i];
                    if (char.IsLetter(c))
                    {
                        char baseChar = char.IsUpper(c) ? 'A' : 'a';
                        c = (char)(((c - shift - baseChar + 26) % 26) + baseChar);
                    }
                    decryptedText += c;
                }
                Console.WriteLine("Расшифрованный текст:\n---------------\n" + decryptedText);
                File.WriteAllText(decryptedFile, decryptedText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        static void EncryptTrisemus(string inputFile, string encryptedFile, char[,] table)
        {
            try
            {
                string text = File.ReadAllText(inputFile);
                var encryptedText = new StringBuilder(text.Length);

                for (int i = 0; i < text.Length; i++)
                {
                    char currentChar = text[i];
                    bool isFound = false;

                    for (int row = 0; row < rows; row++)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            if (table[row, col] == currentChar)
                            {
                                int newRow = (row + 1) % rows;
                                encryptedText.Append(table[newRow, col]);
                                isFound = true;
                                break;
                            }
                        }

                        if (isFound)
                        {
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        encryptedText.Append(currentChar);
                    }
                }
                Console.WriteLine("Зашифрованный текст:\n---------------\n" + encryptedText);
                File.WriteAllText(encryptedFile, encryptedText.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }


        static void DecryptTrisemus(string encryptedFile, string decryptedFile, char[,] table)
        {
            try
            {
                var text = File.ReadAllText(encryptedFile);
                var decryptedText = new StringBuilder(text.Length);

                for (var i = 0; i < text.Length; ++i)
                {
                    bool isReplaced = false;

                    for (var row = 0; row < rows && !isReplaced; ++row)
                    {
                        for (var column = 0; column < cols; ++column)
                        {
                            if (text[i] == table[row, column])
                            {
                                var newRow = (row == 0) ? rows - 1 : row - 1;
                                decryptedText.Append(table[newRow, column]);
                                isReplaced = true;
                                break;
                            }
                        }
                    }

                    if (!isReplaced)
                    {
                        decryptedText.Append(text[i]);
                    }
                }
                Console.WriteLine("Расшифрованный текст:\n---------------\n" + decryptedText);
                File.WriteAllText(decryptedFile, decryptedText.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public static char[,] TrisemusTable(string keyword)
        {
            var table = new char[rows, cols];
            var index = 0;

            foreach (var c in keyword.Distinct())
            {
                table[index / cols, index % cols] = c;
                index++;
            }

            foreach (var c in Alphabet)
            {
                if (index >= rows * cols)
                    break;
                if (!keyword.Contains(c))
                {
                    table[index / cols, index % cols] = c;
                    index++;
                }
            }

            return table;
        }

        static void CountCharacterFrequency(string text)
        {
            // Создаем словарь, который будет содержать частоту появления каждого символа английского алфавита в тексте
            Dictionary<char, int> characterFrequency = new Dictionary<char, int>();

            // Проходим по каждому символу в тексте и увеличиваем его частоту на 1 в словаре, если это символ английского алфавита
            foreach (char c in text)
            {
                text = text.ToLower();
                if (char.IsLetter(c) && char.IsLower(c))
                {
                    if (characterFrequency.ContainsKey(c))
                    {
                        characterFrequency[c]++;
                    }
                    else
                    {
                        characterFrequency.Add(c, 1);
                    }
                }
            }

            // Вычисляем общее количество символов английского алфавита в тексте
            int totalCharacters = characterFrequency.Sum(x => x.Value);

            // Сортируем словарь по убыванию частоты появления символов и выводим пары "символ - частота появления в процентах"
            foreach (KeyValuePair<char, int> pair in characterFrequency.OrderByDescending(key => key.Value))
            {
                double frequencyPercentage = (double)pair.Value / totalCharacters * 100;
                Console.WriteLine("{0} - {1:F2}%", pair.Key, frequencyPercentage);
            }
        }
    }
}
