using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RangeIndicator : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private WeaponBase weapon;

    [Header("Settings")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Color indicatorColor = new Color(0f, 1f, 1f, 0.3f); // Mặc định màu xanh lục bảo mờ mờ
    [SerializeField] private float lineWidth = 0.03f;

    [Header("End Point Marker (Optional)")]
    [Tooltip("Gắn một Prefab hình tròn nhỏ hoặc Particle ở đây để đánh dấu điểm kết thúc tầm bắn")]
    [SerializeField] private GameObject endMarkerPrefab;
    private GameObject spawnedMarker;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        weapon = GetComponent<WeaponBase>(); // Tìm WeaponBase đi kèm để lấy range

        // Tự động tìm firePoint bằng code nếu chưa kéo trong Inspector
        if (firePoint == null)
        {
            // Tìm con có tên là firePoint hoặc lấy chính transform này
            Transform found = transform.Find("firePoint");
            firePoint = found != null ? found : transform;
        }

        SetupLineRenderer();
        CreateMarker();
    }

    private void SetupLineRenderer()
    {
        if (lineRenderer == null) return;

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // Thiết lập màu sắc (sử dụng màu mờ để tránh làm rối mắt người chơi)
        lineRenderer.startColor = indicatorColor;
        lineRenderer.endColor = indicatorColor;

        // Đảm bảo LineRenderer sử dụng tọa độ World để vẽ chính xác
        lineRenderer.useWorldSpace = true;
    }

    private void CreateMarker()
    {
        if (endMarkerPrefab != null && spawnedMarker == null)
        {
            spawnedMarker = Instantiate(endMarkerPrefab, transform);
        }
    }

    private void OnEnable()
    {
        if (lineRenderer != null) lineRenderer.enabled = true;
        if (spawnedMarker != null) spawnedMarker.SetActive(true);
    }

    private void OnDisable()
    {
        if (lineRenderer != null) lineRenderer.enabled = false;
        if (spawnedMarker != null) spawnedMarker.SetActive(false);
    }

    private void LateUpdate()
    {
        if (lineRenderer == null || firePoint == null || weapon == null || weapon.Data == null)
            return;

        Vector3 startPos = firePoint.position;
        // Tia này LUÔN kéo dài đúng bằng tầm bắn thực tế của súng, không bị cản bởi tường
        Vector3 endPos = startPos + (firePoint.forward * weapon.Data.range);

        // Vẽ đường thẳng biểu thị tầm bắn
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);

        // Di chuyển điểm đánh dấu (Marker) tới cuối tầm bắn
        if (spawnedMarker != null)
        {
            spawnedMarker.transform.position = endPos;
            spawnedMarker.transform.rotation = Quaternion.LookRotation(firePoint.forward);
        }
    }
}