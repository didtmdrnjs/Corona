using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private GameObject InputText;


    private int value;
    private bool NewScore;

    private string Name;

    private void Start()
    {
        value = -1;
        gameManager = GameManager.Instance;
        gameManager.save = false;
        for (int i = 0; i < gameManager.ranking.Count; i++)
        {
            if (gameManager.ranking[i].score < gameManager.Score)
            {
                NewScore = true;
                value = i;
                break;
            }
        }
        if (gameManager.ranking.Count < 5 && value == -1)
        {
            value = gameManager.ranking.Count;
            NewScore = true;
        }
        if (NewScore)
        {
            InputText.SetActive(true);
            gameManager.save = true;
        }
        if (gameManager.save)
        {
            StartCoroutine(insert());
        }
    }

    IEnumerator insert()
    {
        if (gameManager.save)
        {
            yield return null;
            StartCoroutine(insert());
        }
        else
        {
            Name = InputText.transform.GetChild(1).GetComponent<InputField>().text;
            InputText.SetActive(false);
            gameManager.ranking.Insert(value, new Rank { rank = value + 1, name = Name, score = gameManager.Score });
            while (gameManager.ranking.Count > 5) gameManager.ranking.RemoveAt(5);
        }
    }
}
