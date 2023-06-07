char[] czechAlphabet = { 'a', 'á', 'b', 'c', 'č', 'd', 'ď', 'e', 'é', 'ě', 'f', 'g', 'h', 'i', 'í', 'j', 'k', 'l', 'm', 'n', 'Ň', 'o', 'Ó', 'p', 'Q', 'r', 'ř', 's', 'š', 't', 'Ť', 'u', 'Ú', 'Ů', 'v', 'w', 'x', 'y', 'ý', 'z', 'ž' };
char[] icelandicAlphabet = { 'a', 'á', 'b', 'd', 'ð', 'e', 'é', 'f', 'g', 'h', 'i', 'í', 'j', 'k', 'l', 'm', 'n', 'o', 'ó', 'p', 'r', 's', 't', 'u', 'ú', 'v', 'x', 'y', 'ý', 'þ', 'æ', 'ö' };
char[] binaryAlphabet = { '1', '0' };


Console.WriteLine("Лабораторная работа 2");
bool flag = true;
double entropyCzech = 0;
double entropyIce = 0;
double entropyBin = 0;

while (flag)
{
    Console.WriteLine("Выберете задание");
    Console.WriteLine("1-czech, 2-icelandic, 3-binary, 4-расчёт количества информации, 5-выход");
    int chooseTask;
    int.TryParse(Console.ReadLine(), out chooseTask);

    switch (chooseTask)
    {
        case 1:
            Console.WriteLine("Энтропия чешского языка по Шеннону:");
            entropyCzech = Entropy.CalculateShannonEntropy("Neztratit se neztratit\r\nty víš\r\nÓsestřičko... \r\n\r\nŇezvedni hlavu a pojď wzhe!\r\nŤbe roztáhni křídla z kostí\r\nÚm nátépníky na rukou tě objímají\r\n \r\n\r\nKdyž srdce rozmrzne,\r\nzačne i bolet fte\r\n\r\nZanechme stopy v kyberprostoru\r\nQaž se naše duše stane daty xnel\r\n\r\nČas je gne neustále-smrtelný\r\nProstor je neustále-přítomný Ů mene", czechAlphabet);
            break;
        case 2:
            Console.WriteLine("Энтропия исландского языка по Шеннону:");
            entropyIce = Entropy.CalculateShannonEntropy("Axar sax og lævarar lax\r\nAxar sax og lævarar lax\r\nHoppara boppara hoppara boppara\r\nÓstagara jagara stagara jagara\r\nNeglings steglings veglings steglings\r\nSkögula gögula ögula skögula\r\nhræfra flotið humýra skotið\r\nAxar sax og lævarar lax\r\n\r\nagara gagara agara gagara\r\nvambara þambara vambara þambara\r\n\r\nAmbra vambra skrambra sker\r\nþambra rambra ræsis\r\nÞusslega ró og frugga frum\r\nskræfra ræfra ræfils hröngli\r\nhagara gagara skruggu skröngli\r\nImbrum kimbrum æsingur\r\nþambra vambra þeysingssprattinn\r\nfríarí bríarí bríarum\r\n\r\nagara gagara agara gagara\r\nvambara þambara vambara þambara\r\n\r\nAglra geglru guglra stögl og geglra rambið\r\nHrjónungs kvot og hyrjargat á hrultara þili\r\nGaglra stiglu giglru strambið\r\nDandala vandala dæsis grund\r\nÞundar und úr þrautum réttir\r\nVargara gargara gresju gramm\r\ngaula auli í baulu fjósi\r\nurgara purgara ysjast fram agara gagara agara gagara\r\nvambara þambara vambara þambara", icelandicAlphabet);
            break;
        case 3:
            Console.WriteLine("Энтропия бинарного алфавита по Шеннону:");
            Console.WriteLine(Entropy.CalculateShannonEntropy(Entropy.Binary("Hello World"), binaryAlphabet));
            break;
        case 4:
            entropyCzech = Entropy.CalculateShannonEntropy1("Neztratit se neztratit\r\nty víš\r\nÓsestřičko... \r\n\r\nŇezvedni hlavu a pojď wzhe!\r\nŤbe roztáhni křídla z kostí\r\nÚm nátépníky na rukou tě objímají\r\n \r\n\r\nKdyž srdce rozmrzne,\r\nzačne i bolet fte\r\n\r\nZanechme stopy v kyberprostoru\r\nQaž se naše duše stane daty xnel\r\n\r\nČas je gne neustále-smrtelný\r\nProstor je neustále-přítomný Ů mene", czechAlphabet);
            entropyIce = Entropy.CalculateShannonEntropy1("Axar sax og lævarar lax\r\nAxar sax og lævarar lax\r\nHoppara boppara hoppara boppara\r\nÓstagara jagara stagara jagara\r\nNeglings steglings veglings steglings\r\nSkögula gögula ögula skögula\r\nhræfra flotið humýra skotið\r\nAxar sax og lævarar lax\r\n\r\nagara gagara agara gagara\r\nvambara þambara vambara þambara\r\n\r\nAmbra vambra skrambra sker\r\nþambra rambra ræsis\r\nÞusslega ró og frugga frum\r\nskræfra ræfra ræfils hröngli\r\nhagara gagara skruggu skröngli\r\nImbrum kimbrum æsingur\r\nþambra vambra þeysingssprattinn\r\nfríarí bríarí bríarum\r\n\r\nagara gagara agara gagara\r\nvambara þambara vambara þambara\r\n\r\nAglra geglru guglra stögl og geglra rambið\r\nHrjónungs kvot og hyrjargat á hrultara þili\r\nGaglra stiglu giglru strambið\r\nDandala vandala dæsis grund\r\nÞundar und úr þrautum réttir\r\nVargara gargara gresju gramm\r\ngaula auli í baulu fjósi\r\nurgara purgara ysjast fram agara gagara agara gagara\r\nvambara þambara vambara þambara", icelandicAlphabet);
            Console.WriteLine("Сообщение: Budanowa Ksenia Andreevna");
            Console.WriteLine("Czech\nКоличество информации:    " + Entropy.CountInformation("Budanowa Ksenia Andreevna", entropyCzech));
            Console.WriteLine("Icelandic\nКоличество информации:    " + Entropy.CountInformation("Budanowa Ksenia Andreevna", entropyIce));
            Console.WriteLine("Binary\nКоличество информации:    " + Entropy.CountInformation(Entropy.Binary("Budanowa Ksenia Andreevna"), entropyBin));
            Console.WriteLine("\nЭффективная энтропия бинарного алфавита:");
            double b1 = Entropy.WithError(0.1);
            Console.WriteLine("0.1: " + Entropy.WithError(0.1) + "\nКоличество информации:    " + Entropy.CountInformation(Entropy.Binary("Budanowa Ksenia Andreevna"), b1));
            double b2 = Entropy.WithError(0.5);
            Console.WriteLine("0.5: " + Entropy.WithError(0.5) + "\nКоличество информации:    " + Entropy.CountInformation(Entropy.Binary("Budanowa Ksenia Andreevna"), b2));
            double b3 = Entropy.WithError(0.9999999999999999);
            Console.WriteLine("1.0: " + Entropy.WithError(0.9999999999999999) + "\nКоличество информации:    " + Entropy.CountInformation(Entropy.Binary("Budanowa Ksenia Andreevna"), b3) + "\n");
            double s1 = Entropy.WithError(0.1);
            Console.WriteLine("Для чешского\n0.1: " + Entropy.WithError(0.1) + "\nКоличество информации:    " + Entropy.CountInformation("Budanowa Ksenia Andreevna", s1));
            double s2 = Entropy.WithError(0.5);
            Console.WriteLine("0.5: " + Entropy.WithError(0.5) + "\nКоличество информации:    " + Entropy.CountInformation("Budanowa Ksenia Andreevna", s2));
            double s3 = Entropy.WithError(0.9999999999999999);
            Console.WriteLine("1.0: " + Entropy.WithError(0.9999999999999999) + "\nКоличество информации:    0");
            break;
        case 5:
        default:
            flag = false;
            break;
    }
}


