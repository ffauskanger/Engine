using OpenTK.Windowing.Desktop;
using System;

namespace Engine
{
    public static class Program
    {
        static void Main(string[] args)
        {
            GameWindowSettings gameWindowSettings = new GameWindowSettings();
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings();

            nativeWindowSettings.Title = "Hello World";

            using (var window = new Window(gameWindowSettings, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}
