using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yeni.YeniCore
{
    public class YeniVariantsCore
    {

    }



    public class PermutationsOfInteger
    {
        static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }


        static IList<IList<int>> Permute(int[] nums)
        {
            var list = new List<IList<int>>();
            return DoPermute(nums, 0, nums.Length - 1, list);
        }


        static IList<IList<int>> DoPermute(int[] nums, int start, int end, IList<IList<int>> list)
        {
            if (start == end)
            {
                // We have one of our possible n! solutions,
                // add it to the list.
                list.Add(new List<int>(nums));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    Swap(ref nums[start], ref nums[i]);
                    DoPermute(nums, start + 1, end, list);
                    Swap(ref nums[start], ref nums[i]);
                }
            }

            return list;
        }


        static void PrintResult(IList<IList<int>> lists)
        {
            Console.WriteLine("[");
            foreach (var list in lists)
            {
                Console.WriteLine($"    [{String.Join(",", list)}]");
            }
            Console.WriteLine("]");
        }


        static void Run(String[] args)
        {
            PrintResult(
                Permute(new int[] { 1, 2, 3 })
            );
        }
    }




    public class GFG_A
    {
        /**
        * permutation function
        * @param str string to
        calculate permutation for
        * @param l starting index
        * @param r end index
        */
        private static void permute(String str, Int32 l, Int32 r)
        {
            if (l == r)
                Console.WriteLine(str);
            else
            {
                for (int i = l; i <= r; i++)
                {
                    str = swap(str, l, i);
                    permute(str, l + 1, r);
                    str = swap(str, l, i);
                }
            }
        }

        /**
         * Swap Characters at position
         * @param a string value
         * @param i position 1
         * @param j position 2
         * @return swapped string
         */
        public static String swap(String a, int i, int j)
        {
            char temp;
            char[] charArray = a.ToCharArray();
            temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            string s = new string(charArray);
            return s;
        }

        // Driver Code
        public static void Run()
        {
            String str = "1234";
            Int32 n = str.Length;
            permute(str, 0, n - 1);
        }
    }

    // This code is contributed by mits



    // https://www.geeksforgeeks.org/make-combinations-size-k/
    // https://www.geeksforgeeks.org/find-all-combinations-that-adds-upto-given-number-2/

    // C# program to print all combinations of size k of elements in set 1..n
    public class GFG
    {

        static List<List<Int32>> ans = new List<List<Int32>>();
        static List<Int32> tmp = new List<Int32>();

        static void MakeCombiUtil(Int32 n, Int32 left, Int32 k)
        {

            // Pushing this vector to a vector of vector
            if (k == 0)
            {
                ans.Add(tmp);
                for (int i = 0; i < tmp.Count; i++)
                {
                    Console.Write(tmp[i] + " ");
                }
                Console.WriteLine();
                return;
            }

            // i iterates from left to n. First time
            // left will be 1
            for (int i = left; i <= n; ++i)
            {
                tmp.Add(i);
                MakeCombiUtil(n, i + 1, k - 1);

                // Popping out last inserted element
                // from the vector
                tmp.RemoveAt(tmp.Count - 1);
            }
        }

        // Prints all combinations of size k of numbers
        // from 1 to n.
        static List<List<int>> makeCombi(int n, int k)
        {
            MakeCombiUtil(n, 1, k);
            return ans;
        }

        static public void Run()
        {

            // given number
            int n = 5;
            int k = 3;
            ans = makeCombi(n, k);

            int re = 90;
        }
    }

    // This code is contributed by rameshtravel07.



}


