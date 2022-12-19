using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardLaba4
{
    internal class TableReader
    {
        public static Table TableRead(Scheme schemeOfTable, string pathTable)
        {
            string[] allLinesTable = File.ReadAllLines("Data\\" + pathTable);

            Table table = new Table();

            for (int i = 0; i < allLinesTable.Length; i++)
            {
                string[] elementsOfLine = allLinesTable[i].Split(";");

                if (elementsOfLine.Length > schemeOfTable.Columns.Count)
                {
                    throw new ArgumentException($"В файле {pathTable} в строке {i + 1} столбцов больше чем {schemeOfTable.Columns.Count}");
                }

                Row row = RowRead(schemeOfTable, pathTable, i, elementsOfLine);
                table.Rows.Add(row);
            }
            return table;
        }

        private static Row RowRead(Scheme schemeOfTable, string pathTable, int i, string[] elementsOfLine)
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
    }
}
