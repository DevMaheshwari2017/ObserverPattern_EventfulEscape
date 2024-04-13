using UnityEngine;

public class LightsOffByGhostEvent : MonoBehaviour
{
    [SerializeField] private int KeysRequiredToTrigger;
    [SerializeField] SoundType soundType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerView>() != null && KeysRequiredToTrigger == GameService.Instance.GetPlayerController().KeysEquipped) 
        {
            EventService.Instance.OnLightsOffByGhostEvent.InvokeEvent();
            GameService.Instance.GetSoundView().PlaySoundEffects(soundType);
        }
    }

}