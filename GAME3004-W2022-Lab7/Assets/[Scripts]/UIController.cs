using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class UIController : MonoBehaviour
{
    public TMP_Text healthBarValue;
    public Slider healthBar;

    public void OnHealthBarValue_Changed()
    {
        healthBarValue.text = healthBar.value.ToString();
    }

    public void TakeDamage(int damage)
    {
        healthBar.value -= damage;
    }
}
