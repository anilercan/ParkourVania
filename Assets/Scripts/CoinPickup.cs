using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    bool wasCollected=false;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag=="Player"&&wasCollected==false){
            wasCollected=true;
            FindObjectOfType<GameSession>().AddToScore();
            AudioSource.PlayClipAtPoint(coinSFX,Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
