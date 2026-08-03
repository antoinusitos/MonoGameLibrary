using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using ImGuiNET;
using MonoGameLibrary.Entities;
using MonoGameLibrary.Graphics.Material;
using MonoGameLibrary.Managers;

namespace MonoGameLibrary.EngineDebug;

public class DataDebugger
{
    private List<IInspectableItem> inspectedItems = new();
    private Dictionary<string, List<Entity>> sortedEntities = new();

    private static bool showConsole = false;
    private static bool showEntities = false;

    private static DataDebugger instance;

    public static DataDebugger Instance => instance;

    public static void Initialize()
    {
        instance = new();
        RegisterManager.Instance.OnEntityRegistered += OnEntityRegistered;
        RegisterManager.Instance.OnEntityUnregistered += OnEntityUnregistered;
    }

    private static void OnEntityRegistered(Entity entity)
    {
        // We consider that the root must have been added somehow
        if (entity.Parent != null)
        {
            return;
        }
        if (!Instance.sortedEntities.TryGetValue(entity.EntityGroup, out List<Entity> list))
        {
            list = new List<Entity>();
            Instance.sortedEntities[entity.EntityGroup] = list;
        }

        if (!list.Contains(entity))
        {
            list.Add(entity);
        }
    }
    
    private static void OnEntityUnregistered(Entity entity)
    {
        if (entity.Parent != null)
        {
            return;
        }
        if (!Instance.sortedEntities.TryGetValue(entity.EntityGroup, out List<Entity> list))
        {
            Debug.LogWarning($"Entity {entity.ID} - {entity.EntityName} was not in group {entity.EntityGroup}.");
            return;
        }

        list.Remove(entity);
    }

    public static void CustomGUI()
    {
        MenuBar();
        DisplayEntityList();
        DisplayInspectedItems();
        DisplayMaterialList();
        DisplayConsole();
    }

    private static void MenuBar()
    {
        ImGui.SetNextWindowSize(new Vector2(Core.realWidth, 20));
        ImGui.SetNextWindowPos(Vector2.Zero);
        if (ImGui.Begin("Menu", ImGuiWindowFlags.MenuBar))
        {
            if (ImGui.BeginMenuBar())
            {
                if (ImGui.BeginMenu("Fichier"))
                {
                    if (ImGui.MenuItem("Hide ImGui")) { Debug.DRAW_IMGUI = false; }
                    if (ImGui.MenuItem("Quitter")) { Core.instance.Exit(); }
                    ImGui.EndMenu();
                }
                if (ImGui.MenuItem("Console", "Ctrl+P")) { showConsole = true; }
                if (ImGui.MenuItem("Entities", "Ctrl+E")) { showEntities = true; }
                ImGui.EndMenuBar();
            }
        }
        ImGui.End();
    }

    private static void DisplayConsole()
    {
        if (!showConsole)
        {
            return;
        }    

        ImGui.Begin("Console", ref showConsole);
        foreach (string line in Debug.debugLines)
        {
            ImGui.Text(line);
        }
        ImGui.End();
    }

    private static void DisplayEntityList()
    {
        if (!showEntities)
        {
            return;
        }

        string windowName = "Entities";
        ImGui.Begin(windowName, ref showEntities);
        ImGUIUtilities.SetWindowSizeToDefault(windowName);
        foreach ((string group, List<Entity> entities) in Instance.sortedEntities)
        {
            if (ImGui.TreeNode(group))
            {
                DisplayEntityListInHierarchy(entities);
                ImGui.TreePop();
            }
        }
        ImGui.End();
    }

    private static void DisplayEntityListInHierarchy(List<Entity> entities)
    {
        foreach (Entity entity in entities)
        {
            if (entity.Children.Count != 0)
            {
                bool isTreeNodeFoldout = ImGui.TreeNode(entity.EntityName);
                DisplayItemContextButton(entity);
                if (isTreeNodeFoldout)
                {
                    DisplayEntityListInHierarchy(entity.Children);
                    ImGui.TreePop();
                }
            }
            else
            {
                ImGui.Text(entity.EntityName);
                DisplayItemContextButton(entity);
            }
        }
    }
    
    private static void DisplayInspectedItems()
    {
        for (var i = Instance.inspectedItems.Count - 1; i >= 0; i--)
        {
            IInspectableItem inspectedItem = Instance.inspectedItems[i];
            DrawInspectedItem(inspectedItem, i);
        }
    }

    private static void DrawInspectedItem(IInspectableItem inspectedItem, int index)
    {
        string windowName = $"Inspector: {inspectedItem.Name}##{inspectedItem.ItemID}";
        ImGui.Begin(windowName);
        ImGUIUtilities.SetWindowSizeToDefault(windowName);
        
        if (ImGui.SmallButton($"x##{inspectedItem.ItemID}"))
        {
            Instance.inspectedItems.RemoveAt(index);
        }
        inspectedItem.DrawProperties();
        ImGui.End();
    }
    
    private static void DisplayMaterialList()
    {
        string windowName = "Materials";
        ImGui.Begin(windowName);
        ImGUIUtilities.SetWindowSizeToDefault(windowName);
        foreach ((string materialName, Material material) in MaterialManager.Instance.Materials)
        {
            ImGui.Text(material.Name);
            DisplayItemContextButton(material);
        }
        ImGui.End();
    }
    
    private static void DisplayItemContextButton(IInspectableItem item)
    {
        ImGui.SameLine();
        bool containsEntity = Instance.inspectedItems.Contains(item);
        string label = containsEntity ? "x" : "o";
        if (ImGui.SmallButton($"{label}##{item.ItemID}"))
        {
            if (containsEntity)
            {
                Instance.inspectedItems.Remove(item);
            }
            else
            {
                Instance.inspectedItems.Add(item);
            }
        }
    }
}