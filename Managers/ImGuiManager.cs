using ImGuiNET;
using Microsoft.Xna.Framework;
using System;

namespace MonoGameLibrary.Managers;

public class ImGuiManager
{
    internal static ImGuiManager instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static ImGuiManager Instance => instance;

    /// <summary>
    /// Creates a new RegisterManager.
    /// </summary>
    public ImGuiManager()
    {
        // Ensure that multiple cores are not created.
        if (instance != null)
        {
            throw new InvalidOperationException($"Only a single ImGuiManager instance can be created");
        }

        // Store reference to engine for global member access.
        instance = this;
    }

    public delegate void CustomGUIDelegate();
    public CustomGUIDelegate customGUI;

    public void Update(GameTime gameTime)
    {
        // Draw debug UI
        Core.ImGuiRenderer.BeforeLayout(gameTime);

        customGUI?.Invoke();

        Core.ImGuiRenderer.AfterLayout();
    }
}
