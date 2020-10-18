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
            int left = 0;
            int right = heights.Length - 1;
            int ans = 0;
            int leftMax = 0;
            int rightMax = 0;

            List<int> tmpList = heights.ToList();

            // let's throw an exception if:
            // the height or length exceeds 32000 OR there are heights less than 0
            //var badInts = (from o in tmpList where o > 32000 select o).ToList();
            var query = tmpList.Where(c => c > 32000 || c < 0);
            if (query.Any() || heights.Length > 32000)
            {
                throw new IndexOutOfRangeException();
            }

            while (left < right)
            {
                if (heights[left] < heights[right])
                {
                    //if height left < left max that means we can trap water
                    if (heights[left] < leftMax)
                    {
                        ans += leftMax - heights[left];
                    }
                    else // otherwise left max needs to be updated
                    {
                        leftMax = heights[left];
                    }
                    left++;
                }
                else
                {
                    if (heights[right] < rightMax)
                    {
                        ans += rightMax - heights[right];
                    }
                    else
                    {
                        rightMax = heights[right];
                    }

                    right--;
                }
            }
            return ans;
        }
    }
}

