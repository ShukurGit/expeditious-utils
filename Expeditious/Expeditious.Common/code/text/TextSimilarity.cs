using System;
using System.Collections.Generic;
using System.Text;

namespace Expeditious.Common
{
    public static class TextSimilarity
    {
        public static (int steps, double percent, int percentInt) WordsSimilarityLevenshtein( string? sourceWord, string? targetWord )
        {
            string source = string.IsNullOrWhiteSpace(sourceWord)
                ? string.Empty
                : TextNormalizer.TextCase.ToLowerAz(sourceWord)!.Trim();

            string target = string.IsNullOrWhiteSpace(targetWord)
                ? string.Empty
                : TextNormalizer.TextCase.ToLowerAz(targetWord)!.Trim();

            int steps = ComputeLevenshteinDistance(source, target);

            int maxLength = Math.Max(source.Length, target.Length);

            double percent = maxLength == 0
                ? 1.0
                : 1.0 - ((double)steps / maxLength);

            int percentInt = (int)Math.Round(percent * 100);

            return (steps, percent, percentInt);
        }



        public static int ComputeLevenshteinDistance(string source, string target)
        {
            source ??= string.Empty;
            target ??= string.Empty;

            if (source.Length == 0)
                return target.Length;

            if (target.Length == 0)
                return source.Length;

            int sourceLength = source.Length;
            int targetLength = target.Length;

            int[,] distance = new int[sourceLength + 1, targetLength + 1];

            for (int i = 0; i <= sourceLength; i++)
                distance[i, 0] = i;

            for (int j = 0; j <= targetLength; j++)
                distance[0, j] = j;

            for (int i = 1; i <= sourceLength; i++)
            {
                for (int j = 1; j <= targetLength; j++)
                {
                    int cost = source[i - 1] == target[j - 1] ? 0 : 1;

                    distance[i, j] = Math.Min(
                        Math.Min(
                            distance[i - 1, j] + 1,
                            distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceLength, targetLength];
        }
    }
}
