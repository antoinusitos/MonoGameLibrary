using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Managers;

public class PerformanceManager
{
    internal static PerformanceManager instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static PerformanceManager Instance => instance;

    /// <summary>
    /// Creates a new RegisterManager.
    /// </summary>
    public PerformanceManager()
    {
        // Ensure that multiple cores are not created.
        if (instance != null)
        {
            throw new InvalidOperationException($"Only a single PerformanceManager instance can be created");
        }

        // Store reference to engine for global member access.
        instance = this;
    }

    // The SpriteFont Description used to draw text
    private SpriteFont _font;

    // Defines the position to draw the score text at.
    private Vector2 _startPosition;

    private bool _mustShow = false;

    public void LoadContent(ContentManager content)
    {
        // Load the font.
        _font = RessourceManager.Instance.GetOrAddSpriteFont("fonts/04B_30");

        _startPosition = new Vector2(10, 10);
    }

    public void Update(float deltaTime)
    {
        // Get a reference to the keyboard inof
        KeyboardInfo keyboard = InputManager.Instance.Keyboard;

        if (keyboard.WasKeyJustPressed(Keys.F1))
        {
            _mustShow = !_mustShow;
        }
        if (keyboard.WasKeyJustPressed(Keys.F2))
        {
            Debug.DRAW_AABB = !Debug.DRAW_AABB;
        }
    }

    public void Render(SpriteBatch spriteBatch)
    {
        if (!_mustShow)
        {
            return;
        }

        // Begin the sprite batch to prepare for rendering.
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        spriteBatch.Draw(Debug.DebugTexture, new Rectangle(0, 0, 300, 80), Color.Black);

        // Draw the entities number.
        Core.SpriteBatch.DrawString(
            _font,              // spriteFont
            "Entities : " + RegisterManager.Instance.registeredEntities.Count,          // text
            _startPosition,  // position
            Color.White,        // color
            0.0f,               // rotation
            Vector2.Zero,    // origin
            1.0f,               // scale
            SpriteEffects.None, // effects
            0.0f                // layerDepth
        );

        // Draw the FPS.
        Core.SpriteBatch.DrawString(
            _font,              // spriteFont
            "FPS : " + Math.Round(1 / TimeManager.Instance.deltaTime),          // text
            _startPosition + Vector2.UnitY * 40,  // position
            Color.White,        // color
            0.0f,               // rotation
            Vector2.Zero,    // origin
            1.0f,               // scale
            SpriteEffects.None, // effects
            0.0f                // layerDepth
        );

        // Always end the sprite batch when finished.
        Core.SpriteBatch.End();
    }
}
