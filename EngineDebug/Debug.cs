using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public static class Debug
{
    public static bool DRAW_AABB = false;

    public static bool DRAW_IMGUI = true;

    public static Texture2D DebugTexture;

    public static SpriteFont DebugFont;

    public static List<string> debugLines = new List<string>();

    public static void Log(string message)
    {
        System.Diagnostics.Debug.WriteLine(message);
        debugLines.Add(message);
    }

    public static void LogWarning(string message)
    {
        System.Diagnostics.Debug.WriteLine("WARNING:" + message);
        debugLines.Add("WARNING:" + message);
    }
}
