using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcade.Spawn;

public class BlackCircle : BaseCircle
{
    RectTransform rectTrans;

    protected override void Explode()
    {
        this.gameObject.SetActive(false);
        spawnListener.RequestRemove(this.gameObject);
        NotifyRemove();
    }

    // Start is called before the first frame update
    void Awake()
    {
        InitTimer();
        this.rectTrans = transform as RectTransform;
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
        NotifyLose();
    }

}
