using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPlayerRotate : MonoBehaviour
{
    private Player player;
    private float Speed;
    private float deg;
    private float dis;

    private void Awake()
    {
        Speed = 150;
        deg = 0;
        dis = 4f;
    }

    private void Start()
    {
        player = Player.Instance;
    }

    private void Update()
    {
        Speed = 150 + player.SubPlayerList.Count * 10;
        deg += Time.deltaTime * Speed;
        if (deg < 360)
        {
            for (int i = 0; i < player.SubPlayerList.Count; i++)
            {
                var rad = Mathf.Deg2Rad * (deg + (i * (360 / player.SubPlayerList.Count)));
                var x = dis * Mathf.Sin(rad);
                var y = dis * Mathf.Cos(rad);
                player.SubPlayerList[i].transform.position = transform.position + new Vector3(x, y);
                player.SubPlayerList[i].transform.rotation = Quaternion.Euler(0, 0, (deg + (i * (360 / player.SubPlayerList.Count))) * -1);
            }

        }
        else
        {
            deg = 0;
        }
    }
}
