using System;
using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    public class ContrastFilter : IImageFilter
    {
        private double factor;
        
        public string Name { get { return "Contrast Filter"; } }
        
        public ContrastFilter(double factor)
        {
            this.factor = factor; // 0.5 = less contrast, 2.0 = more contrast
        }
        
        public Image Apply(Image input)
        {
            Image output = new Image(input.Width, input.Height);
            double factorAdjusted = (259.0 * (factor * 255.0 + 255.0)) / (255.0 * (259.0 - factor * 255.0));
            
            for (int y = 0; y < input.Height; y++)
            {
                for (int x = 0; x < input.Width; x++)
                {
                    int pixel = input.GetPixel(y, x);
                    int newPixel = (int)(factorAdjusted * (pixel - 128) + 128);
                    output.SetPixel(y, x, newPixel);
                }
            }
            
            return output;
        }
    }
}


