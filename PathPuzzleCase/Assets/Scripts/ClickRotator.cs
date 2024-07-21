using UnityEngine;

public class ClickRotator : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        transform.Rotate(0, 0, -90);
    }
}
