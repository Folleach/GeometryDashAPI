namespace GeometryDashAPI.Factories
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
