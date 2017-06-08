using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Character
{
    private int shootTime = 120;
    private bool isShooot = false;

    public GameObject bullt;


    private void FixedUpdate()
    {
        if (shootTime > 0)
        {
            shootTime--;
            isShooot = false;
        }
        else
            isShooot = true;

        if (isShooot && Manager.Instance.PlayerJoin)
        {
            shootTime = 120;
            isShooot = true;
            GameObject bul = PhotonNetwork.Instantiate(bullt.name, this.transform.position, this.transform.rotation, 0) as GameObject;
            bul.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            //bul.GetComponent<bulScript>().dmg = blDamage;
            //bul.GetComponent<bulScript>().name = name;
            Destroy(bul, 1.0f);
            //PhotonNetwork.Destroy(bul);
            //pv.RPC("playShoot", PhotonTargets.AllBuffered, null);
        }
    }
}
