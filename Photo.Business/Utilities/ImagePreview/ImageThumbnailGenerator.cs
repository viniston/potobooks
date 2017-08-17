using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Photo.Business.Entities.Model;

namespace Photo.Business.Utilities.ImagePreview {
    public class ImageThumbnailGenerator {
        public static void ResizeToFixedSize(string imagePath, long fileId, int width = 100, int height = 100) {
            // Read from file
            using (var image = new MagickImage(imagePath)) {
                var size = new MagickGeometry(width, height) { IgnoreAspectRatio = true };
                // This will resize the image to a fixed size without maintaining the aspect ratio.
                // Normally an image will be resized to fit inside the specified size.

                image.Resize(size);

                var baseBath = ConfigurationManager.AppSettings["DocumentStorageLocation"];
                var filePath = baseBath + @"\ImageThumbnails\" + fileId + "_" + width + "x" + height + ".png";

                // Save the result
                image.Write(filePath);
            }
        }
    }
}
