using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Camera;

public class Camera2D
{
    protected float     _zoom; // Camera Zoom

    protected Matrix    _transform; // Matrix Transform

    protected Vector2   _position; // Camera Position
    public Vector2      Position
    {
        get { return _position; }
        set { _position = value; }
    }

    protected Vector2 _offset; // Camera Offset
    public Vector2 Offset
    {
        get { return _offset; }
        set { _offset = value; }
    }

    protected float     _rotation; // Camera Rotation
    public float Zoom
    {
        get { return _zoom; }
        set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
    }

    public float Rotation
    {
        get { return _rotation; }
        set { _rotation = value; }
    }

    public Camera2D()
    {
        _zoom = 1.0f;
        _rotation = 0.0f;
        _position = Vector2.Zero;
    }

    // Auxiliary function to move the camera
    public void Move(Vector2 amount)
    {
        _position += amount;
    }

    public Matrix GetTransformation(GraphicsDevice graphicsDevice)
    {
        _transform =       // Thanks to o KB o for this solution
          Matrix.CreateTranslation(new Vector3(-_position.X + _offset.X, -_position.Y + Offset.Y, 0)) *
                                     Matrix.CreateRotationZ(Rotation) *
                                     Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                     Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
        return _transform;
    }

}
