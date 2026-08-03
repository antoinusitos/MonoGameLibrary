using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.EngineDebug;

namespace MonoGameLibrary.Graphics.Material;

public class Material : IInspectableItem
{
    private uint id;

    private Dictionary<string, EffectParameter> parameterLookup;
    #if DEBUG
    private Dictionary<string, MaterialParameterAnnotation> parameterAnnotations = new();

    public Dictionary<string, MaterialParameterAnnotation> ParameterAnnotations => parameterAnnotations;
#endif

    private Effect effect;

    public Effect Effect => effect;

    public string Name => effect.Name;
    public uint ItemID => id;

    public Material(Effect effect, uint id)
    {
        this.effect = effect;
        this.id = id;
        UpdateParameterCache();
#if DEBUG
        MaterialUtilities.LoadParameterAnnotations(effect.Name, parameterLookup, parameterAnnotations);
#endif
    }

    private void UpdateParameterCache()
    {
        parameterLookup = effect.Parameters.ToDictionary(p => p.Name);
    }

    public bool HasParameter(string name) => TryGetParameter(name, out _);

    public bool TryGetParameter(string name, out EffectParameter parameter)
    {
        return parameterLookup.TryGetValue(name, out parameter);
    }

    public void SetParameter(string name, float value)
    {
        if (TryGetParameter(name, out var parameter))
        {
            parameter.SetValue(value);
        }
        else
        {
            Debug.LogWarning($"cannot set parameter=[{name}] as it does not exist in the shader=[{Effect.Name}]");
        }
    }

    public void SetParameter(string name, RenderTarget2D value)
    {
        if (TryGetParameter(name, out var parameter))
        {
            parameter.SetValue(value);
        }
        else
        {
            Debug.LogWarning($"cannot set parameter=[{name}] as it does not exist in the shader=[{Effect.Name}]");
        }
    }

    public void SetParameter(string name, Vector4 value)
    {
        if (TryGetParameter(name, out var parameter))
        {
            parameter.SetValue(value);
        }
        else
        {
            Debug.LogWarning($"cannot set parameter=[{name}] as it does not exist in the shader=[{Effect.Name}]");
        }
    }

    public static implicit operator Material(Effect effect)
    {
        MaterialManager.TryGetMaterial(effect.Name, out Material material);
        return material?.Effect;
    }

    public static implicit operator Effect(Material material) => material.Effect;
    
    public void DrawProperties()
    {
#if DEBUG
        foreach ((string key, EffectParameter prop) in parameterLookup)
        {
            if (parameterAnnotations.TryGetValue(key, out MaterialParameterAnnotation annotation) &&
                annotation.Name == "hidden")
            {
                continue;
            }
            
            switch (prop.ParameterType, prop.ParameterClass)
            {
                case (EffectParameterType.Single, EffectParameterClass.Scalar):
                    ImGui.AlignTextToFramePadding();
                    this.DrawScalarProperty(prop);
                    break;

                case (EffectParameterType.Single, EffectParameterClass.Vector):
                    ImGui.AlignTextToFramePadding();
                    ImGui.Text(key);

                    ImGui.Indent();
                    this.DrawVectorProperty(prop);
                    ImGui.Unindent();
                    break;

                case (EffectParameterType.Texture2D, EffectParameterClass.Object):
                    ImGui.AlignTextToFramePadding();
                    ImGui.Text(key);
                    ImGui.SameLine();

                    var texture = prop.GetValueTexture2D();
                    if (texture != null)
                    {
                        var texturePtr = Core.ImGuiRenderer.BindTexture(texture);
                        ImGui.Image(texturePtr, new System.Numerics.Vector2(texture.Width, texture.Height));
                    }
                    else
                    {
                        ImGui.Text("(null)");
                    }
                    break;
                default:
                    ImGui.AlignTextToFramePadding();
                    ImGui.Text(key);
                    ImGui.SameLine();
                    ImGui.Text($"(unsupported {prop.ParameterType}, {prop.ParameterClass})");
                    break;
            }
        }
        #endif
    }
}