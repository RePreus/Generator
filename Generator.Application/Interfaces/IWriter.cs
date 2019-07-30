using System.Threading.Tasks;

namespace Generator.Application.Interfaces
{
    public interface IWriter<T>
    {
        Task Save(T input);
    }
}
