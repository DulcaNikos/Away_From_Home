using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    [SerializeField] GameObject ItemsFeedback;

    int Diamonds = 1;

    //When player will collide with Gem tag.. will play a sound and after destroying it will add 1 point to gemscore
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject effect = Instantiate(ItemsFeedback, transform.position, Quaternion.identity);
            GemManager.instance.GemScore(Diamonds);
            AudioManager.audioManager.PlaySFX("collectibles");
            Destroy(effect, 0.7f);
            Destroy(gameObject);
        }
    }
}
