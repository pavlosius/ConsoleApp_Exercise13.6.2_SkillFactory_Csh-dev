using System.Diagnostics;

var fileName = "Text1.txt";
var location = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
var filePath = Path.Combine(location, fileName);
Console.WriteLine($"Путь к файлу: {filePath}");
if (File.Exists(filePath))
{
    string text;
    using (StreamReader sr = File.OpenText(filePath))
    {
        text = sr.ReadToEnd();
    }
    //var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray()); //Плохо работает
    string[] words = text.Split(new char[] { ' ', '.', ',', ';', ':', '!', '?','\n', '«', '»' }, StringSplitOptions.RemoveEmptyEntries);
    Dictionary<string, int> wordCount = new Dictionary<string, int>();
    foreach (string word in words)
    {
        if (wordCount.ContainsKey(word))
        {
            wordCount[word]++;
        }
        else
        {
            wordCount[word] = 1;
        }

    }
    var topWords = wordCount.OrderByDescending(pair => pair.Value).Take(10);
    Console.WriteLine("10 слов, чаще всего встречающиеся в тексте:");
    int i = 0;
    foreach (var word in topWords)
    {
        Console.WriteLine($"{i+1}) \"{word.Key}\" повторяется {word.Value} раз");
        i++;
    }
}
else
{
    Console.WriteLine("По заданному пути файл не существует.");
}
Console.WriteLine("Программа завершена");
Console.ReadKey();