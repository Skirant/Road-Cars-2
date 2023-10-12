using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform _target;
    private Vector3 _offsetPosition;
    private Vector3 _offsetRotation;
    [SerializeField] Vector3 _newOffsetPosition;
    [SerializeField] Vector3 _newOffsetRotation;
    [SerializeField] float _rotationSpeed = 2f;
    [SerializeField] float _transitionTime = 1f;

    private void LateUpdate()
    {
        if (_target)
        {
            transform.position = _target.position + _offsetPosition;
            Quaternion targetRotation = Quaternion.Euler(_offsetRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }

    public void ChangeCameraPositionAndRotation()
    {
        StartCoroutine(TransitionCamera());
    }

    IEnumerator TransitionCamera()
    {
        float elapsedTime = 0f;

        Vector3 startPosition = _offsetPosition;
        Vector3 startRotation = _offsetRotation;

        while (elapsedTime < _transitionTime)
        {
            elapsedTime += Time.deltaTime;

            _offsetPosition = Vector3.Lerp(startPosition, _newOffsetPosition, elapsedTime / _transitionTime);
            _offsetRotation = Vector3.Lerp(startRotation, _newOffsetRotation, elapsedTime / _transitionTime);

            yield return null;
        }

        _offsetPosition = _newOffsetPosition;
        _offsetRotation = _newOffsetRotation;
    }
}
