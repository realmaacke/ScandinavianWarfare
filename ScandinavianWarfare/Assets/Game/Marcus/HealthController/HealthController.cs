using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float MaxHealth;

    public static float currentHealth;

    public Game_UI_Manager GameUI;
    

    public Slider Slider;

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    public void Update()
    {
        //Slider.value = currentHealth;

        if (Input.GetMouseButton(1))
            Damage(1);
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
        }
    }

}
