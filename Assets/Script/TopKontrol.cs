using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopKontrol : MonoBehaviour
{
    public UnityEngine.UI.Text zaman, can, durum;
    public UnityEngine.UI.Button btn;
    private Rigidbody rg;
    float sure = 25f;
    int kalancan = 3;
    bool oyundevam = true;
    bool oyuntamam = false;
    // Start is called before the first frame update
    void Start()
    {
        can.text = kalancan + "";
        rg = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (oyundevam && !oyuntamam)
        {
            sure -= Time.deltaTime;
            zaman.text = (int)sure + "";
        }else if(!oyuntamam)
        {
            durum.text = "Oyun Tamamlanamadý";
            btn.gameObject.SetActive(true);
        }
        if (sure < 0)
            oyundevam = false;
    }
    void FixedUpdate()
    {
        if (oyundevam && !oyuntamam)
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-yatay, 0, -dikey);
            rg.AddForce(kuvvet);
        }
        else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;
        }
    }
    void OnCollisionEnter (Collision cls)
    {
        string obje = cls.gameObject.name;
        if (obje.Equals("VarisNoktasi"))
        {
            oyuntamam = true;
            durum.text="Oyun Tamamlandý...";
            btn.gameObject.SetActive(true);
        }
        else if(!obje.Equals("AnaZemin") && !obje.Equals("Zemin"))
        {
            kalancan -= 1;
            can.text = kalancan + "";
            if (kalancan == 0) oyundevam = false;
        }
    }
}
