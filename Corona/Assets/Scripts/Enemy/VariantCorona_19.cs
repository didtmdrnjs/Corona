using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VariantCorona_19 : Enemy
{
    private GameManager gameManager;
    private Player player;
    private static VariantCorona_19 instance;
    public static VariantCorona_19 Instance { get { return instance; } }

    [SerializeField] private GameObject Bullet1;
    [SerializeField] private GameObject Bullet2;
    [SerializeField] private GameObject Bullet3;
    [SerializeField] private Sprite NVariantCorona_191;
    [SerializeField] private Sprite VariantCorona_19Hit1;
    [SerializeField] private Sprite NVariantCorona_192;
    [SerializeField] private Sprite VariantCorona_19Hit2;
    [SerializeField] private Sprite NVariantCorona_193;
    [SerializeField] private Sprite VariantCorona_19Hit3;

    [SerializeField] private GameObject Bacteria;
    [SerializeField] private GameObject Germ;
    [SerializeField] private GameObject Virus;
    [SerializeField] private GameObject Cancer;

    private float MaxHP;
    private float Speed;
    private bool Inv;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }

        Speed = 1.5f;
        MaxHP = 300000;
        HP = 300000;
        Damage = 15;
        Score = 10000000;
        Inv = true;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        player = Player.Instance;
        player.HPBar.SetActive(true);
        StartCoroutine(Move());
        StartCoroutine(RInv());
    }

    private void Update()
    {
        transform.position += Vector3.down * Speed * Time.deltaTime;
        player.HPBar.transform.GetChild(1).GetComponent<Image>().fillAmount = HP / MaxHP;
        player.HPBar.transform.GetChild(2).GetComponent<Text>().text = HP.ToString() + "/" + MaxHP.ToString();
        if (!Inv && HP <= 0)
        {
            gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine(Next());
        }
        if (player.AllKill)
        {
            StartCoroutine(Next());
        }
    }

    public override void Hit(float Damage)
    {
        if (!Inv)
        {
            base.Hit(Damage);
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = VariantCorona_19Hit1;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = VariantCorona_19Hit2;
            gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = VariantCorona_19Hit2;
            gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = VariantCorona_19Hit2;
            gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = VariantCorona_19Hit2;
            gameObject.transform.GetChild(5).GetComponent<SpriteRenderer>().sprite = VariantCorona_19Hit3;
            gameObject.transform.GetChild(6).GetComponent<SpriteRenderer>().sprite = VariantCorona_19Hit3;
            gameObject.transform.GetChild(7).GetComponent<SpriteRenderer>().sprite = VariantCorona_19Hit3;
            StartCoroutine(HitVariantCorona_19());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit(Damage / 2);
        }
    }

    IEnumerator RInv()
    {
        yield return new WaitForSeconds(5);
        Inv = false;
    }
    IEnumerator Next()
    {
        StopCoroutine("Pattern");
        Inv = true;
        gameManager.Score += Score;
        gameManager.Score += (player.HP / player.MaxHP) * 1000000;
        gameManager.Score += (1 - (player.Pain / player.MaxPain)) * 1000000;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Result");
        Destroy(gameObject);
    }
    IEnumerator Pattern()
    {
        yield return new WaitForSeconds(5f);
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            random = Random.Range(0, 3);
            if (random == 0) SBullet1();
            else if (random == 1) SBullet2();
            else if (random == 2) SBullet3();
        }
        else if (random == 1)
        {
            random = Random.Range(0, 4);
            if (random == 0) SBacteria(Random.Range(2, 4));
            else if (random == 1) SGerm();
            else if (random == 2) SVirus();
            else if (random == 3) SCancer();
        }
        StartCoroutine("Pattern");
    }
    IEnumerator HitVariantCorona_19()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = NVariantCorona_191;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = NVariantCorona_192;
        gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = NVariantCorona_192;
        gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = NVariantCorona_192;
        gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = NVariantCorona_192;
        gameObject.transform.GetChild(5).GetComponent<SpriteRenderer>().sprite = NVariantCorona_193;
        gameObject.transform.GetChild(6).GetComponent<SpriteRenderer>().sprite = NVariantCorona_193;
        gameObject.transform.GetChild(7).GetComponent<SpriteRenderer>().sprite = NVariantCorona_193;
    }
    IEnumerator Move()
    {
        yield return new WaitForSeconds(10);
        Speed = 0;
        StartCoroutine("Pattern");
    }
    IEnumerator Shot1()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.25f);
            Instantiate(Bullet1, transform.position, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(0.25f);
            Instantiate(Bullet1, transform.position, Quaternion.Euler(0, 0, 9));
        }
    }
    IEnumerator Shot2()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.25f);
            Instantiate(Bullet2, new Vector3(Random.Range(-12f, 12f), 12, -9), Quaternion.identity);
        }
    }
    IEnumerator Shot3()
    {
        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Instantiate(Bullet3, new Vector3(-4.4f, 5.5f, -9), Quaternion.Euler(0, 0, Random.Range(140, 220)));
            Instantiate(Bullet3, new Vector3(4.4f, 5.5f, -9), Quaternion.Euler(0, 0, Random.Range(140, 220)));
            Instantiate(Bullet3, new Vector3(-8.8f, 8, -9), Quaternion.Euler(0, 0, Random.Range(140, 220)));
            Instantiate(Bullet3, new Vector3(8.8f, 8, -9), Quaternion.Euler(0, 0, Random.Range(140, 220)));
            Instantiate(Bullet3, new Vector3(-7.4f, 15, -9), Quaternion.Euler(0, 0, Random.Range(140, 220)));
            Instantiate(Bullet3, new Vector3(7.4f, 15, -9), Quaternion.Euler(0, 0, Random.Range(140, 220)));
        }
    }

    private void SBullet1()
    {
        StartCoroutine(Shot1());
    }
    private void SBullet2()
    {
        StartCoroutine(Shot2());
    }
    private void SBullet3()
    {
        StartCoroutine(Shot3());
    }

    private void SBacteria(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Instantiate(Bacteria, new Vector3(-31.5f, 21.5f, -9), Quaternion.Euler(0, 0, Random.Range(210, 250)));
        }
        for (int i = 0; i < num; i++)
        {
            Instantiate(Bacteria, new Vector3(31.5f, 21.5f, -9), Quaternion.Euler(0, 0, Random.Range(130, 170)));
        }
    }
    private void SVirus()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            Instantiate(Virus, new Vector3(10, 21.5f, -9), Quaternion.identity);
            Instantiate(Virus, new Vector3(-10, 21.5f, -9), Quaternion.identity);
        }
        else
        {
            Instantiate(Virus, new Vector3(28, 7, -9), Quaternion.identity);
            Instantiate(Virus, new Vector3(-28, 7, -9), Quaternion.identity);
        }
    }
    private void SGerm()
    {
        int random = Random.Range(0, 3);
        if (random == 0)
        {
            Instantiate(Germ, new Vector3(10, 21.5f, -9), Quaternion.identity);
            Instantiate(Germ, new Vector3(-10, 21.5f, -9), Quaternion.identity);
        }
        else if (random == 1)
        {
            Instantiate(Germ, new Vector3(-26, 10, -9), Quaternion.identity);
            Instantiate(Germ, new Vector3(-26, 3, -9), Quaternion.identity);
        }
        else
        {
            Instantiate(Germ, new Vector3(26, 10, -9), Quaternion.identity);
            Instantiate(Germ, new Vector3(26, 3, -9), Quaternion.identity);
        }
    }
    private void SCancer()
    {
        Instantiate(Cancer, new Vector3(Random.Range(-15f, 15f), 21.5f, -9), Quaternion.identity);
    }
}
