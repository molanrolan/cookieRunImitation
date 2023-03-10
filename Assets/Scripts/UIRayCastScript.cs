using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIRayCastScript : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("UIRayCast pointer down executed" + eventData.pointerCurrentRaycast.gameObject.name);
        PauseManager.Instance.onPaused();
    }
}
