using System;
using System.Collections.Generic;
using Aggraze.Domain.Types;

namespace Aggraze.UnitTests;

public class TradeRowDataBuilder
{
    private readonly List<TradeRowData> _rows = [];
    private TradeRowData _data = new();

    public TradeRowDataBuilder WithOpenTime(TimeOnly openTime)
    {
        _data.OpenTime = openTime;
        return this;
    }

    public TradeRowDataBuilder WithClosingTime(TimeOnly closingTime)
    {
        _data.ClosingTime = closingTime;
        return this;
    }

    public TradeRowDataBuilder WithMutation(int mutation)
    {
        _data.Mutation = mutation;
        return this;
    }
    
    public TradeRowDataBuilder WithMaximumDrawdown(Pips maximumDrawdown)
    {
        _data.MaximumDrawdown = maximumDrawdown;
        return this;
    }

    public TradeRowDataBuilder WithResult(Result result)
    {
        _data.Result = result;
        return this;
    }
    
    public TradeRowDataBuilder WithMaximumResult(decimal maximumResult)
    {
        _data.MaximumResult = maximumResult;
        return this;
    }

    public TradeRowDataBuilder Add()
    {
        _rows.Add(_data);
        _data = new TradeRowData();
        return this;
    }

    public List<TradeRowData> Build() => _rows;
}