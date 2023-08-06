using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.ConstrainedExecution;

namespace AtominaCraft.ZResources.GFX {
    public class TextureAtlas {
        private const int TextureSize = 64;
        private readonly Bitmap bitmap;

        public TextureAtlas() {
            this.bitmap = new Bitmap(TextureSize * 8, TextureSize * 8, PixelFormat.Format32bppRgb);
        }

        public void SetBlock(Bitmap bitmap, int x, int y) {
            // BitmapData data = this.bitmap.LockBits(new Rectangle(x * TextureSize, y * TextureSize, TextureSize, TextureSize), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(this.bitmap);
            graphics.DrawImage(bitmap, new Rectangle(x * TextureSize, y * TextureSize, TextureSize, TextureSize));
        }

        public void SetBlock(Image image, int x, int y) {
            // BitmapData data = this.bitmap.LockBits(new Rectangle(x * TextureSize, y * TextureSize, TextureSize, TextureSize), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(this.bitmap);
            graphics.DrawImage(image, new Rectangle(x * TextureSize, y * TextureSize, TextureSize, TextureSize));
        }

        public Bitmap GetBitmap() {
            return this.bitmap;
        }
    }
}