using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    public Enums.Direction position = Enums.Direction.Up;
    public int margin = 3;


    void Start()
    {
        SetPositionAndScale();
    }

    private void OnTriggerEnter(Collider other)
    {
        var timebody = other.gameObject.GetComponent<TimeBody>();

        if (timebody != null)
        {
            timebody.Despawn();
        }
    }




    void SetPositionAndScale()
    {
        switch (position)
        {
            case Enums.Direction.Up:
                transform.position = new Vector3(0,0, CameraManager.Instance.portraitSize + margin);
                transform.localScale = new Vector3(CameraManager.Instance.width + margin * 2, 2f, 2f); 
                break;

            case Enums.Direction.Down:
                transform.position = new Vector3(0, 0, -CameraManager.Instance.portraitSize - margin);
                transform.localScale = new Vector3(CameraManager.Instance.width + margin * 2, 2f, 2f); 
                break;

            case Enums.Direction.Right:
                transform.position = new Vector3(CameraManager.Instance.landscapeSize + margin, 0, 0);
                transform.localScale = new Vector3(2f, 2f, CameraManager.Instance.height + margin * 2);
                break;

            case Enums.Direction.Left:
                transform.position = new Vector3(-CameraManager.Instance.landscapeSize - margin, 0, 0);
                transform.localScale = new Vector3(2f, 2f, CameraManager.Instance.height + margin * 2);
                break;

            default:
                break;
        }
    } 
}
