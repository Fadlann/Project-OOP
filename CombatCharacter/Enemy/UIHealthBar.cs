using UnityEngine;
using UnityEngine.UI;

namespace CombatCharacter.Enemy
{
public class UIHealthBar : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    [SerializeField] Image foreground;
    [SerializeField] Image background;

    private void LateUpdate() 
    {
        Vector3 direction = (target.position - Camera.main.transform.position).normalized;
        bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= .0f;
        foreground.enabled = !isBehind;
        background.enabled = !isBehind;
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }

    public void SetHealthBarPercentage(float percentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
}