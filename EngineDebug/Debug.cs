using Microsoft.Xna.Framework.Graphics;

public static class Debug
{
    public static bool DRAW_AABB = true;

    public static Texture2D DebugTexture;

    public static void Log(string message)
    {
        System.Diagnostics.Debug.WriteLine(message);
    }

    public static void LogWarning(string message)
    {
        System.Diagnostics.Debug.WriteLine("WARNING:" + message);
    }
}
