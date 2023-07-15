using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyFeedback;

    public void Death()
    {
        GameObject effect = Instantiate(enemyFeedback, transform.position, Quaternion.identity);
        AudioManager.audioManager.PlaySFX("enemy");
        Destroy(effect, 0.7f);
        Destroy(gameObject);
    }
}
