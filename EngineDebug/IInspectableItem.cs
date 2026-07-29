namespace MonoGameLibrary.EngineDebug;

public interface IInspectableItem
{
    string Name { get; }
    uint ItemID { get; }

    void DrawProperties();
}