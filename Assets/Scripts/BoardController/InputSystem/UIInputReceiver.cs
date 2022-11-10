using UnityEngine;
using UnityEngine.Events;

public class UIInputReceiver : InputReceiver
{
    [SerializeField] private UnityEvent clickEvent;

    public override void OnInputReceived()
    {
        foreach (var handler in InputHandlers)
        {
            handler.ProcessInput(Input.mousePosition, gameObject, () => clickEvent.Invoke());
        }
    }
}