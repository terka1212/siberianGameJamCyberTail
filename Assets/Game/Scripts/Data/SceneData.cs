namespace Game.Data
{
    public class SceneData
    {
        public SceneName sceneLoadedFrom { get; set; }
    }
    
    public enum SceneName
    {
        Bootstrap,
        Menu,
        Tutorial,
        TutorialNew,
        Street,
        StreetNew,
        Market,
        MarketNew,
        Roof,
        RoofNew,
        SampleScene,
    }
}