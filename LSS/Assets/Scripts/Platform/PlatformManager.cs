using UnityEngine;
using System.Collections;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    private GameObject platformPrefab;
    

}

public class Manager: MonoBehaviour
{
    [SerializeField]
    private GameObject Prefab;
    [SerializeField]
    private Transform Parent;
    [SerializeField]
    private Transform Pool;

    public void Initialize()
    {
        
    }

    private void CreateObject()
       => Instantiate(Prefab, Pool);

    private void GiveObject()
    {
        Pool.GetChild(0).transform.parent = Parent;
    }

    private void BackInPool(Transform transform)
       => transform.parent = Pool;

    private void HideAll()
    {
        foreach (Transform transform in Parent)
            if (!transform.Equals(Pool))
                BackInPool(transform);
    }
}
