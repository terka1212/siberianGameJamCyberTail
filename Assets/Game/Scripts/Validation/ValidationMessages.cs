namespace Game.Validation
{
    public static class ValidationMessages
    {
        public const string POINT_AND_CLICK_BLOCKED = "Point and Click: Click was blocked, but still trying do click";

        public const string POINT_AND_CLICK_AGENT_NOT_EXIST =
            "Point and Click: Agent is not existed in this LifetimeScope, but still trying do click";

        public const string AUDIO_MUSIC_DONT_FOUND = "Audio: music that tried to play now dont exists";
        public const string AUDIO_EFFECT_DONT_FOUND = "Audio: effec that tried to play now dont exists";
    }
}