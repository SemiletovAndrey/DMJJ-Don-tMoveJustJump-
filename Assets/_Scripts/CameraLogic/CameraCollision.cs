using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform player; // Игрок
    public float minDistance = 2f; // Минимальное расстояние от игрока до камеры
    public float maxDistance = 15.0f; // Максимальное расстояние от игрока до камеры
    public float smoothSpeed = 10.0f; // Скорость сглаживания движения камеры

    private Vector3 _desiredCameraPosition;
    private CameraFollower _follower;

    private void Start()
    {
        _follower = GetComponent<CameraFollower>();
    }

    void LateUpdate()
    {
        // Желаемое положение камеры (исходное)
        _desiredCameraPosition = player.position - transform.forward * maxDistance;

        // Если луч пересекается с препятствием
        if (Physics.Raycast(player.position, -transform.forward, out RaycastHit hit, maxDistance))
        {
            // Устанавливаем новое положение камеры с учетом пересечения
            float distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            _desiredCameraPosition = player.position - transform.forward * distance;
            _follower.gameObject.SetActive(false);
        }

        // Плавно перемещаем камеру
        transform.position = Vector3.Lerp(transform.position, _desiredCameraPosition, Time.deltaTime * smoothSpeed);
    }

    public void FollowCollision(GameObject following) =>
        player = following.transform;
}
