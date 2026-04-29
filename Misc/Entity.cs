using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Shapes;

namespace MonoGameLibrary.Misc;

public class Entity
{
    protected bool _canUpdate;
    public bool CanUpdate => _canUpdate;

    protected bool _canRender;
    public bool CanRender => _canRender;

    protected bool _canCollide;
    public bool CanCollide => _canCollide;

    protected bool _active;
    public bool Active => _active;

    protected Vector2 _position;
    public Vector2 Position => _position;

    protected Vector2 _velocity;
    public Vector2 Velocity => _velocity;

    protected AnimatedSprite _animatedSprite;

    protected Sprite _sprite;

    protected Circle _collider;
    public Circle Collider => _collider;

    protected float _scale = 1f;
    public float Scale => _scale;

    protected Color _color = Color.White;
    public Color Color => _color;

    public virtual void LoadContent(ContentManager content)
    {
    }

    public virtual void Initialize()
    {
        
    }

    public virtual void Update(GameTime gameTime)
    {
        if (_animatedSprite != null)
        {
            if (_collider != null)
            {
                _collider.X = (int)(_position.X + (_animatedSprite.Width * 0.5f));
                _collider.Y = (int)(_position.Y + (_animatedSprite.Height * 0.5f));
            }

            _animatedSprite.Update(gameTime);
        }
    }

    public virtual void Render(SpriteBatch spriteBatch)
    {
        if (_animatedSprite != null)
        {
            _animatedSprite.Draw(spriteBatch, _position);
        }
        if (_sprite != null)
        {
            _sprite.Draw(spriteBatch, _position);
        }
    }

    public virtual void SetPosition(Vector2 position)
    {
        _position = position;
        if (_collider != null)
        {
            _collider.X = (int)(_position.X + (_animatedSprite.Width * 0.5f));
            _collider.Y = (int)(_position.Y + (_animatedSprite.Height * 0.5f));
        }
    }

    public virtual void OnCollide(Entity other)
    {

    }
}
