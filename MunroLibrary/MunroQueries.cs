using MunroLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MunroLibrary.Model;

namespace MunroLibrary
{
    public class MunroQueries
    {
        public MunroFiltteringResult SortByCategory(List<Munro> munroData, HillCategory hillCategory)
        {
            var result = new MunroFiltteringResult();

            if (munroData?.Count > 0)
            {

                switch (hillCategory)
                {
                    case HillCategory.Munro:
                    {
                        result.MunroList = munroData.Where(element => element.Category.Equals("MUN")).ToList();
                        break;
                    }
                    case HillCategory.MunroTop:
                    {
                        result.MunroList = munroData.Where(element => element.Category.Equals("TOP")).ToList();
                      
                        break;
                    }
                    default:
                    {
                        result.MunroList = munroData.Where(element => element.Category.Length > 0).ToList();
                        break;
                    }
                }
            }
            else
            {
                result.Error = "list of Data is null or Empty";
            }
            return result;
        }

        public MunroFiltteringResult SortByHeighAndAlphabet(List<Munro> munroData, bool height , bool alphabeth, bool ascending, int resultsNumber = 0, int min = 0 , int max = 0)
        {
            var result = new MunroFiltteringResult();

            if (munroData != null && munroData.Count > 0)
            {
                var returnResult = new List<Munro>();
                //query to sort my height
                if (min == 0 && max == 0)
                {
                    result.MunroList =  simpleQueries(munroData, height, alphabeth, ascending, resultsNumber);
                }
                else
                {
                    if (min > max && max > 0)
                    {
                        result.Error = "Minimum cannot be larger than maximum";
                        return result;
                    }
                    if (min > 0)
                        returnResult = munroData.Where(munro => munro.Height > min).ToList();
                    if (max > 0)
                        returnResult = munroData.Where(munro => munro.Height < max).ToList();
                    if (min > 0 && max > 0)
                        returnResult = munroData.Where(munro => munro.Height > min && munro.Height < max).ToList();

                    result.MunroList = simpleQueries(returnResult, height, alphabeth, ascending, resultsNumber);
                }

            }
            else
            {
                result.Error = "list of Data is null or Empty";
            }

            return result;
        }

        private List<Munro> simpleQueries(List<Munro> munroData, bool height, bool alphabeth, bool ascending, int resultsNumber = 0)
        {
            var returnResult = munroData;
            var takeResults = munroData.Count;
            if (resultsNumber != 0)
                takeResults = resultsNumber;
           
            if (height && ascending)
            {
                returnResult = munroData.OrderBy(munro => munro.Height).Take(takeResults).ToList();
            }
            if (alphabeth && ascending)
            {
                returnResult = munroData.OrderBy(munro => munro.Name).Take(takeResults).ToList();
            }
            if (height && !ascending)
            {
                returnResult = munroData.OrderByDescending(munro => munro.Height).Take(takeResults).ToList();
            }
            if (alphabeth && !ascending)
            {
                returnResult = munroData.OrderByDescending(munro => munro.Name).Take(takeResults).ToList();
            }
            if (height && alphabeth && ascending)
            {
                returnResult = munroData.OrderBy(h => h.Name).ThenBy(h => h.Height).Take(takeResults).ToList();
            }

            if (height && alphabeth && !ascending)
            {
                returnResult = munroData.OrderByDescending(h => h.Height).ThenBy(n => n.Name).Take(takeResults).ToList();
            }

            return returnResult;
        }

    }

    
}
