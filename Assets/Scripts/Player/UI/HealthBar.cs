using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Slider slider;

    public void UpdateHealthBar()
    {
        slider.value = playerHealth.HealthPoints;
    }
}
