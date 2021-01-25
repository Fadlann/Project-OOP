using UnityEngine;
using UnityEngine.UI;

namespace CombatCharacter
{
public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private CombatEntity target;
    [SerializeField] private Vector3 offset;
    [SerializeField] bool hoverOverTarget = false;

    [SerializeField] private Image foreground;
    [SerializeField] private Image background;

    private void Start() 
    {
        target.OnHit += SetHealthBarPercentage;
        target.OnDeath += Deactivate;
    }

    private void LateUpdate() 
    {
        if (target.Dead)
            return;
        
        if (hoverOverTarget)
        {
            Vector3 direction = (target.transform.position - Camera.main.transform.position).normalized;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 1000, ~LayerMask.GetMask("Player")))
            {
                if (hit.transform.gameObject == target.gameObject && Vector3.Dot(direction, Camera.main.transform.forward) > .0f)
                    Activate();
                else
                    Deactivate();                
            }
            transform.position = Camera.main.WorldToScreenPoint(target.transform.position + offset);
        }
    }

    private void SetHealthBarPercentage()
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * (target.CurrentHealth / target.MaxHealth);
        foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    private void Deactivate()
    {
        foreground.enabled = background.enabled = false;
    }

    private void Activate()
    {
        foreground.enabled = background.enabled = true;
    }
}
}