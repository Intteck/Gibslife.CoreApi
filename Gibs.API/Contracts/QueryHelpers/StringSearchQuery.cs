using System;

namespace Gibs.Api.Contracts.V1
{
    public class StringSearchQuery
    {
        internal string[] SearchStrings { get; private set; } = [];

        public string SearchText
        {
            get
            {
                return string.Join(' ', SearchStrings);
            }
            set
            {
                char[] separator = [' '];
                SearchStrings = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        internal bool CanSearch => SearchStrings.Length > 0;
    }
}
