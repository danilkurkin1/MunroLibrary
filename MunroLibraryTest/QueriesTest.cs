using MunroLibrary;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using MunroLibrary.Model;
using Xunit;

namespace MunroLibraryTest
{
    public class QueriesTest
    {
        public CSVParcer TestParcer { get; set; }
        List<string> TestHeaderList { get; set; }
        List<string[]> TestDataList { get; set; }
        List<string[]> TestDataList2 { get; set; }

        private List<Munro> TestMunroData { get;set;}
        private List<Munro> TestMunroData2 { get; set; }
        private static List<Munro> emptyList { get; set; }

        private void  InitParcer()
        {
            string fileName = "test.csv";
            string path = Path.Combine(Environment.CurrentDirectory, @"Resources\", fileName);
            TestParcer = new CSVParcer();
            TestHeaderList = new List<string>() { "Name", "Height (m)", "post 1997", "Grid Ref" };
            TestDataList = new List<string[]>()
            {
                new string[] {"A", "66", "TOP", "NN"},
                new string[] {"B", "91", "MUN", "NM"},
                new string[] {"C", "34", "", "NO"}
            };

            TestDataList2 = new List<string[]>()
            {
                new string[] {"A", "66", "TOP", "NN"},
                new string[] {"A", "36", "TOP", "NN"},
                new string[] {"A", "68", "TOP", "NN"},
                new string[] {"B", "91", "MUN", "NM"},
                new string[] {"C", "34", "", "NO"},
                new string[] {"D", "91", "MUN", "NM"},
                new string[] {"E", "91", "MUN", "NM"},
            };


            TestMunroData = TestParcer.PopulateDataToObject(TestHeaderList, TestDataList);
            TestMunroData2 = TestParcer.PopulateDataToObject(TestHeaderList, TestDataList2);

            emptyList = new List<Munro>();
        }

        [Fact]
        public void DataIsNullOrEmpty()
        {
            //Arrange
            InitParcer();

            //Act 
            var query = new MunroQueries();
            var result = query.SortByCategory(null, MunroLibrary.Enums.HillCategory.Munro);
            //Assert
            Assert.Null(result);
           
        }

        [Fact]
        public void DataIsEmpty()
        {
            //Arrange
            InitParcer();

            //Act 
            var query = new MunroQueries();
            var result = query.SortByCategory(new List<Munro>(), MunroLibrary.Enums.HillCategory.Munro);
            //Assert
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void SortByCategory_Munro()
        {
            //Arrange
            InitParcer();
           
            //Act 
            var query = new MunroQueries();
            var result = query.SortByCategory(TestMunroData, MunroLibrary.Enums.HillCategory.Munro);

            var haveOtherCategories = 0;
            foreach (var element in result.MunroList)
            {
                if (!element.Category.Equals("MUN"))
                {
                    haveOtherCategories++;
                }
            }

            //Assert
            Assert.NotNull(result.MunroList);
            Assert.Equal("MUN", result.MunroList[0].Category);
            Assert.Equal(0, haveOtherCategories);
        }

        [Fact]
        public void SortByCategory_MunroTop()
        {
            //Arrange
            InitParcer();

            //Act 
            var query = new MunroQueries();
            var result = query.SortByCategory(TestMunroData, MunroLibrary.Enums.HillCategory.MunroTop);

            var haveOtherCategories = 0;
            foreach (var element in result.MunroList)
            {
               
                if (!element.Category.Equals("TOP"))
                {
                    haveOtherCategories++;
                }
            }

            //Assert
            Assert.NotNull(result.MunroList);
            Assert.Equal(0, haveOtherCategories);
        }

        [Fact]
        public void SortByCategory_Ether()
        {
            //Arrange
            InitParcer();

            //Act 
            var query = new MunroQueries();
            var result = query.SortByCategory(TestMunroData, MunroLibrary.Enums.HillCategory.Ether);

            var haveEmptyField = 0;
            foreach (var element in result.MunroList)
            {
                if (element.Category.Length == 0)
                {
                    haveEmptyField++;
                }
            }

            //Assert
            Assert.NotNull(result.MunroList);
            Assert.Equal(0, haveEmptyField);
        }


        [Fact]
        public void DataIsNull()
        {
            //Arrange
            InitParcer();

            //Act 
            var query = new MunroQueries();
            var result = query.SortByHeighAndAlphabet(null, true, false, true);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("list of Data is null or Empty", result.Error);
        }


        [Fact]
        public void FilterHeightAsceding()
        {
            //Arrange
            InitParcer();

            //Act 
            var query = new MunroQueries();
            var result = query.SortByHeighAndAlphabet(TestMunroData, true, false, true);
            
            //Assert
            Assert.NotNull(result);
            Assert.Equal(34, result.MunroList[0].Height);
            Assert.Equal(91, result.MunroList[2].Height);
        }

        [Fact]
        public void FilterHeightDesc()
        {
            //Arrange
            InitParcer();

            //Act 
            var query = new MunroQueries();
            var result = query.SortByHeighAndAlphabet(TestMunroData, true, false, false);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(91, result.MunroList[0].Height);
            Assert.Equal(34, result.MunroList[2].Height);
        }

        [Fact]
        public void FilterAlphabe()
        {
            //Arrange
            InitParcer();

            //Act 
            var query = new MunroQueries();
            var result = query.SortByHeighAndAlphabet(TestMunroData, false, true, true);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("A", result.MunroList[0].Name);
            Assert.Equal("C", result.MunroList[2].Name);
        }

        [Fact]
        public void FilterAlphabeDesc()
        {
            //Arrange
            InitParcer();

            //Act 
            var query = new MunroQueries();
            var result = query.SortByHeighAndAlphabet(TestMunroData, false, true, false);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("C", result.MunroList[0].Name);
            Assert.Equal("A", result.MunroList[2].Name);
        }


        [Fact]
        public void FilterBoth()
        {
            //Arrange
            InitParcer();
           

            //Act 
            var query = new MunroQueries();
            var result = query.SortByHeighAndAlphabet(TestMunroData2, true, true, true);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("A", result.MunroList[0].Name);
            Assert.Equal("A", result.MunroList[1].Name);
            Assert.Equal("A", result.MunroList[2].Name);
            Assert.Equal(36, result.MunroList[0].Height);
            Assert.Equal(66, result.MunroList[1].Height);
            Assert.Equal(68, result.MunroList[2].Height);
          
        }

        [Fact]
        public void GetLimitResults2()
        {
            //Arrange
            InitParcer();
            var numberOfResults = 2;

            //Act 
            var query = new MunroQueries();
            var result = query.SortByHeighAndAlphabet(TestMunroData2, true, true, true, numberOfResults);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(numberOfResults, result.MunroList.Count);
        }

        [Fact]
        public void GetLimitResultsNotSet()
        {
            //Arrange
            InitParcer();
            var numberOfResults = TestMunroData2.Count;

            //Act 
            var query = new MunroQueries();
            var result = query.SortByHeighAndAlphabet(TestMunroData2, true, true, true);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(numberOfResults, result.MunroList.Count);
        }


        [Fact]
        public void GetResultsBetweenMaxAndMin()
        {
            //Arrange
            InitParcer();
            var numberOfResults = 4;

            //Act 
            var query = new MunroQueries();
            var result = query.SortByHeighAndAlphabet(TestMunroData2, false, false, false, 0, 30, 90);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(numberOfResults, result.MunroList.Count);
        }

        [Fact]
        public void GetResultsExcludingMin()
        {
            //Arrange
            InitParcer();
            var numberOfResults = 5;

            //Act 
            var query = new MunroQueries();
            var result = query.SortByHeighAndAlphabet(TestMunroData2, false, false, false, 0, 40);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(numberOfResults, result.MunroList.Count);
        }

        [Fact]
        public void GetResultsExcludingMax()
        {
            //Arrange
            InitParcer();
            var numberOfResults = 4;

            //Act 
            var query = new MunroQueries();
            var result = query.SortByHeighAndAlphabet(TestMunroData2, false, false, false, 0, 0, 90);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(numberOfResults, result.MunroList.Count);
        }

        [Fact]
        public void GetResultsMinBiggerMax()
        {
            //Arrange
            InitParcer();
            var numberOfResults = 4;

            //Act 
            var query = new MunroQueries();
            var result = query.SortByHeighAndAlphabet(TestMunroData2, false, false, false, 0, 60, 30);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Minimum cannot be larger than maximum", result.Error);
        }


    }
}
