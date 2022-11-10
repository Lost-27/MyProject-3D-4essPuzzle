using UnityEngine;

public class ColliderInputReceiver : InputReceiver
{
    private Vector3 _clickPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                _clickPosition = hit.point;
                OnInputReceived();
            }
        }
    }

    public override void OnInputReceived()
    {
        foreach (var handler in InputHandlers)
        {
            handler.ProcessInput(_clickPosition, null, null);
        }
    }
}