using MonoGameLibrary.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Managers;

public class UIManager
{
    internal static UIManager instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static UIManager Instance => instance;

    public UIManager()
    {
        // Ensure that multiple cores are not created.
        if (instance != null)
        {
            throw new InvalidOperationException($"Only a single UIManager instance can be created");
        }

        // Store reference to engine for global member access.
        instance = this;
    }

    public UIEntity currentUIEntity;
}
