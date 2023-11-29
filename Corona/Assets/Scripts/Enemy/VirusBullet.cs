using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBullet : MonoBehaviour
{
    private Virus virus;
    private float Speed;

    private void Awake()
    {
        Speed = 30;
    }

    private void Start()
    {
        virus = Virus.Instance;
        StartCoroutine(Delete());
    }

    private void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime;
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit(virus.Damage);
            Destroy(gameObject);
        }
    }
}
