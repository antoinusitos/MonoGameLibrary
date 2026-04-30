using System;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Shapes;

public class Box : IEquatable<Box>
{
    private static Box s_empty = new Box(0, 0, 1, 1);

    /// <summary>
    /// The x-coordinate of the center of this circle.
    /// </summary>
    public int X;

    /// <summary>
    /// The y-coordinate of the center of this circle.
    /// </summary>
    public int Y;

    /// <summary>
    /// The x-coordinate of the center of this circle.
    /// </summary>
    public int Width;

    /// <summary>
    /// The y-coordinate of the center of this circle.
    /// </summary>
    public int Height;

    /// <summary>
    /// Gets the location of the center of this circle.
    /// </summary>
    public Point Location => new Point(X, Y);

    /// <summary>
    /// Gets a circle with X=0, Y=0, and Radius=0.
    /// </summary>
    public static Box Empty => s_empty;

    // <summary>
    /// Gets a value that indicates whether this circle has a radius of 0 and a location of (0, 0).
    /// </summary>
    public bool IsEmpty => X == 0 && Y == 0 && Width == 0 && Height == 0;

    /// <summary>
    /// Gets the y-coordinate of the highest point on this circle.
    /// </summary>
    public int Top => Y - Height / 2;

    /// <summary>
    /// Gets the y-coordinate of the lowest point on this circle.
    /// </summary>
    public int Bottom => Y + Height / 2;

    /// <summary>
    /// Gets the x-coordinate of the leftmost point on this circle.
    /// </summary>
    public int Left => X - Width / 2;

    /// <summary>
    /// Gets the x-coordinate of the rightmost point on this circle.
    /// </summary>
    public int Right => X + Width / 2;

    /// <summary>
    /// Creates a new circle with the specified position and radius.
    /// </summary>
    /// <param name="x">The x-coordinate of the center of the circle.</param>
    /// <param name="y">The y-coordinate of the center of the circle..</param>
    /// <param name="width">The length from the center of the circle to an edge.</param>
    /// <param name="height">The length from the center of the circle to an edge.</param>
    public Box(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Creates a new circle with the specified position and radius.
    /// </summary>
    /// <param name="location">The center of the circle.</param>
    /// <param name="width">The length from the center of the circle to an edge.</param>
    /// <param name="height">The length from the center of the circle to an edge.</param>
    public Box(Point location, int width, int height)
    {
        X = location.X;
        Y = location.Y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Returns a value that indicates whether the specified circle intersects with this circle.
    /// </summary>
    /// <param name="other">The other circle to check.</param>
    /// <returns>true if the other circle intersects with this circle; otherwise, false.</returns>
    public bool Intersects(Box other)
    {
        if ((other.X >= X + Width)          // On Right
        || (other.X + other.Width <= X)     // On Left
        || (other.Y >= Y + Height)          // Under
        || (other.Y + other.Height <= Y))  // Above
            return false;
        else
            return true;
    }

    /// <summary>
    /// Returns a value that indicates whether this circle and the specified object are equal
    /// </summary>
    /// <param name="obj">The object to compare with this circle.</param>
    /// <returns>true if this circle and the specified object are equal; otherwise, false.</returns>
    public override bool Equals(object obj) => obj is Box other && Equals(other);

    /// <summary>
    /// Returns a value that indicates whether this circle and the specified circle are equal.
    /// </summary>
    /// <param name="other">The circle to compare with this circle.</param>
    /// <returns>true if this circle and the specified circle are equal; otherwise, false.</returns>
    public bool Equals(Box other)
    {
        if (ReferenceEquals(other, null))
            return false;
        if (ReferenceEquals(this, other))
            return true;

        return Equals(this.X, other.X) && Equals(this.Y, other.Y) && Equals(this.Width, other.Width) && Equals(this.Height, other.Height);
    }

    /// <summary>
    /// Returns the hash code for this circle.
    /// </summary>
    /// <returns>The hash code for this circle as a 32-bit signed integer.</returns>
    public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    /// <summary>
    /// Returns a value that indicates if the circle on the left hand side of the equality operator is equal to the
    /// circle on the right hand side of the equality operator.
    /// </summary>
    /// <param name="lhs">The circle on the left hand side of the equality operator.</param>
    /// <param name="rhs">The circle on the right hand side of the equality operator.</param>
    /// <returns>true if the two circles are equal; otherwise, false.</returns>
    public static bool operator ==(Box lhs, Box rhs)
    {
        if (ReferenceEquals(lhs, rhs))
        {
            return true;
        }

        if (ReferenceEquals(lhs, null))
        {
            return false;
        }
        if (ReferenceEquals(rhs, null))
        {
            return false;
        }

        return lhs.Equals(rhs);
    }

    /// <summary>
    /// Returns a value that indicates if the circle on the left hand side of the inequality operator is not equal to the
    /// circle on the right hand side of the inequality operator.
    /// </summary>
    /// <param name="lhs">The circle on the left hand side of the inequality operator.</param>
    /// <param name="rhs">The circle on the right hand side fo the inequality operator.</param>
    /// <returns>true if the two circle are not equal; otherwise, false.</returns>
    public static bool operator !=(Box lhs, Box rhs) => !(lhs == rhs);
}
