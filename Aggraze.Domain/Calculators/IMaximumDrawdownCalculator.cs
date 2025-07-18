﻿using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IMaximumDrawdownCalculator
{
    IInsightResult Calculate(string name, IReadOnlyList<TradeRow> trades);
}