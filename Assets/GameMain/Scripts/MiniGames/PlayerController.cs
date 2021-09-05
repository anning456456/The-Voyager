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
    public GameObject popUp;
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead)
        {
            Time.timeScale = 0;
            popUp.SetActive(true);
            return;
        }
        if (leftLong)
        {
            Leftwards();
        }
        if (rightLong)
        {
            Rightwards();
        }
        if (upLong)
        {
            Upwards();
        }
        if (downLong)
        {
            Downwards();
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
    public void Upwards()
    {

        transform.Translate(Vector3.up * Time.deltaTime * vSpeed);

    }
    public void UpPressDown(bool isPressed)
    {
        upLong = isPressed;
    }
    public void Downwards()
    {
        transform.Translate(-Vector3.up * Time.deltaTime * vSpeed);
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
            slider.value =hp;
            if (hp <= 0.02)
            {
                sliderFillArea.SetActive(false);
                isDead = true;
            }
        }
    }

}
