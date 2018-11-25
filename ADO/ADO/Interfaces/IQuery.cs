namespace ADO.Interfaces
{
    public interface IQuery
    {
        string Title { get; }

        string Execute();
    }
}
