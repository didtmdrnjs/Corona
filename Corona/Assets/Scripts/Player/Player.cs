using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance {  get { return instance; } }

    [HideInInspector] public float Damage;
    [HideInInspector] public float HP;
    [HideInInspector] public float MaxHP;
    [HideInInspector] public float MaxPain;
    [HideInInspector] public bool inv;
    [HideInInspector] public float shotDelay;
    [HideInInspector] public float BulletLevel;
    [HideInInspector] public List<GameObject> SubPlayerList;
    [HideInInspector] public bool isShield;

    public float Pain;
    
    [SerializeField] private GameObject SubPlayerObject;
    [SerializeField] private GameObject ReverseGravityBullet;

    public GameObject HPBar;

    public bool AllKill;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Damage = 5;
        MaxHP = 300;
        HP = 300;
        MaxPain = 100;
        inv = false;
        shotDelay = 0.1f;
        BulletLevel = 1;
        isShield = false;
    }

    private void Update()
    {
        if (HP <= 0 || Pain >= MaxPain)
        {
            SceneManager.LoadScene("Result");
        }
        if (HP > 300) HP = 300;
        if (Pain < 0) Pain = 0;
    }

    public void Hit(float Damage)
    {
        if (!inv)
        {
            HP -= Damage;
            inv = true;
            StartCoroutine(ReleaseInv(1.5f));
            StartCoroutine(RealDelayInv(1.5f));
            StartCoroutine("ActiveInv");
        }
    }
    public void ChangeInv(float Delay, float RealDelay)
    {
        if (!inv)
        {
            inv = true;
            StartCoroutine("ReleaseInv", Delay);
            StartCoroutine("RealDelayInv", RealDelay);
            StartCoroutine("ActiveInv");
        }
        else
        {
            StopCoroutine("ReleaseInv");
            StopCoroutine("RealDelayInv");
            StopCoroutine("ActiveInv1");
            StopCoroutine("ActiveInv");
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            StartCoroutine("ReleaseInv", Delay);
            StartCoroutine("RealDelayInv", RealDelay);
            StartCoroutine("ActiveInv");
        }
    }
    public void Upgrade()
    {
        BulletLevel++;
        if (BulletLevel > 5)
        {
            BulletLevel--;
        }
        ActiveBulletLevel();
    }
    public void ActiveBulletLevel()
    {
        if (BulletLevel == 2) shotDelay = 0.05f;
        else if (BulletLevel == 3)
        {
            shotDelay = 0.1f;
            Damage = 10;
        }
        else if (BulletLevel == 4) shotDelay = 0.05f;
        else if (BulletLevel == 5)
        {
            shotDelay = 0.08f;
            Damage = 30;
        }
    }
    public void ActiveSubPlayer()
    {
        if (SubPlayerList.Count < 8)
        {
            if (SubPlayerList.Count == 0)
            {
                SubPlayerList.Add(Instantiate(SubPlayerObject, transform.position, Quaternion.identity));
            }
            else
            {
                int size = SubPlayerList.Count;
                for (int i = 0; i < size; i++)
                {
                    SubPlayerList.Add(Instantiate(SubPlayerObject, transform.position, Quaternion.identity));
                }
            }
        }
        else if (SubPlayerList.Count == 8)
        {
            SubPlayerList.Add(Instantiate(SubPlayerObject, transform.position, Quaternion.identity));
            SubPlayerList.Add(Instantiate(SubPlayerObject, transform.position, Quaternion.identity));
        }
    }
    public void SpawnReverseGravityBullet()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(ReverseGravityBullet, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-8f, 0f), -10, 0), ForceMode2D.Impulse);
        }
        for (int i = 0; i < 5; i++)
        {
            Instantiate(ReverseGravityBullet, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(0f, 8f), -10, 0), ForceMode2D.Impulse);
        }
    }

    IEnumerator ReleaseInv(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        StopCoroutine("ActiveInv1");
        StopCoroutine("ActiveInv");
    }
    IEnumerator ActiveInv()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.75f);
        yield return new WaitForSeconds(0.3f);
        StartCoroutine("ActiveInv1");
    }
    IEnumerator ActiveInv1()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.3f);
        StartCoroutine("ActiveInv");
    }
    IEnumerator RealDelayInv(float RealDelay)
    {
        yield return new WaitForSeconds(RealDelay);
        inv = false;
    }
}
