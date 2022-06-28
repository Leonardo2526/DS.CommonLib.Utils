using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.MainUtils
{
    public static class Matrix
    {
        /// <summary>
        /// Get 3D matrix determinant
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Return determinant value.</returns>
        public static double GetMatrixDeterminant(double[,] matrix)
        {
            double a1 = matrix[0, 0] * matrix[1, 1] * matrix[2, 2];
            double a2 = matrix[0, 0] * matrix[1, 2] * matrix[2, 1];
            double a3 = matrix[0, 1] * matrix[1, 0] * matrix[2, 2];
            double a4 = matrix[0, 1] * matrix[1, 2] * matrix[2, 0];
            double a5 = matrix[0, 2] * matrix[1, 0] * matrix[2, 1];
            double a6 = matrix[0, 2] * matrix[1, 1] * matrix[2, 0];

            return a1 - a2 - a3 + a4 + a5 - a6;
        }
    }
}
