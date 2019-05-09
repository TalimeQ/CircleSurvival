using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Arcade.Spawn {
    public class Spawner : MonoBehaviour , ISpawnListener
    {
        Vector2 screenSize;

        [SerializeField]
        public List<RectTransform> circlePositions;
            
        private void Start()
        {
            screenSize = new Vector2(Screen.width, Screen.height);
            circlePositions = new List<RectTransform>();
        }

        private void RandomizePosition(RectTransform trans)
        {
            SetNewPosition(trans);
            CheckIfOverlaps(trans);
        }

        private void CheckIfOverlaps(RectTransform trans)
        {
            bool Overlapped = false;
            if (circlePositions.Count == 0) return;
            foreach (RectTransform checkedTrans in circlePositions)
            {
                if (RectOverlaps(checkedTrans, trans))
                {
                    Overlapped = true;
                    break;
                }
            }
            if (Overlapped) RandomizePosition(trans);
        }

        private void SetNewPosition(RectTransform trans)
        {
            Vector3 newPosition = new Vector3(Random.Range(0.0f, screenSize.x), Random.Range(0.0f, screenSize.y), 0);
            trans.position = newPosition;
        }

        public void RequestRespawn(BaseCircle circle)
        {
            Spawn(circle.gameObject.tag, circle.CurrentExplosionModifier);
        }

        public void RequestRemove(GameObject removedObj)
        {
            circlePositions.Remove(removedObj.transform as RectTransform);
        }

        public void Spawn(string tag, float explosionIntervalModifier)
        {
            GameObject spawnedObject = ObjectPooler.SharedInstance.GetPooledObject(tag);
            RectTransform objTransform = spawnedObject.transform as RectTransform;
            RandomizePosition(objTransform);
            circlePositions.Add(objTransform);
            spawnedObject.GetComponent<BaseCircle>().CurrentExplosionModifier = explosionIntervalModifier;
            spawnedObject.SetActive(true);

        }

        bool RectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
        {
            Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
            Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

            return rect1.Overlaps(rect2);
        }

    }
}