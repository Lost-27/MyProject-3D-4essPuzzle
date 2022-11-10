using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIInputReceiver))]
public class UIButton : Button
{
    private InputReceiver _reciever;

    protected override void Awake()
    {
        _reciever = GetComponent<UIInputReceiver>();
        onClick.AddListener(() => _reciever.OnInputReceived());
    }
}