using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Text>().text = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        StartCoroutine(NotActive());
    }

    IEnumerator NotActive()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
