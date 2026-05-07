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

    protected bool _canInteract;
    public bool CanInteract => _canInteract;

    protected bool _wantToInteract;
    public bool WantToInteract => _wantToInteract;
    
    protected bool _canRender;
    public bool CanRender => _canRender;

    protected bool _canCollide;
    public bool CanCollide => _canCollide;

    protected bool _canMove;
    public bool CanMove => _canMove;

    protected string _entityName;
    public string EntityName => _entityName;

    protected bool _isParticle;
    public bool IsParticle => _isParticle;

    protected bool _active;
    public bool Active => _active;

    protected Vector2 _position;
    public Vector2 Position => (_parent != null) ? _parent.Position + _relativePosition : _position;

    protected Vector2 _relativePosition;
    public Vector2 RelativePosition => _relativePosition;

    protected Vector2 _velocity;
    public Vector2 Velocity;// => _velocity;

    protected AnimatedSprite _animatedSprite;

    protected Sprite _sprite;

    protected Box _collider;
    public Box Collider => _collider;

    protected bool _isTrigger;
    public bool IsTrigger => _isTrigger;

    protected CollisionType _collisionType;
    public CollisionType CollisionType => _collisionType;

    protected float _scale = 1f;
    public float Scale => _scale;

    protected Color _color = Color.White;
    public Color Color => _color;

    protected Entity _parent = null;
    public Entity Parent => _parent;

    protected Entity _children = null;
    public Entity Children => _children;

    protected Entity _interactionTarget = null;
    public Entity InteractionTarget => _interactionTarget;

    public bool PendingDestroy = false;

    public Entity(string name)
    {
        _entityName = name;
    }

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
            Vector2 pos = new Vector2(_position.X,_position.Y);
            if (_parent != null)
            {
                pos += _relativePosition;
            }
            spriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)pos.X, (int)pos.Y, (int)(_collider.Width * _collider.Scale), (int)(_collider.Height * _collider.Scale)), new Color(0, 255, 0, 100));
        }

        if (_animatedSprite != null)
        {
            _animatedSprite.Draw(spriteBatch, _position, _scale);
        }
        if (_sprite != null)
        {
            _sprite.Draw(spriteBatch, _position, _scale);
        }

        if (Debug.DRAW_AABB)
        {
            spriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)_position.X, (int)_position.Y, 5, 5), Color.Red);

            if (_collider != null)
            {
                spriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)_collider.X, (int)_collider.Y, 5, 5), Color.Orange);
            }
        }
    }

    public virtual void SetPosition(Vector2 position)
    {
        if (_parent != null)
        {
            _position = position + _relativePosition;
            if (_collider != null)
            {
                _collider.X = _position.X;
                _collider.Y = _position.Y;
            }
            return;
        }
        _position = position;
        if (_collider != null)
        {
            _collider.X = _position.X;
            _collider.Y = _position.Y;
        }
        if (_children != null)
        {
            _children.SetPosition(position);
        }
    }

    public virtual void SetPosition(float x, float y)
    {
        if (_parent != null)
        {
            _position = new Vector2(x, y) + _relativePosition;
            if (_collider != null)
            {
                _collider.X = _position.X;
                _collider.Y = _position.Y;
            }
            return;
        }
        _position = new Vector2(x, y);
        if (_collider != null)
        {
            _collider.X = _position.X;
            _collider.Y = _position.Y;
        }
        if (_children != null)
        {
            _children.SetPosition(_position);
        }
    }

    public virtual void SetRelativePosition(float x, float y)
    {
        _relativePosition = new Vector2(x, y);
        _position += _relativePosition;
    }

    public virtual void SetScale(float scale)
    { 
        _scale = scale;
        if (_collider != null)
        {
            _collider.Scale = scale;
        }
    }

    public virtual void SetColor(Color color)
    {
        _color = color;
    }

    public virtual void OnCollide(Entity other)
    {

    }

    public void AttachTo(Entity entity)
    {
        entity._children = this;
        _parent = entity;
    }

    public void Detach()
    {
        if (_parent  != null)
        {
            _parent._children = null;
            _parent = null;
        }
    }

    public void SetWantToInteract(bool newState)
    {
        _wantToInteract = newState;
    }

    public void SetInteractionTarget(Entity target)
    {
        _interactionTarget = target;
    }

    public virtual void InteractWithTarget()
    {
        if (_interactionTarget != null)
        {
            Debug.Log("interact with target");
        }
    }
}
