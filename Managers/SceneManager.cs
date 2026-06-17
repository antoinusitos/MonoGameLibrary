using MonoGameLibrary.Scenes;
using System;

namespace MonoGameLibrary.Managers;

public class SceneManager
{
    internal static SceneManager instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static SceneManager Instance => instance;

    // The scene that is currently active.
    private Scene activeScene;
    public Scene ActiveScene => activeScene;

    // The next scene to switch to, if there is one.
    private Scene nextScene;

    private bool isDirty = true;
    public bool IsDirty => isDirty;

    public SceneManager()
    {
        // Ensure that multiple cores are not created.
        if (instance != null)
        {
            throw new InvalidOperationException($"Only a single SceneManager instance can be created");
        }

        // Store reference to engine for global member access.
        instance = this;
    }

    public void Update(float deltaTime)
    {
        if (activeScene != null)
        {
            activeScene.Update(deltaTime);
        }

        // if there is a next scene waiting to be switch to, then transition
        // to that scene.
        if (nextScene != null)
        {
            TransitionScene();
        }
    }

    public void ChangeScene(Scene next)
    {
        // Only set the next scene value if it is not the same
        // instance as the currently active scene.
        if (activeScene != next)
        {
            nextScene = next;
        }
    }

    private void TransitionScene()
    {
        // If there is an active scene, dispose of it.
        if (activeScene != null)
        {
            activeScene.Dispose();
        }

        // Force the garbage collector to collect to ensure memory is cleared.
        GC.Collect();

        // Change the currently active scene to the new scene.
        activeScene = nextScene;

        // Null out the next scene value so it does not trigger a change over and over.
        nextScene = null;

        // If the active scene now is not null, initialize it.
        // Remember, just like with Game, the Initialize call also calls the
        // Scene.LoadContent
        if (activeScene != null)
        {
            activeScene.Initialize();
        }
    }

    public void SetIsDirty(bool value)
    {
        isDirty = value;
    }
}
