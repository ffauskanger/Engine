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
            int _vertexShader, _fragmentShader;
            string _vertexShaderSource, _fragmentShaderSource;
            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                _vertexShaderSource = reader.ReadToEnd();
            }

            using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                _fragmentShaderSource = reader.ReadToEnd();
            }

            _vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(_vertexShader, _vertexShaderSource);

            _fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(_fragmentShader, _fragmentShaderSource);

            GL.CompileShader(_vertexShader);
            GL.CompileShader(_fragmentShader);

            _handle = GL.CreateProgram();
            GL.AttachShader(_handle, _vertexShader);
            GL.AttachShader(_handle, _fragmentShader);

            GL.LinkProgram(_handle);

            GL.DetachShader(_handle, _vertexShader);
            GL.DetachShader(_handle, _fragmentShader);
            GL.DeleteShader(_fragmentShader);
            GL.DeleteShader(_vertexShader);
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
