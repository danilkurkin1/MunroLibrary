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

        private List<Munro> TestMunroData { get;set;}
        private static List<Munro> emptyList { get; set; }

        private void  InitParcer()
        {
            string fileName = "test.csv";
            string path = Path.Combine(Environment.CurrentDirectory, @"Resources\", fileName);
            TestParcer = new CSVParcer();
            TestHeaderList = new List<string>() { "Name", "Height (m)", "post 1997", "Grid Ref" };
            TestDataList = new List<string[]>()
            {
                new string[] {"A", "10", "TOP", "NN"},
                new string[] {"B", "20", "MUN", "NM"},
                new string[] {"C", "30", "", "NO"}
            };

            TestMunroData = TestParcer.PopulateDataToObject(TestHeaderList, TestDataList);

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
    }
}
