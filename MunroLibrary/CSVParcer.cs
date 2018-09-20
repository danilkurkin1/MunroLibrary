using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MunroLibrary
{
    public class CSVParcer
    {
        public List<string> HeaderList { get; set; }
        public List<string[]> DataList { get; set; }


        public CSVParcer(string pathToTheFile)
        {
            HeaderList = new List<string>();
            DataList = new List<string[]>();

            //HeadersDictionary = new Dictionary<string, int>
            //{
            //    { "Name" , 0 },
            //    { "Height (m)", 0 },
            //    { "post 1997", 0 },
            //    { "Grid Ref", 0 }
            //};

            ParseCSVFile(pathToTheFile);
        }

        public void ParseCSVFile(string pathToTheFile)
        {
            bool isHeader = true;
            var reader = new StreamReader(File.OpenRead(pathToTheFile));

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                if (isHeader)
                {
                    isHeader = false;
                    HeaderList = values.ToList();
                }
                else
                {
                    DataList.Add(values);
                }
            }
        }

    }
}
