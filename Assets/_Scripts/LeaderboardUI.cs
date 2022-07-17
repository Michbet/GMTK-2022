using LootLocker.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class LeaderboardUI : MonoBehaviour
    {
        public LeaderboardSlot[] slots;

        private int SlotsPerPage => slots.Length;
        
        private int _pageIndex = 0;

        private LootLockerLeaderboardMember[] Scores => LeaderboardManager.Scores;
        private int MaxPageIndex => Scores == null ? 0 : Scores.Length / SlotsPerPage;

        private void Start()
        {
            LeaderboardManager.Instance.LoadScores();
        }

        private void Update()
        {
            if (LeaderboardManager.Scores == null)
            {
                foreach(var slot in slots) slot.gameObject.SetActive(false);
                return;
            }
            
            for (int i = 0; i < SlotsPerPage; i++)
            {
                var scoreIndex = _pageIndex * SlotsPerPage + i;
                slots[i].PopulateSlot(scoreIndex < LeaderboardManager.Scores.Length ? LeaderboardManager.Scores[scoreIndex] : null);
            }
        }

        public void NextPage()
        {
            if(_pageIndex < MaxPageIndex)
                _pageIndex++;
        }
        
        public void PrevPage()
        {
            if(_pageIndex > 0)
                _pageIndex--;
        }

        public void PlayAgain() => SceneManager.LoadScene("SampleScene");

    }
}