using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravityBullet : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = Player.Instance;
        StartCoroutine(Delete());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(player.Damage);
            Destroy(gameObject);
        }
        if (collision.transform.CompareTag("NPC"))
        {
            collision.gameObject.GetComponent<NPC>().Hit();
            Destroy(gameObject);
        }
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
