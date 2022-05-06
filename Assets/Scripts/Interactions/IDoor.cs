namespace Interactions
{
    public interface IDoor
    {
        void Open();
        void ForceOpen();
        void Close();
        bool IsOpen { get; }
    }
}