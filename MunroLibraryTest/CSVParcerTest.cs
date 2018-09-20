using MunroLibrary;
using System;
using System.IO;
using Xunit;

namespace MunroLibraryTest
{
    public class CSVParcerTest
    {
        [Fact]
        public void ParcingCorrectCSV()
        {
            //Arrange
            string fileName = "test.csv";
            string path = Path.Combine(Environment.CurrentDirectory, @"Resources\", fileName);

            //Act 
            var TestParcer = new CSVParcer(path);

            //Assert
            Assert.NotNull(TestParcer.HeaderList);
            Assert.NotNull(TestParcer.DataList);
            Assert.Equal("Running No", TestParcer.HeaderList[0]);
            Assert.Equal("Comments", TestParcer.HeaderList[TestParcer.HeaderList.Count - 1]);
            Assert.Equal("1", TestParcer.DataList[0][0]);
            Assert.Equal("30", TestParcer.DataList[10][1]);
        }
    }
}
