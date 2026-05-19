using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] public int currentHealth;

    [SerializeField] private Slider healthSlider;

    //Events

    public event Action OnDeath;


    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //Test Damage on Enemy
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Before Damage: {currentHealth}");

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);


        Debug.Log($"After Damage: {currentHealth}");

        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;

        healthSlider.value = currentHealth;
    }

    public void Death()
    {
        Debug.Log(gameObject.name + " is dead!");

        Destroy(gameObject);
    }
}
