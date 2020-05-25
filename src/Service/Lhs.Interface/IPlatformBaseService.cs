using Core.Data;

namespace Lhs.Interface
{
    public interface IPlatformBaseService<T> : IDenpendencyRepository, IBaseService<T> where T : class
    {

    }
}
