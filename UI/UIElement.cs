using System.Collections.Generic;

namespace MonoGameLibrary.UI;

public abstract class UIElement
{
    public float x;
    public float y;
    public float width;
    public float height;

    public List<UIElement> children = new();

    public UIElement parent;

    public void AddTo(UIElement uIElement)
    {
        children.Add(uIElement);
        uIElement.parent = this;
    }
}
