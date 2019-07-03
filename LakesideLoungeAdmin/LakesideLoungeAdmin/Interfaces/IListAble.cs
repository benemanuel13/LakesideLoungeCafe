using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakesideLoungeAdmin.Interfaces
{
    public interface IListAble<T>
    {
        int Id { get; }
        int ParentId { get; }
        string Description { get; }
        bool HasChildren { get; }
        bool Subselected { get; }
        List<T> Children { get; }
        bool ShowIcon { get; set; }
    }
}
