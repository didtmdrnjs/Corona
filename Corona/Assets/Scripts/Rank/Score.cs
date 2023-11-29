using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (gameManager.ranking.Count > 0 && gameManager.ranking.Count-1 >= transform.parent.transform.parent.name[0] - '1')
        {
            gameObject.GetComponent<Text>().text = ((int)gameManager.ranking[transform.parent.transform.parent.name[0] - '1'].score).ToString();
        }
    }
}
