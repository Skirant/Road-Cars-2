using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] PlayerMove _playerMove;
    [SerializeField] PreFinishBehavior _preFinishBehavior;

    [SerializeField] CameraMove _cameraMove;
    [SerializeField] GameObject _slider;

    private void Start()
    {
        _playerMove.enabled = false;
        _preFinishBehavior.enabled = false;
    }

    public void Play()
    {
        _playerMove.enabled = true;
        _slider.SetActive(true);
        _cameraMove.ChangeCameraPositionAndRotation();
    }
        
    public void StartPreFinishBehavior()
    {
        _playerMove.enabled = false;
        _preFinishBehavior.enabled = true;

        _slider.SetActive(false);
    }

    public void StartFinishBehavior()
    {
        _preFinishBehavior.enabled = false;
    }
}
