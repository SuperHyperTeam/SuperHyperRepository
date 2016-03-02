using UnityEngine;
using System.Collections;

public class PlayerPickup : MonoBehaviour {

    private PlayerStats stats;
    public PickupSpawner pickupSpawner;

    void Start()
    {
        stats = gameObject.GetComponent<PlayerStats>();
    }

	void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            stats.score += 1;
            stats.UpdateScore();
            pickupSpawner.runTimer = true;
            Destroy(other.gameObject);
        }
    }

}
