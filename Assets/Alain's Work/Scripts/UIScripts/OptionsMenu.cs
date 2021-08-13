using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
  
    [SerializeField] private Button back;
    [SerializeField] private GameObject BG1;
    [SerializeField] private GameObject BG2;
    [SerializeField] private GameObject parent1;
    [SerializeField] private GameObject parent2;
    [SerializeField] private GameObject parent3;

    // Start is called before the first frame update
    void Start()
    {
        this.parent1.SetActive(false);
        this.parent2.SetActive(false);
        this.parent3.SetActive(true);
        this.BG1.SetActive(false);
        this.BG2.SetActive(true);

        Button btn = back.GetComponent<Button>();
        btn.onClick.AddListener(OnTapBack);

 
    }

    public void OnTapBack()
    {
        this.parent2.SetActive(true);
        this.parent3.SetActive(false);
        this.BG2.SetActive(true);
    }
   
}
