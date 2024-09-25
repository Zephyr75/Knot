using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : Module
{
    public GameObject bullet, focus;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        SetMainInput(KeyCode.C);
        SetSecondInput(KeyCode.C);
    }

    // Update is called once per frame
    void Update()
    {
        if (machine.GetComponent<Editor>().start)
        {
            if (Input.GetKeyDown(GetMainInput()) && canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        GameObject newBullet = GameObject.Instantiate(bullet, transform.position, transform.rotation);
        Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), GetComponent<Collider>(), true);
        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2500);
        if (transform.parent.GetComponent<Rigidbody>() != null)
        {
            transform.parent.GetComponent<Rigidbody>().AddForce(-transform.forward * 2500);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward * 2500);
        }
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
