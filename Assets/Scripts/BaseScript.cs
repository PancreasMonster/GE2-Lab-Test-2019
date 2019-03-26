using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour {

    public float tiberium = 5;
    bool refresh = false;
    TextMesh text;

    public GameObject fighterPrefab;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMesh>();
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

        if (tiberium >= 10)
        {
            tiberium -= 10;
            GameObject clone = Instantiate(fighterPrefab, transform.position, Quaternion.identity);
            clone.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        }
    }

    IEnumerator tiberiumAdd()
    {
        yield return new WaitForSeconds(1);
        refresh = false;
    }
}
