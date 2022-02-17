using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class UIController : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text healthBarValueLabel;

    public void OnHealthBarValue_Changed()
    {
        healthBarValueLabel.text = healthBar.value.ToString();
    }

    public void TakeDamage(int damage)
    {
        healthBar.value -= damage;
    }
}
