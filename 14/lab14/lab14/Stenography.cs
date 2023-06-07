using System.Collections.Generic;
using System.Drawing;
using System.Text;

public class Steganography
{
    public static Bitmap EmbedPixelPermutation(Bitmap container, byte[] message)
    {
        int width = container.Width;
        int height = container.Height;

        Bitmap stegoContainer = new Bitmap(container);

        int messageIndex = 0;
        int bitIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (messageIndex < message.Length)
                {
                    Color pixel = container.GetPixel(x, y);
                    int red = pixel.R;
                    int green = pixel.G;
                    int blue = pixel.B;

                    // Получаем текущий байт сообщения и его бит
                    byte currentByte = message[messageIndex];
                    int currentBit = (currentByte >> (7 - bitIndex)) & 0x01;

                    // Заменяем младший бит компоненты цвета пикселя на бит сообщения
                    red = (red & 0xFE) | currentBit;

                    // Создаем новый пиксель с измененной компонентой цвета
                    Color stegoPixel = Color.FromArgb(red, green, blue);

                    // Заменяем пиксель в стегоконтейнере
                    stegoContainer.SetPixel(x, y, stegoPixel);

                    bitIndex++;

                    // Если все 8 бит текущего байта сообщения были внедрены, переходим к следующему байту
                    if (bitIndex >= 8)
                    {
                        bitIndex = 0;
                        messageIndex++;
                    }
                }
            }
        }

        return stegoContainer;
    }

    public static string ExtractPixelPermutation(Bitmap stegoContainer, int messageLength)
    {
        int width = stegoContainer.Width;
        int height = stegoContainer.Height;

        List<byte> messageBytes = new List<byte>();
        int bitIndex = 0;
        byte currentByte = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixel = stegoContainer.GetPixel(x, y);

                // Извлекаем младший бит компоненты цвета пикселя
                int redBit = pixel.R & 0x01;
                int greenBit = pixel.G & 0x01;
                int blueBit = pixel.B & 0x01;

                // Комбинируем младшие биты в байт сообщения
                currentByte = (byte)((currentByte << 1) | redBit);
                bitIndex++;

                // Если извлечены все 8 бит текущего байта, добавляем его в сообщение
                if (bitIndex >= 8)
                {
                    messageBytes.Add(currentByte);
                    currentByte = 0;
                    bitIndex = 0;
                }

                // Если извлечено достаточное количество бит для сообщения, завершаем извлечение
                if (messageBytes.Count >= messageLength)
                {
                    break;
                }
            }

            if (messageBytes.Count >= messageLength)
            {
                break;
            }
        }

        // Преобразуем массив байт в строку с использованием кодировки UTF-8
        string message = Encoding.UTF8.GetString(messageBytes.ToArray());

        return message;
    }

    public static Bitmap EmbedLSB(string text, Bitmap bmp)
    {
        int state = 1; // Состояние встраивания (1 - в процессе встраивания, 0 - завершено)
        int textIndex = 0; // Индекс текущего символа текста
        int charValue = 0; // Значение текущего символа в числовом представлении
        long pixelIndex = 0; // Индекс текущего пикселя
        int zeros = 0; // Счетчик количества нулей
        for (int i = 0; i < bmp.Height; i++)
        {
            for (int j = 0; j < bmp.Width; j++)
            {
                Color pixel = bmp.GetPixel(j, i);
                int modifiedR = pixel.R - pixel.R % 2; // Модифицированное значение компонента R
                int modifiedG = pixel.G - pixel.G % 2; // Модифицированное значение компонента G
                int modifiedB = pixel.B - pixel.B % 2; // Модифицированное значение компонента B
                for (int k = 0; k < 3; k++)
                {
                    if (pixelIndex % 8 == 0)
                    {
                        if (state == 0 && zeros == 8)
                        {
                            // Вставляем последний пиксель и завершаем процесс встраивания
                            if ((pixelIndex - 1) % 3 < 2)
                            {
                                bmp.SetPixel(j, i, Color.FromArgb(modifiedR, modifiedG, modifiedB));
                            }
                            return bmp;
                        }
                        if (textIndex >= text.Length)
                        {
                            state = 0; // Все символы текста встроены, переключаем состояние на завершение
                        }
                        else
                        {
                            charValue = text[textIndex++];
                        }
                    }
                    switch (pixelIndex % 3)
                    {
                        case 0:
                            {
                                if (state == 1)
                                {
                                    modifiedR += charValue % 2;
                                    charValue /= 2;
                                }
                            }
                            break;
                        case 1:
                            {
                                if (state == 1)
                                {
                                    modifiedG += charValue % 2;
                                    charValue /= 2;
                                }
                            }
                            break;
                        case 2:
                            {
                                if (state == 1)
                                {
                                    modifiedB += charValue % 2;
                                    charValue /= 2;
                                }
                                bmp.SetPixel(j, i, Color.FromArgb(modifiedR, modifiedG, modifiedB));
                            }
                            break;
                    }
                    pixelIndex++;
                    if (state == 0)
                    {
                        zeros++;
                    }
                }
            }
        }
        return bmp;
    }

    public static string ExtractLSB(Bitmap bmp)
    {
        int pixelIndex = 0; // Индекс текущего пикселя
        int charValue = 0; // Значение текущего символа
        StringBuilder extractedText = new StringBuilder(); // Извлеченный текст
        for (int i = 0; i < bmp.Height; i++)
        {
            for (int j = 0; j < bmp.Width; j++)
            {
                Color pixel = bmp.GetPixel(j, i);
                for (int k = 0; k < 3; k++)
                {
                    switch (pixelIndex % 3)
                    {
                        case 0:
                            {
                                charValue = charValue * 2 + pixel.R % 2;
                            }
                            break;
                        case 1:
                            {
                                charValue = charValue * 2 + pixel.G % 2;
                            }
                            break;
                        case 2:
                            {
                                charValue = charValue * 2 + pixel.B % 2;
                            }
                            break;
                    }
                    pixelIndex++;
                    if (pixelIndex % 8 == 0)
                    {
                        charValue = SwapByteBits(charValue);
                        if (charValue == 0)
                        {
                            return extractedText.ToString();
                        }
                        char c = (char)charValue;
                        extractedText.Append(c);
                    }
                }
            }
        }
        return extractedText.ToString();
    }


    public static Bitmap GenerateColorMatrix(Bitmap container, int bitLevel)
    {
        int width = container.Width;
        int height = container.Height;

        Bitmap colorMatrix = new Bitmap(width, height);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixel = container.GetPixel(x, y);

                // Извлекаем компоненты цвета
                int red = pixel.R;
                int green = pixel.G;
                int blue = pixel.B;

                // Получаем значение бита на соответствующем уровне
                int bitValue = GetBitValue(red, green, blue, bitLevel);

                // Создаем пиксель с цветом, соответствующим значению бита
                Color matrixPixel = GetColorForBit(bitValue);

                // Заменяем пиксель в цветовой матрице
                colorMatrix.SetPixel(x, y, matrixPixel);
            }
        }

        return colorMatrix;
    }

    public static int GetBitValue(int red, int green, int blue, int bitLevel)
    {
        int bitMask = 1 << bitLevel;

        int redBit = (red & bitMask) >> bitLevel;
        int greenBit = (green & bitMask) >> bitLevel;
        int blueBit = (blue & bitMask) >> bitLevel;

        return (redBit << 2) | (greenBit << 1) | blueBit;
    }

    public static Color GetColorForBit(int bitValue)
    {
        // Задаем значения цветовых компонент в соответствии с битом
        int red = (bitValue & 0x04) != 0 ? 255 : 0;   // Красный канал
        int green = (bitValue & 0x02) != 0 ? 255 : 0; // Зеленый канал
        int blue = (bitValue & 0x01) != 0 ? 255 : 0;  // Синий канал

        Color color = Color.FromArgb(red, green, blue);
        return color;
    }

    private static int SwapByteBits(int n)
    {
        n = (n & 0xF0) >> 4 | (n & 0x0F) << 4;
        n = (n & 0xCC) >> 2 | (n & 0x33) << 2;
        n = (n & 0xAA) >> 1 | (n & 0x55) << 1;
        return n;
    }
}
