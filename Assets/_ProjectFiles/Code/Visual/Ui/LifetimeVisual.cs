using Egsp.Core.Ui;
using Game.Extensions;

namespace Game.Visual.Ui
{
    [TerminateFormat(TerminateFormat.Deactivate)]
    public class LifetimeVisual : LifetimeMonoBehaviour, IVisual
    {
        public void Enable()
        {
            if (IsTerminated)
                return;
            
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            if (IsTerminated)
                return;
            
            gameObject.SetActive(false);
        }
    }
}