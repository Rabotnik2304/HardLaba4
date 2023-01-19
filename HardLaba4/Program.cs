using Newtonsoft.Json;

namespace HardLaba4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string pathScheme = "Scheme\\Generals.scheme.json";
            // с помошью Newtonsoft.Json и классов Scheme и SchemeColumn
            // парсим информацию из json файла со схемой таблички
            Scheme schemeOfTable = Scheme.ReadJson(pathScheme);

            try
            {
                string pathTable = "Generals.csv";
                Table table = TableReader.TableRead(schemeOfTable, pathTable);

                // вывод информации, считанной из csv файла
                foreach (SchemeColumn key in table.Rows[0].Data.Keys)
                {
                    Console.Write(key.Name+" ");
                }
                Console.WriteLine();
                foreach (Row row in table.Rows)
                {
                    Console.WriteLine(string.Join(" ",row.Data.Values));
                }
            }
            catch (ArgumentException ex)
            {
                Console.Clear();
                Console.Write("Ошибка:");
                Console.WriteLine(ex.Message);
            }
        } 
    }
}