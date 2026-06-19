using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Camera;

public class Camera2D
{
    protected float     zoom = 1f; // Camera Zoom

    protected Matrix    transform; // Matrix Transform

    protected Vector2   position; // Camera Position

    public Matrix       screenScaleMatrix;

    public Vector2      Position
    {
        get { return position; }
        set { position = value; }
    }

    protected Vector2 offset; // Camera Offset
    public Vector2 Offset
    {
        get { return offset; }
        set { offset = value; }
    }

    protected float     rotation; // Camera Rotation
    public float Zoom
    {
        get { return zoom; }
        set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; } // Negative zoom will flip image
    }

    public float Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public Camera2D()
    {
        zoom = 1.0f;
        rotation = 0.0f;
        position = Vector2.Zero;
    }

    // Auxiliary function to move the camera
    public void Move(Vector2 amount)
    {
        position += amount;
    }

    public Matrix GetTransformation(GraphicsDevice graphicsDevice)
    {
        transform =       // Thanks to o KB o for this solution
          Matrix.CreateTranslation(new Vector3(-position.X + offset.X, -position.Y + Offset.Y, 0)) *
                                     Matrix.CreateRotationZ(Rotation) *
                                     Matrix.CreateScale(Core.renderedWidth / (float)Core.realWidth * zoom) *
                                     Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
        return transform;
    }

}
