using Microsoft.Xna.Framework;

namespace MonoGameLibrary.UI;

public enum Button_Type 
{
    ONCE,
    FILLING,
}

public delegate void ButtonFunc();

public abstract class Button : UIElement
{
	public Color backgroundColor;
	public Color hoverColor;
	public Color clickedColor;
	public Color fillColor;
	public Color disabledColor;
	public Button_Type buttonType;
	public bool isHover;
	public bool isClicked;
	public bool disabled;
	public bool active; // don't update or draw

	public bool filledDone;
	public float fillPercent;
	public float fillMax;
	public bool fillAutoReset;

	public string text;
	public int textSize;
	protected Vector2 textBound;
	public Vector2 textOffset;

	public virtual void Update()
	{

	}
	public virtual void Draw()
    {

    }

    public ButtonFunc OnClick;
	public ButtonFunc OnDown;
	public ButtonFunc OnRelease;
	public ButtonFunc OnFilled;
	public ButtonFunc OnHover;
	public ButtonFunc OnExit;

	public void SetText(string newText)
	{ 
		text = newText;
        textBound = Debug.DebugFont.MeasureString(text) * textSize;
        textOffset = new Vector2(width, height) - textBound;
        textOffset /= 2;
    }

};