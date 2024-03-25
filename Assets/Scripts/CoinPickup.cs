using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placed on coin prefab to execute behaviour for the coin
public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoin = 100;

    bool wasCollected = false;

    //If the player collides with the coin than play the sFX, add points, and mark coin as collected
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoin);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
