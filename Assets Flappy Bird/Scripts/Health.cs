using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 3; // عدد الأرواح
    public int numOfHearts = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        if (health < 0)
        {
            health = 0;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            hearts[i].enabled = i < numOfHearts;
        }
    }

    public void DecreaseHealth()
    {
        if (health > 0)
        {
            health--;
            Update();
            if (health <= 0)
            {
                // Notify GameManager to handle game over
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.GameOver();
                }
            }
        }
    }

    public void ResetHealth()
    {
        health = numOfHearts;
        Update();
    }
}