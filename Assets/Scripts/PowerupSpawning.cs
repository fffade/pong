using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawning : MonoBehaviour
{
    // The bounds to spawn powerups in is the attached collider
    private BoxCollider2D _spawningBounds;
    
    // The prefab powerups this spawner can use
    [SerializeField] private List<GameObject> powerups;
    
    // Whether or not this powerup spawning is currently working
    public bool IsSpawning;
    
    // How often to spawn a powerup
    [SerializeField] private float spawnInterval;


    void Awake()
    {
        _spawningBounds = GetComponent<BoxCollider2D>();
    }
    
    void Start()
    {
        StartCoroutine(nameof(SpawnLoop));
    }

    // The separate async loop for spawning powerups
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (IsSpawning)
            {
                SpawnPowerup(GetRandomPowerup());
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    // Returns the prefab for a random powerup this spawner can use
    private GameObject GetRandomPowerup()
    {
        return powerups[Random.Range(0, powerups.Count)];
    }
    
    // Spawn a given powerup at a random point in these bounds
    public void SpawnPowerup(GameObject powerup)
    {
        Bounds bounds = _spawningBounds.bounds;
        
        // Determine a position between the min and max of spawning bounds
        Vector2 randomPosition = new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
        
        // Instantiate a new powerup object
        GameObject newPowerupObject = GameObject.Instantiate(powerup, randomPosition, Quaternion.identity, transform);
    }
}
