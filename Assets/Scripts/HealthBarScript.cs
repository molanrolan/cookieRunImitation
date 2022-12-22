using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private PlayerScript _Player;
    private int _MaxHealth;
    [SerializeField] Image _Image;
    [SerializeField] TMPro.TMP_Text _Text;
    // Start is called before the first frame update
    void Start()
    {
        _Player=FindObjectOfType<PlayerScript>();
        _MaxHealth=_Player.Health;
    }

    // Update is called once per frame
    void Update()
    {
        _Image.fillAmount=(float)_Player.Health/(float)_MaxHealth;
        _Text.text = _Player.Health+"/"+_MaxHealth;
    }
}
