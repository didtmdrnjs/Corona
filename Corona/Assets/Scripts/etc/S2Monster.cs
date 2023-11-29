using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S2Monster : MonoBehaviour
{
    [SerializeField] private GameObject Bacteria;
    [SerializeField] private GameObject Germ;
    [SerializeField] private GameObject Virus;
    [SerializeField] private GameObject Cancer;
    [SerializeField] private GameObject Leukocyte;
    [SerializeField] private GameObject RedBloodCell;
    [SerializeField] private GameObject VariantCorona_19;
    [SerializeField] private GameObject BossText;

    private void Start()
    {
        StartCoroutine(Wait());
        StartCoroutine(SBoss());
        StartCoroutine("RandomPattern");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(SpawnLeukocyte());
    }
    IEnumerator RandomPattern()
    {
        yield return new WaitForSeconds(8);
        int random = UnityEngine.Random.Range(0, 4);
        if (random == 0) SBacteria(UnityEngine.Random.Range(1, 5));
        if (random == 1) SVirus();
        if (random == 2) SGerm();
        if (random == 3) SCancer();
        StartCoroutine("RandomPattern");
    }
    IEnumerator SBoss()
    {
        BossText.gameObject.SetActive(false);
        yield return new WaitForSeconds(300);
        StopCoroutine("RandomPattern");
        BossText.gameObject.SetActive(true);
        Instantiate(VariantCorona_19, new Vector3(0, 27, -9), Quaternion.identity);
        yield return new WaitForSeconds(3);
        BossText.gameObject.SetActive(false);
    }
    IEnumerator SpawnLeukocyte()
    {
        yield return new WaitForSeconds(7.5f);
        SNPC();
        StartCoroutine(SpawnLeukocyte());
    }

    private void SNPC()
    {
        int spawnPercent = UnityEngine.Random.Range(0, 100);
        if (spawnPercent % 4 != 0) Instantiate(Leukocyte, new Vector3(UnityEngine.Random.Range(-21.5f, 21.5f), UnityEngine.Random.Range(-1f, 15f), -9.5f), Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
        else Instantiate(RedBloodCell, new Vector3(UnityEngine.Random.Range(-21.5f, 21.5f), UnityEngine.Random.Range(-1f, 15f), -9.5f), Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
    }
    private void SBacteria(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Instantiate(Bacteria, new Vector3(-31.5f, 21.5f, -9.2f), Quaternion.Euler(0, 0, UnityEngine.Random.Range(210, 250)));
        }
        for (int i = 0; i < num; i++)
        {
            Instantiate(Bacteria, new Vector3(31.5f, 21.5f, -9.2f), Quaternion.Euler(0, 0, UnityEngine.Random.Range(130, 170)));
        }
    }
    private void SVirus()
    {
        int random = UnityEngine.Random.Range(0, 2);
        if (random == 0)
        {
            Instantiate(Virus, new Vector3(10, 21.5f, -9.2f), Quaternion.identity);
            Instantiate(Virus, new Vector3(-10, 21.5f, -9.2f), Quaternion.identity);
        }
        else
        {
            Instantiate(Virus, new Vector3(28, 7, -9.2f), Quaternion.identity);
            Instantiate(Virus, new Vector3(-28, 7, -9.2f), Quaternion.identity);
        }
    }
    private void SGerm()
    {
        int random = UnityEngine.Random.Range(0, 3);
        if (random == 0)
        {
            Instantiate(Germ, new Vector3(10, 21.5f, -9.2f), Quaternion.identity);
            Instantiate(Germ, new Vector3(-10, 21.5f, -9.2f), Quaternion.identity);
        }
        else if (random == 1)
        {
            Instantiate(Germ, new Vector3(-26, 10, -9.2f), Quaternion.identity);
            Instantiate(Germ, new Vector3(-26, 3, -9.2f), Quaternion.identity);
        }
        else
        {
            Instantiate(Germ, new Vector3(26, 10, -9.2f), Quaternion.identity);
            Instantiate(Germ, new Vector3(26, 3, -9.2f), Quaternion.identity);
        }
    }
    private void SCancer()
    {
        Instantiate(Cancer, new Vector3(UnityEngine.Random.Range(-15f, 15f), 21.5f, -9.2f), Quaternion.identity);
    }
}
