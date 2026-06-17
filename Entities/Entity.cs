using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Managers;
using MonoGameLibrary.Misc;
using MonoGameLibrary.Shapes;
using System.Collections.Generic;

namespace MonoGameLibrary.Entities;

public class Entity
{
    protected bool canUpdate;
    public bool CanUpdate => canUpdate;

    protected bool canInteract;
    public bool CanInteract => canInteract;

    protected bool _wantToInteract;
    public bool WantToInteract => _wantToInteract;
    
    protected bool canRender;
    public bool CanRender => canRender;

    protected bool canCollide;
    public bool CanCollide => canCollide;

    protected bool canMove;
    public bool CanMove => canMove;

    protected string _entityName;
    public string EntityName => _entityName;

    protected bool isParticle;
    public bool IsParticle => isParticle;

    protected bool active = true;
    public bool Active => active;

    protected Vector2 position;
    public Vector2 Position => parent != null ? parent.Position + relativePosition : position;

    protected Vector2 relativePosition;
    public Vector2 RelativePosition => relativePosition;

    protected Vector2 velocity;
    public Vector2 Velocity;// => _velocity;

    protected AnimatedSprite animatedSprite;

    protected Sprite sprite;

    protected Box collider;
    public Box Collider => collider;

    protected bool isTrigger;
    public bool IsTrigger => isTrigger;

    protected CollisionType collisionType;
    public CollisionType CollisionType => collisionType;

    protected float scale = 1f;
    public float Scale => scale;

    protected int layer = 0;
    public int Layer => layer;

    protected Color color = Color.White;
    public Color Color => color;

    public Color DebugColor = new Color(0, 255, 0, 100);

    protected Entity parent = null;
    public Entity Parent => parent;

    protected List<Entity> children = new List<Entity>();
    public List<Entity> Children => children;

    public bool PendingDestroy = false;

    public uint ID = 0;

    protected bool useGravity;
    public bool UseGravity => useGravity;

    protected float mass = 1f;
    public float Mass => mass;

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
        if (animatedSprite != null)
        {
            if (collider != null)
            {
                collider.X = position.X;
                collider.Y = position.Y;
            }

            animatedSprite.Update(deltaTime);
        }
    }

    public void Register()
    {
        RegisterManager.Instance.RegisterEntity(this);
    }

    public virtual void Render(SpriteBatch spriteBatch)
    {
        if (active == false)
        {
            return;
        }

        if (animatedSprite != null)
        {
            animatedSprite.Draw(spriteBatch, position, scale, color);
        }
        if (sprite != null)
        {
            sprite.Draw(spriteBatch, position, scale, color);
        }

        if (canCollide && Debug.DRAW_AABB)
        {
            Vector2 pos = new Vector2(position.X, position.Y);
            if (parent != null)
            {
                pos += relativePosition;
            }
            spriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)pos.X, (int)pos.Y, (int)(collider.Width * collider.Scale), (int)(collider.Height * collider.Scale)), new Rectangle(0, 0, 8, 8), DebugColor, 0, Vector2.Zero, SpriteEffects.None, 0f);
        }

        if (Debug.DRAW_AABB)
        {
            spriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)position.X, (int)position.Y, 5, 5), Color.Red);

            if (collider != null)
            {
                spriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)collider.X, (int)collider.Y, 5, 5), Color.Orange);
            }
        }
    }

    public virtual void SetPosition(Vector2 position)
    {
        if (parent != null)
        {
            this.position = position + relativePosition;
            if (collider != null)
            {
                collider.X = this.position.X;
                collider.Y = this.position.Y;
            }
            return;
        }
        this.position = position;
        if (collider != null)
        {
            collider.X = this.position.X;
            collider.Y = this.position.Y;
        }
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetPosition(position);
        }
    }

    public virtual void SetPosition(float x, float y)
    {
        if (parent != null)
        {
            position = new Vector2(x, y) + relativePosition;
            if (collider != null)
            {
                collider.X = position.X;
                collider.Y = position.Y;
            }
            return;
        }
        position = new Vector2(x, y);
        if (collider != null)
        {
            collider.X = position.X;
            collider.Y = position.Y;
        }
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetPosition(position);
        }
    }

    public virtual void SetRelativePosition(float x, float y)
    {
        relativePosition = new Vector2(x, y);
        position += relativePosition;
    }

    public virtual void SetScale(float scale)
    { 
        this.scale = scale;
        if (collider != null)
        {
            collider.Scale = scale;
        }
    }

    public virtual void SetColor(Color color)
    {
        this.color = color;
    }

    public virtual void OnCollide(Entity other)
    {

    }

    public void AttachTo(Entity entity)
    {
        entity.children.Add(this);
        parent = entity;
    }

    public void Detach()
    {
        if (parent  != null)
        {
            parent.children = null;
            parent = null;
        }
    }

    public void SetWantToInteract(bool newState)
    {
        _wantToInteract = newState;
    }

    public void SetActive(bool active)
    {
        this.active = active;
    }

    public void SetLayer(int newLayer)
    {
        layer = newLayer;
    }

    public void SetCollision(bool newState)
    {
        canCollide = newState;
    }
}
