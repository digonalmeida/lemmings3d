namespace LevelMap
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class MapSettings
    {
        [SerializeField]
        private int lemmingsCount = 10;

        [SerializeField]
        private int minimumVictoryCount = 3;

        [SerializeField]
        private SkillsCounter skillsCounter = new SkillsCounter();

        public int LemmingsCount
        {
            get
            {
                return lemmingsCount;
            }
            set
            {
                lemmingsCount = value;
            }
        }

        public int MinimumVictoryCount
        {
            get
            {
                return minimumVictoryCount;
            }
            set
            {
                minimumVictoryCount = value;
            }
        }

        public SkillsCounter SkillsCounter
        {
            get
            {
                return skillsCounter;
            }
            set
            {
                skillsCounter = value;
            }
        }
    }
}