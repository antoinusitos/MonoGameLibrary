using Microsoft.Xna.Framework;
using MonoGameLibrary.Entities;
using MonoGameLibrary.Shapes;
using System.Collections.Generic;

namespace MonoGameLibrary.Misc;

public class Trigger : Entity
{
    public delegate void OnTriggerEventDelegate(Entity other);
    public OnTriggerEventDelegate onTriggerEnter;
    public OnTriggerEventDelegate onTriggerStay;
    public OnTriggerEventDelegate onTriggerExit;

    public delegate void OnCollideEventDelegate(Entity other);
    public OnCollideEventDelegate onCollisionEnter;

    public List<Entity> entitiesThisFrame = new List<Entity>();
    public List<Entity> entities = new List<Entity>();

    public Trigger(string name) : base(name)
    {
    }

    public override void Initialize()
    {
        base.Initialize();

        canCollide = true;
        isTrigger = true;
        canRender = true;
        collisionType = CollisionType.DYNAMIC;

        collider = new Box(
            (int)(position.X),
            (int)(position.Y),
            64,
            64
        );
    }

    public override void OnCollide(Entity other)
    {
        base.OnCollide(other);

        onCollisionEnter?.Invoke(other);
    }

    public void UpdateTrigger()
    {
        for (int i = 0; i < entitiesThisFrame.Count; i++)
        {
            if (!entities.Contains(entitiesThisFrame[i]))
            {
                onTriggerEnter?.Invoke(entitiesThisFrame[i]);
            }
            else if (entities.Contains(entitiesThisFrame[i]))
            {
                onTriggerStay?.Invoke(entitiesThisFrame[i]);
            }
        }
        for (int i = 0; i < entities.Count; i++)
        {
            if (!entitiesThisFrame.Contains(entities[i]))
            {
                onTriggerExit?.Invoke(entities[i]);
            }
        }

        entities.Clear();
        for (int i = 0; i < entitiesThisFrame.Count; i++)
        {
            entities.Add(entitiesThisFrame[i]);

        }
        entitiesThisFrame.Clear();
    }

    public void SetTriggerSize(Vector2 size)
    {
        collider.Width = size.X;
        collider.Height = size.Y;
    }
}
