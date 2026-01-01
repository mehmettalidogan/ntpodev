using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    public class SepiaFilter : IImageFilter
    {
        public string Name { get { return "Sepia Filter"; } }
        
        public Image Apply(Image input)
        {
            Image output = new Image(input.Width, input.Height);
            
            for (int y = 0; y < input.Height; y++)
            {
                for (int x = 0; x < input.Width; x++)
                {
                    int pixel = input.GetPixel(y, x);
                    
                    // Sepia tone formülü
                    int sepia = (int)(pixel * 0.9);
                    
                    output.SetPixel(y, x, sepia);
                }
            }
            
            return output;
        }
    }
}


