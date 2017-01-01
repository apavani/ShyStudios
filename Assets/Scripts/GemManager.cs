using UnityEngine;

public class GemManager : MonoBehaviour
{

    private Vector3 startPosition;
    public void Start()
    {
        this.startPosition = transform.position;
    }

    public void EvaporateToPosition()
    {
        transform.parent = null;
        transform.position = startPosition;
    }
}