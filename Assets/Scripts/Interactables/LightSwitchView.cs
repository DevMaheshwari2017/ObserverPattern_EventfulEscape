using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    private void Start() => currentState = SwitchState.Off;

    public delegate void LightSwitchDelegate();//Created delegate - signature
    public static LightSwitchDelegate LightSwitch;//Instance

    private void OnEnable() => LightSwitch += LightToggeled; //Adding Function to delegate
    private void OnDisable() => LightSwitch -= LightToggeled; //unsub listner/observer

    public void Interact()
    {
        //Todo - Implement Interaction
        LightSwitch?.Invoke(); // ?. does a null check before Invoking
    }
    private void toggleLights()
    {
        bool lights = false;

        switch (currentState)
        {
            case SwitchState.On:
                currentState = SwitchState.Off;
                lights = false;
                break;
            case SwitchState.Off:
                currentState = SwitchState.On;
                lights = true;
                break;
            case SwitchState.Unresponsive:
                break;
        }
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }

    private void LightToggeled() 
    {
        toggleLights();
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
    }
}
