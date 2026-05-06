using MonoGameLibrary.Shapes;

namespace MonoGameLibrary.Misc;

public class Trigger : Entity
{
    public delegate void OnTriggerEventDelegate(Entity other);
    public OnTriggerEventDelegate onTriggerEvent;

    public Trigger(string name) : base(name)
    {
    }

    public override void Initialize()
    {
        base.Initialize();

        _canCollide = true;
        _isTrigger = true;
        _canRender = true;
        _collisionType = CollisionType.STATIC;

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
        onTriggerEvent?.Invoke(other);
    }
}
