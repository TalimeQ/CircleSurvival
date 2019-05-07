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


    private float currentBlowInterval = 3.0f;
    private float blowTime = 0.0f;


    public float BlowInterval { get { return currentBlowInterval; } }
    public float BlowTime { get { return blowTime; } }


    protected abstract void Explode();
    protected abstract void OnTouched();

    void Update()
    {
        CheckExplosion();
    }

    protected virtual void CheckExplosion()
    {
        FillShape();
        if (Time.time >= blowTime)
        {
            this.Explode();
        }

    }

    protected void InitTimer()
    {
        blowTime = Time.time + BlowInterval;
    }

    virtual protected void FillShape()
    {

        float newFillAmount = (this.BlowTime - Time.time) / BlowTime;

        fillImage.fillAmount = newFillAmount;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnTouched();
    }
}
