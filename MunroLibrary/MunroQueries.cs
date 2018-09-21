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
        public List<Munro> SortByCategory(List<Munro> munroData, HillCategory hillCategory)
        {
            if (munroData?.Count > 0)
            {

                switch (hillCategory)
                {
                    case HillCategory.Munro:
                    {
                        return munroData.Where(element => element.Category.Equals("MUN")).ToList();
                        break;
                    }
                    case HillCategory.MunroTop:
                    {
                        return munroData.Where(element => element.Category.Equals("TOP")).ToList();
                        break;
                    }
                    default:
                    {
                        return munroData.Where(element => element.Category.Length > 0).ToList();
                        break;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public List<Munro> SortByHeighAndAlphabet(List<Munro> munroData, bool height , bool alphabeth, bool ascending)
        {
            if (munroData != null && munroData.Count > 0)
            {
                //query to sort my height
                var returnResult = new List<Munro>();

              
                if (height && ascending)
                    returnResult = munroData.OrderBy(munro => munro.Height).ToList(); 

                if (alphabeth && ascending)
                    returnResult = munroData.OrderBy(munro => munro.Name).ToList();

                if (height && !ascending)
                    returnResult = munroData.OrderByDescending(munro => munro.Height).ToList();

                if (alphabeth && !ascending)
                    returnResult = munroData.OrderByDescending(munro => munro.Name).ToList();

                if (height && alphabeth && ascending)
                {
                    returnResult = munroData.OrderBy(h => h.Name).ThenBy(h => h.Height).ToList();
                }

                if (height && alphabeth && !ascending)
                {
                    returnResult = munroData.OrderByDescending(h => h.Height).ThenBy(n => n.Name).ToList();
                }


                return returnResult;
            }
            else
            {
                return null;
            }
        }

    }
}
