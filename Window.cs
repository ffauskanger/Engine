using OpenTK.Windowing.Desktop;

namespace Engine
{
    public class Window : GameWindow
    {
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }
    }
}
