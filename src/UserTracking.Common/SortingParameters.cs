using System.Collections.Generic;
using System.Text;

namespace UserTracking.Common
{
    public class SortingParameters
    {
        public SortingParameters(IList<SortingPair> sortingPairs)
        {
            this.Sorters = sortingPairs;
        }

        public IList<SortingPair> Sorters { get; }

        public string GetSortExpression()
        {
            StringBuilder expression = new StringBuilder();

            for (int i = 0; i < Sorters.Count; i++)
            {
                expression.Append(Sorters[i].GetSortExpression());

                if (!(i == Sorters.Count - 1))
                {
                    expression.Append(", ");
                }
            }
            return expression.ToString();
        }
    }

    public class SortingPair
    {
        public SortingPair(string orderBy, string direction)
        {
            this.OrderBy = orderBy;
            this.Direction = direction;
        }
        public string OrderBy { get; }

        public string Direction { get; }

        public string GetSortExpression()
        {
            return $"{OrderBy} {Direction}";
        }
    }
}
