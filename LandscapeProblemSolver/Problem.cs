using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace LandscapeProblemSolver
{
    public class Problem
    {
        public static int Solve(int[] heights)
        {
            int maxHeight = 0;
            int previousHeight = 0;
            int previousHeightIndex = 0;
            int coll = 0;
            int temp = 0;
            List<int> tmpList = heights.ToList();

            // let's throw an exception if the height or length exceeds 32000
            var badInts = (from o in tmpList where o > 32000 select o).ToList();
            if (badInts.Any() || heights.Length > 32000)
            {
                throw new IndexOutOfRangeException();
            }

            // find the first peak (all water before will not be collected)
            while (heights[previousHeightIndex] > maxHeight)
            {
                maxHeight = heights[previousHeightIndex];
                previousHeightIndex++;
                // in case of stairs (no water collected)
                if (previousHeightIndex == heights.Length)            
                    return coll;
                else
                    previousHeight = heights[previousHeightIndex];
            }

            for (int i = previousHeightIndex; i < heights.Length; i++)
            {
                if (heights[i] >= maxHeight)
                {      // collect all temp water
                    coll += temp;
                    temp = 0;
                    maxHeight = heights[i];        // new max height
                }
                else
                {
                    temp += maxHeight - heights[i];
                    if (heights[i] > previousHeight)
                    {  // we went up... collect some water
                        int collWater = (i - previousHeightIndex) * (heights[i] - previousHeight);
                        coll += collWater;
                        temp -= collWater;
                    }
                }

                // previousHeight only changes if consecutive towers are not same height
                if (heights[i] != previousHeight)
                {
                    previousHeight = heights[i];
                    previousHeightIndex = i;
                }
            }
            return coll;
        }
    }
}
