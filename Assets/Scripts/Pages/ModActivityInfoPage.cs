﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ModActivityInfoPage : MonoBehaviour
{
    public KeyValuePair<string, string> ModNameAndDisplayName = new KeyValuePair<string, string>(null, null);
    public TextMesh DetailText = null;

    private Page _page = null;

    private void Awake()
    {
        _page = GetComponent<Page>();
    }

    public void OnEnable()
    {
        if (ModNameAndDisplayName.Key == null || ModNameAndDisplayName.Value == null)
        {
            return;
        }

        _page.HeaderText = string.Format("<b>{0}</b>\n<size=16>Active Profile Configuration</size>", ModNameAndDisplayName.Value == null ? "**NULL**" : ModNameAndDisplayName.Value);

        if (DetailText == null)
        {
            return;
        }

        List<ProfileManager.ProfileEntry> profileEntries = null;
        if (!ProfileManager.ActiveProfilesEntries.TryGetValue(ModNameAndDisplayName.Key, out profileEntries) || profileEntries == null || profileEntries.Count == 0)
        {
            DetailText.text = "<i>Not referenced in any active profiles.</i>";
            return;
        }

        StringBuilder builder = new StringBuilder();

        foreach (ProfileManager.ProfileEntry entry in profileEntries.OrderByDescending((x) => x.SetOperation).ThenBy((x) => x.EnableFlag).ThenBy((y) => y.ProfileName))
        {
            string enabled = "";
            string color = "";
            switch (entry.EnableFlag)
            {
                case Profile.EnableFlag.Enabled:
                    enabled = "Enabled";
                    color = "008800";
                    break;
                case Profile.EnableFlag.Disabled:
                    enabled = "Disabled";
                    color = "880000";
                    break;

                default:
                    enabled = "Enabled";
                    color = "008800";
                    break;

            }

            builder.AppendFormat("• {0}: <color=#{3}><b>{1}</b> <i>({2})</i></color>\n", entry.ProfileName, enabled, entry.SetOperation.ToString(), color);
        }

        DetailText.text = builder.ToString();
    }
}
