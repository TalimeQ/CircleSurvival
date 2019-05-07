using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplodingCircle : BaseCircle
{

    
    protected override void Explode()
    {
        this.gameObject.SetActive(false);
        print("Green Exploded!");
    }

    // Start is called before the first frame update
    void Awake()
    {
        InitTimer();
    }

    // Update is called once per frame
    void Update()
    {
        CheckExplosion();
    }

    protected override void CheckExplosion() 
    {
        base.CheckExplosion();
    }

    protected override void OnTouched()
    {
        print("Exploding Touched!");
    }
}
