using MunroLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace MunroLibraryTest
{
    public class QueriesTest
    {
        public CSVParcer TestParcer { get; set; }

        private void  InitParcer()
        {
            string fileName = "test.csv";
            string path = Path.Combine(Environment.CurrentDirectory, @"Resources\", fileName);
            TestParcer = new CSVParcer(path);
        }

        [Fact]
        public void SortByCategory_Munro()
        {
            //Arrange
            InitParcer();

            //Act 
            var query = new MunroQueries();
            var result = query.SortByCategory(MunroLibrary.Enums.HillCategory.Munro);

            var haveOnlyMunro = 0;
            foreach (var element in result)
            {
                if (!element[element.Length - 2].Equals("MUN"))
                {
                    haveOnlyMunro++;
                }
            }


            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, haveOnlyMunro);


        }

        [Fact]
        public void SortByCategory_MunroTop()
        {
            //Arrange
            InitParcer();

            //Act 
            var query = new MunroQueries();
            var result = query.SortByCategory(MunroLibrary.Enums.HillCategory.MunroTop);

            var haveOnlyMunroTop = 0;
            foreach (var element in result)
            {
                if (!element[element.Length - 2].Equals("TOP"))
                {
                    haveOnlyMunroTop++;
                }
            }


            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, haveOnlyMunroTop);
        }

        [Fact]
        public void SortByCategory_Ether()
        {
            //Arrange
            InitParcer();

            //Act 
            var query = new MunroQueries();
            var result = query.SortByCategory(MunroLibrary.Enums.HillCategory.MunroTop);

            var haveEmptyField = 0;
            foreach (var element in result)
            {
                if (element[element.Length - 2].Equals(""))
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
