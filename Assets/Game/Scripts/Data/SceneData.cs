using System;

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
        Street,
        Market,
        Roof,
        SampleScene,
        SampleScene2
    }
}