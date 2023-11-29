using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancerBullet : MonoBehaviour
{
    private Cancer cancer;
    private float Speed;
    
    public bool isCopyed;

    private void Awake()
    {
        Speed = 8;
    }

    private void Start()
    {
        cancer = Cancer.Instance;
        StartCoroutine(Delete());
        if (!isCopyed) StartCoroutine(Split());
    }

    private void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit(cancer.Damage);
            Destroy(gameObject);
        }
    }

    IEnumerator Delete()
    {
        if (!isCopyed) yield return new WaitForSeconds(8);
        else yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }

    IEnumerator Split()
    {
        yield return new WaitForSeconds(2f);
        GameObject a = Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 10));
        GameObject b = Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 20));
        GameObject c = Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z - 10));
        GameObject d = Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z - 20));
        isCopyed = true;
        a.GetComponent<CancerBullet>().isCopyed = true;
        b.GetComponent<CancerBullet>().isCopyed = true;
        c.GetComponent<CancerBullet>().isCopyed = true;
        d.GetComponent<CancerBullet>().isCopyed = true;
    }
}
