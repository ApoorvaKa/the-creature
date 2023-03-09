using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class organismSpawner : MonoBehaviour
{
    [Header("Organism")]
    [SerializeField]
    private GameObject organism;
    [Space(2)]

    [Header("Spawn System")]
    [SerializeField]
    private float spawnTimer = 0f;

    public Tilemap tileMap = null;

    public List<Vector3> availablePlaces;

    private bool spawned = false;

    public GameObject player;
    private bool movePlayer = false;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //InvokeRepeating("SpawnOrg", spawnTimer, 30f);


    }

    private void SpawnOrg()
    {
        if (!spawned)
        {
            for (int i = 0; i < availablePlaces.Count; i++)
            {
                if (!movePlayer)
                {
                    player.transform.position = new Vector3(availablePlaces[i+30].x + 0.5f, availablePlaces[i+30].y + 0.5f, availablePlaces[i+30].z);
                    movePlayer = true;
                    continue;
                }
                int rand = Random.Range(0, 100);
                if (rand == 99) { 
                    Instantiate(organism, new Vector3(availablePlaces[i].x + 0.5f, availablePlaces[i].y + 0.5f, availablePlaces[i].z), Quaternion.identity);
                }
            }
            spawned = true;
        }
    }

    public void resetSpawner()
    {
        spawned = false;
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.1f);
        tileMap = transform.parent.GetComponent<Tilemap>();
        availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)tileMap.transform.position.y);
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
        SpawnOrg();
    }


}

 