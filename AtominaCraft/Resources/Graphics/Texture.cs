using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace AtominaCraft.Resources.Graphics
{
    public class Texture : IDisposable
    {
        /// <summary>
        /// ID to the location of the texture in video memory
        /// </summary>
        public int TextureID { get; set; }
        public bool Is3D { get; set; }

        public Texture(string texturePath, int rows, int columns)
        {
            Is3D = (rows > 1 || columns > 1);

            if (!File.Exists(texturePath))
            {
                TextureID = 0;
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
                System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            TextureID = GL.GenTexture();

            if (Is3D)
            {
                GL.BindTexture(TextureTarget.Texture2DArray, TextureID);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapNearest);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
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

            else
            {
                GL.BindTexture(TextureTarget.Texture2D, TextureID);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
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

        public void Use()
        {
            if (Is3D) 
                GL.BindTexture(TextureTarget.Texture2DArray, TextureID);
            else 
                GL.BindTexture(TextureTarget.Texture2D, TextureID);
        }

        public void Dispose()
        {
            GL.DeleteTexture(TextureID);
        }
    }
}
