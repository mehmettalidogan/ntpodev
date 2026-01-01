using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    public class InvertFilter : IImageFilter
    {
        public string Name { get { return "Invert Filter"; } }
        
        public Image Apply(Image input)
        {
            Image output = new Image(input.Width, input.Height);
            
            for (int y = 0; y < input.Height; y++)
            {
                for (int x = 0; x < input.Width; x++)
                {
                    int pixel = input.GetPixel(y, x);
                    output.SetPixel(y, x, 255 - pixel);
                }
            }
            
            return output;
        }
    }
}


