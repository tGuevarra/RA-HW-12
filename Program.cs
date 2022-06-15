using System.IO;
using System.IO.Compression;

namespace RA_HW_12
{
    public class Program
    {
        public static void Main()
        { 
                var filePath = Directory.GetCurrentDirectory() + "\\";
                var fileName = "archive.zip";

                Console.WriteLine($"Путь файла {filePath + fileName}");

                if (Directory.Exists(filePath + "archive"))
                {
                    Console.WriteLine($"Архив уже существует, хотите продолжить (д/н)?");
                    string input = Console.ReadLine();
                    if (input == "н")
                    {
                        return;
                    }
                    else if (input != "д")
                    {
                        return;
                    }
                }
                else
                {
                    try
                    {
                        Directory.CreateDirectory(filePath + "archive");
                        ZipFile.ExtractToDirectory(filePath + fileName, filePath + "archive");
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine();

                        return;
                    }
                }

                List<ItemInfo> items = new();
                ItemInfo item;

                string[] names = Directory.GetDirectories(filePath + "archive");

                if (names.Length > 0)
                {
                    for (int i = 0; i < names.Length; i++)
                    {
                        item = new(ItemInfo.Type.Directory, names[i].Substring(names[i].LastIndexOf('\\') + 1),
                            Directory.GetLastAccessTime(names[i]));
                        items.Add(item);
                    }
                }

                names = Directory.GetFiles(filePath + "archive");
                if (names.Length > 0)
                {
                    for (int i = 0; i < names.Length; i++)
                    {
                        item = new(ItemInfo.Type.File, names[i].Substring(names[i].LastIndexOf('\\') + 1),
                            Directory.GetLastAccessTime(names[i]));
                        items.Add(item);
                    }
                }

                foreach (var itemInfo in items)
                {
                    Console.WriteLine(itemInfo.ToString());
                }

                using (StreamWriter stream = new(filePath + "textfile.csv", false))
                {
                    try
                    {
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (i < items.Count - 1)
                            {
                                stream.Write(items[i].ToString() + "\t");
                            }
                            else if (i == items.Count - 1)
                            {
                                stream.Write(items[i].ToString());
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                        return;
                    }
                }

                Console.WriteLine("Файл формата .csv создан");
                
                Directory.Delete(filePath + "archive", true);

                using (StreamWriter stream = new(filePath + "Lesson12Homework.txt", false))
                {
                    stream.Write(filePath + "textfile.csv");
                }

                Console.WriteLine($"Расположение .csv файла {filePath}textfile.csv");
                Console.ReadKey();
            }
        }
    }