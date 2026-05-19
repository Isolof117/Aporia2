using System;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] public int currentHealth;

    [Header("UI")]
    [SerializeField] private Slider healthSlider;

    [Header("Damage Audio")]
    [SerializeField] private AudioSource damageAudio;
    [SerializeField] private AudioClip[] damageSounds; // 3 clips

    [Header("Death Screen")]
    [SerializeField] private GameObject objectToHide;
    [SerializeField] private GameObject objectToShow;

    private bool isDead = false;

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
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthSlider.value = currentHealth;

        //  Random damage sound
        if (damageAudio != null && damageSounds != null && damageSounds.Length > 0)
        {
            AudioClip clip = damageSounds[Random.Range(0, damageSounds.Length)];
            damageAudio.PlayOneShot(clip);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log(gameObject.name + " is dead!");

        // Swap UI
        if (objectToHide != null)
            objectToHide.SetActive(false);

        if (objectToShow != null)
            objectToShow.SetActive(true);

        // Cursor unlock
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Pause game
        Time.timeScale = 0f;

        OnDeath?.Invoke();
    }

    public void ResetHealth()
    {
        isDead = false;

        currentHealth = maxHealth;
        healthSlider.value = currentHealth;

        Time.timeScale = 1f;
    }
}