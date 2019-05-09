using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


abstract public class BaseCircle : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    float minBlowInterval;
    [SerializeField]
    float maxBlowInterval;
    [SerializeField]
    Image fillImage;
    [SerializeField]
    public ISpawnListener spawnListener;


    private float currentBlowInterval = 3.0f;
    private float currentExplosionModifier = 0.0f;
    private float blowTime = 0.0f;


    public float BlowInterval { get { return currentBlowInterval; } }
    public float BlowTime { get { return blowTime; } }
    public float CurrentExplosionModifier { set { currentExplosionModifier = value; } get { return currentExplosionModifier; } }


    protected abstract void Explode();
    protected abstract void OnTouched();

    void Awake()
    {
        InitTimer();
    }

    void OnEnable()
    {
        InitTimer();

    }

    void Update()
    {
        CheckExplosion();
    }

    protected void InitTimer()
    {
        blowTime = Time.time + BlowInterval;
    }

    virtual protected void FillShape()
    { 
        float newFillAmount = (this.BlowTime - Time.time) / BlowInterval;
        fillImage.fillAmount = newFillAmount;
    }
        
    protected virtual void CheckExplosion()
    {
        FillShape();
        if (Time.time >= blowTime)
        {
            this.Explode();
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnTouched();
    }

    
}
