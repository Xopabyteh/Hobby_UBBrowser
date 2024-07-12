using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Hobby_UBBrowser.Contracts;

/// <summary>The actual response is serialized to Base64 String!!</summary>
/// <param name="ImageData">
/// The array works in triplets, combining the RGB values of each pixel.
/// One pixel is [i + 0], [i + 1], [i + 2] in the array.
/// The amount of pixels is Width * Height.
/// Pixels are stored in rows, from left to right, top to bottom.
/// </param>
public readonly record struct BrowserStateResponseData(
    byte[] ImageData,
    string Url)
{
    public static async Task<BrowserStateResponseData> SerializeAsync(byte[] pngBytes, int width, int height, string withUrl)
    {
        // Clamp the requested size to a maximum
        width = Math.Min(width, k_MaxWidth);
        height = Math.Min(height, k_MaxHeight);

        // Load png using image sharp
        await using var bytesStream = new MemoryStream(pngBytes);
        using var image = await Image.LoadAsync<Rgba32>(bytesStream);

        // Downscale to match the requested size
        image.Mutate(x => x.Resize(width, height));

        // Convert to raw bytes
        var imageData = new byte[width * height * 3];
        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < height; y++)
            {
                var pixelRow = accessor.GetRowSpan(y);
                for (int x = 0; x < pixelRow.Length; x++)
                {
                    var pixel = pixelRow[x];

                    var i = (y * width + x) * 3;
                    imageData[i + 0] = pixel.R;
                    imageData[i + 1] = pixel.G;
                    imageData[i + 2] = pixel.B;
                }
            }
        });

        return new BrowserStateResponseData(imageData, withUrl);
    }

    private const int k_MaxWidth = 1600;
    private const int k_MaxHeight = 900;
}