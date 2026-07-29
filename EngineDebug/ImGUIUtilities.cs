using ImGuiNET;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary.EngineDebug;

public class ImGUIUtilities
{
    public static void InputVector2(ref Vector2 vector, string label)
    {
        System.Numerics.Vector2 castedPosition = new System.Numerics.Vector2(vector.X, vector.Y);
        ImGui.InputFloat2(label, ref castedPosition);
        if (castedPosition.X != vector.X || castedPosition.Y != vector.Y)
        {
            vector = new(castedPosition.X, castedPosition.Y);
        }
    }

    public static void InputColor(ref Color color, string label)
    {
        System.Numerics.Vector4 castedColor = color.ToVector4().ToNumerics();
        ImGui.ColorEdit4(label, ref castedColor);
        color = new Color(castedColor);
    }
}