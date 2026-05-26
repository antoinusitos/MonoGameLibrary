using MonoGameLibrary.Entities;

namespace MonoGameLibrary.Interfaces;

public interface IInteractable
{
    public abstract bool CanBeInteracted();

    public abstract void OnInteract(Entity interactor);

    public abstract Entity GetEntity();
}
