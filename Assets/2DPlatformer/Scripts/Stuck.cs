using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuck : MonoBehaviour {

    private Vector2 stuckPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            stuckPlayer = other.transform.localPosition;
            stuckPlayer.y += 2.5f;
            other.transform.localPosition = stuckPlayer;
        }
    }
}
