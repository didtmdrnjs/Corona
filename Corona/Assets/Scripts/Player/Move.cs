using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    Rigidbody2D rb2;
    private float moveSpeed;
    private Vector2 minVel;
    private Vector2 maxVel;
    private Vector2 minPos;
    private Vector2 maxPos;

    [SerializeField] private Sprite PlayerM;
    [SerializeField] private Sprite PlayerL;
    [SerializeField] private Sprite PlayerR;

    [SerializeField] private GameObject Leukocyte;
    [SerializeField] private GameObject RedBloodCell;

    [SerializeField] private GameObject Value;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();

        moveSpeed = 15;
        minVel = new Vector2(-20, -20);
        maxVel = new Vector2(20, 20);
        minPos = new Vector2(-22, -15);
        maxPos = new Vector2(22, 15);
    }

    private void Start()
    {
        Value.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PMove();

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (SceneManager.GetActiveScene().name == "Stage1") SceneManager.LoadScene("Stage2");
            else if (SceneManager.GetActiveScene().name == "Stage2") SceneManager.LoadScene("Stage1");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Player.Instance.AllKill = true;
            StartCoroutine(Kill());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Player.Instance.Upgrade();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Player.Instance.inv) Player.Instance.inv = false;
            else Player.Instance.inv = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(Leukocyte, new Vector3(0, 0, -9.5f), Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(RedBloodCell, new Vector3(0, 0, -9.5f), Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Value.SetActive(true);
            StartCoroutine(Sli("HP"));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Value.SetActive(true);
            StartCoroutine(Sli("Pain"));
        }
    }

    private void PMove()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = PlayerM;
            rb2.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = PlayerM;
            rb2.AddForce(Vector2.down * moveSpeed, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = PlayerR;
            rb2.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = PlayerL;
            rb2.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
        }
        else gameObject.GetComponent<SpriteRenderer>().sprite = PlayerM;

        rb2.velocity = new Vector2(Mathf.Clamp(rb2.velocity.x, minVel.x, maxVel.x), Mathf.Clamp(rb2.velocity.y, minVel.y, maxVel.y));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x), Mathf.Clamp(transform.position.y, minPos.y, maxPos.y), -9);
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(0.5f);
        Player.Instance.AllKill = false;
    }

    IEnumerator Sli(string val)
    {
        yield return new WaitForSeconds(2f);
        if (val == "HP")
        {
            Player.Instance.HP = (int)(Value.GetComponent<Slider>().value * 300);
        }
        else if (val == "Pain")
        {
            Player.Instance.Pain = (int)(Value.GetComponent<Slider>().value * 100);
        }
        Value.SetActive(false);
    }
}
