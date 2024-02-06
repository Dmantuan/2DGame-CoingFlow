using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text melonsText;
    [SerializeField] private AudioSource collectSoundEffect;

    private int melons = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Melon"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            melons++;
            melonsText.text = "Melons: " + melons;
        }   
    }
}
