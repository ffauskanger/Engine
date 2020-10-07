using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Drawing;

namespace Engine
{
    public class Window : GameWindow
    {
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }
        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private Shader _shader;
        private readonly float[] _vertices =
        {
            -0.5f, -0.5f, 0.0f, // Bottom-left vertex 
            0.5f, -0.5f, 0.0f, // Bottom-right vertex
            0.0f, 0.5f, 0.0f // Top Vertex
        };

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            var input = KeyboardState;

            // Close Window
            if(input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            base.OnUpdateFrame(args);
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            //StaticDraw: the data will most likely not change at all or very rarely.
            //DynamicDraw: the data is likely to change a lot.
            //StreamDraw: the data will change every time it is drawn.

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            // Load shader
            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

            // Binding shader
            _shader.Use();

            //
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            base.OnLoad();  
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // Binding a buffer to 0 basically sets it to null, so any calls that modify a buffer without binding one first will result in a crash. This is easier to debug than accidentally modifying a buffer that we didn't want modified.
            GL.DeleteBuffer(_vertexBufferObject);
            _shader.Dispose();

            base.OnUnload();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Binding shader
            _shader.Use();

            // Bind the VAO

            GL.BindVertexArray(_vertexArrayObject);

            // Drawing

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            // Window manages two buffers - One is rendered, while one is displayed. This avoids screentearing as after you draw - we swap the buffers. (Won't work without this).
            SwapBuffers();


            base.OnRenderFrame(args);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);

            base.OnResize(e);
        }




    }
}
