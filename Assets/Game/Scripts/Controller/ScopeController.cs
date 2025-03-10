using UnityEngine;

public class ScopeController : MonoBehaviour
{
    private RectTransform _rectTransform;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
