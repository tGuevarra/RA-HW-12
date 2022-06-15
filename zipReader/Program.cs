using RA_HW_12;

namespace zipReader
{
    public class Program
    {
        public static void Main()
        {
            string currentDirectory = (@"C:\Users\denis\source\repos\RA-HW-12\RA-HW-12\bin\Debug\net6.0\");

            if (!File.Exists(currentDirectory + "Lesson12Homework.txt"))
            {
                Console.WriteLine("Файл не найден");
                Console.ReadLine();
                return;
            }

            string zipReaderFileName;
            string zipReaderFilePath;

            using (StreamReader stream = new(currentDirectory + "Lesson12Homework.txt"))
            {
                string str = stream.ReadToEnd();

                zipReaderFileName = str.Substring(str.LastIndexOf('\\') + 1) + "\\";
                zipReaderFilePath = str.Substring(0, str.LastIndexOf('\\'));
            }

            if (!File.Exists(zipReaderFilePath + zipReaderFileName))
            {
                Console.WriteLine($"Файл {zipReaderFileName} не найден");
                Console.ReadLine();
                return;
            }

            string[] strings;

            using (StreamReader stream = new(zipReaderFilePath + zipReaderFileName))
            {
                strings = stream.ReadToEnd().Split('\t');
            }

            List<ItemInfo> items = new();
            ItemInfo item;
            ItemInfo.Type itemType;
            DateTime updateTime;
            string[] s;
            string[] date;
            string[] time;

            foreach (var str in strings)
            {
                s = str.Split(' ');

                if (s[0] == "Путь")
                {
                    itemType = ItemInfo.Type.Directory;
                }
                else if (s[0] == "Файл")
                {
                    itemType = ItemInfo.Type.File;
                }
                else
                {
                    Console.WriteLine("Неверный тип");
                    return;
                }

                date = s[2].Split('.');
                time = s[3].Split(':');
                updateTime = new(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]),
                    Int32.Parse(time[0]), Int32.Parse(time[1]), Int32.Parse(time[2]));

                item = new(itemType, s[1], updateTime);
                items.Add(item);
            }

            Console.WriteLine($"Файл {zipReaderFileName} содержит {items.Count} количество элементов");
            
            var sortedItems = from i in items orderby i.itemUpdateTime select i;

            foreach (var i in sortedItems)
            {
                Console.WriteLine(i.ToString());
            }

            File.Delete(currentDirectory + "Lesson12Homework.txt");
            Console.ReadKey();
        }
    }
}