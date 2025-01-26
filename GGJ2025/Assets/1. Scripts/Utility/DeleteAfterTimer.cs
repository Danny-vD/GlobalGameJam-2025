using UnityEngine;
using VDFramework.Utility.TimerUtil;

public class DeleteAfterTimer : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;

    private void OnEnable()
    {
        TimerManager.StartNewTimer(destroyTime, destroySelf);
    }

    private void destroySelf()
    {
        Destroy(gameObject);
    }
}
