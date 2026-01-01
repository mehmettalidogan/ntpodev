using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    public class SharpenFilter : IImageFilter
    {
        public string Name { get { return "Sharpen Filter"; } }
        
        public Image Apply(Image input)
        {
            Image output = new Image(input.Width, input.Height);
            
            // Sharpen kernel
            int[,] kernel = {
                {  0, -1,  0 },
                { -1,  5, -1 },
                {  0, -1,  0 }
            };
            
            for (int y = 0; y < input.Height; y++)
            {
                for (int x = 0; x < input.Width; x++)
                {
                    int sum = 0;
                    
                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            int ny = y + ky;
                            int nx = x + kx;
                            
                            if (ny >= 0 && ny < input.Height && nx >= 0 && nx < input.Width)
                            {
                                sum += input.GetPixel(ny, nx) * kernel[ky + 1, kx + 1];
                            }
                        }
                    }
                    
                    output.SetPixel(y, x, sum);
                }
            }
            
            return output;
        }
    }
}


