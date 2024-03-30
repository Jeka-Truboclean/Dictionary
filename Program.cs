using System;
using System.IO;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Dictionary
{
    class Program
    {
        static string pathEng = @"C:\Dictionary\eng.txt";
        static string pathRus = @"C:\Dictionary\rus.txt";
        static void Main()
        { 
            FileInfo eng = new FileInfo(pathEng);
            FileInfo rus = new FileInfo(pathRus);
            File.WriteAllTextAsync(pathEng, "Hello\nApple\nCity\n");
            File.WriteAllTextAsync(pathRus, "Привет\nЯблоко\nГород\n");

            bool running = true;
            while (running)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Просмотреть словарь");
                Console.WriteLine("2. Перевести слово");
                Console.WriteLine("3. Добавить слово");
                Console.WriteLine("4. Удалить слово");
                Console.WriteLine("5. Заменить слово");
                Console.WriteLine("6. Завершить программу");

                Console.WriteLine(new string('_',30));

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Print();
                        Console.WriteLine(new string('_', 30));
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Введите слово для перевода:");
                        string wordToTranslate = Console.ReadLine();
                        Translate(wordToTranslate);
                        Console.WriteLine(new string('_', 30));
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Введите слово на русском:");
                        string rusWord = Console.ReadLine();
                        Console.WriteLine("Введите слово на английском:");
                        string engWord = Console.ReadLine();
                        AddWord(rusWord, engWord);
                        Console.WriteLine(new string('_', 30));
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Введите слово для удаления:");
                        string wordToDelete = Console.ReadLine();
                        DeleteWord(wordToDelete);
                        Console.WriteLine(new string('_', 30));
                        break;
                    case "5":
                        Console.Clear();
                        Print();
                        Console.WriteLine("Введите слово для замены:");
                        string delWord = Console.ReadLine();
                        Console.WriteLine("Введите слово на русском:");
                        rusWord = Console.ReadLine();
                        Console.WriteLine("Введите слово на английском:");
                        engWord = Console.ReadLine();
                        AddWord(rusWord, engWord);
                        Console.WriteLine(new string('_', 30));
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный ввод. Пожалуйста, выберите действие снова.");
                        Console.WriteLine(new string('_', 30));
                        break;
                }
            }
        }
        static void Print()
        {
            string[] lines = File.ReadAllLines(pathEng);
            string[] lines2 = File.ReadAllLines(pathRus);
            for (int i = 0; i < lines.Length; i++) 
            {
                Console.WriteLine($"{lines[i]} - {lines2[i]}");
            }
        }
        static void Translate(string word)
        {
            string[] lines = File.ReadAllLines(pathEng);
            string[] lines2 = File.ReadAllLines(pathRus);
            if (ContainsOnlyLatin(word))
            {
                
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == word)
                    {
                        Console.WriteLine($"{word} - {lines2[i]}");
                        break;
                    }
                }
            }
            else if (ContainsOnlyCyrillic(word))
            {
                
                for (int i = 0; i < lines2.Length; i++)
                {
                    if (lines2[i] == word)
                    {
                        Console.WriteLine($"{word} - {lines[i]}");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("ERROR!");
            }
        }
        static void AddWord(string wordRus, string wordEng)
        {
            File.AppendAllTextAsync(pathEng, $"{wordEng}\n");
            File.AppendAllTextAsync(pathRus, $"{wordRus}\n");
        }
        static void DeleteWord(string word)
        {
            if (ContainsOnlyLatin(word))
            {
                string[] lines = File.ReadAllLines(pathEng);
                string[] lines2 = File.ReadAllLines(pathRus);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == word)
                    {
                        lines[i] = null;
                        lines2[i] = null;
                        break;
                    }
                }
                File.Delete(pathEng);
                File.Delete(pathRus);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] != null)
                    {
                        File.AppendAllTextAsync(pathRus, $"{lines2[i]}\n");
                        File.AppendAllTextAsync(pathEng, $"{lines[i]}\n");
                    }
                }
            }
            else if (ContainsOnlyCyrillic(word))
            {
                string[] lines = File.ReadAllLines(pathRus);
                string[] lines2 = File.ReadAllLines(pathEng);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == word)
                    {
                        lines[i] = null;
                        lines2[i] = null;
                        break;
                    }
                }
                File.Delete(pathEng);
                File.Delete(pathRus);
                for (int i = 0;i < lines.Length; i++)
                {
                    if (lines[i] != null)
                    {
                        File.AppendAllTextAsync(pathRus, $"{lines[i]}\n");
                        File.AppendAllTextAsync(pathEng, $"{lines2[i]}\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("Not only one language word!");
            }
        }
        static void ChangeWord(string del, string rus, string eng)
        {
            DeleteWord(del);
            AddWord(rus,eng);
        }

        static bool ContainsOnlyLatin(string text)
        {
            return Regex.IsMatch(text, @"^[a-zA-Z\s]+$");
        }
        static bool ContainsOnlyCyrillic(string text)
        {
            return Regex.IsMatch(text, @"^[а-яА-Я\s]+$");
        }
    }
}
