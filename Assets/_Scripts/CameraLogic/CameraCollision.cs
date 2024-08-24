using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform player; // �����
    public float minDistance = 2f; // ����������� ���������� �� ������ �� ������
    public float maxDistance = 15.0f; // ������������ ���������� �� ������ �� ������
    public float smoothSpeed = 10.0f; // �������� ����������� �������� ������

    private Vector3 _desiredCameraPosition;
    private CameraFollower _follower;

    private void Start()
    {
        _follower = GetComponent<CameraFollower>();
    }

    void LateUpdate()
    {
        // �������� ��������� ������ (��������)
        _desiredCameraPosition = player.position - transform.forward * maxDistance;

        // ���� ��� ������������ � ������������
        if (Physics.Raycast(player.position, -transform.forward, out RaycastHit hit, maxDistance))
        {
            // ������������� ����� ��������� ������ � ������ �����������
            float distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            _desiredCameraPosition = player.position - transform.forward * distance;
            _follower.gameObject.SetActive(false);
        }

        // ������ ���������� ������
        transform.position = Vector3.Lerp(transform.position, _desiredCameraPosition, Time.deltaTime * smoothSpeed);
    }

    public void FollowCollision(GameObject following) =>
        player = following.transform;
}
