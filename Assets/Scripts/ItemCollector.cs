using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int _fruits = 0;
    private int _allFruits;

    [SerializeField] private Text _fruitsText;

    [SerializeField] private AudioSource _collectItemSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        _allFruits = GameObject.FindGameObjectsWithTag("CollectibleFruit").Length;
        _fruitsText.text += $"/{_allFruits}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CollectibleFruit"))
        {
            _collectItemSoundEffect.Play();
            Destroy(collision.gameObject);
            _fruits++;
            _fruitsText.text = _fruitsText.text.Substring(0, _fruitsText.text.IndexOf(":") + 1) + $"{_fruits}/{_allFruits}";
        }
    }
}
