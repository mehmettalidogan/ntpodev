using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    public class BlurFilter : IImageFilter
    {
        public string Name { get { return "Blur Filter"; } }
        
        public Image Apply(Image input)
        {
            Image output = new Image(input.Width, input.Height);
            
            for (int y = 0; y < input.Height; y++)
            {
                for (int x = 0; x < input.Width; x++)
                {
                    int sum = 0;
                    int count = 0;
                    
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            int ny = y + dy;
                            int nx = x + dx;
                            
                            if (ny >= 0 && ny < input.Height && nx >= 0 && nx < input.Width)
                            {
                                sum += input.GetPixel(ny, nx);
                                count++;
                            }
                        }
                    }
                    
                    output.SetPixel(y, x, sum / count);
                }
            }
            
            return output;
        }
    }
}

