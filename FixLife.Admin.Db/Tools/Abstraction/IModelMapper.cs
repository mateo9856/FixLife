namespace FixLife.Admin.Db.Tools.Abstraction
{
    public interface IModelMapper<T1, T2> 
    {
        T2 Map(T1 source);
        T1 MapBack(T2 source);
    }
}
