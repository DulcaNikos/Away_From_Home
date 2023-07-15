using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    [SerializeField] GameObject ItemsFeedback;

    int cherries = 1;

    //When player will collide with cherry tag.. will play a sound and after destroying it will add 1 point to cherryscore
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject effect = Instantiate(ItemsFeedback, transform.position, Quaternion.identity);
            CherryManager.instance.CherryScore(cherries);
            AudioManager.audioManager.PlaySFX("collectibles");
            Destroy(effect, 0.7f);
            Destroy(gameObject);
        }
    }
}
