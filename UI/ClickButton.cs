using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Input;

namespace MonoGameLibrary.UI;

public class ClickButton : Button
{
    public ClickButton()
    {
        buttonType = Button_Type.ONCE;
        active = true;
        backgroundColor = Color.LightGray;
        hoverColor = Color.Red;
        clickedColor = Color.Yellow;
        disabledColor = Color.Gray;
    }

    public override void Update()
    {
        if (!active)
        {
            return;

        }

        if (disabled)
        {
            isClicked = false;

            return;

        }

        MouseInfo mouse = InputManager.Instance.Mouse;
        Point mousePos = mouse.CurrentState.Position;

        if (mousePos.X >= x && mousePos.X <= x + width &&
            mousePos.Y >= y && mousePos.Y <= y + height)
        {
            isHover = true;

            OnHover?.Invoke();

            if (mouse.CurrentState.LeftButton == ButtonState.Pressed)
            {
                if (!isClicked)
                {
                    OnClick?.Invoke();
                }

                isClicked = true;

            }
            else if (mouse.CurrentState.LeftButton == ButtonState.Released)
            {
                if (isClicked)
                {
                    OnRelease?.Invoke();
                }

                isClicked = false;
            }
        }
        else
        {
            if (mouse.CurrentState.LeftButton == ButtonState.Released)
            {
                if (isClicked)
                {
                    OnRelease?.Invoke();
                }
                isClicked = false;

            }

            if (isHover)
            {
                OnExit?.Invoke();

            }
            isHover = false;

        }
    }

    public override void Draw()
    {
        if (!active)
        {
            return;
        }

        if (disabled)
        {
            Core.SpriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)x, (int)y, (int)width, (int)height), disabledColor);
            Core.SpriteBatch.DrawString(Debug.DebugFont, text, new Vector2(x + textOffset.X, y + textOffset.Y), Color.White, 0.0f, Vector2.Zero, textSize, SpriteEffects.None, 1.0f);

            return;

        }
        if (isClicked)
        {
            Core.SpriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)x, (int)y, (int)width, (int)height), clickedColor);
        }
        else
        {
            Core.SpriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)x, (int)y, (int)width, (int)height), isHover ? hoverColor : backgroundColor);
        }
        Core.SpriteBatch.DrawString(Debug.DebugFont, text, new Vector2(x + textOffset.X, y + textOffset.Y), Color.White, 0.0f, Vector2.Zero, textSize, SpriteEffects.None, 1.0f);
    }
}
