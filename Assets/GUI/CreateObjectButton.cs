using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CreateObjectButton:MonoBehaviour
{
    static readonly int s_buttonCount = 10;
    Button m_parentButton;
    Color m_normalColor;
    Color m_highLitedColor;
    Color m_pressedColor;
    public Image m_prefabImage;
    public CreatableObject m_prefab;
    void Start()
    {
        
        m_parentButton = GetComponent<Button>();
        m_normalColor = m_parentButton.colors.normalColor;
        m_highLitedColor = m_parentButton.colors.highlightedColor;
        m_pressedColor = m_parentButton.colors.pressedColor;
        m_prefabImage.sprite = Resources.Load<Sprite>(m_prefab.IconName);
        m_prefabImage.rectTransform.localPosition = new Vector3(0, 0, 0);
       
    }
    public void InitButton(int position)
    {
        if(position>=s_buttonCount)return;
        RectTransform buttonTransform = GetComponent<RectTransform>();
        buttonTransform.anchorMin = new Vector2(0, 1 - (1.0f / s_buttonCount) * (position + 1));
        buttonTransform.anchorMax = new Vector2(0.95f, 1 - (1.0f / s_buttonCount) * (position));
        buttonTransform.offsetMin = new Vector2(0, 0);
        buttonTransform.offsetMax = new Vector2(1, 1);
    }
    public void SetPressed()
    {
        GUIMachine.SelectedPrefab = m_prefab.gameObject;
        ColorBlock colors = m_parentButton.colors;
        colors.highlightedColor = m_pressedColor;
        colors.normalColor = m_pressedColor;
        m_parentButton.colors = colors;
        GUIMachine.SelectedButton = this;
    }
    public void Release()
    {
        ColorBlock colors = m_parentButton.colors;
        colors.highlightedColor = m_highLitedColor;
        colors.normalColor = m_normalColor;
        m_parentButton.colors = colors;
        GUIMachine.SelectedButton = null;
    }

}
