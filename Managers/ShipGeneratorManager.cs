using System;

namespace MonoGameLibrary.Managers;

public class ShipGeneratorManager
{
    internal static ShipGeneratorManager s_instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static ShipGeneratorManager Instance => s_instance;

    public ShipGeneratorManager()
    {
        // Ensure that multiple cores are not created.
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a single ShipGeneratorManager instance can be created");
        }

        // Store reference to engine for global member access.
        s_instance = this;
    }
}
