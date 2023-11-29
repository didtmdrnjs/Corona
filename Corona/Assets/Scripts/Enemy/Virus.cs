using System.Collections;
using UnityEditor;
using UnityEngine;

public class Virus : Enemy
{
    private GameManager gameManager;
    private Player player;

    private static Virus instance;
    public static Virus Instance { get { return instance; } }

    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private Sprite NVirus;
    [SerializeField]
    private Sprite VirusHit;

    private float Speed;
    private int random;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }

        Speed = 3;
        HP = 90;
        Damage = 15;
        Score = 45000;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        player = Player.Instance;
        if (transform.position == new Vector3(-10, 21.5f, -9.2f) || transform.position == new Vector3(10, 21.5f, -9.2f)) random = 0;
        else if (transform.position == new Vector3(-28, 7, -9.2f)) random = 1;
        else if (transform.position == new Vector3(28, 7f, -9.2f)) random = 2;
        StartCoroutine(Shot());
    }

    private void Update()
    {
        if (random == 0) transform.position += Vector3.down * Speed * Time.deltaTime;
        else if (random == 1) transform.position += Vector3.right * Speed * Time.deltaTime;
        else if (random == 2) transform.position += Vector3.left * Speed * Time.deltaTime;

        if (random == 0 && transform.position.y < -21.4f)
        {
            player.Pain += Damage / 2;
            Destroy(gameObject);
        }
        else if (random == 1 && transform.position.x > 25.9f)
        {
            player.Pain += Damage / 2;
            Destroy(gameObject);
        }
        else if (random == 2 && transform.position.x < -25.9f)
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
        gameObject.GetComponent<SpriteRenderer>().sprite = VirusHit;
        StartCoroutine(HitVirus());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit(Damage/2);
        }
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(Bullet, transform.position, Quaternion.identity);
        StartCoroutine(Shot());
    }
    IEnumerator HitVirus()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().sprite = NVirus;
    }
}
