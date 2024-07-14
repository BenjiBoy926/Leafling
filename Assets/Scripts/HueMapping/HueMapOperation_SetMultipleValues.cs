public class HueMapOperation_SetMultipleValues
{
    private HueMapOperation_SetValue[] _operations;

    public HueMapOperation_SetMultipleValues(params HueMapOperation_SetValue[] operations)
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
