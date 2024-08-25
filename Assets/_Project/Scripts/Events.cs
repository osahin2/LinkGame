using System;
namespace Event
{
    public class Events
    {
        public const string RESTART_LEVEL = "RestartLevel";
        public const string NEXT_LEVEL = "NextLevel";
        public const string LINK_COMPLETED = "LinkCompleted";
        public const string LEVEL_FAILED = "LevelFailed";

        public class RestartLevel : EventArgs { }
        public class NextLevel : EventArgs { }
        public class LinkCompleted : EventArgs
        {
            public int NewScore { get; }
            public int OldScore { get; }
            public int MoveCount { get; }
            public LinkCompleted(int newScore, int oldScore, int moveCount)
            {

                NewScore = newScore;
                OldScore = oldScore;
                MoveCount = moveCount;
            }
        }
    }
}
