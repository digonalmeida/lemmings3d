namespace LevelMap
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class MapSettings
    {
        [SerializeField] private int lemmingsCount = 10;
        [SerializeField] private int minimumVictoryCount = 3;
        [SerializeField] private int minimumSpawnRate = 30;
        [SerializeField] private int maximumSpawnRate = 70;
        [SerializeField] private int startSpawnRate = 50;
        [SerializeField] private float levelTime = 120f;
        [SerializeField] private SkillsCounter skillsCounter = new SkillsCounter();

        public MapSettings()
        {

        }

        public MapSettings(MapSettings other)
        {
            lemmingsCount = other.lemmingsCount;
            minimumVictoryCount = other.minimumVictoryCount;
            skillsCounter = new SkillsCounter(other.skillsCounter);
            minimumSpawnRate = other.minimumSpawnRate;
            maximumSpawnRate = other.maximumSpawnRate;
            startSpawnRate = other.startSpawnRate;
            levelTime = other.levelTime;
        }

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

        public int MinimumSpawnRate
        {
            get
            {
                return minimumSpawnRate;
            }

            set
            {
                minimumSpawnRate = value;
            }
        }

        public int MaximumSpawnRate
        {
            get
            {
                return maximumSpawnRate;
            }

            set
            {
                maximumSpawnRate = value;
            }
        }

        public int StartSpawnRate
        {
            get
            {
                return startSpawnRate;
            }

            set
            {
                startSpawnRate = value;
            }
        }

        public float LevelTime
        {
            get
            {
                return levelTime;
            }

            set
            {
                levelTime = value;
            }
        }
    }
}