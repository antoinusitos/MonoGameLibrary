using MonoGameLibrary.Shapes;
using System.Collections.Generic;

namespace MonoGameLibrary.Misc;

public class Trigger : Entity
{
    public delegate void OnTriggerEventDelegate(Entity other);
    public OnTriggerEventDelegate onTriggerEnter;
    public OnTriggerEventDelegate onTriggerStay;
    public OnTriggerEventDelegate onTriggerExit;

    public List<Entity> entitiesThisFrame = new List<Entity>();
    public List<Entity> entities = new List<Entity>();

    public Trigger(string name) : base(name)
    {
    }

    public override void Initialize()
    {
        base.Initialize();

        _canCollide = true;
        _isTrigger = true;
        _canRender = true;
        _collisionType = CollisionType.DYNAMIC;

        _collider = new Box(
            (int)(_position.X),
            (int)(_position.Y),
            64,
            64
        );
    }

    public override void OnCollide(Entity other)
    {
        base.OnCollide(other);
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
}
