using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Germ : Enemy
{
    private GameManager gameManager;
    private Player player;

    private static Germ instance;
    public static Germ Instance { get { return instance; } }

    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private Sprite NGerm;
    [SerializeField]
    private Sprite GermHit;

    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    [SerializeField] private GameObject p3;
    [SerializeField] private GameObject p4;
    [SerializeField] private GameObject p5;
    [SerializeField] private GameObject p6;

    private float Speed;
    private float pos;
    private int random;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }

        HP = 75;
        Damage = 8;
        Score = 37500;
        pos = 0;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        if (transform.position == new Vector3(10, 21.5f, -9.2f)) random = 0;
        else if (transform.position == new Vector3(-10, 21.5f, -9.2f)) random = 1;
        else if (transform.position == new Vector3(-26, 10, -9.2f)) random = 2;
        else if (transform.position == new Vector3(-26, 3, -9.2f)) random = 3;
        else if (transform.position == new Vector3(26, 10, -9.2f)) random = 4;
        else if (transform.position == new Vector3(26, 3, -9.2f)) random = 5;
        if (random == 0 || random == 1) Speed = 0.001f;
        else Speed = 0.001f;
        player = Player.Instance;
        StartCoroutine(Shot());
    }

    private void Update()
    {
        if (random == 0) transform.position = BezierMove(p1, pos);
        else if (random == 1) transform.position = BezierMove(p2, pos);
        else if (random == 2) transform.position = BezierMove(p3, pos);
        else if (random == 3) transform.position = BezierMove(p4, pos);
        else if (random == 4) transform.position = BezierMove(p5, pos);
        else if (random == 5) transform.position = BezierMove(p6, pos);

        if ((random == 0 || random == 1) && (transform.position.x > 25.9f || transform.position.x < -25.9f))
        {
            player.Pain += Damage / 2;
            Destroy(gameObject);
        }
        else if ((random == 2 || random == 3) && transform.position.x > 25.9f)
        {
            player.Pain += Damage / 2;
            Destroy(gameObject);
        }
        else if ((random == 4 || random == 5) && transform.position.x < -25.9f)
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

        pos += Speed;
        if (pos > 1) pos = 1;

        if (player.AllKill)
        {
            Hit(1000);
        }
    }

    public override void Hit(float Damage)
    {
        base.Hit(Damage);
        gameObject.GetComponent<SpriteRenderer>().sprite = GermHit;
        StartCoroutine(HitGerm());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit(Damage / 2);
        }
    }

    private Vector3 BezierMove(GameObject p, float value)
    {
        Vector3 p1 = p.transform.GetChild(0).transform.position;
        Vector3 p2 = p.transform.GetChild(1).transform.position;
        Vector3 p3 = p.transform.GetChild(2).transform.position;
        Vector3 p4 = p.transform.GetChild(3).transform.position;

        Vector3 A = Vector3.Lerp(p1, p2, value);
        Vector3 B = Vector3.Lerp(p2, p3, value);
        Vector3 C = Vector3.Lerp(p3, p4, value);

        Vector3 D = Vector3.Lerp(A, B, value);
        Vector3 E = Vector3.Lerp(B, C, value);

        Vector3 F = Vector3.Lerp(D, E, value);

        return F;
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(0.25f);
        Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, Random.Range(-10, 11)));
        StartCoroutine(Shot());
    }
    IEnumerator HitGerm()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().sprite = NGerm;
    }
}
