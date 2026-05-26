using MonoGameLibrary.Interfaces;

namespace MonoGameLibrary.Entities;

public class Character : Entity
{
    protected IInteractable _interactionTarget = null;
    public IInteractable InteractionTarget => _interactionTarget;

    public Character(string name) : base(name)
    {

    }

    public void SetInteractionTarget(IInteractable target)
    {
        _interactionTarget = target;
    }

    public virtual void InteractWithTarget()
    {
        if (_interactionTarget != null)
        {
            _interactionTarget.OnInteract(this);
        }
    }
}
