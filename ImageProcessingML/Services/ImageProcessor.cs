using System;
using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    public class ImageProcessor
    {
        public Image Normalize(Image input)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            
            for (int y = 0; y < input.Height; y++)
            {
                for (int x = 0; x < input.Width; x++)
                {
                    int pixel = input.GetPixel(y, x);
                    min = Math.Min(min, pixel);
                    max = Math.Max(max, pixel);
                }
            }
            
            Image output = new Image(input.Width, input.Height);
            double range = max - min;
            
            if (range > 0)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    for (int x = 0; x < input.Width; x++)
                    {
                        int pixel = input.GetPixel(y, x);
                        int normalized = (int)((pixel - min) * 255.0 / range);
                        output.SetPixel(y, x, normalized);
                    }
                }
            }
            
            return output;
        }
    }
}

