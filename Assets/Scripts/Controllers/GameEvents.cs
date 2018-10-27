using System;

public static class GameEvents {

    public static class GameState {
        public delegate void GameStateAction();

        public static event GameStateAction OnStartGame;
        public static event GameStateAction OnEndGame;
        public static event GameStateAction OnLoadGame;

        public static void TriggerOnLoadGame()
        {
            if (OnLoadGame != null)
            {
                OnLoadGame.Invoke();
            }
        }

        public static void TriggerOnStartGame()
        {
            if (OnStartGame != null)
            {
                OnStartGame.Invoke();
            }
        }

        public static void TriggerOnEndGame() {
            if (OnEndGame != null)
            {
                OnEndGame.Invoke();
            }
        }
}

    public static class UI {
        public static Action SelectedSkill;
        public static Action DeselectedSkill;
        public static Action IncreasedSpawnRate;
        public static Action DecreasedSpawnRate;
    }

    public static class Lemmings {

        public delegate void SpawnRateAction(int newRate);

        public static Action LemmingReachedExit;
        public static Action LemmingSpawned;
        public static Action LemmingUsedSkill;
        public static event SpawnRateAction ChangedSpawnRate;

        
        public static void TriggerSpawnRateChange(int newRate)
        {
            if (ChangedSpawnRate != null)
                ChangedSpawnRate.Invoke(newRate);
        }

    }
}
