﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float tiberium = 5;
    bool refresh;
    public TextMeshPro text;

    public GameObject fighterPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!refresh)
        {
            tiberium += 1;
            text.text = tiberium.ToString();
            refresh = true;
            StartCoroutine(tiberiumAdd());
        }
    }

    IEnumerator tiberiumAdd ()
    {
        yield return new WaitForSeconds(1);
        refresh = false;
    }
}
