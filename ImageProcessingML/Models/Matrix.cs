using System;

namespace ImageProcessingML.Models
{
    /// <summary>
    /// Matrix sınıfı - Encapsulation prensibi
    /// </summary>
    public class Matrix
    {
        private double[,] data;
        
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        
        public Matrix(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
                throw new ArgumentException("Satır ve sütun sayısı pozitif olmalıdır.");
                
            Rows = rows;
            Columns = columns;
            data = new double[rows, columns];
        }
        
        public void SetValue(int row, int col, double value)
        {
            ValidateIndices(row, col);
            data[row, col] = value;
        }
        
        public double GetValue(int row, int col)
        {
            ValidateIndices(row, col);
            return data[row, col];
        }
        
        private void ValidateIndices(int row, int col)
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
                throw new IndexOutOfRangeException("Geçersiz matrix indeksi.");
        }
        
        public Matrix Clone()
        {
            Matrix clone = new Matrix(Rows, Columns);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    clone.SetValue(i, j, data[i, j]);
                }
            }
            return clone;
        }
    }
}

