﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float loopLength = 180f;
    [SerializeField] private float captureShowDelay = 0.5f;
    [SerializeField] private Image captureShow = null;
    [SerializeField] private PlayerMovement playerMovement = null;

    private int loopsCount = 0;

    // Start is called before the first frame update
    private void Start()
    {
        TimeManager.timeManagerInstance.RegisterTimeEvent(loopLength, endOfLoopEvent);
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void endOfLoopEvent(float timeDelta)
    {
        Debug.Log("End of loop");
        playerMovement.enabled = false;
        loopsCount++;
        ShowLoopPast();
    }

    private void ShowLoopPast()
    {
        DirectoryInfo di = new DirectoryInfo(Application.temporaryCachePath);
        FileInfo[] captureFiles = di.GetFiles("capture*.png");
        Array.Sort(captureFiles, CompareCaptures);
        Array.Reverse(captureFiles);

        StartCoroutine(SlideThrowCaptures(captureFiles));
    }

    private IEnumerator SlideThrowCaptures(FileInfo[] captureFiles)
    {
        captureShow.enabled = true;
        foreach (var capture in captureFiles)
        {
            captureShow.sprite = Image2Sprite.LoadNewSprite(capture.FullName, 32);
            yield return new WaitForSeconds(captureShowDelay);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private int CompareCaptures(FileInfo a, FileInfo b)
    {
        int file1Index = Int32.Parse(Regex.Match(a.Name, @"\d+").Value);
        int file2Index = Int32.Parse(Regex.Match(b.Name, @"\d+").Value);
        if (file1Index > file2Index)
        {
            return 1;
        }
        else if (file1Index < file2Index)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
