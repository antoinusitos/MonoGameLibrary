using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.UI;

public class Text : UIElement
{
    public string text;
    public int textSize;
    public Color textColor;

    public void Draw()
    {
        Core.SpriteBatch.DrawString(Debug.DebugFont, text, new Vector2(x, y), textColor, 0.0f, Vector2.Zero, textSize, SpriteEffects.None, 1.0f);
    }
}
