using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1 : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rb2;

    private void Start()
    {
        gameManager = GameManager.Instance;
        rb2 = GetComponent<Rigidbody2D>();
        rb2.AddForce(new Vector3(Random.Range(-6f, 6f), 10, 0), ForceMode2D.Impulse);
        StartCoroutine(Delete());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.Itemcnt++;
            collision.gameObject.GetComponent<Player>().Upgrade();
            gameManager.Score += gameManager.Itemcnt * 1000;
            Destroy(gameObject);
        }
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
}
