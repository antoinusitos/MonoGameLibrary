using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Camera;
using MonoGameLibrary.Input;
using System;
using System.Numerics;

namespace MonoGameLibrary.Managers;

public  class CameraManager
{
    internal static CameraManager s_instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static CameraManager Instance => s_instance;

    /// <summary>
    /// Creates a new RegisterManager.
    /// </summary>
    public CameraManager()
    {
        // Ensure that multiple cores are not created.
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a single CameraManager instance can be created");
        }

        // Store reference to engine for global member access.
        s_instance = this;

        Camera = new Camera2D();
    }

    public Camera2D Camera;

    public void Update(float deltaTime)
    {
        // Get a reference to the keyboard inof
        KeyboardInfo keyboard = InputManager.Instance.Keyboard;

        Vector2 velocity = Vector2.Zero;
        float speed = 5.0f;

        // If the W or Up keys are down, move the slime up on the screen.
        if (keyboard.IsKeyDown(Keys.Up))
        {
            velocity.Y -= speed;
        }

        // if the S or Down keys are down, move the slime down on the screen.
        if (keyboard.IsKeyDown(Keys.Down))
        {
            velocity.Y += speed;
        }

        // If the A or Left keys are down, move the slime left on the screen.
        if (keyboard.IsKeyDown(Keys.Left))
        {
            velocity.X -= speed;
        }

        // If the D or Right keys are down, move the slime right on the screen.
        if (keyboard.IsKeyDown(Keys.Right))
        {
            velocity.X += speed;
        }

        Camera.Move(velocity);
    }
}
