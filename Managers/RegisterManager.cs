using MonoGameLibrary.Misc;
using System;
using System.Collections.Generic;

namespace MonoGameLibrary.Managers;

public class RegisterManager
{
    internal static RegisterManager s_instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static RegisterManager Instance => s_instance;

    /// <summary>
    /// Creates a new RegisterManager.
    /// </summary>
    public RegisterManager()
    {
        // Ensure that multiple cores are not created.
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a single RegisterManager instance can be created");
        }

        // Store reference to engine for global member access.
        s_instance = this;
    }

    public List<Entity> registeredEntities = new();
    public List<Entity> registeredColliders = new();
    public List<Entity> registeredRenderers = new();
    public List<Entity> registeredUpdaters = new();

    public void RegisterEntity(Entity entity)
    {
        registeredEntities.Add(entity);
        if (entity.CanCollide)
        {
            registeredColliders.Add(entity);
        }
        if (entity.CanRender)
        {
            registeredRenderers.Add(entity);
        }
        if (entity.CanUpdate)
        {
            registeredUpdaters.Add(entity);
        }
    }
}
