using MunroLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MunroLibraryTest
{
    public class CSVParcerTest
    {
        public CSVParcer TestParcer { get; set; }
        List<string> TestHeaderList { get; set; }
        List<string[]> TestDataList { get; set; }

        private void InitParcer()
        {
            TestParcer = new CSVParcer();
            TestHeaderList = new List<string>() { "Name", "Height (m)", "post 1997", "Grid Ref" };
            TestDataList = new List<string[]>()
            {
                new string[] {"A", "10", "TOP", "NN"},
                new string[] {"B", "20", "MUN", "NM"},
                new string[] {"C", "30", "", "NO"}
            };
        }

        [Fact]
        public void ParcingCorrectCSV()
        {
            //Arrange
            InitParcer();
            string fileName = "test.csv";
            string path = Path.Combine(Environment.CurrentDirectory, @"Resources\", fileName);

            //Act 
            var result = TestParcer.ParseCSVFile(path);

            //Assert
            Assert.Empty(result);
            Assert.NotNull(TestParcer.HeaderList);
            Assert.NotNull(TestParcer.DataList);
            Assert.Equal("Running No", TestParcer.HeaderList[0]);
            Assert.Equal("Comments", TestParcer.HeaderList[TestParcer.HeaderList.Count - 1]);
            Assert.Equal("1", TestParcer.DataList[0][0]);
            Assert.Equal("30", TestParcer.DataList[10][1]);
        }

        [Fact]
        public void WrongPathToCSV()
        {
            //Arrange
            InitParcer();
            string fileName = "test.csv";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);

            //Act 

            var result =  TestParcer.ParseCSVFile(path);

            //Assert
            Assert.NotEmpty(result);
            Assert.NotNull(TestParcer.HeaderList);
            Assert.NotNull(TestParcer.DataList);
            Assert.Empty(TestParcer.HeaderList);
            Assert.Empty(TestParcer.DataList);
        }

        [Fact]
        public void DataToObject()
        {
            //Arrange
            InitParcer();
            var testHeaderList = TestHeaderList;
            var testDataList = TestDataList;

            //Act 
            var result = TestParcer.PopulateDataToObject(testHeaderList, testDataList);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Error);
            Assert.Equal(3, result.MunroList.Count);
            Assert.Equal(10, result.MunroList[0].Height);
            Assert.Equal("C", result.MunroList[2].Name);
        }

        [Fact]
        public void CorruptedData()
        {
            //Arrange
            InitParcer();
            var testHeaderList = TestHeaderList;
            var testDataList = TestDataList;
            testDataList[0][1] = "string";

            //Act 
            var result = TestParcer.PopulateDataToObject(testHeaderList, testDataList);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Error);
        }


    }
}
