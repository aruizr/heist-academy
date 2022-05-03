namespace Interactions.Final
{
    public interface IDoor
    {
        void Open();
        void ForceOpen();
        void Close();
        bool IsOpen { get; }
    }
}