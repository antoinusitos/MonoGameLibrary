using Microsoft.Xna.Framework.Content;
using MonoGameLibrary.Entities;
using MonoGameLibrary.Managers;
using MonoGameLibrary.Shapes;
using MonoGameLibrary.Misc;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Graphics;

public class Tile : Entity
{
    private string spriteName;
    private string atlasName;
    private int index;
    public int Index => index;
    private int x;
    public int X => x;
    private int y;
    public int Y => y;

    public TileInfo tileInfo;
    public Entity entity = null;

    public Tile(string name, string spriteName, string atlasName, int index) : base(name)
    {
        canRender = true;
        this.spriteName = spriteName;
        this.atlasName = atlasName;
        layer = 0;
        this.index = index;

        collisionType = CollisionType.STATIC;
    }

    public override void Initialize()
    {
        base.Initialize();

        collider = new Box(
            position.X,
            position.Y,
            sprite.Width,
            sprite.Height
        );
    }

    public override void LoadContent(ContentManager content)
    {
        base.LoadContent(content);

        TextureAtlas _atlas2 = RessourceManager.Instance.GetOrAddTextureAtlas(atlasName);

        sprite = RessourceManager.Instance.GetOrAddSprite(spriteName, _atlas2);
    }

    public void SetSpriteName(string spriteName)
    {
        this.spriteName = spriteName; 
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        base.Render(spriteBatch);

        //spriteBatch.DrawString(Debug.DebugFont, _index.ToString(), Position, Color.White, 0, Vector2.Zero, 0.1f, SpriteEffects.None, 0);
    }
}
