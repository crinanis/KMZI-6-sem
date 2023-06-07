Console.WriteLine("1. Скрыть текст");
Console.WriteLine("2. Извлечь текст");
Console.Write("Выберите действие (1 или 2): ");
string choice = Console.ReadLine();

if (choice == "1")
{
    Console.Write("Введите путь к документу-контейнеру (docx): ");
    string containerPath = Console.ReadLine();
    Console.Write("Введите текст, который нужно скрыть: ");
    string textToHide = Console.ReadLine();
    Console.Write("Введите путь для сохранения измененного документа (docx): ");
    string outputPath = Console.ReadLine();

    TextSteganography.HideText(containerPath, textToHide, outputPath);
    Console.WriteLine("Текст успешно скрыт!");
}
else if (choice == "2")
{
    Console.Write("Введите путь к документу со скрытым текстом (docx): ");
    string containerPath = Console.ReadLine();
    string hiddenText = TextSteganography.ExtractText(containerPath);
    Console.WriteLine("Скрытый текст: " + hiddenText);
}
else
{
    Console.WriteLine("Неверный выбор.");
}

Console.ReadLine();