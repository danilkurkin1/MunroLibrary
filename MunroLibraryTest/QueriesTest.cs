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
            Assert.Null(result);
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
            foreach (var element in result)
            {
                if (!element.Category.Equals("MUN"))
                {
                    haveOtherCategories++;
                }
            }

            //Assert
            Assert.NotNull(result);
            Assert.Equal("MUN", result[0].Category);
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
            foreach (var element in result)
            {
               
                if (!element.Category.Equals("TOP"))
                {
                    haveOtherCategories++;
                }
            }

            //Assert
            Assert.NotNull(result);
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
            foreach (var element in result)
            {
                if (element.Category.Length == 0)
                {
                    haveEmptyField++;
                }
            }

            //Assert
            Assert.NotNull(result);
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
            Assert.Null(result);
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
            Assert.Equal(34, result[0].Height);
            Assert.Equal(91, result[2].Height);
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
            Assert.Equal(91, result[0].Height);
            Assert.Equal(34, result[2].Height);
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
            Assert.Equal("A", result[0].Name);
            Assert.Equal("C", result[2].Name);
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
            Assert.Equal("C", result[0].Name);
            Assert.Equal("A", result[2].Name);
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
            Assert.Equal("A", result[0].Name);
            Assert.Equal("A", result[1].Name);
            Assert.Equal("A", result[2].Name);
            Assert.Equal(36, result[0].Height);
            Assert.Equal(66, result[1].Height);
            Assert.Equal(68, result[2].Height);

        }


    }
}
