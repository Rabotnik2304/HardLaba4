using Newtonsoft.Json;

namespace HardLaba4
{
    public class Scheme
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("columns")]
        public List<SchemeColumn> Columns { get; set; }
    }    
    public class SchemeColumn
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
    public class Table
    {
        public List<Row> Rows { get; set; }
        public Table()
        {
            Rows = new List<Row>();
        }
    }
    public class Row
    {
        public Dictionary<SchemeColumn, object> Data { get; set; }
        public Row()
        {
            Data = new Dictionary<SchemeColumn, object>();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string pathScheme = "Scheme\\FootballMatch.scheme.json";
            Scheme schemeOfTable = readJson(pathScheme);

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try
            {
                string pathTable = "Data\\FootballMatch.csv";
                Table table = TableInitialization(schemeOfTable, pathTable);
                
                foreach (SchemeColumn key in table.Rows[0].Data.Keys)
                {
                    Console.Write(key.Name+" ");
                }
                Console.WriteLine();
                foreach (Row row in table.Rows)
                {
                    foreach (object value in row.Data.Values)
                    {
                        Console.Write(value + " ");
                    }
                    Console.WriteLine();
                }
            }
            catch (ArgumentException ex)
            {
                Console.Clear();
                Console.Write("Ошибка:");
                Console.WriteLine(ex.Message);
            }
        }
        

        private static Table TableInitialization(Scheme schemeOfTable, string pathTable)
        {
            string[] allLinesTable = File.ReadAllLines(pathTable);

            Table table = new Table();
            
            for (int i = 0; i < allLinesTable.Length; i++)
            {
                string[] elementsOfLine = allLinesTable[i].Split(";");

                if (elementsOfLine.Length > schemeOfTable.Columns.Count)
                {
                    throw new ArgumentException($"В файле {pathTable} в строке {i + 1} столбцов больше чем {schemeOfTable.Columns.Count}");
                }

                Row row = RowInitialization(schemeOfTable, pathTable, i, elementsOfLine);
                table.Rows.Add(row);
            }
            return table;
        }

        private static Row RowInitialization(Scheme schemeOfTable, string pathTable, int i, string[] elementsOfLine)
        {
            Row row = new Row();

            for (int j = 0; j < elementsOfLine.Length; j++)
            {
                switch (schemeOfTable.Columns[j].Type)
                {
                    case "uint":
                        if (uint.TryParse(elementsOfLine[j], out uint number))
                        {
                            row.Data.Add(schemeOfTable.Columns[j], number);
                        }
                        else
                        {
                            throw new ArgumentException($"В файле {pathTable} в строке {i + 1} в столбце {j + 1} записаны некорректные данные");
                        }
                        break;
                    case "double":
                        if (double.TryParse(elementsOfLine[j], out double doubleNumber))
                        {
                            row.Data.Add(schemeOfTable.Columns[j], doubleNumber);
                        }
                        else
                        {
                            throw new ArgumentException($"В файле {pathTable} в строке {i + 1} в столбце {j + 1} записаны некорректные данные");
                        }
                        break;
                    case "datetime":
                        if (DateTime.TryParse(elementsOfLine[j], out DateTime date))
                        {
                            row.Data.Add(schemeOfTable.Columns[j], date);
                        }
                        else
                        {
                            throw new ArgumentException($"В файле {pathTable} в строке {i + 1} в столбце {j + 1} записаны некорректные данные");
                        }
                        break;
                    default:
                        row.Data.Add(schemeOfTable.Columns[j], elementsOfLine[j]);
                        break;
                }
            }
            return row;
        }

        private static Scheme readJson(string path)
        {
            return JsonConvert.DeserializeObject<Scheme>(File.ReadAllText(path));
        }
    }
}