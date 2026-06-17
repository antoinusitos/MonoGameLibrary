using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;

namespace MonoGameLibrary.Managers;

public class RessourceManager
{
    internal static RessourceManager instance;

    /// <summary>
    /// Gets a reference to the Core instance.
    /// </summary>
    public static RessourceManager Instance => instance;

    private Dictionary<string, TextureRegion> loadedTextureRegions = new Dictionary<string, TextureRegion>();
    private Dictionary<string, SoundEffect> loadedSoundEffects = new Dictionary<string, SoundEffect>();
    private Dictionary<string, TextureAtlas> loadedTextureAtlas = new Dictionary<string, TextureAtlas>();
    private Dictionary<string, Tilemap> loadedTilemap = new Dictionary<string, Tilemap>();
    private Dictionary<string, SpriteFont> loadedSpriteFont = new Dictionary<string, SpriteFont>();
    private Dictionary<string, Texture2D> loadedTexture2D = new Dictionary<string, Texture2D>();
    private Dictionary<string, Song> loadedSong = new Dictionary<string, Song>();
    private Dictionary<string, Sprite> loadedSprite = new Dictionary<string, Sprite>();

    public RessourceManager()
    {
        // Ensure that multiple cores are not created.
        if (instance != null)
        {
            throw new InvalidOperationException($"Only a single RessourceManager instance can be created");
        }

        // Store reference to engine for global member access.
        instance = this;
    }

    public TextureAtlas GetOrAddTextureAtlas(string name)
    {
        if (loadedTextureAtlas.ContainsKey(name))
        {
            return loadedTextureAtlas[name];
        }
        else
        {
            TextureAtlas atlas = TextureAtlas.FromFile(Core.Content, name);
            loadedTextureAtlas.Add(name, atlas);
            return atlas;
        }
    }

    public SoundEffect GetOrAddSoundEffect(string name)
    {
        if (loadedSoundEffects.ContainsKey(name))
        {
            return loadedSoundEffects[name];
        }
        else
        {
            SoundEffect sound = Core.Content.Load<SoundEffect>(name);
            loadedSoundEffects.Add(name, sound);
            return sound;
        }
    }


    public TextureRegion GetOrAddTextureRegion(string name, TextureAtlas atlas)
    {
        if (loadedTextureRegions.ContainsKey(name))
        {
            return loadedTextureRegions[name];
        }
        else
        {
            TextureRegion textureRegion = atlas.GetRegion(name);
            loadedTextureRegions.Add(name, textureRegion);
            return textureRegion;
        }
    }

    public Tilemap GetOrAddTilemap(string name, out TilemapJSON tilemapJSON, string atlas = "")
    {
        Tilemap tileMap = Tilemap.FromJsonFile(Core.Content, name, atlas, out tilemapJSON);
        //Tilemap tileMap = Tilemap.FromFile(Core.Content, name, atlas);

        //_loadedTilemap.Add(name, tileMap);
        return tileMap;

        /*if (_loadedTilemap.ContainsKey(name))
        {
            return _loadedTilemap[name];
        }
        else
        {
            Tilemap tileMap = Tilemap.FromFile(Core.Content, name, atlas);
            _loadedTilemap.Add(name, tileMap);
            return tileMap;
        }*/
    }

    public SpriteFont GetOrAddSpriteFont(string name)
    {
        if (loadedSpriteFont.ContainsKey(name))
        {
            return loadedSpriteFont[name];
        }
        else
        {
            SpriteFont spriteFont = Core.Content.Load<SpriteFont>(name);
            loadedSpriteFont.Add(name, spriteFont);
            return spriteFont;
        }
    }

    public Texture2D GetOrAddTexture2D(string name)
    {
        if (loadedTexture2D.ContainsKey(name))
        {
            return loadedTexture2D[name];
        }
        else
        {
            Texture2D texture2D = Core.Content.Load<Texture2D>(name);
            loadedTexture2D.Add(name, texture2D);
            return texture2D;
        }
    }

    public Song GetOrAddSong(string name)
    {
        if (loadedSong.ContainsKey(name))
        {
            return loadedSong[name];
        }
        else
        {
            Song song = Core.Content.Load<Song>(name);
            loadedSong.Add(name, song);
            return song;
        }
    }

    public Sprite GetOrAddSprite(string name, TextureAtlas atlas)
    {
        if (loadedSprite.ContainsKey(name))
        {
            return loadedSprite[name];
        }
        else
        {
            Sprite sprite = atlas.CreateSprite(name);
            loadedSprite.Add(name, sprite);
            return sprite;
        }
    }
}
