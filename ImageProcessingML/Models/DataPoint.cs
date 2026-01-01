namespace ImageProcessingML.Models
{
    public class DataPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        
        public DataPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    
    public class Point2D
    {
        public double X { get; set; }
        public double Y { get; set; }
        
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        
        public double DistanceTo(Point2D other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            return System.Math.Sqrt(dx * dx + dy * dy);
        }
    }
}

