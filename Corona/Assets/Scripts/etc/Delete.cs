using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Del");
    }

    IEnumerator Del()
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
}
