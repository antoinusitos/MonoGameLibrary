using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Shapes;

public abstract class Shape
{
    /// <summary>
    /// The x-coordinate of the shape.
    /// </summary>
    public float X;

    /// <summary>
    /// The y-coordinate of the shape.
    /// </summary>
    public float Y;

    /// <summary>
    /// Gets the location of the shape.
    /// </summary>
    public Vector2 Location => new (X, Y);

    /// <summary>
    /// The scale of the Shape
    /// </summary>
    public float Scale;
}
