using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "d:\\1POIT\\3\\Crypto\\labs\\5\\lab5\\text.txt";
            string encryptedFile = "d:\\1POIT\\3\\Crypto\\labs\\5\\lab5\\zigzag-encoded.txt";
            string decryptedFile = "d:\\1POIT\\3\\Crypto\\labs\\5\\lab5\\zigzag-decoded.txt";
            Console.WriteLine("Введите количество строк для таблицы: ");
            int rows = int.Parse(Console.ReadLine());

            // Создаем объект Stopwatch для измерения времени выполнения
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            ZigZagEncrypt(inputFile, encryptedFile, rows);
            stopwatch.Stop();
            Console.WriteLine("\nZigZagEncrypt занял {0} мс ", stopwatch.ElapsedMilliseconds);

            // Шифр маршрутной перестановки (зигзаг)
            stopwatch.Reset();
            stopwatch.Start();
            ZigZagDecrypt(encryptedFile, decryptedFile, rows);
            stopwatch.Stop();
            Console.WriteLine("\nZigZagDecrypt занял {0} мс ", stopwatch.ElapsedMilliseconds);

            Console.WriteLine("Частота появления символов в тексте, зашифрованном шифром ");
            CountCharacterFrequency(File.ReadAllText(encryptedFile));
            Console.WriteLine("Частота появления символов в расшифрованном тексте:");
            CountCharacterFrequency(File.ReadAllText(decryptedFile));

            // Шифр множественной перестановки с ключом 
            encryptedFile = "d:\\1POIT\\3\\Crypto\\labs\\5\\lab5\\permutation-encoded.txt";
            decryptedFile = "d:\\1POIT\\3\\Crypto\\labs\\5\\lab5\\permutation-decoded.txt";
            string key1 = "ksenya";
            string key2 = "budanowa";

            stopwatch.Reset();
            stopwatch.Start();
            MultiplePermutationEncrypt(inputFile, encryptedFile, key1, key2);
            stopwatch.Stop();
            Console.WriteLine("\nMultiplePermutationEncrypt занял {0} мс ", stopwatch.ElapsedMilliseconds);

            stopwatch.Reset();
            stopwatch.Start();
            MultiplePermutationDecrypt(encryptedFile, decryptedFile, key1, key2);
            stopwatch.Stop();
            Console.WriteLine("\nMultiplePermutationDecrypt занял {0} мс ", stopwatch.ElapsedMilliseconds);

            Console.WriteLine("Частота появления символов в тексте, зашифрованном шифром множественной перестановки с ключом:");
            CountCharacterFrequency(File.ReadAllText(encryptedFile));
            Console.WriteLine("Частота появления символов в расшифрованном тексте:");
            CountCharacterFrequency(File.ReadAllText(decryptedFile));
        }

        static void ZigZagEncrypt(string inputFile, string encryptedFile, int rows)
        {
            string plainText = File.ReadAllText(inputFile);
            string encryptedText = "";

            // Вычисляем количество столбцов
            int cols = (int)Math.Ceiling((double)plainText.Length / rows);

            // Создаем матрицу перестановки
            char[,] permutation = new char[rows, cols];

            // Заполняем матрицу перестановки
            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (k < plainText.Length)
                    {
                        permutation[i, j] = plainText[k++];
                    }
                    else
                    {
                        permutation[i, j] = '!';
                    }
                }
            }

            // Формируем зашифрованный текст
            for (int j = 0; j < cols; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    encryptedText += permutation[i, j];
                }
            }
            Console.WriteLine("\nEncrypted:\n----------------------\n" + encryptedText);
            File.WriteAllText(encryptedFile, encryptedText);
        }

        static void ZigZagDecrypt(string encryptedFile, string decryptedFile, int rows)
        {
            string encryptedText = File.ReadAllText(encryptedFile);
            string decryptedText = "";

            // Вычисляем количество столбцов
            int cols = (int)Math.Ceiling((double)encryptedText.Length / rows);

            // Создаем матрицу перестановки
            char[,] permutation = new char[rows, cols];

            // Заполняем матрицу перестановки
            int k = 0;
            for (int j = 0; j < cols; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    permutation[i, j] = encryptedText[k++];
                }
            }

            // Формируем дешифрованный текст
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (permutation[i, j] != '!')
                    {
                        decryptedText += permutation[i, j];
                    }
                }
            }
            Console.WriteLine("\nDecrypted text:\n------------------------------\n" + decryptedText);
            File.WriteAllText(decryptedFile, decryptedText);
        }


        #region Multi

        static void MultiplePermutationEncrypt(string inputFile, string encryptedFile, string key1, string key2)
        {
            string plainText = File.ReadAllText(inputFile);
            string encryptedText = "";
            int n = key1.Length, m = key2.Length;
            int remainder = plainText.Length % (m * n);
            if (remainder != 0)
            {
                int charactersToRemove = (m * n) - remainder;
                plainText = plainText.Substring(0, plainText.Length - charactersToRemove);
            }

            var key1sorted = key1.ToCharArray();
            var key2sorted = key2.ToCharArray();

            var separatedText = Regex.Matches(plainText, ".{1," + (m * n) + "}").Cast<Match>().Select(m => m.Value).ToList();

            foreach (var substring in separatedText)
            {
                var localResult = substring;

                int temp = 0;
                char lastch = '`';
                var locseckey1 = key1;
                foreach (var ch in key1sorted)
                {
                    if (lastch != ch)
                    {
                        SwapRow(ref localResult, locseckey1.IndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey1, locseckey1.IndexOf(ch), temp++);
                    }
                    else
                    {
                        SwapRow(ref localResult, locseckey1.LastIndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey1, locseckey1.LastIndexOf(ch), temp++);
                    }
                    lastch = ch;
                }

                temp = 0;
                lastch = '`';
                var locseckey2 = key2;
                foreach (var ch in key2sorted)
                {
                    if (lastch != ch)
                    {
                        SwapColumn(ref localResult, locseckey2.IndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey2, locseckey2.IndexOf(ch), temp++);
                    }
                    else
                    {
                        SwapColumn(ref localResult, locseckey2.LastIndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey2, locseckey2.LastIndexOf(ch), temp++);
                    }
                    lastch = ch;
                }
                encryptedText += localResult;
            }
            Console.WriteLine("\nEncrypted:\n----------------------\n" +encryptedText);
            File.WriteAllText(encryptedFile, encryptedText);
        }
        static void MultiplePermutationDecrypt(string encryptedFile, string decryptedFile, string key1, string key2)
        {
            string encryptedText = File.ReadAllText(encryptedFile);
            string decryptedText = "";

            int n = key1.Length, m = key2.Length;

            var key1sorted = key1.ToCharArray();
            var key2sorted = key2.ToCharArray();

            
           var separatedText = Split(encryptedText, m * n);

            foreach (var substring in separatedText)
            {
                var localResult = substring;
                int temp = 0;
                char lastch = '`';
                var locseckey1 = String.Concat(key1sorted.Where(c => key1sorted.Contains(c)));
                foreach (var ch in key1)
                {
                    if (lastch != ch)
                    {
                        SwapRow(ref localResult, locseckey1.IndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey1, locseckey1.IndexOf(ch), temp++);
                    }
                    else
                    {
                        SwapRow(ref localResult, locseckey1.LastIndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey1, locseckey1.LastIndexOf(ch), temp++);
                    }
                    lastch = ch;
                }

                temp = 0;
                lastch = '`';
                var locseckey2 = String.Concat(key2sorted.Where(c => key2sorted.Contains(c)));
                foreach (var ch in key2)
                {
                    if (lastch != ch)
                    {
                        SwapColumn(ref localResult, locseckey2.IndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey2, locseckey2.IndexOf(ch), temp++);
                    }
                    else
                    {
                        SwapColumn(ref localResult, locseckey2.LastIndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey2, locseckey2.LastIndexOf(ch), temp++);
                    }
                    lastch = ch;
                }
                decryptedText += localResult;
            }
            Console.WriteLine("\nDecrypted text:\n------------------------------\n" + decryptedText);
            File.WriteAllText(decryptedFile, decryptedText);
        }
        private static void SwapCharacters(ref string str, int poschar1, int poschar2)
        {
            var aStringBuilder = new StringBuilder(str);
            char ch1 = str[poschar1];
            char ch2 = str[poschar2];
            aStringBuilder.Remove(poschar1, 1);
            aStringBuilder.Insert(poschar1, ch2);
            aStringBuilder.Remove(poschar2, 1);
            aStringBuilder.Insert(poschar2, ch1);
            str = aStringBuilder.ToString();
        }

        private static void SwapColumn(ref string str, int column1, int column2, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                SwapCharacters(ref str, i * m + column1, i * m + column2);
            }
        }

        private static void SwapRow(ref string str, int row1, int row2, int n, int m)
        {
            for (int i = 0; i < m; i++)
            {
                SwapCharacters(ref str, row1 * m + i, row2 * m + i);
            }
        }
        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        #endregion

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
