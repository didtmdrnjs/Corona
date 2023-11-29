using System.Collections;
using UnityEngine;

public class RedBloodCell : NPC
{
    private Player player;

    private float Speed;
    private int cnt;
    private Vector3 dir;

    private void Awake()
    {
        Speed = 20;
        cnt = 0;
    }

    private void Start()
    {
        player = Player.Instance;
        dir = transform.up.normalized;
    }

    private void Update()
    {
        transform.position += dir * Speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Hit();
        }
        else if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("NPC"))
        {
            if (cnt < 16)
            {
                Vector3 income = dir;
                Vector3 normal = collision.contacts[0].normal;
                dir = Vector3.Reflect(income, normal);
                cnt++;
            }
            else
            {
                StartCoroutine(Delete());
            }
        }
    }

    public override void Hit()
    {
        player.Pain += 10;
        Destroy(gameObject);
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
