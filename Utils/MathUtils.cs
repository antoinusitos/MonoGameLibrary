using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Utils;

public static class MathUtils
{
    public static float Dist(Vector2 a, Vector2 b)
    {
        return (b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y);
    }
}
