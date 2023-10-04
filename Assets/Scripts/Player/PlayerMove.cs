using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Camera _mainCamera; // Ссылка на камеру
    [SerializeField] private float _rotationLimit = 80f; // Ограничение поворота

    private void Update()
    {
        // Персонаж движется вперед с постоянной скоростью
        Vector3 newPosition = transform.position + transform.forward * Time.deltaTime * _speed;
        newPosition.x = Mathf.Clamp(newPosition.x, -2.5f, 2.5f);
        transform.position = newPosition;

        if (Input.GetMouseButton(0))
        {
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            float enter;

            if (groundPlane.Raycast(ray, out enter))
            {
                Vector3 targetPosition = ray.GetPoint(enter);
                targetPosition.y = transform.position.y;

                Vector3 direction = (targetPosition - transform.position).normalized;

                // Ограничиваем поворот
                Vector3 currentEulerAngles = transform.eulerAngles;
                float targetYRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float clampedYRotation = Mathf.Clamp(targetYRotation, -_rotationLimit, _rotationLimit);
                currentEulerAngles.y = clampedYRotation;

                transform.rotation = Quaternion.Euler(currentEulerAngles);
            }
        }
    }
}
