using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace AtominaCraft.ZResources.GFX {
    public class Texture : IDisposable {
        public bool Is3D;

        /// <summary>
        ///     ID to the location of the texture in video memory
        /// </summary>
        public int TextureID;

        public Texture(string texturePath, int rows, int columns) {
            this.Is3D = rows > 1 || columns > 1;

            if (!File.Exists(texturePath)) {
                this.TextureID = 0;
                return;
            }

            Bitmap bitmap = new Bitmap(texturePath);
            BitmapData data = bitmap.LockBits(
                new Rectangle(
                    0,
                    0,
                    bitmap.Width,
                    bitmap.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            //GL.Enable(EnableCap.Texture2D);
            //GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            this.TextureID = GL.GenTexture();

            if (this.Is3D) {
                GL.BindTexture(TextureTarget.Texture2DArray, this.TextureID);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.LinearMipmapNearest);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMagFilter, (int) TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.GenerateMipmap, 1);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                GL.TexImage3D(
                    TextureTarget.Texture2DArray,
                    0,
                    PixelInternalFormat.Rgba,
                    data.Width,
                    data.Height,
                    rows * columns,
                    0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    data.Scan0);
                bitmap.UnlockBits(data);
            }

            else {
                GL.BindTexture(TextureTarget.Texture2D, this.TextureID);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMinFilter.Linear);
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    data.Width,
                    data.Height,
                    0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    data.Scan0);
                bitmap.UnlockBits(data);
            }

            bitmap.Dispose();
        }

        public Texture(Bitmap bitmap, int rows, int columns) {
            this.Is3D = rows > 1 || columns > 1;

            BitmapData data = bitmap.LockBits(
                new Rectangle(
                    0,
                    0,
                    bitmap.Width,
                    bitmap.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            //GL.Enable(EnableCap.Texture2D);
            //GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            this.TextureID = GL.GenTexture();

            if (this.Is3D) {
                // GL.BindTexture(TextureTarget.Texture2DArray, this.TextureID);
                // GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.LinearMipmapNearest);
                // GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMagFilter, (int) TextureMinFilter.Linear);
                // GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
                // GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
                // GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.GenerateMipmap, 1);
                // GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                GL.BindTexture(TextureTarget.Texture1DArray, this.TextureID);
                GL.TexParameter(TextureTarget.Texture1DArray, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture1DArray, TextureParameterName.TextureMagFilter, (int) TextureMinFilter.Linear);
                // GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
                // GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
                // GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.GenerateMipmap, 1);
                // GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                // GL.TexImage3D(
                //     TextureTarget.Texture2DArray,
                //     0,
                //     PixelInternalFormat.Rgba,
                //     data.Width,
                //     data.Height,
                //     rows * columns,
                //     0,
                //     OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                //     PixelType.UnsignedByte,
                //     data.Scan0);

                GL.TexImage2D(
                    TextureTarget.Texture1DArray,
                    0,
                    PixelInternalFormat.Rgba,
                    data.Width,
                    data.Height,
                    0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    data.Scan0);

                bitmap.UnlockBits(data);
            }

            else {
                GL.BindTexture(TextureTarget.Texture2D, this.TextureID);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMinFilter.Linear);
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    data.Width,
                    data.Height,
                    0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    data.Scan0);
                bitmap.UnlockBits(data);
            }

            bitmap.Dispose();
        }

        public void Dispose() {
            GL.DeleteTexture(this.TextureID);
        }

        public void Use() {
            if (this.Is3D)
                GL.BindTexture(TextureTarget.Texture2DArray, this.TextureID);
            else
                GL.BindTexture(TextureTarget.Texture2D, this.TextureID);
        }
    }
}