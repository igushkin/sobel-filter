using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        /* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
        public static double[,] MedianFilter(double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var answer = new double[width, height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    answer[i, j] = GetValue(original, i, j);
            return answer;
        }

        public static double GetValue(double[,] original, int i, int j)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var left = i - 1 < 0 ? i : i - 1;
            var top = j - 1 < 0 ? j : j - 1;
            var list = new List<double>();
            for (var l = left; l <= i + 1 && l < width; l++)
                for (var t = top; t <= j + 1 && t < height; t++)
                    list.Add(original[l, t]);
            list.Sort();
            if (list.Count % 2 != 0)
                return list[list.Count / 2];
            else
                return (list[list.Count / 2 - 1] + list[list.Count / 2]) / 2;
        }

    }
}