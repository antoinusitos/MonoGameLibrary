using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.Graphics;
using System;
using System.Collections.Generic;

namespace MonoGameLibrary.Managers;

public class RessourceManager
{
    internal static RessourceManager s_instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static RessourceManager Instance => s_instance;

    private Dictionary<string, TextureRegion> _loadedTextureRegions = new Dictionary<string, TextureRegion>();
    private Dictionary<string, SoundEffect> _loadedSoundEffects = new Dictionary<string, SoundEffect>();
    private Dictionary<string, TextureAtlas> _loadedTextureAtlas = new Dictionary<string, TextureAtlas>();
    private Dictionary<string, Tilemap> _loadedTilemap = new Dictionary<string, Tilemap>();
    private Dictionary<string, SpriteFont> _loadedSpriteFont = new Dictionary<string, SpriteFont>();
    private Dictionary<string, Texture2D> _loadedTexture2D = new Dictionary<string, Texture2D>();
    private Dictionary<string, Song> _loadedSong = new Dictionary<string, Song>();
    private Dictionary<string, Sprite> _loadedSprite = new Dictionary<string, Sprite>();

    public RessourceManager()
    {
        // Ensure that multiple cores are not created.
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a single RessourceManager instance can be created");
        }

        // Store reference to engine for global member access.
        s_instance = this;
    }

    public TextureAtlas GetOrAddTextureAtlas(string name)
    {
        if (_loadedTextureAtlas.ContainsKey(name))
        {
            return _loadedTextureAtlas[name];
        }
        else
        {
            TextureAtlas atlas = TextureAtlas.FromFile(Core.Content, name);
            _loadedTextureAtlas.Add(name, atlas);
            return atlas;
        }
    }

    public SoundEffect GetOrAddSoundEffect(string name)
    {
        if (_loadedSoundEffects.ContainsKey(name))
        {
            return _loadedSoundEffects[name];
        }
        else
        {
            SoundEffect sound = Core.Content.Load<SoundEffect>(name);
            _loadedSoundEffects.Add(name, sound);
            return sound;
        }
    }


    public TextureRegion GetOrAddTextureRegion(string name, TextureAtlas atlas)
    {
        if (_loadedTextureRegions.ContainsKey(name))
        {
            return _loadedTextureRegions[name];
        }
        else
        {
            TextureRegion textureRegion = atlas.GetRegion(name);
            _loadedTextureRegions.Add(name, textureRegion);
            return textureRegion;
        }
    }

    public Tilemap GetOrAddTilemap(string name)
    {
        if (_loadedTilemap.ContainsKey(name))
        {
            return _loadedTilemap[name];
        }
        else
        {
            Tilemap tileMap = Tilemap.FromFile(Core.Content, name);
            _loadedTilemap.Add(name, tileMap);
            return tileMap;
        }
    }

    public SpriteFont GetOrAddSpriteFont(string name)
    {
        if (_loadedSpriteFont.ContainsKey(name))
        {
            return _loadedSpriteFont[name];
        }
        else
        {
            SpriteFont spriteFont = Core.Content.Load<SpriteFont>(name);
            _loadedSpriteFont.Add(name, spriteFont);
            return spriteFont;
        }
    }

    public Texture2D GetOrAddTexture2D(string name)
    {
        if (_loadedTexture2D.ContainsKey(name))
        {
            return _loadedTexture2D[name];
        }
        else
        {
            Texture2D texture2D = Core.Content.Load<Texture2D>(name);
            _loadedTexture2D.Add(name, texture2D);
            return texture2D;
        }
    }

    public Song GetOrAddSong(string name)
    {
        if (_loadedSong.ContainsKey(name))
        {
            return _loadedSong[name];
        }
        else
        {
            Song song = Core.Content.Load<Song>(name);
            _loadedSong.Add(name, song);
            return song;
        }
    }

    public Sprite GetOrAddSprite(string name, TextureAtlas atlas)
    {
        if (_loadedSprite.ContainsKey(name))
        {
            return _loadedSprite[name];
        }
        else
        {
            Sprite sprite = atlas.CreateSprite(name);
            _loadedSprite.Add(name, sprite);
            return sprite;
        }
    }
}
