using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Graphics;

public class DoorJSON
{
    public int id { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public int toX { get; set; }
    public int toY { get; set; }
    public string toScene { get; set; }
}

public class NpcJSON
{
    public int id { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public string name { get; set; }
}

public class TilemapJSON
{
    public string region { get; set; }
    public int tileWidth { get; set; }
    public int tileHeight { get; set; }
    public int renderMaxRows { get; set; }
    public int renderMaxCols { get; set; }
    public string name { get; set; }
    public string tileset { get; set; }
    public string[] tiles { get; set; }
    public DoorJSON[] doors { get; set; }
    public NpcJSON[] npcs { get; set; }
    public NpcJSON[] chests { get; set; }
}

public class Tilemap
{
    private readonly Tileset tileset;
    public Tileset Tileset => tileset;
    private readonly int[] tiles;

    public int[] Tiles => tiles;

    /// <summary>
    /// Gets the total number of rows in this tilemap.
    /// </summary>
    public int Rows { get; }

    /// <summary>
    /// Gets the total number of columns in this tilemap.
    /// </summary>
    public int Columns { get; }

    /// <summary>
    /// Gets the total number of tiles in this tilemap.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Gets or Sets the scale factor to draw each tile at.
    /// </summary>
    public Vector2 Scale { get; set; }

    /// <summary>
    /// Gets the width, in pixels, each tile is drawn at.
    /// </summary>
    public float TileWidth => tileset.TileWidth * Scale.X;

    /// <summary>
    /// Gets the height, in pixels, each tile is drawn at.
    /// </summary>
    public float TileHeight => tileset.TileHeight * Scale.Y;

    /// <summary>
    /// Gets the number of rows to render.
    /// </summary>
    public int RenderMaxRows { get; }

    /// <summary>
    /// Gets the number of columns to render.
    /// </summary>
    public int RenderMaxCols { get; }

    /// <summary>
    /// Gets the name of the tilemap.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Creates a new tilemap.
    /// </summary>
    /// <param name="tileset">The tileset used by this tilemap.</param>
    /// <param name="columns">The total number of columns in this tilemap.</param>
    /// <param name="rows">The total number of rows in this tilemap.</param>
    public Tilemap(Tileset tileset, int columns, int rows, int renderMaxCols, int renderMaxRows, string name)
    {
        this.tileset = tileset;
        Rows = rows;
        Columns = columns;
        Count = Columns * Rows;
        Scale = Vector2.One;
        tiles = new int[Count];
        RenderMaxCols = renderMaxCols;
        RenderMaxRows = renderMaxRows;
        Name = name;
    }

    /// <summary>
    /// Sets the tile at the given index in this tilemap to use the tile from
    /// the tileset at the specified tileset id.
    /// </summary>
    /// <param name="index">The index of the tile in this tilemap.</param>
    /// <param name="tilesetID">The tileset id of the tile from the tileset to use.</param>
    public void SetTile(int index, int tilesetID)
    {
        tiles[index] = tilesetID;
    }

    /// <summary>
    /// Sets the tile at the given column and row in this tilemap to use the tile
    /// from the tileset at the specified tileset id.
    /// </summary>
    /// <param name="column">The column of the tile in this tilemap.</param>
    /// <param name="row">The row of the tile in this tilemap.</param>
    /// <param name="tilesetID">The tileset id of the tile from the tileset to use.</param>
    public void SetTile(int column, int row, int tilesetID)
    {
        int index = row * Columns + column;
        SetTile(index, tilesetID);
    }

    /// <summary>
    /// Draws this tilemap using the given sprite batch.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used to draw this tilemap.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        
    }

    public static Tilemap FromJsonFile(ContentManager content, string filename, string atlas, out TilemapJSON tilemapJSONOut)
    {
        string filePath = Path.Combine(content.RootDirectory, filename);
        if (!filePath.EndsWith(".json"))
        {
            tilemapJSONOut = null;
            return null;
        }

        using (StreamReader r = new StreamReader(filePath))
        {
            string json = r.ReadToEnd();
            TilemapJSON? tilemapJson = JsonSerializer.Deserialize<TilemapJSON>(json);

            // Load the texture 2d at the content path
            Texture2D texture = content.Load<Texture2D>(tilemapJson.tileset);

            string[] split = tilemapJson.region.Split(" ");
            int x = int.Parse(split[0]);
            int y = int.Parse(split[1]);
            int width = int.Parse(split[2]);
            int height = int.Parse(split[3]);

            // Create the texture region from the texture
            TextureRegion textureRegion = new TextureRegion(texture, x, y, width, height);

            // Create the tileset using the texture region
            Tileset tileset = new Tileset(textureRegion, tilemapJson.tileWidth, tilemapJson.tileHeight, atlas);

            // The <Tiles> element contains lines of strings where each line
            // represents a row in the tilemap.  Each line is a space
            // separated string where each element represents a column in that
            // row.  The value of the column is the id of the tile in the
            // tileset to draw for that location.
            //
            // Example:
            // <Tiles>
            //      00 01 01 02
            //      03 04 04 05
            //      03 04 04 05
            //      06 07 07 08
            // </Tiles>

            // Split the value of the first row to determine the total number of columns
            int columnCount = width / tilemapJson.tileWidth;
            int rowCount = height / tilemapJson.tileHeight;

            // Create the tilemap
            Tilemap tilemap = new Tilemap(tileset, columnCount, rowCount, tilemapJson.renderMaxCols, tilemapJson.renderMaxRows, tilemapJson.name);

            // Process each row
            for (int row = 0; row < rowCount; row++)
            {
                // Process each column of the current row
                for (int column = 0; column < columnCount; column++)
                {
                    // Get the tileset index for this location
                    int tilesetIndex = int.Parse(tilemapJson.tiles[row * columnCount + column]);

                    // Add that region to the tilemap at the row and column location
                    tilemap.SetTile(column, row, tilesetIndex);

                    Tile tile = tilemap.Tileset.GetTile(column, row);
                    if (tilesetIndex == 1)
                    {
                        tile.SetSpriteName("#");
                        tile.SetCollision(true);
                    }
                    else
                    {
                        tile.SetSpriteName(".");
                    }
                    tile.LoadContent(Core.Content);
                    tile.Initialize();
                    tile.Register();
                    tile.SetScale(0.25f);
                    tile.SetPosition(new Vector2(column * 8, row * 8));
                }
            }

            tilemapJSONOut = tilemapJson;

            return tilemap;
        }
    }
}
