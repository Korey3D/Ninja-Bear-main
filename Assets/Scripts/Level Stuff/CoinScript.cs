using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinScript : MonoBehaviour
{
    private int coins = 0;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private AudioSource coinSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);

            coins++;

            coinsText.text = "Golden Shurikens: " + coins;

            coinSound.Play();

        }
    }

}
