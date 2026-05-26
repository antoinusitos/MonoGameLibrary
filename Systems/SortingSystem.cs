using MonoGameLibrary.Entities;
using MonoGameLibrary.Managers;

namespace MonoGameLibrary.Systems;

public class SortingSystem : GameSytem
{
    public override void Update(float deltaTime)
    {
        if (SceneManager.Instance.IsDirty)
        {
            RegisterManager.Instance.registeredRenderers.Sort(CompareEntity);
            SceneManager.Instance.SetIsDirty(false);
        }
    }

    private int CompareEntity(Entity x, Entity y)
    {
        if (x == null)
        {
            if (y == null)
            {
                // If x is null and y is null, they're
                // equal.
                return 0;
            }
            else
            {
                // If x is null and y is not null, y
                // is greater.
                return -1;
            }
        }
        else
        {
            // If x is not null...
            //
            if (y == null)
            // ...and y is null, x is greater.
            {
                return 1;
            }
            else
            {
                // ...and y is not null, compare the
                // lengths of the two strings.
                //
                int retval = x.Layer.CompareTo(y.Layer);

                if (retval != 0)
                {
                    // If the strings are not of equal length,
                    // the longer string is greater.
                    //
                    return retval;
                }
                else
                {
                    // If the strings are of equal length,
                    // sort them with ordinary string comparison.
                    //
                    return x.Position.Y.CompareTo(y.Position.Y);
                }
            }
        }
    }

}
