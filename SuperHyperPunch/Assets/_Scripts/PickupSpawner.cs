using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupSpawner : MonoBehaviour {

    public List<Transform> pickupTransforms;
    public GameObject pickupItem;
    public float spawnDelay = 1.0f;

    private float spawnTimer;
    public bool runTimer;

	// Use this for initialization
	void Start () {
        spawnTimer = 0.0f;
        SpawnNewPickup();
	}

    void Update()
    {
        if (runTimer)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnDelay)
            {
                runTimer = false;
                spawnTimer = 0.0f;
                SpawnNewPickup();
            }
        }
    }

    public void SpawnNewPickup()
    {
        GameObject clone = Instantiate(pickupItem) as GameObject;
        clone.transform.position = randomSpawnPicker().position;
    }

    public Transform randomSpawnPicker()
    {
        Transform result;
        int chosenIndex = (Random.Range(0, pickupTransforms.Count));
        result = pickupTransforms[chosenIndex];
        return result;
    }
}
