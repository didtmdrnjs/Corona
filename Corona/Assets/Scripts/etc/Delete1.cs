using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete1 : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Del");
    }

    IEnumerator Del()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
