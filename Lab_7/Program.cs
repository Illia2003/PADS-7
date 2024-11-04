using System;
using System.IO;
using System.Text;

namespace Lab_7
{
    public class VigenereCipher
    {
        public static string Encrypt(string text, string key)
        {
            key = key.ToUpper();
            var encryptedText = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsLetter(c))
                {
                    bool isUpper = char.IsUpper(c);
                    c = char.ToUpper(c);
                    int offset = (c - 'A' + (key[i % key.Length] - 'A')) % 26;
                    encryptedText.Append((char)(offset + 'A'));
                    if (!isUpper) encryptedText[encryptedText.Length - 1] = char.ToLower(encryptedText[encryptedText.Length - 1]);
                }
                else
                {
                    encryptedText.Append(c);
                }
            }
            return encryptedText.ToString();
        }

        public static string Decrypt(string text, string key)
        {
            key = key.ToUpper();
            var decryptedText = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsLetter(c))
                {
                    bool isUpper = char.IsUpper(c);
                    c = char.ToUpper(c);
                    int offset = (c - 'A' - (key[i % key.Length] - 'A') + 26) % 26;
                    decryptedText.Append((char)(offset + 'A'));
                    if (!isUpper) decryptedText[decryptedText.Length - 1] = char.ToLower(decryptedText[decryptedText.Length - 1]);
                }
                else
                {
                    decryptedText.Append(c);
                }
            }
            return decryptedText.ToString();
        }
    }

    public class FileManager
    {
        private string directoryPath = "C:\\";
        private string encryptionKey = "KEY";

        public void SetDirectory(string directory) { this.directoryPath = directory; }

        public void OpenFile(string fileName)
        {
            string filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine("Вміст файлу:\n" + content);
            }
            else
            {
                Console.WriteLine("Файл не знайдено.");
            }
        }

        public void SaveFile(string fileName, string content)
        {
            string filePath = Path.Combine(directoryPath, fileName);
            File.WriteAllText(filePath, content);
            Console.WriteLine("Файл збережено.");
        }

        public void EncryptFile(string fileName)
        {
            string filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                string encryptedContent = VigenereCipher.Encrypt(content, encryptionKey);
                File.WriteAllText(filePath, encryptedContent);
                Console.WriteLine("Файл зашифровано.");
            }
            else
            {
                Console.WriteLine("Файл не знайдено.");
            }
        }

        public void DecryptFile(string fileName)
        {
            string filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                string decryptedContent = VigenereCipher.Decrypt(content, encryptionKey);
                File.WriteAllText(filePath, decryptedContent);
                Console.WriteLine("Файл розшифровано.");
            }
            else
            {
                Console.WriteLine("Файл не знайдено.");
            }
        }

        public void ViewFileInfo(string fileName)
        {
            string filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                Console.WriteLine($"Ім'я файлу: {fileInfo.Name}");
                Console.WriteLine($"Розмір файлу: {fileInfo.Length} байт");
                Console.WriteLine($"Дата створення: {fileInfo.CreationTime}");
                Console.WriteLine($"Тип файлу: {fileInfo.Extension}");
            }
            else
            {
                Console.WriteLine("Файл не знайдено.");
            }
        }

        public void SearchFiles(string keyword)
        {
            var files = Directory.GetFiles(directoryPath, $"*{keyword}*");
            if (files.Length > 0)
            {
                Console.WriteLine("Знайдені файли:");
                foreach (var file in files)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }
            }
            else
            {
                Console.WriteLine("Файлів не знайдено.");
            }
        }

        public void PrintFile(string fileName)
        {
            string filePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine("Файл на друк:\n" + content);
            }
            else
            {
                Console.WriteLine("Файл не знайдено.");
            }
        }

        public void ViewFiles()
        {
            var files = Directory.GetFiles(directoryPath);
            Console.WriteLine("Список файлів:");
            foreach (var file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }

        public void ShowHelp()
        {
            Console.WriteLine("Ця програма дозволяє виконувати операції з файлами:");
            Console.WriteLine("1. Відкриття файлу");
            Console.WriteLine("2. Збереження файлу");
            Console.WriteLine("3. Шифрування файлу");
            Console.WriteLine("4. Розшифрування файлу");
            Console.WriteLine("5. Перегляд інформації про файл");
            Console.WriteLine("6. Пошук файлів");
            Console.WriteLine("7. Друк файлу");
            Console.WriteLine("8. Перегляд списку файлів");
            Console.WriteLine("9. Налаштування системи");
        }
    }

    public class Program
    {
        private static readonly string StoredEncryptedKey = VigenereCipher.Encrypt("password", "KEY"); 

        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var fileManager = new FileManager();
            Console.WriteLine("Введіть ключ для доступу до функцій:");
            string userKey = Console.ReadLine();
            string encryptedUserKey = VigenereCipher.Encrypt(userKey, "KEY"); 

            if (encryptedUserKey == StoredEncryptedKey) 
            {
                Console.WriteLine("Введіть деректорію:");
                string directory = Console.ReadLine();
                fileManager.SetDirectory(directory);
                bool running = true;
                while (running)
                {
                    Console.WriteLine("Виберіть функцію:");
                    Console.WriteLine("1. Відкрити файл");
                    Console.WriteLine("2. Зберегти файл");
                    Console.WriteLine("3. Зашифрувати файл");
                    Console.WriteLine("4. Розшифрувати файл");
                    Console.WriteLine("5. Переглянути інформацію про файл");
                    Console.WriteLine("6. Пошук файлів");
                    Console.WriteLine("7. Друк файлу");
                    Console.WriteLine("8. Перегляд списку файлів");
                    Console.WriteLine("9. Довідка");
                    Console.WriteLine("10. Вийти");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Введіть ім'я файлу:");
                            string openFileName = Console.ReadLine();
                            fileManager.OpenFile(openFileName);
                            break;
                        case 2:
                            Console.WriteLine("Введіть ім'я файлу:");
                            string saveFileName = Console.ReadLine();
                            Console.WriteLine("Введіть вміст файлу:");
                            string content = Console.ReadLine();
                            fileManager.SaveFile(saveFileName, content);
                            break;
                        case 3:
                            Console.WriteLine("Введіть ім'я файлу:");
                            string encryptFileName = Console.ReadLine();
                            fileManager.EncryptFile(encryptFileName);
                            break;
                        case 4:
                            Console.WriteLine("Введіть ім'я файлу:");
                            string decryptFileName = Console.ReadLine();
                            fileManager.DecryptFile(decryptFileName);
                            break;
                        case 5:
                            Console.WriteLine("Введіть ім'я файлу:");
                            string infoFileName = Console.ReadLine();
                            fileManager.ViewFileInfo(infoFileName);
                            break;
                        case 6:
                            Console.WriteLine("Введіть ключове слово для пошуку:");
                            string keyword = Console.ReadLine();
                            fileManager.SearchFiles(keyword);
                            break;
                        case 7:
                            Console.WriteLine("Введіть ім'я файлу:");
                            string printFileName = Console.ReadLine();
                            fileManager.PrintFile(printFileName);
                            break;
                        case 8:
                            fileManager.ViewFiles();
                            break;
                        case 9:
                            fileManager.ShowHelp();
                            break;
                        case 10:
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Невірний вибір.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Неправильний ключ.");
            }
        }
    }

}