using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    private HealthBar healthBar;

    private void Awake()
    {
        // Only the player should search for the health bar
        if (gameObject.CompareTag("Player"))
        {
            healthBar = FindObjectOfType<HealthBar>();
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        
        // Again, only the player should update the health bar UI
        if (gameObject.CompareTag("Player") && healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }


    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (gameObject.CompareTag("Player") && healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }



    private IEnumerator FadeOut(float fadeDuration)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        float fadeSpeed = 1.0f / fadeDuration;

        for(float t = 0.0f; t < 1.0f; t += Time.deltaTime * fadeSpeed)
        {
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(originalColor.a, 0, t));
            yield return null;
        }

        sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        Destroy(gameObject);
    }

    private void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            // Restart the game
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

        }
        else if (gameObject.CompareTag("enemy"))
        {
            // Fade out and destroy the enemy
            StartCoroutine(FadeOut(1.0f)); // 1 second fade duration, adjust as needed
        }
    }

}
