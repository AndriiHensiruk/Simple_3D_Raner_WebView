using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> activeTiles;
    public GameObject[] tilePrefabs;

    public float tileLength = 30;
    public int numberOfTiles = 3;
    public int totalNumOfTiles = 5;
    private bool finish = false;
    public float zSpawn = 0;

    [SerializeField] private Transform playerTransform;

    private int previousIndex;

    void Start()
    {
        activeTiles = new List<GameObject>();
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, totalNumOfTiles));
        }

        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void Update()
    {
        if (playerTransform.position.z - 30 >= zSpawn - (numberOfTiles * tileLength))
        {
            if (!finish)
            {
                int index = Random.Range(0, totalNumOfTiles);
                SpawnTile(index);
            }
            
            DeleteTile();
            
        }

        if (GameManager.inst.ShowScore() ==  4)
        {
            SpawnTile(totalNumOfTiles);
            finish = true;
        }

    }

    public void SpawnTile(int index)
    {

        GameObject go = Instantiate(tilePrefabs[index], transform.forward * zSpawn, transform.rotation);
            activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        GameManager.inst.IncrementScore();
    }
}