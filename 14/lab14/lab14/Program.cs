using System.Drawing.Imaging;
using System.Drawing;
using System.Text;

Bitmap container = new Bitmap("d:\\1POIT\\3\\Crypto\\labs\\14\\lab14\\cat.bmp");

Console.WriteLine("Метод псевдослучаной перестановки");
string message = "Буданова Ксения Андреевна";
byte[] messageBytes = Encoding.UTF8.GetBytes(message);

Bitmap stegoContainer = Steganography.EmbedPixelPermutation(container, messageBytes);

stegoContainer.Save("d:\\1POIT\\3\\Crypto\\labs\\14\\lab14\\stego_containerPP.bmp", ImageFormat.Bmp);

Bitmap colorMatrix = Steganography.GenerateColorMatrix(stegoContainer, 3);
colorMatrix.Save("d:\\1POIT\\3\\Crypto\\labs\\14\\lab14\\matrixPP.bmp", ImageFormat.Bmp);

int messageLength = messageBytes.Length;
string extractedMessage = Steganography.ExtractPixelPermutation(stegoContainer, messageLength);

Console.WriteLine($"Исходное сообщение: {message}");
Console.WriteLine($"Извлеченное сообщение: {extractedMessage}\n");

Console.WriteLine("Метод LSB");
string message1 = "POIT 5";

Bitmap stegoContainer1 = Steganography.EmbedLSB(message1, container);

stegoContainer1.Save("d:\\1POIT\\3\\Crypto\\labs\\14\\lab14\\stego_containerLSB.bmp", ImageFormat.Bmp);

colorMatrix = Steganography.GenerateColorMatrix(stegoContainer1, 3);
colorMatrix.Save("d:\\1POIT\\3\\Crypto\\labs\\14\\lab14\\matrixLSB.bmp", ImageFormat.Bmp);

string extracted = Steganography.ExtractLSB(stegoContainer1);

Console.WriteLine($"Исходное сообщение: {message1}");
Console.WriteLine($"Извлеченное сообщение: {extracted}\n");