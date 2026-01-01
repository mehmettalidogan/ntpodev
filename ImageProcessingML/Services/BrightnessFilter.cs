using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    public class BrightnessFilter : IImageFilter
    {
        private int adjustment;
        
        public string Name { get { return "Brightness Filter"; } }
        
        public BrightnessFilter(int adjustment)
        {
            this.adjustment = adjustment; // -255 to +255
        }
        
        public Image Apply(Image input)
        {
            Image output = new Image(input.Width, input.Height);
            
            for (int y = 0; y < input.Height; y++)
            {
                for (int x = 0; x < input.Width; x++)
                {
                    int pixel = input.GetPixel(y, x);
                    int newPixel = pixel + adjustment;
                    output.SetPixel(y, x, newPixel);
                }
            }
            
            return output;
        }
    }
}


