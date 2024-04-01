using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public Image boss_HP_Bar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHPBar();
    }

    void DisplayHPBar()
    {
        boss_HP_Bar.fillAmount = Boss.inst.hp / 100f;
    }
}
