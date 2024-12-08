using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplays : MonoBehaviour
{
    
    [SerializeField] private Slider healthDisplay;
    [SerializeField] private Slider strikesDisplay;
    [SerializeField] private Slider hackDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.value = HackPlayer.Instance.Health / HackPlayer.Instance.MaxHealth;
        strikesDisplay.value = 1 - (float) StrikeManger._instance.numStrikes / StrikeManger._instance.maxStrikes;
        hackDisplay.value = 1 - GameManager._instance.timer / GameManager._instance.maxTime;
    }
}
