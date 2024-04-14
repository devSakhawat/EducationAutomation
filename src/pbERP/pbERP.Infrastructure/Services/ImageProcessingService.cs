using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace pbERP.Infrastructure.Services;

public static class ImageProcessingService
{
   public static bool IsSupportedImageFile(IFormFile file)
   {
      string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
      string fileExtension = Path.GetExtension(file.FileName);
      return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
   }

   public static async Task<byte[]> ProcessAndConvertImageAsync(IFormFile imageFile)
   {
      using (var memoryStream = new MemoryStream())
      {
         long maxSize = 200 * 1024; // 200KB

         if (imageFile.Length > maxSize)
         {
            // Load the image using ImageSharp asynchronously
            using (var image = await Image.LoadAsync(imageFile.OpenReadStream()))
            {
               // Calculate the new dimensions to keep the aspect ratio and fit within 500KB
               var newSize = CalculateNewSize(image.Width, image.Height, maxSize);

               // Resize the image while maintaining aspect ratio
               image.Mutate(x => x.Resize(new ResizeOptions
               {
                  Size = newSize,
                  Mode = ResizeMode.Max
               }));

               // Save the resized image to the memory stream as JPEG with a quality of 90%
               await image.SaveAsJpegAsync(memoryStream, new JpegEncoder
               {
                  Quality = 70
               });
            }
         }
         else
         {
            // If the image is not large, simply copy it to the memory stream asynchronously
            await imageFile.CopyToAsync(memoryStream);
         }

         // Return the image data as a byte array
         return memoryStream.ToArray();
      }
   }

   private static Size CalculateNewSize(int width, int height, long maxSize)
   {
      double aspectRatio = (double)width / height;
      double newSizeWidth = Math.Sqrt(maxSize * aspectRatio);
      double newSizeHeight = newSizeWidth / aspectRatio;

      return new Size((int)newSizeWidth, (int)newSizeHeight);
   }
}
