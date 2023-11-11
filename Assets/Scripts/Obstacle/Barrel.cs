using TMPro;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private int barrelHealth;
    private TMP_Text healthText;

    private void Start()
    {
        InitializeHealth();
    }

    private void InitializeHealth()
    {
        GetHealth();
        healthText = GetComponentInChildren<TMP_Text>();
        UpdateHealthText();
    }

    private void GetHealth()
    {
        if (int.TryParse(GetComponentInChildren<TMP_Text>().text, out int healthValue))
        {
            barrelHealth = healthValue;
        }
        else
        {
            Debug.LogError("Failed to parse health value from TMP_Text.");
        }
    }

    private void Update()
    {
        DestroyBarrel();
    }

    private void DestroyBarrel()
    {
        if (barrelHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DecreaseHealth()
    {
        barrelHealth--;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = barrelHealth.ToString();
    }
}
