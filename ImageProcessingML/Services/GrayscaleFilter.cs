using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    public class GrayscaleFilter : IImageFilter
    {
        public string Name { get { return "Grayscale Filter"; } }
        
        public Image Apply(Image input)
        {
            // Zaten grayscale formatta olduğu için direkt kopyala
            return input.Clone();
        }
    }
}


