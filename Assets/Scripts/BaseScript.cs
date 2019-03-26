using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour {

    public List<GameObject> bases = new List<GameObject>();
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
        text.text = tiberium.ToString();
        if (!refresh)
        {
            tiberium += 1;
            refresh = true;
            StartCoroutine(tiberiumAdd());
        }

        if (tiberium >= 10)
        {
            tiberium -= 10;
            GameObject clone = Instantiate(fighterPrefab, transform.position, Quaternion.identity);
            clone.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
            int rand = Random.Range(0, bases.Count - 1);
            clone.GetComponent<Arrive>().targetPosition = bases[rand].GetComponent<Transform>().position;
            clone.GetComponent<Arrive>().slowingDistance = 15;
            clone.GetComponent<FighterTiberium>().AssignTarget(bases[rand], this.gameObject);
        }
    }

    void OnTriggerEnter (Collider col)
    {
        if(col.transform.tag == "bullet")
        {
            Destroy(col.transform.gameObject);
            tiberium -= .5f;
        }
    }

    IEnumerator tiberiumAdd()
    {
        yield return new WaitForSeconds(1);
        refresh = false;
    }
}
