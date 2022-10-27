public interface ILevelProgressUpdateHandler : ISubscriber
{
    public void OnLevelProgressUpdate(int progress);
}
