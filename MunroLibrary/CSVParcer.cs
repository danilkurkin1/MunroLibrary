using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MunroLibrary.Model;

namespace MunroLibrary
{
    public class CSVParcer
    {
        public List<string> HeaderList { get; set; }
        public List<string[]> DataList { get; set; }
        public List<Munro> MunroData { get; set; }

       

        public string ParseCSVFile(string pathToTheFile)
        {
            string error = "";
            try
            {
                HeaderList = new List<string>();
                DataList = new List<string[]>();

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
            catch (Exception ex)
            {
                Debug.WriteLine("Exception:" + ex.Message);
                Debug.WriteLine("Exception:" + ex.StackTrace);
                error = ex.Message;
            }
            return error;
        }

        public MunroFiltteringResult PopulateDataToObject(List<string> headerList , List<string[]> dataList)
        {
            var results = new MunroFiltteringResult();
            
            MunroData = new List<Munro>();
            var tempIndexHolder = new int[4];
            for(var i = 0; i < headerList.Count; i++)
            {
                if (headerList[i].Equals("Name"))
                    tempIndexHolder[0] = i;
                if (headerList[i].Equals("Height (m)"))
                    tempIndexHolder[1] = i;
                if (headerList[i].Equals("post 1997"))
                    tempIndexHolder[2] = i;
                if (headerList[i].Equals("Grid Ref"))
                    tempIndexHolder[3] = i;
            }

            try
            {

                foreach (var element in dataList)
                {
                    var munroObject = new Munro()
                    {
                        Name = element[tempIndexHolder[0]],
                        Height = Convert.ToInt32(element[tempIndexHolder[1]]),
                        Category = element[tempIndexHolder[2]],
                        GridReference = element[tempIndexHolder[3]]
                    };

                    MunroData.Add(munroObject);
                }

                results.MunroList = MunroData;
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Convert Exception:" + ex.Message);
                Debug.WriteLine("Convert Exception:" + ex.StackTrace);
                results.Error = ex.Message;
            }

            return results;
        }

    }
}
