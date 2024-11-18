using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SliderContr : MonoBehaviour,IDragHandler
{
    public RecordContrUI contrUI;
    public void OnDrag(PointerEventData eventData)
    {
        SetUpTimeValue();
    }
    void SetUpTimeValue()
    {
        contrUI.recordContr.playback.SetTimeThroughPlayback(contrUI.slider.value * contrUI.recordContr.playback.RecordingDuration());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
