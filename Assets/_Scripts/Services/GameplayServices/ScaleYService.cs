using UnityEngine;

public class ScaleYService 
{
    private readonly Transform _transform;

    public ScaleYService(Transform transform)
    {
        _transform = transform;
    }

    public void ScaleDown(float duration, float elapsedTime)
    {
        Vector3 initialScale = _transform.localScale;
        Vector3 targetScale = new Vector3(initialScale.x, 0, initialScale.z);
        float t = elapsedTime / duration;
        _transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
    }

    public void ScaleUp(float duration, float elapsedTime)
    {
        Vector3 initialScale = _transform.localScale;
        Vector3 targetScale = new Vector3(initialScale.x, 1, initialScale.z);
        float t = elapsedTime / duration;
        _transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
    }
}
