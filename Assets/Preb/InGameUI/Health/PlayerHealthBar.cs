using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Image amountOfHealthImage;
    [SerializeField] TextMeshProUGUI amountText;

    public void UpdateHeath(float health, float delta, float maxHealth)
    {
        amountOfHealthImage.fillAmount = health/maxHealth;
        amountText.SetText(health.ToString());
    }
}
