using ImageProcessingML.Models;

namespace ImageProcessingML.Services
{
    /// <summary>
    /// IImageFilter interface - Polymorphism prensibi
    /// </summary>
    public interface IImageFilter
    {
        Image Apply(Image input);
        string Name { get; }
    }
}

