using Microsoft.Xna.Framework;
using MonoGameLibrary.Scenes;
using System;

namespace MonoGameLibrary.Managers;

public class SceneManager
{
    internal static SceneManager s_instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static SceneManager Instance => s_instance;

    // The scene that is currently active.
    private Scene _activeScene;
    public Scene ActiveScene => _activeScene;

    // The next scene to switch to, if there is one.
    private Scene _nextScene;

    public SceneManager()
    {
        // Ensure that multiple cores are not created.
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a single SceneManager instance can be created");
        }

        // Store reference to engine for global member access.
        s_instance = this;
    }

    public void Update(GameTime gameTime)
    {
        if (_activeScene != null)
        {
            _activeScene.Update(gameTime);
        }

        // if there is a next scene waiting to be switch to, then transition
        // to that scene.
        if (_nextScene != null)
        {
            TransitionScene();
        }
    }

    public void ChangeScene(Scene next)
    {
        // Only set the next scene value if it is not the same
        // instance as the currently active scene.
        if (_activeScene != next)
        {
            _nextScene = next;
        }
    }

    private void TransitionScene()
    {
        // If there is an active scene, dispose of it.
        if (_activeScene != null)
        {
            _activeScene.Dispose();
        }

        // Force the garbage collector to collect to ensure memory is cleared.
        GC.Collect();

        // Change the currently active scene to the new scene.
        _activeScene = _nextScene;

        // Null out the next scene value so it does not trigger a change over and over.
        _nextScene = null;

        // If the active scene now is not null, initialize it.
        // Remember, just like with Game, the Initialize call also calls the
        // Scene.LoadContent
        if (_activeScene != null)
        {
            _activeScene.Initialize();
        }
    }
}
