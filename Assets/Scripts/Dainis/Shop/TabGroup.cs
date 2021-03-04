using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public TabButton selectedTab;
    public List<GameObject> tabsToSwap;

    public void SubscribeTab(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.Background.color = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();

    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs(); 
        button.Background.color = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < tabsToSwap.Count; i++)
        {
            if (i == index)
            {
                tabsToSwap[i].SetActive(true);
            }
            else
            {
                tabsToSwap[i].SetActive(false);
            }
        }

    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab) 
            {
                continue;
            }
            button.Background.color = tabIdle;
        }
    }

}
