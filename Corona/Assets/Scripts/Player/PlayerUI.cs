using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private GameManager gameManager;
    private Player player;

    [SerializeField] private Image HPValue;
    [SerializeField] private Text HPText;
    [SerializeField] private Image PainValue;
    [SerializeField] private Text PainText;
    [SerializeField] private Text Score;

    private void Start()
    {
        gameManager = GameManager.Instance;
        player = Player.Instance;
    }

    private void Update()
    {
        HPValue.fillAmount = player.HP / player.MaxHP;
        HPText.text = player.HP.ToString() + "/" + player.MaxHP.ToString();
        PainValue.fillAmount = player.Pain / player.MaxPain;
        PainText.text = player.Pain.ToString() + "/" + player.MaxPain.ToString();
        Score.text = "";
        for (int i = 0; i < 9 - gameManager.Score.ToString().Length; i++)
        {
            Score.text += "0";
        }
        Score.text += gameManager.Score.ToString();
    }
}
