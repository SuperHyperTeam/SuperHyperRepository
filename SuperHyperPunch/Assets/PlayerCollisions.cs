using UnityEngine;
using System.Collections;

namespace Acrocatic
{
    public class PlayerCollisions : MonoBehaviour
    {

        private Player player;

        void Start()
        {
            player = GetComponent<Player>();
        }

        void OnCollisionEnter2D(Collision2D other)
        {

        }

    }
}

