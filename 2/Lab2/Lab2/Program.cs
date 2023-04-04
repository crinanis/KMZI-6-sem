using System;
using System.Text;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] czech = { 'a', 'á', 'b', 'c', 'č', 'd', 'ď', 'e', 'é', 'ě', 'f', 'g', 'h', 'i', 'í', 'j', 'k', 'l', 'm', 'n', 'Ň', 'o', 'Ó',
                'p', 'Q', 'r', 'ř', 's', 'š', 't', 'Ť', 'u', 'Ú', 'Ů', 'v', 'w', 'x', 'y', 'ý', 'z', 'ž' };
            char[] icelandic = { 'a', 'á', 'b', 'd', 'ð', 'e', 'é', 'f', 'g', 'h', 'i', 'í', 'j', 'k', 'l', 'm', 'n', 'o', 'ó',
                'p', 'r', 's', 't', 'u', 'ú', 'v', 'x', 'y', 'ý', 'þ', 'æ', 'ö' }; 
            char[] binary = { '1', '0' };

            Console.WriteLine("Лабораторная работа 2");
            Console.WriteLine("Задание 1-2 - Рассчитать энтропию алфавитов");
            bool flag = true;
            while(flag){
                Console.WriteLine("Выберете задание");
                Console.WriteLine("1-czech, 2-icelandic, 3-binary, 4-расчёт количества информации, 5-выход");
                int choose_task = 0;
                int.TryParse(Console.ReadLine(), out choose_task);

                switch (choose_task)
                {
                    case (1):
                        Console.WriteLine("Энтропия чешского языка по Шеннону:");
                        Entropy_Shen("Neztratit se neztratit\r\nty víš\r\nsestřičko... \r\n\r\nzvedni hlavu a pojď!\r\nroztáhni křídla z kostí\r\nnátepníky na rukou tě objímají\r\n \r\n\r\nKdyž srdce rozmrzne,\r\nzačne i bolet\r\n\r\nZanechme stopy v kyberprostoru\r\naž se naše duše stane daty\r\n\r\nČas je neustále-smrtelný\r\nProstor je neustále-přítomný", czech);
                        break;
                    case (2):
                        Console.WriteLine("Энтропия исландского языка по Шеннону:");
                        Entropy_Shen("Axar sax og lævarar lax\r\nAxar sax og lævarar lax\r\nHoppara boppara hoppara boppara\r\nstagara jagara stagara jagara\r\nNeglings steglings veglings steglings\r\nSkögula gögula ögula skögula\r\nhræfra flotið humra skotið\r\nAxar sax og lævarar lax\r\n\r\nagara gagara agara gagara\r\nvambara þambara vambara þambara\r\n\r\nAmbra vambra skrambra sker\r\nþambra rambra ræsis\r\nÞusslega ró og frugga frum\r\nskræfra ræfra ræfils hröngli\r\nhagara gagara skruggu skröngli\r\nImbrum kimbrum æsingur\r\nþambra vambra þeysingssprattinn\r\nfríarí bríarí bríarum\r\n\r\nagara gagara agara gagara\r\nvambara þambara vambara þambara\r\n\r\nAglra geglru guglra stögl og geglra rambið\r\nHrjónungs kvot og hyrjargat á hrultara þili\r\nGaglra stiglu giglru strambið\r\nDandala vandala dæsis grund\r\nÞundar und úr þrautum réttir\r\nVargara gargara gresju gramm\r\ngaula auli í baulu fjósi\r\nurgara purgara ysjast fram agara gagara agara gagara\r\nvambara þambara vambara þambara\r\n\r\nViggjara þöggara vúgrar brúgrar\r\nfrugrar skrugrar frá því skreytti\r\nVampara stampara vumparar bumpara\r\nfrumbara þumbara fjandans lómur\r\nára diks á priksum, krunkum\r\nnagla stúss og nafra púss\r\nklastra stir og kjóla ruð\r\nhellirs dagra hallar suð\r\n\r\nagara gagara agara gagara\r\nvambara þambara vambara þambara", icelandic);
                        break;
                    case (3):
                        Console.WriteLine("Энтропия bin EN по Шеннону:");
                        Console.WriteLine(Binary("Hello World"));
                        Entropy_Shen(Binary("Hello World"), binary);
                        break;
                    case (4):
                        Console.WriteLine("Задание 3 - посчитать количество информации");
                        Console.WriteLine("Сообщение: Budanowa Ksenia Andreevna");
                        Console.WriteLine("Czech");
                        double a1 = Entropy_Shen("Budanowa Ksenia Andreevna", czech);
                        Console.WriteLine("Количество информации:    " + CountInfo("Budanowa Ksenia Andreevna", a1));
                        Console.WriteLine();
                        Console.WriteLine("Icelandic");
                        double a2 = Entropy_Shen("Budanova Ksenia Andreevna", icelandic);
                        Console.WriteLine("Количество информации:    " + CountInfo("Budanowa Ksenia Andreevna", a2));
                        Console.WriteLine();
                        Console.WriteLine("Binary");
                        double a3 = Entropy_Shen(Binary("Budanowa Ksenia Andreevna"), binary);
                        Console.WriteLine("Количество информации:    " + CountInfo(Binary("Budanowa Ksenia Andreevna"), a3));
                        Console.WriteLine();

                        Console.WriteLine("4 задание");
                        Console.WriteLine("Эффективная энтропия бинарного алфавита:");
                        double b1 = WithError(0.1);
                        Console.WriteLine("0.1: " + WithError(0.1));
                        Console.WriteLine("Количество информации:    " + CountInfo(Binary("Budanowa Ksenia Andreevna"), b1));
                        double b2 = WithError(0.5);
                        Console.WriteLine("0.5: " + WithError(0.5));
                        Console.WriteLine("Количество информации:    " + CountInfo(Binary("Budanowa Ksenia Andreevna"), b2));
                        double b3 = WithError(0.9999999999999999);
                        Console.WriteLine("1.0: " + WithError(0.9999999999999999));
                        Console.WriteLine("Количество информации:    " + CountInfo(Binary("Budanowa Ksenia Andreevna"), b3));
                        Console.WriteLine();
                        Console.WriteLine("Для чешского");
                        double s1 = WithError(0.1);
                        Console.WriteLine("0.1: " + WithError(0.1));
                        Console.WriteLine("Количество информации:    " + CountInfo("Budanowa Ksenia Andreevna", s1));
                        double s2 = WithError(0.5);
                        Console.WriteLine("0.5: " + WithError(0.5));
                        Console.WriteLine("Количество информации:    " + CountInfo("Budanowa Ksenia Andreevna", s2));
                        double s3 = WithError(0.9999999999999999);
                        Console.WriteLine("1.0: " + WithError(0.9999999999999999));
                        Console.WriteLine("Количество информации:    0");
                        break;
                    case (5):
                    default:
                        flag = false;
                        break;
                }
            }            
        }

        public static double Entropy_Shen(string text, char[] alphabet)
        {
            int[] charCount = new int[alphabet.Length];
            int textLength = text.Length;

            for (int i = 0; i < textLength; i++)
            {
                char c = text[i];
                int index = Array.IndexOf(alphabet, c);
                if (index != -1)
                {
                    charCount[index]++;
                }
            }

            double result = 0;
            for(int i=0; i < alphabet.Length; i++)
            {
                int count = charCount[i];
                double probability = (double)count / textLength;
                if (probability != 0)
                {
                    result += probability * Math.Log(probability, 2);
                    Console.WriteLine(-probability * Math.Log(probability, 2));
                }
                else
                {
                    Console.WriteLine(0);
                }
            }

            double entropy = -result;
            Console.WriteLine("Энтропия: {0}", entropy);
            return entropy;
        }

        public static string Binary(string text)
        {
             byte[] buf =Encoding.ASCII.GetBytes(text);
            char[] binaryChars = new char[buf.Length * 8];
            int index = 0;

            foreach(byte b in buf)
            {
                int bitIndex = 7;
                while (bitIndex >= 0)
                {
                    binaryChars[index] = ((b >> bitIndex) & 1) == 1 ? '1' : '0';
                    index++;
                    bitIndex--;
                }
            }

            string binaryStr = new string(binaryChars);
            return binaryStr;
        }

        static double CountInfo(string text, double entropy)
        {
            return text.Length * entropy;
        }

        static double WithError(double error)
        {
            double puk = (double)(1 - (-error * Math.Log(error, 2) - (1 - error) * Math.Log((1 - error), 2)));
            return puk;
        }
    }
}
