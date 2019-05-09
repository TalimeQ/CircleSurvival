using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplodingCircle : BaseCircle
{

    
    protected override void Explode()
    {
        this.gameObject.SetActive(false);
        NotifyLose();
        spawnListener.RequestRemove(this.gameObject);
    }

    void Awake()
    {
        InitTimer();
    }

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
        spawnListener.RequestRemove(this.gameObject);
        NotifyRemove();
        this.gameObject.SetActive(false);
    }
}
