using MonoGameLibrary.Interfaces;

namespace MonoGameLibrary.Entities;

public class Character : Entity
{
    protected IInteractable interactionTarget = null;
    public IInteractable InteractionTarget => interactionTarget;

    public Character(string name) : base(name)
    {

    }

    public void SetInteractionTarget(IInteractable target)
    {
        interactionTarget = target;
    }

    public virtual void InteractWithTarget()
    {
        if (interactionTarget != null)
        {
            interactionTarget.OnInteract(this);
        }
    }
}
