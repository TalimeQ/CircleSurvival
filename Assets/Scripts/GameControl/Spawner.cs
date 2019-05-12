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

        private void RandomizeSpawnPosition(RectTransform trans)
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
            if (Overlapped) RandomizeSpawnPosition(trans);
        }

        private void SetNewPosition(RectTransform trans)
        {
            float yMin, yMax, xMin, xMax;
            // This setup will keep new position in the bounds of the screen, meaning that game is fair
            xMin = trans.rect.width;
            xMax = screenSize.x - trans.rect.width;
            yMin = trans.rect.height;
            yMax = screenSize.y - trans.rect.height;

            Vector3 newPosition = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
            trans.position = newPosition;
        }

        private void SetSpawnPosition(GameObject spawnedObject)
        {
            RectTransform objTransform = spawnedObject.transform as RectTransform;
            RandomizeSpawnPosition(objTransform);
            circlePositions.Add(objTransform);
        }

        private bool RectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
        {
            Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
            Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

            return rect1.Overlaps(rect2);
        }

        public void RequestRemove(GameObject removedObj)
        {
            circlePositions.Remove(removedObj.transform as RectTransform);
        }

        public void Spawn(string tag, float explosionIntervalModifier)
        {
            GameObject spawnedObject = ObjectPooler.SharedInstance.GetPooledObject(tag);
            SetSpawnPosition(spawnedObject);
            spawnedObject.GetComponent<BaseCircle>().CurrentExplosionModifier = explosionIntervalModifier;
            spawnedObject.SetActive(true);

        }

    }
}