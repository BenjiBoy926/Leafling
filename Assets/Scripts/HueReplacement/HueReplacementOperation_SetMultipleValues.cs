public class HueReplacementOperation_SetMultipleValues
{
    private HueReplacementOperation_SetValue[] _operations;

    public HueReplacementOperation_SetMultipleValues(params HueReplacementOperation_SetValue[] operations)
    {
        _operations = operations;
    }

    public void Perform(HueReplacement map)
    {
        foreach (var operation in _operations)
        {
            operation.Perform(map);
        }
    }
}
