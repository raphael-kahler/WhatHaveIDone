using System.Collections.Generic;

namespace Whid.Domain
{
    public class PeriodOrdering
    {
        private readonly Dictionary<PeriodTypeEnum, int> _nameToOrderValueMap;
        private readonly Dictionary<int, PeriodTypeEnum> _orderValueToNameMap;

        public PeriodOrdering(IEnumerable<PeriodTypeEnum> ordering)
        {
            _nameToOrderValueMap = new Dictionary<PeriodTypeEnum, int>();
            _orderValueToNameMap = new Dictionary<int, PeriodTypeEnum>();

            var orderValue = 0;
            foreach (var element in ordering)
            {
                _nameToOrderValueMap.Add(element, orderValue);
                _orderValueToNameMap.Add(orderValue, element);
                orderValue++;
            }
        }

        public bool SummarizesOthers(PeriodTypeEnum type) =>
            _nameToOrderValueMap[type] > 0;

        public bool SummarizedByOthers(PeriodTypeEnum type) =>
            _nameToOrderValueMap[type] < _nameToOrderValueMap.Count - 1;

        public PeriodTypeEnum GetSummarizedType(PeriodTypeEnum type) =>
            _orderValueToNameMap[_nameToOrderValueMap[type] - 1];

        public PeriodTypeEnum GetSummarizingType(PeriodTypeEnum type) =>
            _orderValueToNameMap[_nameToOrderValueMap[type] + 1];

        ///// <summary>
        ///// Check if the summary type directly summarizes the specified other summary type.
        ///// </summary>
        ///// <param name="other">The other summary type.</param>
        ///// <returns>True if the summary type directly summarizes the specified summary type. False otherwise.</returns>
        //public bool Summarizes(PeriodType first, PeriodType other) =>
        //    _nameToOrderValueMap[first.Type] - 1 == _nameToOrderValueMap[other.Type];

        ///// <summary>
        ///// Check if the summary type summarizes the specified other type, either directly or indirectly.
        ///// This check evaluates to true if the give type summarizes zero to many other types which in turn summarize the specified type.
        ///// </summary>
        ///// <param name="other">The other summary type.</param>
        ///// <returns>True if the summary type summarizes the specified summary type. False otherwise.</returns>
        //public bool SummarizesRecursively(PeriodType first, PeriodType other) =>
        //    _nameToOrderValueMap[first.Type] > _nameToOrderValueMap[other.Type];


    }
}
