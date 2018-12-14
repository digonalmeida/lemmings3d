using System;
using LevelMap;
using UnityEngine;

public static class GameEvents
{
    public static class GameState
    {
        public static Action OnStartGame;
        public static Action OnEndGame;
        public static Action OnLoadGame;
        public static Action<Player> OnPlayerWin;
        public static Action<Player> OnBothPlayersWin;
        public static Action OnBothPlayersLose;
    }

    public static class UI
    {
        public static Action SelectedSkill;
        public static Action DeselectedSkill;
        public static Action IncreasedSpawnRate;
        public static Action DecreasedSpawnRate;
        public static Action ToggleMapEditor;
        public static Action OnSkillsLoaded;
        public static Action OpenInGameUI;
        public static Action CloseInGameUI;
    }

    public static class Lemmings
    {
        public delegate void SpawnRateAction(int newRate);

        public static Action<LemmingStateController> LemmingReachedExit;
        public static Action<LemmingStateController> LemmingSpawned;
        public static Action<LemmingStateController> LemmingUsedSkill;
        public static Action<LemmingStateController> LemmingDied;
        public static Action<Player> ChangedSpawnRate;
        public static Action<LemmingSpawnInfo> OnSpawnRequest;
    }

    public static class NetworkLemmings
    {
        public static Action<LemmingStateController> LemmingReachedExit;
        public static Action<LemmingStateController> LemmingSpawned;
        public static Action<LemmingStateController> LemmingDied;
    }

    public static class Map
    {
        public static Action OnMapLoaded;
        public static Action<Vector3Int, MapBlock> OnAddBlock;
        public static Action<Vector3Int> OnRemoveBlock;
    }
}