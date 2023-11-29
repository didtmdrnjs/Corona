using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP;
    public float Damage;
    public float Score;

    public virtual void Hit(float Damage)
    {
        HP -= Damage;
    }
}
