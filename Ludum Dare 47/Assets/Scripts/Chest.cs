﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Sprite openSprite = null;
    [SerializeField] private AudioSource chestOpenSound = null;

    private bool isOpen = false;

    public void Open()
    {
        chestOpenSound.Play();
        GetComponent<SpriteRenderer>().sprite = openSprite;
        isOpen = true;
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}
