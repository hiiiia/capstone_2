using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{

    public Image boss_HP_Bar;

    public TMP_Text PlayerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHPBar();
        DisplayPlayerHp();
    }

    void DisplayPlayerHp()
    {
        PlayerText.text = "HP : " + Player.inst.hp.ToString();
    }

    void DisplayHPBar()
    {
        boss_HP_Bar.fillAmount = Boss.inst.hp / 100f;
    }
}
