using System;
using System.Collections.Generic;
using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    public class LinearRegression
    {
        public double Slope { get; private set; }
        public double Intercept { get; private set; }
        
        public void Train(List<DataPoint> data)
        {
            if (data == null || data.Count < 2)
                throw new ArgumentException("En az 2 veri noktası gerekli.");
                
            int n = data.Count;
            double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;
            
            foreach (var point in data)
            {
                sumX += point.X;
                sumY += point.Y;
                sumXY += point.X * point.Y;
                sumX2 += point.X * point.X;
            }
            
            Slope = (n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX);
            Intercept = (sumY - Slope * sumX) / n;
        }
        
        public double Predict(double x)
        {
            return Slope * x + Intercept;
        }
    }
}

