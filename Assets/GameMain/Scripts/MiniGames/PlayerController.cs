using FrameworkExtension;
using NPOI.SS.Formula.Functions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float hSpeed, vSpeed, hp;
    private bool leftLong, rightLong, upLong, downLong;
    public Slider slider;
    public GameObject sliderFillArea;
    Animator anim;
    bool isDead;
    public GameObject diePopUp;
    public GameObject successPopUp;
    public GameObject mask;
    public GameObject end;
    public float delay;
    [HideInInspector]
    public bool reachEnd;
    [Tooltip("关卡持续时间")]
    public int countDownIndicator = 10;
    public UnityEngine.UI.Text countDown;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("CountDown");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (reachEnd)
        {
            if (RockSpawn.instance.Rocks.Count != 0)
            {
                foreach(var temp in RockSpawn.instance.Rocks)
                {
                    Destroy(temp);
                }
            }

            mask.SetActive(true);
            successPopUp.SetActive(true);
            Time.timeScale = 0;
        }
        if (isDead)
        {
            mask.SetActive(true);
            diePopUp.SetActive(true);
            Time.timeScale = 0;
        }
        if (leftLong)
        {
            Invoke("Leftwards",delay-0.2f);
        }
        if (rightLong)
        {
            Invoke("Rightwards", delay - 0.2f);
        }
        if (upLong)
        {
            Invoke("Upwards", delay - 0.2f);
        }
        if (downLong)
        {
            Invoke("Downwards", delay - 0.2f);
        }
    }

    /// <summary>
    /// movement methods
    /// </summary>
    #region
    public void Leftwards()
    {
        transform.Translate(-Vector3.right * Time.deltaTime * hSpeed);
    }
    public void LeftwardsBtn()
    {
        Invoke("Leftwards", delay);
    }
    public void LeftPressDown(bool isPressed)
    {
        leftLong = isPressed;
    }
    public void Rightwards()
    {
        transform.Translate(Vector3.right * Time.deltaTime * hSpeed);
    }
    public void RightPressDown(bool isPressed)
    {
        rightLong = isPressed;
    }
    public void RightwardsBtn()
    {
        Invoke("Rightwards", delay);
    }
    public void Upwards()
    {
        transform.Translate(Vector3.up * Time.deltaTime * vSpeed);
    }
    public void UpwardsBtn()
    {
        Invoke("UpwardsBtn", delay);
    }
    public void UpPressDown(bool isPressed)
    {
        upLong = isPressed;
    }
    public void Downwards()
    {
        transform.Translate(-Vector3.up * Time.deltaTime * vSpeed);
    }
    public void DownwardsBtn()
    {
        Invoke("Downwards", delay);
    }
    public void DownPressDown(bool isPressed)
    {
        downLong = isPressed;
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            anim.SetTrigger("isHurt");
            hp -= 0.2f;
            slider.value = hp;
            if (hp <= 0.02)
            {
                sliderFillArea.SetActive(false);
                isDead = true;
            }
        }
        if (collision.gameObject.CompareTag("End"))
        {
            reachEnd = true;
        }
    }
    IEnumerator CountDown()
    {
        for ( ; countDownIndicator > 0; countDownIndicator--)
        {
            countDown.text = countDownIndicator.ToString();
            if (countDownIndicator == 10)
            {
                end.GetComponent<Image>().enabled = true;
                end.GetComponent<BoxCollider2D>().enabled = true;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
