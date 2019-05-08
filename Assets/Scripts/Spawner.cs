using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Arcade.Spawn {
    public class Spawner : MonoBehaviour
    {
        Vector2 screenSize;
        private void Start()
        {
            screenSize = new Vector2(Screen.width, Screen.height);
        }

        public void Spawn(string tag, float explosionIntervalModifier)
        {
            GameObject spawnedObject = ObjectPooler.SharedInstance.GetPooledObject(tag);
            spawnedObject.GetComponent<RectTransform>().position = new Vector3(1.0f, 1.0f, 0);
            spawnedObject.SetActive(true);


        }

        private void RandomizePosition()
        {

        }
    }
}