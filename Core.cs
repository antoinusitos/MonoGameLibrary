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
    internal static Core instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static Core Instance => instance;

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

    private SceneManager sceneManager;

    private RessourceManager ressourceManager;

    private InputManager inputManager;

    private TimeManager timeManager;

    private UIManager UIManager;

    protected PerformanceManager performanceManager;

    private RegisterManager registerManager;

    private CameraManager cameraManager;

    public static UpdateSystem UpdateSystem { get; private set; }

    public static ParticleSystem ParticleSystem { get; private set; }
    public static RenderSystem RenderSystem { get; private set; }
    public static InteractionSystem InteractionSystem { get; private set; }
    public static SortingSystem SortingSystem { get; private set; }

    public static CollisionSystem CollisionSystem { get; private set; }

    public static MoveSystem MoveSystem { get; private set; }

    public static int realWidth = 320;
    public static int realHeight = 180;

    public static int renderedWidth = 1920;
    public static int renderedHeight = 1080;
    private bool isResizing = false;
    public Viewport viewport;


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
        if (instance != null)
        {
            throw new InvalidOperationException($"Only a single Core instance can be created");
        }

        // Store reference to engine for global member access.
        instance = this;

        // Create a new graphics device manager.
        Graphics = new GraphicsDeviceManager(this);

        // Set the graphics defaults.
        Graphics.PreferredBackBufferWidth = renderedWidth;
        Graphics.PreferredBackBufferHeight = renderedHeight;
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

        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += OnClientSizeChanged;
    }

    private void OnClientSizeChanged(object sender, EventArgs e)
    {
        if (!isResizing && Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
        {
            isResizing = true;
            UpdateScreenScaleMatrix();
            isResizing = false;
        }
    }

    protected override void Initialize()
    {
        base.Initialize();

        // Set the core's graphics device to a reference of the base Game's
        // graphics device.
        GraphicsDevice = base.GraphicsDevice;

        // Create the sprite batch instance.
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        timeManager = new TimeManager();

        ressourceManager = new RessourceManager();

        // Create a new input manager.
        inputManager = new InputManager();

        // Create a new audio controller.
        Audio = new AudioController();

        sceneManager = new SceneManager();

        UpdateSystem = new UpdateSystem();
        CollisionSystem = new CollisionSystem();
        MoveSystem = new MoveSystem();
        InteractionSystem = new InteractionSystem();
        SortingSystem = new SortingSystem();

        ParticleSystem = new ParticleSystem();

        RenderSystem = new RenderSystem();

        registerManager = new RegisterManager();

        cameraManager = new CameraManager();

        UIManager = new UIManager();

        performanceManager = new PerformanceManager();
        performanceManager.LoadContent(Content);

        Debug.DebugTexture = new Texture2D(SpriteBatch.GraphicsDevice, 1, 1);
        Debug.DebugTexture.SetData(new Color[] { Color.White });

        Debug.DebugFont = RessourceManager.Instance.GetOrAddSpriteFont("fonts/04B_30");

        UpdateScreenScaleMatrix();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

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

        timeManager.gameTime = gameTime;
        timeManager.deltaTime = deltaTime;

        // Update the input manager.
        inputManager.Update(deltaTime);

        // Update the audio controller.
        Audio.Update();

        if (ExitOnEscape && inputManager.Keyboard.IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        SceneManager.Instance.Update(deltaTime);

        CameraManager.Instance.Update(deltaTime);

        UpdateSystem.Update(deltaTime);
        CollisionSystem.Update(deltaTime);
        MoveSystem.Update(deltaTime);
        ParticleSystem.Update(deltaTime);
        if (GameManager.UseInteractionSystem)
        {
            InteractionSystem.Update(deltaTime);
        }
        SortingSystem.Update(deltaTime);

        if (UIManager.currentUIEntity != null)
        {
            UIManager.currentUIEntity.Update(deltaTime);
        }

        SceneManager.Instance.ActiveScene.UpdateUI(deltaTime);

        performanceManager.Update(deltaTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Clear the back buffer.
        GraphicsDevice.Clear(Color.Black);

        GraphicsDevice.Viewport = viewport;

        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: CameraManager.Instance.Camera.GetTransformation(GraphicsDevice), sortMode: SpriteSortMode.Deferred);

        SceneManager.Instance.ActiveScene.Draw(deltaTime);

        RenderSystem.Update(deltaTime);
        ParticleSystem.Render(deltaTime);

        // Always end the sprite batch when finished.
        SpriteBatch.End();

        if (UIManager.currentUIEntity != null)
        {
            UIManager.currentUIEntity.Render(SpriteBatch);
        }

        performanceManager.Render(SpriteBatch);

        base.Draw(gameTime);
    }

    protected void BaseGameDraw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }

    private void UpdateScreenScaleMatrix()
    {
        float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
        float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

        if (screenWidth / realWidth >  screenHeight / realHeight)
        {
            float aspect = screenHeight / realHeight;
            renderedWidth = (int)(aspect  * realWidth);
            renderedHeight = (int)screenHeight;
        }
        else
        {
            float aspect = screenWidth / realWidth;
            renderedWidth = (int)screenWidth;
            renderedHeight = (int)(aspect * realHeight);
        }

        CameraManager.Instance.Camera.screenScaleMatrix = Matrix.CreateScale(renderedWidth / (float)realWidth);

        viewport = new Viewport
        {
            X = (int)(screenWidth / 2 - renderedWidth / 2),
            Y = (int)(screenHeight / 2 - renderedHeight / 2),
            Width = renderedWidth,
            Height = renderedHeight,
            MinDepth = 0,
            MaxDepth = 1,
        };

    }
}
