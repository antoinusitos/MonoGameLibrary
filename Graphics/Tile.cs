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
    private string _spriteName;
    private string _atlasName;
    private int _index;
    public int index => _index;
    private int _x;
    public int x => _x;
    private int _y;
    public int y => _y;

    public TileInfo tileInfo;
    public Entity entity = null;

    public Tile(string name, string spriteName, string atlasName, int index) : base(name)
    {
        _canRender = true;
        _spriteName = spriteName;
        _atlasName = atlasName;
        _layer = 0;
        _index = index;

        _collisionType = CollisionType.STATIC;
    }

    public override void Initialize()
    {
        base.Initialize();

        _collider = new Box(
            _position.X,
            _position.Y,
            _sprite.Width,
            _sprite.Height
        );
    }

    public override void LoadContent(ContentManager content)
    {
        base.LoadContent(content);

        TextureAtlas _atlas2 = RessourceManager.Instance.GetOrAddTextureAtlas(_atlasName);

        _sprite = RessourceManager.Instance.GetOrAddSprite(_spriteName, _atlas2);
    }

    public void SetSpriteName(string spriteName)
    {
        _spriteName = spriteName; 
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        base.Render(spriteBatch);

        //spriteBatch.DrawString(Debug.DebugFont, _index.ToString(), Position, Color.White, 0, Vector2.Zero, 0.1f, SpriteEffects.None, 0);
    }
}
