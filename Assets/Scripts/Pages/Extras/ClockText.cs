﻿using System;
using System.Collections;
using UnityEngine;

public class ClockText : MonoBehaviour
{
    private TextMesh _textMesh = null;

    private void Awake()
    {
        _textMesh = GetComponent<TextMesh>();
    }

    private void OnEnable()
    {
        StartCoroutine(UpdatePerSecond());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator UpdatePerSecond()
    {
        while (true)
        {
            _textMesh.text = DateTime.Now.ToString("dddd dd MMMM HH:mm");
            yield return new WaitForSeconds(1.0f);
        }
    }
}
