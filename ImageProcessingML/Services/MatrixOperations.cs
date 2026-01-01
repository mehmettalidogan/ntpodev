using System;
using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    public class MatrixOperations
    {
        public Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.Columns != b.Rows)
                throw new ArgumentException("Matrix boyutları çarpım için uygun değil.");
                
            Matrix result = new Matrix(a.Rows, b.Columns);
            
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < b.Columns; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < a.Columns; k++)
                    {
                        sum += a.GetValue(i, k) * b.GetValue(k, j);
                    }
                    result.SetValue(i, j, sum);
                }
            }
            
            return result;
        }
        
        public Matrix Transpose(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Columns, matrix.Rows);
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result.SetValue(j, i, matrix.GetValue(i, j));
                }
            }
            
            return result;
        }
    }
}

