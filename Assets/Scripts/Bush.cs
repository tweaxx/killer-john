using UnityEngine;

public class Bush : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Color _initialColor;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _initialColor = _renderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {
            unit.SetVisibility(false);
            SetVisibility(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {
            unit.SetVisibility(true);
            SetVisibility(true);
        }
    }

    public void SetVisibility(bool visible)
    {
        var color = _initialColor;
        color.a = 0.8f;

        _renderer.color = visible ? _initialColor : color;
    }
}
