using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] original, double[,] sx)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var filteredPixels = new double[width, height];

            var offsetX = sx.GetLength(0) / 2;
            var offsetY = sx.GetLength(1) / 2;

            var sy = GetTransposedMatrix(sx);

            for (var x = offsetX; x < width - offsetX; x++)
            {
                for (var y = offsetY; y < height - offsetY; y++)
                {
                    var gx = GetConvolution(original, sx, x, y, offsetX);
                    var gy = GetConvolution(original, sy, x, y, offsetY);

                    filteredPixels[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            }

            return filteredPixels;
        }

        private static double[,] GetTransposedMatrix(double[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            var transposedMatrix = new double[width, height];

            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    transposedMatrix[x, y] = matrix[y, x];

            return transposedMatrix;
        }

        private static double GetConvolution(double[,] original, double[,] s, int x, int y, int offset)
        {
            var width = s.GetLength(0);
            var height = s.GetLength(1);
            var result = 0.0;

            for (var sx = 0; sx < width; sx++)
                for (var sy = 0; sy < height; sy++)
                    result += s[sx, sy] * original[x + sx - offset, y + sy - offset];

            return result;
        }
    }
}