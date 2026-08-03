using System;
using System.Collections.Generic;
using System.IO;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.EngineDebug;

namespace MonoGameLibrary.Graphics.Material;

public static class MaterialUtilities
{
    private const string annotationMarker = "// @editor";
    
    public static void LoadParameterAnnotations(string name, Dictionary<string, EffectParameter> parameterLookup, Dictionary<string, MaterialParameterAnnotation> parameterAnnotations)
    {
        string path = Path.Combine(
            AppContext.BaseDirectory,
            "Content",
            name + ".fx"
        );

        if (!File.Exists(path))
        {
            Debug.LogWarning($"Effect source not found: {path}");
            return;
        }

        string[] lines = File.ReadAllLines(path);
        for (var i = 0; i < lines.Length; i++)
        {
            if (i + 1 > lines.Length)
            {
                continue;
            } 
            if (!lines[i].StartsWith(annotationMarker))
            {
                continue;
            }

            string annotation = lines[i].Replace(annotationMarker, "").ToLower().Trim();
            string nextLine = lines[i + 1];
            foreach ((string parameterName, _) in parameterLookup)
            {
                if (!nextLine.Contains(parameterName))
                {
                    continue;
                }

                parameterAnnotations[parameterName] = new MaterialParameterAnnotation()
                {
                    Name = annotation,
                };
            }
        }
    }
    
    public static void DrawVectorProperty(this Material material, EffectParameter property)
    {
        bool hasChanged = false;
        var annotation = material.ParameterAnnotations.GetValueOrDefault(property.Name);
        if (annotation.Name == "color")
        {
            Color color = new Color(property.GetValueVector4());
            if (ImGUIUtilities.InputColor(ref color, property.Name))
            {
                property.SetValue(color.ToVector4());
            }
            return;
        }
        
        Vector4 value = property.GetValueVector4();
        if (property.ColumnCount >= 1)
        {
            hasChanged |= DrawScalar(annotation, property.Name, "X", ref value.X);
        }
        if (property.ColumnCount >= 2)
        {
            hasChanged |= DrawScalar(annotation, property.Name, "Y", ref value.Y);
        }
        if (property.ColumnCount >= 3)
        {
            hasChanged |= DrawScalar(annotation, property.Name, "Z", ref value.Z);
        }
        if (property.ColumnCount >= 4)
        {
            hasChanged |= DrawScalar(annotation, property.Name, "W", ref value.W);
        }

        if (hasChanged)
        {
            property.SetValue(value);
        }
    }
    
    public static void DrawScalarProperty(this Material material, EffectParameter property)
    {
        var value = property.GetValueSingle();
        if (DrawScalar(material.ParameterAnnotations.GetValueOrDefault(property.Name),property.Name, property.Name, ref value))
        {
            property.SetValue(value);
        }
    }

    public static bool ScalarSlider(string key, ref float value, float min = 0, float max = 1)
    {
        return ImGui.SliderFloat($"##_prop{key}", ref value, min, max);
    }
    
    public static bool ScalarDrag(string key, ref float value)
    {
        return ImGui.DragFloat($"##_prop{key}", ref value, 0.1f);
    }

    public static bool DrawScalar(MaterialParameterAnnotation annotation, string key, string label, ref float value)
    {
        ImGui.Text(label);
        ImGui.SameLine();

        if (annotation.Name == "normalised") {
            return ScalarSlider($"{key}.{label}", ref value);
        }

        return ScalarDrag($"{key}.{label}", ref value);
    }
}