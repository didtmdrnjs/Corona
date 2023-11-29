using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bacteria : Enemy
{
    private GameManager gameManager;
    private Player player;

    [SerializeField]
    private Sprite NBacteria;
    [SerializeField]
    private Sprite BacteriaHit;

    private float Speed;
    private int cnt;
    private Vector3 dir;

    private void Awake()
    {
        HP = 20;
        Damage = 10;
        Speed = 20;
        Score = 20000;
        cnt = 0;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        player = Player.Instance;
        dir = transform.up.normalized;
    }

    private void Update()
    {
        transform.position += dir * Speed * Time.deltaTime;

        if (HP <= 0)
        {
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            gameManager.Score += Score;
        }


        if (player.AllKill)
        {
            Hit(1000);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit(Damage / 2);
        }
        else if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("NPC"))
        {
            if (cnt <= 9)
            {
                if (cnt > 1)
                {
                    Vector3 income = dir;
                    Vector3 normal = collision.contacts[0].normal;
                    dir = Vector3.Reflect(income, normal);
                }
                cnt++;
            }
            else
            {
                player.Pain += Damage / 2;
                Destroy(gameObject);
            }
        }
    }
    
    public override void Hit(float Damage)
    {
        base.Hit(Damage);
        gameObject.GetComponent<SpriteRenderer>().sprite = BacteriaHit;
        StartCoroutine(HitBacteria());
    }

    IEnumerator HitBacteria()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().sprite = NBacteria;
    }
}
