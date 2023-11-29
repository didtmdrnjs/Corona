using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPlayer : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = Player.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(player.Damage / 2);
        }
        if (collision.transform.CompareTag("NPC"))
        {
            collision.gameObject.GetComponent<NPC>().Hit();
        }
    }
}
