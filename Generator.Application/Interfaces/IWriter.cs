using System.Collections.Generic;

namespace Generator.Application.Interfaces
{
    public interface IWriter
    {
        void Save(string[] pictures, Domain.Choice choice);
    }
}
