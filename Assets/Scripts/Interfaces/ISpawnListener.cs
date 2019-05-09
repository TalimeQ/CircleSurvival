using UnityEngine;

public interface ISpawnListener
{
    void RequestRespawn(BaseCircle circle);
    void RequestRemove(GameObject removedObj);
}
