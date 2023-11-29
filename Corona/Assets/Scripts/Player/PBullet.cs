using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    private Player player;
    private float Speed;

    private void Awake()
    {
        Speed = 50;
    }

    private void Start()
    {
        player = Player.Instance;
        StartCoroutine(Delete());
    }

    private void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
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
}
