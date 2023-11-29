using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancer : Enemy
{
    private GameManager gameManager;
    private Player player;

    private static Cancer instance;
    public static Cancer Instance { get { return instance; } }

    [SerializeField] private GameObject Bullet;
    [SerializeField] private Sprite NCancer1;
    [SerializeField] private Sprite CancerHit1;
    [SerializeField] private Sprite NCancer2;
    [SerializeField] private Sprite CancerHit2;
    [SerializeField] private Sprite NCancer3;
    [SerializeField] private Sprite CancerHit3;

    private float Speed;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }

        Speed = 2;
        HP = 50;
        Damage = 12;
        Score = 25000;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        player = Player.Instance;
        StartCoroutine(Shot());
    }

    private void Update()
    {
        transform.position -= transform.up * Speed * Time.deltaTime;

        if (transform.position.y < -21.4f)
        {
            player.Pain += Damage / 2;
            Destroy(gameObject);
        }

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

    public override void Hit(float Damage)
    {
        base.Hit(Damage);
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CancerHit1;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = CancerHit2;
        gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = CancerHit3;
        StartCoroutine(HitGerm());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit(Damage / 2);
        }
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, Random.Range(160, 201)));
        StartCoroutine(Shot());
    }
    IEnumerator HitGerm()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = NCancer1;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = NCancer3;
        gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = NCancer3;
    }
}
