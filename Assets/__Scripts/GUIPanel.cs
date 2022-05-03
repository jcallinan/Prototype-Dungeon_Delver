using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPanel : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Dray dray;
    public Sprite healthEmpty;
    public Sprite healthHalf;
    public Sprite healthFull;
    public Text keyCountText;
    public List<SpriteRenderer> healthBars;

    private Sprite[] _healthSprites;


    private void Start()
    {
        keyCountText = transform.Find("Key Count").GetComponent<Text>();

        healthBars = new List<SpriteRenderer>();
        Transform healthPanel = transform.Find("Health Panel");
        if (healthPanel != null)
        {
            SpriteRenderer[] children = healthPanel.GetComponentsInChildren<SpriteRenderer>();
            healthBars = children.ToList();
        }
        
        _healthSprites = new Sprite[]{healthEmpty, healthHalf, healthFull};
    }

    private void Update()
    {
        keyCountText.text = dray.numKeys.ToString();

        int health = (dray.health < 0) ? 0 : dray.health;           
        if (health > 4)
        {
            healthBars[0].sprite = _healthSprites[(health - 1) % 3];
            healthBars[1].sprite = healthFull;
            healthBars[2].sprite = healthFull;
        }
        else if (2 < health && health <= 4)
        {
            healthBars[0].sprite = healthEmpty;
            healthBars[1].sprite = _healthSprites[(health + 1) % 3];
            healthBars[2].sprite = healthFull;
        }
        else
        {
            healthBars[0].sprite = healthEmpty;
            healthBars[1].sprite = healthEmpty;
            healthBars[2].sprite = _healthSprites[health];
        }
    }
}