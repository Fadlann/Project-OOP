using UnityEngine;
using UnityEngine.Events;

namespace Weaponry
{

public class AnimationEvent : UnityEvent<string>
{

}

public class WeaponAnimationEvents : MonoBehaviour 
{
    public readonly AnimationEvent WeaponAnimationEvent = new AnimationEvent();

    public void OnAnimationEvent(string eventName)    
    {
        WeaponAnimationEvent.Invoke(eventName);
    }
}
}