using Microsoft.Xna.Framework;
using System;

namespace MonoGameLibrary.Managers;

public class TimeManager
{
    internal static TimeManager s_instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static TimeManager Instance => s_instance;

    public TimeManager()
    {
        // Ensure that multiple cores are not created.
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a single TimeManager instance can be created");
        }

        // Store reference to engine for global member access.
        s_instance = this;
    }

    public GameTime gameTime;
}
