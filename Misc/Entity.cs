using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Managers;
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

    protected bool _canMove;
    public bool CanMove => _canMove;

    protected bool _isParticle;
    public bool IsParticle => _isParticle;

    protected bool _active;
    public bool Active => _active;

    protected Vector2 _position;
    public Vector2 Position => _position;

    protected Vector2 _velocity;
    public Vector2 Velocity;// => _velocity;

    protected AnimatedSprite _animatedSprite;

    protected Sprite _sprite;

    protected Box _collider;
    public Box Collider => _collider;

    protected CollisionType _collisionType;
    public CollisionType CollisionType => _collisionType;

    protected float _scale = 1f;
    public float Scale => _scale;

    protected Color _color = Color.White;
    public Color Color => _color;

    public bool PendingDestroy = false;

    public virtual void LoadContent(ContentManager content)
    {
    }

    public virtual void Initialize()
    {
        
    }

    public virtual void Update(float deltaTime)
    {
        if (_animatedSprite != null)
        {
            if (_collider != null)
            {
                _collider.X = _position.X;
                _collider.Y = _position.Y;
            }

            _animatedSprite.Update(deltaTime);
        }
    }

    public void Register()
    {
        RegisterManager.Instance.RegisterEntity(this);
    }

    public virtual void Render(SpriteBatch spriteBatch)
    {
        if (_canCollide && Debug.DRAW_AABB)
        {
            spriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)_position.X, (int)_position.Y, (int)(_collider.Width * _collider.Scale), (int)(_collider.Height * _collider.Scale)), Color.White);
        }

        if (_animatedSprite != null)
        {
            _animatedSprite.Draw(spriteBatch, _position);
        }
        if (_sprite != null)
        {
            _sprite.Draw(spriteBatch, _position);
        }

        if (Debug.DRAW_AABB)
        {
            spriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)_position.X, (int)_position.Y, 5, 5), Color.Red);

            spriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)_collider.X, (int)_collider.Y, 5, 5), Color.Orange);
        }
    }

    public virtual void SetPosition(Vector2 position)
    {
        _position = position;
        if (_collider != null)
        {
            _collider.X = _position.X;
            _collider.Y = _position.Y;
        }
    }

    public virtual void SetPosition(float x, float y)
    {
        _position = new Vector2(x, y);
        if (_collider != null)
        {
            _collider.X = _position.X;
            _collider.Y = _position.Y;
        }
    }

    public virtual void SetScale(float scale)
    { 
        _scale = scale;
        if (_collider != null)
        {
            _collider.Scale = scale;
        }
        if (_sprite != null)
        {
            _sprite.Scale = Vector2.One * scale;
        }
        if (_animatedSprite != null)
        {
            _animatedSprite.Scale = Vector2.One * scale;
        }
    }

    public virtual void SetColor(Color color)
    {
        _color = color;
    }

    public virtual void OnCollide(Entity other)
    {

    }
}
