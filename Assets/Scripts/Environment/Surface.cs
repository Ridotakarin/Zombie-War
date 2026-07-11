using UnityEngine;

public enum SurfaceType
{
    Default,
    Flesh,
    Object
}
public class Surface : MonoBehaviour
{
    [SerializeField] private SurfaceType surfaceType = SurfaceType.Default;

    public SurfaceType SurfaceType => surfaceType;
}
