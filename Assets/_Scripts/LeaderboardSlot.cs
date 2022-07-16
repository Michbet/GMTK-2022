using System;
using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class LeaderboardSlot : MonoBehaviour
    {
        public TMP_Text rank;
        public TMP_Text username;
        public TMP_Text level;
        public Image panelToHighlight;
        public Color highlightColor;

        private Color _initColor;

        private void Start()
        {
            _initColor = panelToHighlight.color;
        }

        public void PopulateSlot(LootLockerLeaderboardMember member, bool highlight = false)
        {
            if (member == null)
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            rank.text = member.rank.ToString();
            username.text = member.player.name;
            level.text = member.score.ToString();

            panelToHighlight.color = highlight ? highlightColor : _initColor;
        }
    }
}