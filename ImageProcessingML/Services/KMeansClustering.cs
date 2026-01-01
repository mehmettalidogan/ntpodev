using System;
using System.Collections.Generic;
using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    public class KMeansClustering
    {
        private Point2D[] centroids;
        private int maxIterations;
        
        public int K { get; private set; }
        
        public KMeansClustering(int k, int maxIterations = 100)
        {
            if (k <= 0)
                throw new ArgumentException("K pozitif olmalıdır.");
                
            K = k;
            this.maxIterations = maxIterations;
            centroids = new Point2D[k];
        }
        
        public void Fit(List<Point2D> points)
        {
            if (points == null || points.Count < K)
                throw new ArgumentException("En az " + K + " veri noktası gerekli.");
                
            Random rand = new Random();
            for (int i = 0; i < K; i++)
            {
                var point = points[rand.Next(points.Count)];
                centroids[i] = new Point2D(point.X, point.Y);
            }
            
            for (int iter = 0; iter < maxIterations; iter++)
            {
                List<List<Point2D>> clusters = new List<List<Point2D>>();
                for (int i = 0; i < K; i++)
                    clusters.Add(new List<Point2D>());
                    
                foreach (var point in points)
                {
                    int nearestCluster = FindNearestCentroid(point);
                    clusters[nearestCluster].Add(point);
                }
                
                bool changed = false;
                for (int i = 0; i < K; i++)
                {
                    if (clusters[i].Count > 0)
                    {
                        Point2D newCentroid = CalculateMean(clusters[i]);
                        
                        if (Math.Abs(newCentroid.X - centroids[i].X) > 0.001 ||
                            Math.Abs(newCentroid.Y - centroids[i].Y) > 0.001)
                        {
                            changed = true;
                            centroids[i] = newCentroid;
                        }
                    }
                }
                
                if (!changed)
                    break;
            }
        }
        
        public int Predict(Point2D point)
        {
            return FindNearestCentroid(point);
        }
        
        public Point2D GetClusterCenter(int clusterIndex)
        {
            if (clusterIndex < 0 || clusterIndex >= K)
                throw new ArgumentException("Geçersiz küme indeksi.");
                
            return centroids[clusterIndex];
        }
        
        private int FindNearestCentroid(Point2D point)
        {
            int nearest = 0;
            double minDistance = point.DistanceTo(centroids[0]);
            
            for (int i = 1; i < K; i++)
            {
                double distance = point.DistanceTo(centroids[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = i;
                }
            }
            
            return nearest;
        }
        
        private Point2D CalculateMean(List<Point2D> points)
        {
            double sumX = 0, sumY = 0;
            
            foreach (var point in points)
            {
                sumX += point.X;
                sumY += point.Y;
            }
            
            return new Point2D(sumX / points.Count, sumY / points.Count);
        }
    }
}

