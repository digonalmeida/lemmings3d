using System;

public static class GameEvents {

    public static class GameState {

        public static Action OnStartGame;
        public static Action OnEndGame;
        public static Action OnLoadGame;
    }

    public static class UI {
        public static Action SelectedSkill;
        public static Action DeselectedSkill;
        public static Action IncreasedSpawnRate;
        public static Action DecreasedSpawnRate;
        public static Action ToggleMapEditor;
    }

    public static class Lemmings {

        public delegate void SpawnRateAction(int newRate);

        public static Action LemmingReachedExit;
        public static Action LemmingSpawned;
        public static Action LemmingUsedSkill;
        public static Action LemmingDied;
        public static Action<int> ChangedSpawnRate;
    }
}
