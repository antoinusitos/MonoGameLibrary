using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Audio;
using MonoGameLibrary.Input;
using MonoGameLibrary.Managers;
using MonoGameLibrary.Systems;

namespace MonoGameLibrary;

public class Core : Game
{
    internal static Core s_instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static Core Instance => s_instance;

    /// <summary>
    /// Gets the graphics device manager to control the presentation of graphics.
    /// </summary>
    public static GraphicsDeviceManager Graphics { get; private set; }

    /// <summary>
    /// Gets the graphics device used to create graphical resources and perform primitive rendering.
    /// </summary>
    public static new GraphicsDevice GraphicsDevice { get; private set; }

    /// <summary>
    /// Gets the sprite batch used for all 2D rendering.
    /// </summary>
    public static SpriteBatch SpriteBatch { get; private set; }

    /// <summary>
    /// Gets the content manager used to load global assets.
    /// </summary>
    public static new ContentManager Content { get; private set; }

    /// <summary>
    /// Gets or Sets a value that indicates if the game should exit when the esc key on the keyboard is pressed.
    /// </summary>
    public static bool ExitOnEscape { get; set; }

    /// <summary>
    /// Gets a reference to the audio control system.
    /// </summary>
    public static AudioController Audio { get; private set; }

    private SceneManager _sceneManager;

    private RessourceManager _ressourceManager;

    private InputManager _inputManager;

    private TimeManager _timeManager;

    public static UpdateSystem UpdateSystem { get; private set; }

    public static RenderSystem RenderSystem { get; private set; }

    private RegisterManager _registerManager;

    public static CollisionSystem CollisionSystem { get; private set; }

    public static MoveSystem MoveSystem { get; private set; }

    /// <summary>
    /// Creates a new Core instance.
    /// </summary>
    /// <param name="title">The title to display in the title bar of the game window.</param>
    /// <param name="width">The initial width, in pixels, of the game window.</param>
    /// <param name="height">The initial height, in pixels, of the game window.</param>
    /// <param name="fullScreen">Indicates if the game should start in fullscreen mode.</param>
    public Core(string title, int width, int height, bool fullScreen)
    {
        // Ensure that multiple cores are not created.
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a single Core instance can be created");
        }

        // Store reference to engine for global member access.
        s_instance = this;

        // Create a new graphics device manager.
        Graphics = new GraphicsDeviceManager(this);

        // Set the graphics defaults.
        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullScreen;

        // Apply the graphic presentation changes.
        Graphics.ApplyChanges();

        // Set the window title.
        Window.Title = title;

        // Set the core's content manager to a reference of the base Game's
        // content manager.
        Content = base.Content;

        // Set the root directory for content.
        Content.RootDirectory = "Content";

        // Mouse is visible by default.
        IsMouseVisible = true;

        // Exit on escape is true by default
        ExitOnEscape = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        // Set the core's graphics device to a reference of the base Game's
        // graphics device.
        GraphicsDevice = base.GraphicsDevice;

        // Create the sprite batch instance.
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        _timeManager = new TimeManager();

        _ressourceManager = new RessourceManager();

        // Create a new input manager.
        _inputManager = new InputManager();

        // Create a new audio controller.
        Audio = new AudioController();

        _sceneManager = new SceneManager();

        UpdateSystem = new UpdateSystem();
        CollisionSystem = new CollisionSystem();
        MoveSystem = new MoveSystem();

        RenderSystem = new RenderSystem();

        _registerManager = new RegisterManager();

        Debug.DebugTexture = new Texture2D(SpriteBatch.GraphicsDevice, 1, 1);
        Debug.DebugTexture.SetData(new Color[] { Color.White });
    }

    protected override void UnloadContent()
    {
        // Dispose of the audio controller.
        Audio.Dispose();

        base.UnloadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _timeManager.gameTime = gameTime;

        // Update the input manager.
        _inputManager.Update(deltaTime);

        // Update the audio controller.
        Audio.Update();

        if (ExitOnEscape && _inputManager.Keyboard.IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        SceneManager.Instance.Update(deltaTime);

        UpdateSystem.Update(deltaTime);
        CollisionSystem.Update(deltaTime);
        MoveSystem.Update(deltaTime);

        SceneManager.Instance.ActiveScene.UpdateUI(deltaTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Clear the back buffer.
        Core.GraphicsDevice.Clear(Color.CornflowerBlue);

        // Begin the sprite batch to prepare for rendering.
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        SceneManager.Instance.ActiveScene.Draw(deltaTime);

        RenderSystem.Update(deltaTime);

        // Always end the sprite batch when finished.
        Core.SpriteBatch.End();

        SceneManager.Instance.ActiveScene.DrawUI(deltaTime);

        base.Draw(gameTime);
    }
}
