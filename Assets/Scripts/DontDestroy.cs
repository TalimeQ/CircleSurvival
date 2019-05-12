using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
        [SerializeField]
        string notDestroyedTag;
        void Awake()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(notDestroyedTag);

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
        }
    
}
