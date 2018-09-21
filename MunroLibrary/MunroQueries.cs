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

    }
}
