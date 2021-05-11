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
        current.tooltip.Show();
    }

    public static void Hide()
    {
        current.tooltip.Hide();
    }
}
