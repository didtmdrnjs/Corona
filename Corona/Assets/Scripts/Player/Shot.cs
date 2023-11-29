using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Player player;

    [SerializeField]
    private GameObject Bullet1;
    [SerializeField]
    private GameObject Bullet3;
    [SerializeField]
    private GameObject Bullet5;

    private bool isShot;

    private void Awake()
    {
        isShot = true;
    }

    private void Start()
    {
        player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && isShot)
        {
            GetComponent<AudioSource>().Play();
            if (player.BulletLevel == 1 || player.BulletLevel == 2) Instantiate(Bullet1, transform.position, Quaternion.identity);
            else if (player.BulletLevel == 3 || player.BulletLevel == 4) Instantiate(Bullet3, transform.position, Quaternion.identity);
            else if (player.BulletLevel == 5) Instantiate(Bullet5, transform.position, Quaternion.identity);
            isShot = false;
            StartCoroutine(CoolTime());
        }
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(player.shotDelay);
        isShot = true;
    }
}
