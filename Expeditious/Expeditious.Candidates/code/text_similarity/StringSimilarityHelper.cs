using Expeditious.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expeditious.Candidates.code.text_similarity
{
    public class StringSimilarityHelper
    {

        static public (Int32 steps, Double percent, Int32 percentInt) WordsSimilarityLevenshtein(String sourceWord, String targetWord)
        {
            //if (oprimizedWord1 == null & oprimizedWord2 == null) return (0, 100);
            //if (oprimizedWord1 == null & oprimizedWord2 != null) return (0, 100);
            //if (oprimizedWord1 != null & oprimizedWord2 == null) return (0, 100);

            String oprimizedSourceWord = String.IsNullOrWhiteSpace(sourceWord) ? "" : TextNormalizer.TextCase.ToLowerAz(sourceWord).Trim();
            String oprimizedTargetWord = String.IsNullOrWhiteSpace(targetWord) ? "" : TextNormalizer.TextCase.ToLowerAz(targetWord).Trim();

            Int32 steps = GetDamerauLevenshteinDistance(oprimizedSourceWord, oprimizedTargetWord);
            Double percent = (1.0 - ((double)steps / (double)Math.Max(oprimizedSourceWord.Length, oprimizedTargetWord.Length)));
            Int32 percentInt = Convert.ToInt32(percent * 100);
            return (steps, percent, percentInt);
        }





        // (FrankSchwieterman) - Damerau–Levenshtein distance
        public static int GetDamerauLevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentNullException(s, "String Cannot Be Null Or Empty");
            }

            if (string.IsNullOrEmpty(t))
            {
                throw new ArgumentNullException(t, "String Cannot Be Null Or Empty");
            }

            int n = s.Length; // length of s
            int m = t.Length; // length of t

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            int[] p = new int[n + 1]; //'previous' cost array, horizontally
            int[] d = new int[n + 1]; // cost array, horizontally

            // indexes into strings s and t
            int i; // iterates through s
            int j; // iterates through t

            for (i = 0; i <= n; i++)
            {
                p[i] = i;
            }

            for (j = 1; j <= m; j++)
            {
                char tJ = t[j - 1]; // jth character of t
                d[0] = j;

                for (i = 1; i <= n; i++)
                {
                    int cost = s[i - 1] == tJ ? 0 : 1; // cost
                                                       // minimum of cell to the left+1, to the top+1, diagonally left and up +cost                
                    d[i] = Math.Min(Math.Min(d[i - 1] + 1, p[i] + 1), p[i - 1] + cost);
                }

                // copy current distance counts to 'previous row' distance counts
                int[] dPlaceholder = p; //placeholder to assist in swapping p and d
                p = d;
                d = dPlaceholder;
            }

            // our last action in the above loop was to switch d and p, so p now 
            // actually has the most recent cost counts
            return p[n];
        }




        /// <summary>
        /// Returns the number of steps required to transform the source string into the target string. Damerau–Levenshtein distance.
        /// </summary>
        public static int ComputeLevenshteinDistance(String source, String target)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(target) ? 0 : target.Length;

            if (string.IsNullOrEmpty(target))
                return string.IsNullOrEmpty(source) ? 0 : source.Length;

            int sourceLength = source.Length;
            int targetLength = target.Length;

            int[,] distance = new int[sourceLength + 1, targetLength + 1];

            // Step 1
            for (int i = 0; i <= sourceLength; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetLength; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceLength; i++)
            {
                for (int j = 1; j <= targetLength; j++)
                {
                    // Step 2
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 3
                    distance[i, j] = Math.Min(
                                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceLength, targetLength];
        }



        /// <summary> 
        /// Calculate percentage similarity of two strings
        /// <param name="source">Source String to Compare with</param>
        /// <param name="target">Targeted String to Compare</param>
        /// <returns>Return Similarity between two strings from 0 to 1.0</returns>
        /// </summary>
        public static double CalculateSimilarity(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(target) ? 1 : 0;

            if (string.IsNullOrEmpty(target))
                return string.IsNullOrEmpty(source) ? 1 : 0;

            double stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - (stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }
    }
}
