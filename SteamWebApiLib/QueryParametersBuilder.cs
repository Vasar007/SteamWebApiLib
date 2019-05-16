using System.Text;

namespace SteamWebApiLib
{
    internal class QueryParametersBuilder
    {
        private readonly StringBuilder _stringBuilder;

        internal bool HasValue { get; private set; }


        internal QueryParametersBuilder()
        {
            _stringBuilder = new StringBuilder();
        }

        internal QueryParametersBuilder(int capacity)
        {
            _stringBuilder = new StringBuilder(capacity);
        }

        internal void Reset()
        {
            HasValue = false;
            _stringBuilder.Clear();
        }

        internal void AppendParameter(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(value)) return;

            if (HasValue)
            {
                _stringBuilder.Append($"&{name}={value}");
            }
            else
            {
                HasValue = true;
                _stringBuilder.Append($"?{name}={value}");
            }
        }

        #region Object Overridden Methods

        public override string ToString() => _stringBuilder.ToString();

        #endregion
    }
}
