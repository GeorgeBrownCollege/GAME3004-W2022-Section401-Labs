using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Player References")] 
    public Transform player;
    public Transform spawnPoint;

    [Header("World Properties")] 
    [Range(8, 64)]
    public int height = 8;
    [Range(8, 64)]
    public int width = 8;
    [Range(8, 64)]
    public int depth = 8;

    [Header("Scaling Values")]
    [Range(8, 64)]
    public float min = 16.0f;
    [Range(8, 64)]
    public float max = 24.0f;

    [Header("Tile Properties")] 
    public Transform tileParent;
    public GameObject threeDTile;

    [Header("Grid")] 
    public List<GameObject> grid;

    private int startHeight;
    private int startWidth;
    private int startDepth;
    private float startMin;
    private float startMax;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (height != startHeight || depth != startDepth || width != startWidth || min != startMin || max != startMax)
        {
            Generate();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Generate();
        }
    }

    private void Generate()
    {
        Initialize();
        ResetMap();
        Regenerate();
        //DisableCollidersAndMeshRenderers();
        RemoveInternalTiles();
        PositionPlayer();
    }

    private void Initialize()
    {
        startHeight = height;
        startDepth = depth;
        startWidth = width;
        startMin = min;
        startMax = max;
    }

    private void Regenerate()
    {
        // world generation happens here
        float randomScale = Random.Range(min, max);
        float offsetX = Random.Range(-1024.0f, 1024.0f);
        float offsetZ = Random.Range(-1024.0f, 1024.0f);

        for (int y = 0; y < height; y++)
        {
            for (int z = 0; z < depth; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    var perlinValue = Mathf.PerlinNoise((x + offsetX) / randomScale, (z + offsetZ) / randomScale) * depth * 0.5f;

                    if (y < perlinValue)
                    {
                        var tile = Instantiate(threeDTile, new Vector3(x, y, z), Quaternion.identity);
                        tile.transform.SetParent(tileParent);
                        grid.Add(tile);

                    }
                }
            }
        }
    }

    private void ResetMap()
    {
        var size = grid.Count;

        for (int i = 0; i < size; i++)
        {
            grid[i].GetComponent<BoxCollider>().enabled = false;
            Destroy(grid[i]);
        }

        grid.Clear();
    }

    private void DisableCollidersAndMeshRenderers()
    {
        // detect if each tile has "contacts" with each face around
        var normalArray = new Vector3[] {Vector3.up, Vector3.down, Vector3.right, Vector3.left, Vector3.forward, Vector3.back};
        List<GameObject> disabledTiles = new List<GameObject>();

        // for each tile in the grid mark the tiles that are internal and add them to the disabledTiles list
        foreach (var tile in grid)
        {
            int collisionCounter = 0;
            for (int i = 0; i < normalArray.Length; i++)
            {
                if (Physics.Raycast(tile.transform.position, normalArray[i], tile.transform.localScale.magnitude * 0.3f))
                {
                    collisionCounter++;
                }
            }

            if (collisionCounter > 5)
            {
                disabledTiles.Add(tile);
            }
        }

        foreach (var tile in disabledTiles)
        {
            var boxCollider = tile.GetComponent<BoxCollider>();
            var meshRenderer = tile.GetComponent<MeshRenderer>();

            boxCollider.enabled = false;
            meshRenderer.enabled = false;
        }
    }

    private void RemoveInternalTiles()
    {
        // detect if each tile has "contacts" with each face around
        var normalArray = new Vector3[] { Vector3.up, Vector3.down, Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
        List<GameObject> tilesToBeRemoved = new List<GameObject>();

        // for each tile in the grid mark the tiles that are internal and add them to the disabledTiles list
        foreach (var tile in grid)
        {
            int collisionCounter = 0;
            for (int i = 0; i < normalArray.Length; i++)
            {
                if (Physics.Raycast(tile.transform.position, normalArray[i], tile.transform.localScale.magnitude * 0.3f))
                {
                    collisionCounter++;
                }
            }

            if (collisionCounter > 5)
            {
                tilesToBeRemoved.Add(tile);
            }
        }

        var size = tilesToBeRemoved.Count;
        for (int i = 0; i < size; i++)
        {
            grid.Remove(tilesToBeRemoved[i]);
            Destroy(tilesToBeRemoved[i].gameObject);
        }
    }

    private void PositionPlayer()
    {
        player.gameObject.GetComponent<CharacterController>().enabled = false;
        player.position = new Vector3(width * 0.5f, height + 5.0f, depth * 0.5f);
        spawnPoint.position = player.position;
        player.gameObject.GetComponent<CharacterController>().enabled = true;
    }
}
