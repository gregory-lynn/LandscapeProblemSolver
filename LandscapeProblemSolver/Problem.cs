using System;
using System.Collections.Generic;
using System.Linq;

namespace LandscapeProblemSolver
{
    public class Problem
    {
        public static int Solve(int[] heights)
        {
            List<int> tmpList = heights.ToList();
            int result = 0;
            int leftSideMax = 0;
            int rightSideMax = 0;
            int lefthandside = 0;
            int righthandside = heights.Length - 1;
            // let's throw an exception if:
            // the height or length exceeds 32000 OR there are heights less than 0
            var query = tmpList.Where(c => c > 32000 || c < 0);
            if (query.Any() || heights.Length > 32000)
            {
                throw new IndexOutOfRangeException();
            }
            while (lefthandside < righthandside)
            {
                if (heights[lefthandside] < heights[righthandside])
                {
                    //if height left < left side max that means we can trap water
                    if (heights[lefthandside] < leftSideMax)
                    {
                        result += leftSideMax - heights[lefthandside];
                    }
                    else // otherwise left side max needs to be updated
                    {
                        leftSideMax = heights[lefthandside];
                    }
                    lefthandside++;
                }
                else
                {
                    if (heights[righthandside] < rightSideMax)
                    {
                        result += rightSideMax - heights[righthandside];
                    }
                    else
                    {
                        rightSideMax = heights[righthandside];
                    }

                    righthandside--;
                }
            }
            return result;
        }
    }
}
