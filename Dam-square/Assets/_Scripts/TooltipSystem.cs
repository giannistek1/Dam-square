using System;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    #region Fields
    private static TooltipSystem current;

    public Tooltip tooltip;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        current = this;
    }

    #endregion

    public static void Show(string content, string header = "")
    {
        current.tooltip.setText(content, header);
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}
