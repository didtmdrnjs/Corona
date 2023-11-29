using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonEvent : MonoBehaviour
{

    public void ClickCheck()
    {
        GameManager.Instance.save = false;
    }
    public void ClickHome()
    {
        SceneManager.LoadScene("Start");
    }
    public void ClickStart()
    {
        SceneManager.LoadScene("Stage1");
    }
}
