using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;
using System.Text;

namespace Engine
{
    public class Shader
    {
        int _handle;
        private bool disposedValue = false;

        public Shader(string vertexPath, string fragmentPath)
        {

            string _vertexShaderSource, _fragmentShaderSource;
            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                _vertexShaderSource = reader.ReadToEnd();
            }

            using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                _fragmentShaderSource = reader.ReadToEnd();
            }


        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(_handle);
                disposedValue = true;
            }
        }


        ~Shader()
        {
            GL.DeleteProgram(_handle);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public void Use()
        {
            GL.UseProgram(_handle);
        }   
    }
}
