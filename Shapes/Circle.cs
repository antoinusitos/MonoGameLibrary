using System;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Shapes;

public class Circle : IEquatable<Circle>
{
    private static Circle s_empty = new Circle(0, 0, 1);

    /// <summary>
    /// The x-coordinate of the center of this circle.
    /// </summary>
    public int X;

    /// <summary>
    /// The y-coordinate of the center of this circle.
    /// </summary>
    public int Y;

    /// <summary>
    /// The length, in pixels, from the center of this circle to the edge.
    /// </summary>
    public readonly int Radius;

    /// <summary>
    /// Gets the location of the center of this circle.
    /// </summary>
    public Point Location => new Point(X, Y);

    /// <summary>
    /// Gets a circle with X=0, Y=0, and Radius=0.
    /// </summary>
    public static Circle Empty => s_empty;

    /// <summary>
    /// Gets a value that indicates whether this circle has a radius of 0 and a location of (0, 0).
    /// </summary>
    public bool IsEmpty => X == 0 && Y == 0 && Radius == 0;

    /// <summary>
    /// Gets the y-coordinate of the highest point on this circle.
    /// </summary>
    public int Top => Y - Radius;

    /// <summary>
    /// Gets the y-coordinate of the lowest point on this circle.
    /// </summary>
    public int Bottom => Y + Radius;

    /// <summary>
    /// Gets the x-coordinate of the leftmost point on this circle.
    /// </summary>
    public int Left => X - Radius;

    /// <summary>
    /// Gets the x-coordinate of the rightmost point on this circle.
    /// </summary>
    public int Right => X + Radius;

    /// <summary>
    /// Creates a new circle with the specified position and radius.
    /// </summary>
    /// <param name="x">The x-coordinate of the center of the circle.</param>
    /// <param name="y">The y-coordinate of the center of the circle..</param>
    /// <param name="radius">The length from the center of the circle to an edge.</param>
    public Circle(int x, int y, int radius)
    {
        X = x;
        Y = y;
        Radius = radius;
    }

    /// <summary>
    /// Creates a new circle with the specified position and radius.
    /// </summary>
    /// <param name="location">The center of the circle.</param>
    /// <param name="radius">The length from the center of the circle to an edge.</param>
    public Circle(Point location, int radius)
    {
        X = location.X;
        Y = location.Y;
        Radius = radius;
    }

    /// <summary>
    /// Returns a value that indicates whether the specified circle intersects with this circle.
    /// </summary>
    /// <param name="other">The other circle to check.</param>
    /// <returns>true if the other circle intersects with this circle; otherwise, false.</returns>
    public bool Intersects(Circle other)
    {
        int radiiSquared = (this.Radius + other.Radius) * (this.Radius + other.Radius);
        float distanceSquared = Vector2.DistanceSquared(this.Location.ToVector2(), other.Location.ToVector2());
        return distanceSquared < radiiSquared;
    }

    /// <summary>
    /// Returns a value that indicates whether this circle and the specified object are equal
    /// </summary>
    /// <param name="obj">The object to compare with this circle.</param>
    /// <returns>true if this circle and the specified object are equal; otherwise, false.</returns>
    public override bool Equals(object obj) => obj is Circle other && Equals(other);

    /// <summary>
    /// Returns a value that indicates whether this circle and the specified circle are equal.
    /// </summary>
    /// <param name="other">The circle to compare with this circle.</param>
    /// <returns>true if this circle and the specified circle are equal; otherwise, false.</returns>
    public bool Equals(Circle other)
    {
        if (ReferenceEquals(other, null))
            return false;
        if (ReferenceEquals(this, other))
            return true;

        return Equals(this.X, other.X) && Equals(this.Y, other.Y) && Equals(this.Radius, other.Radius);
    }

    /// <summary>
    /// Returns the hash code for this circle.
    /// </summary>
    /// <returns>The hash code for this circle as a 32-bit signed integer.</returns>
    public override int GetHashCode() => HashCode.Combine(X, Y, Radius);

    /// <summary>
    /// Returns a value that indicates if the circle on the left hand side of the equality operator is equal to the
    /// circle on the right hand side of the equality operator.
    /// </summary>
    /// <param name="lhs">The circle on the left hand side of the equality operator.</param>
    /// <param name="rhs">The circle on the right hand side of the equality operator.</param>
    /// <returns>true if the two circles are equal; otherwise, false.</returns>
    public static bool operator ==(Circle lhs, Circle rhs)
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
    public static bool operator !=(Circle lhs, Circle rhs) => !(lhs == rhs);

}
