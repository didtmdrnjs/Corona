using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corona_19Bullet2 : MonoBehaviour
{
    private Corona_19 corona;
    private Player player;
    private float Speed;

    private void Awake()
    {
        Speed = 2.5f;
    }

    private void Start()
    {
        corona = Corona_19.Instance;
        player = Player.Instance;
        StartCoroutine(Delete());
    }

    private void Update()
    {
        transform.position += Vector3.down * Speed * 16 * Time.deltaTime;
        transform.position += new Vector3((player.transform.position.x - transform.position.x) * Speed * Time.deltaTime, 0, 0);
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit(corona.Damage);
            Destroy(gameObject);
        }
    }
}
