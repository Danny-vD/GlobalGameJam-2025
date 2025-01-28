using UnityEngine;

public class EnableObjectsOnEnter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToEnable;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            objectsToEnable[i].SetActive(true);
        }
    }
}
