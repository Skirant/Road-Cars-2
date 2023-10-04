using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform _target;

    private void LateUpdate()
    {
        if (_target)
        {
            transform.position = _target.position;
        }       
    }
}
