using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
            var amountOfWhite = (int)(whitePixelsFraction * original.Length);
            var answer = new double[original.GetLength(0), original.GetLength(1)];
            var newArray = new List<double>();
            for (var l = 0; l < original.GetLength(0); l++)
                for (var h = 0; h < original.GetLength(1); h++)
                    newArray.Add(original[l, h]);
            newArray.Sort();
            newArray.Reverse();
            var condition = newArray[0];
            for (int i = 0; i < newArray.Count; i++)
            {
                if (i >= amountOfWhite && newArray[i] < condition)
                    break;
                if (newArray[i] < condition)
                    condition = newArray[i];
            }
            var white = amountOfWhite > 0 ? 1.0 : 0.0;
            for (int i = 0; i < original.GetLength(0); i++)
                for (int j = 0; j < original.GetLength(1); j++)
                    answer[i, j] = original[i, j] < condition ? 0.0 : white;
            return answer;
		}
	}
}