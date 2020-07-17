using Zenject;

namespace Battle.UI
{
    class StatBar : Bar
    {
        public int StatId { get; private set; }

        [Inject]
        public void Init(int statId, string icon)
        {
            InitUIComponents();
            StatId = statId;
            SetIcon(icon);
        }

        /// <summary>
        /// First argumet - stat id.
        /// Second argumet - icon name;
        /// </summary>
        public class Factory : PlaceholderFactory<int, string, StatBar>
        {
        }
    }
}
