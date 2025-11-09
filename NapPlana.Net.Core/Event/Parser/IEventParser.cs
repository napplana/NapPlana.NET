namespace NapPlana.Core.Event.Parser;

public interface IEventParser
{
    public void ParseEvent(string jsonEventData);
}