using TeamProject.DAL.Repositories;

namespace TeamProject.DAL
{
    public interface ICinemaWork
    {
        UserRepository Users { get; }
        MovieRepository Movies { get; }
        ViewRepository Views { get; }
        void Save();
    }

}