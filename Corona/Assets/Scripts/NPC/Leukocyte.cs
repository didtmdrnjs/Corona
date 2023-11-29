using System.Collections;
using UnityEngine;

public class Leukocyte : NPC
{
    private Player player;

    [SerializeField]
    private GameObject Item1;
    [SerializeField] 
    private GameObject Item2;
    [SerializeField] 
    private GameObject Item3;
    [SerializeField] 
    private GameObject Item4;
    [SerializeField]
    private GameObject Item5;
    [SerializeField]
    private GameObject Item6;

    private float Speed;
    private int cnt;
    private bool die;
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
        if (!die)
        {
            die = true;
            int random = Random.Range(0, 6);
            if (random == 0) Instantiate(Item1, transform.position, Quaternion.identity);
            else if (random == 1) Instantiate(Item2, transform.position, Quaternion.identity);
            else if (random == 2) Instantiate(Item3, transform.position, Quaternion.identity);
            else if (random == 3) Instantiate(Item4, transform.position, Quaternion.identity);
            else if (random == 4) Instantiate(Item5, transform.position, Quaternion.identity);
            else if (random == 5) Instantiate(Item6, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
