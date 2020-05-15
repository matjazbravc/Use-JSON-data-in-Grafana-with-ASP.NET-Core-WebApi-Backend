namespace GrafanaJsonWebApiDemo.Contracts
{
    /// <summary>
    /// Generic converter interface
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public interface IConverter<in TSource, out TDestination>
    {
        TDestination Convert(TSource source);
    }
}
