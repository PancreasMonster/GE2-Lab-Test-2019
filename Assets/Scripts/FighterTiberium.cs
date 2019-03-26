using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterTiberium : MonoBehaviour {

    TextMesh text;
    public float fireRate = .4f, force = 500;
    bool bulletCooldown, refueling;
    public float tiberium = 7;
    GameObject targetBase, homeBase;
    public GameObject bullet;
    Arrive arrive;
    Boid boid;

    // Use this for initialization
    void Start () {
        text = GetComponentInChildren<TextMesh>();
        arrive = GetComponent<Arrive>();
        boid = GetComponent<Boid>();
    }
	
	// Update is called once per frame
	void Update () {
        text.text = tiberium.ToString();

        if (Vector3.Distance(transform.position, targetBase.transform.position) < 15 && !refueling)
        {
            arrive.weight = 0;
            boid.velocity = Vector3.zero;
            if (!bulletCooldown)
                FireBullet();
        }

        if (tiberium <= 0)
        {
            arrive.weight = 1;
            arrive.targetGameObject = homeBase;
            refueling = true;
        }
	}

    void OnTriggerStay (Collider col)
    {
        if (col.transform.tag == "Base" && refueling == true && col.GetComponent<BaseScript>().tiberium >= 7)
        {
            col.GetComponent<BaseScript>().tiberium -= 7;
            tiberium = 7;
            refueling = false;
            arrive.targetGameObject = targetBase;
        }
    }
    public void AssignTarget (GameObject target, GameObject home)
    {
        targetBase = target;
        homeBase = home;
    }

    void FireBullet ()
    {
        tiberium -= 1;
        Vector3 dir = targetBase.transform.position - transform.position;
        dir.Normalize();
        GameObject clone = Instantiate(bullet, transform.position, transform.rotation);
        clone.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        rb.AddForce(dir * force);
        rb.useGravity = false;
        bulletCooldown = true;
        StartCoroutine(BulletCooldownFunction());
    }

    IEnumerator BulletCooldownFunction ()
    {
        yield return new WaitForSeconds(fireRate);
        bulletCooldown = false;
    }
}
