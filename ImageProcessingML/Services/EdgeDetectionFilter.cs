using System;
using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    public class EdgeDetectionFilter : IImageFilter
    {
        public string Name { get { return "Edge Detection Filter"; } }
        
        public Image Apply(Image input)
        {
            Image output = new Image(input.Width, input.Height);
            
            int[,] sobelX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] sobelY = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
            
            for (int y = 1; y < input.Height - 1; y++)
            {
                for (int x = 1; x < input.Width - 1; x++)
                {
                    int gx = 0, gy = 0;
                    
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            int pixel = input.GetPixel(y + dy, x + dx);
                            gx += pixel * sobelX[dy + 1, dx + 1];
                            gy += pixel * sobelY[dy + 1, dx + 1];
                        }
                    }
                    
                    int magnitude = (int)Math.Sqrt(gx * gx + gy * gy);
                    output.SetPixel(y, x, magnitude);
                }
            }
            
            return output;
        }
    }
}

