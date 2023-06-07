using System;
using System.Globalization;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using NPOI.XWPF.UserModel;

public class TextSteganography
{
    public static void HideText(string containerPath, string textToHide, string outputPath)
    {
        using (FileStream stream = new FileStream(containerPath, FileMode.Open, FileAccess.ReadWrite))
        {
            XWPFDocument doc = new XWPFDocument(stream);

            foreach (XWPFParagraph paragraph in doc.Paragraphs)
            {
                foreach (XWPFRun run in paragraph.Runs)
                {
                    string text = run.Text;

                    string modifiedText = ApplyKerning(text);

                    if (modifiedText != text)
                    {
                        run.SetText(modifiedText);
                        run.FontSize = 14;
                    }
                }
            }

            XWPFParagraph lastParagraph = doc.Paragraphs[doc.Paragraphs.Count - 1];
            XWPFRun hiddenRun = lastParagraph.InsertNewRun(0);
            hiddenRun.SetText(textToHide);
            hiddenRun.FontSize = 1;

            using (FileStream output = new FileStream(outputPath, FileMode.Create))
            {
                doc.Write(output);
            }
        }
    }

    public static string ExtractText(string containerPath)
    {
        using (FileStream stream = new FileStream(containerPath, FileMode.Open, FileAccess.Read))
        {
            XWPFDocument doc = new XWPFDocument(stream);

            StringBuilder hiddenText = new StringBuilder();

            foreach (XWPFParagraph paragraph in doc.Paragraphs)
            {
                foreach (XWPFRun run in paragraph.Runs)
                {
                    string text = run.Text;

                    if (run.FontSize == 1)
                    {
                        hiddenText.Append(text);
                    }
                }
            }

            return hiddenText.ToString();
        }
    }

    static string ApplyKerning(string text)
    {
        StringBuilder modifiedText = new StringBuilder();

        for (int i = 0; i < text.Length - 1; i++)
        {
            char currentChar = text[i];
            char nextChar = text[i + 1];

            modifiedText.Append(currentChar);

            if (CharUnicodeInfo.GetUnicodeCategory(currentChar) != CharUnicodeInfo.GetUnicodeCategory(nextChar))
            {
                modifiedText.Append("\u200A");
            }
        }

        modifiedText.Append(text[text.Length - 1]);

        return modifiedText.ToString();
    }
}
