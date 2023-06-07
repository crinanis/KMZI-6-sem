using System.Text;

public class Entropy
{
    public static double CalculateShannonEntropy(string text, char[] alphabet)
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
        for (int i = 0; i < alphabet.Length; i++)
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

    public static double CalculateShannonEntropy1(string text, char[] alphabet)
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
        for (int i = 0; i < alphabet.Length; i++)
        {
            int count = charCount[i];
            double probability = (double)count / textLength;
            if (probability != 0)
            {
                result += probability * Math.Log(probability, 2);
            }
        }

        double entropy = -result;
        return entropy;
    }

    public static string Binary(string text)
    {
        byte[] buf = Encoding.ASCII.GetBytes(text);
        char[] binaryChars = new char[buf.Length * 8];
        int index = 0;

        foreach (byte b in buf)
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

    public static double CountInformation(string text, double entropy)
    {
        text = text.Replace(" ", "");
        return text.Length * entropy;
    }

    public static double WithError(double error)
    {
        double puk = (double)(1 - (-error * Math.Log(error, 2) - (1 - error) * Math.Log((1 - error), 2)));
        return puk;
    }

}
