using System.Collections;
using VContainer;

namespace Game.UI
{
    public class FadeService
    {
        private readonly Fade _fade;

        [Inject]
        public FadeService(Fade fade)
        {
            _fade = fade;
        }
        
        public IEnumerator Show() => _fade.FadeIn();

        public IEnumerator Hide() => _fade.FadeOut();
    }
}