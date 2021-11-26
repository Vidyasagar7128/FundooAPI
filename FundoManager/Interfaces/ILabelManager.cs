using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundoManager.Interfaces
{
    public interface ILabelManager
    {
        Task<string> AddLabel(LabelStatusModel labelStatusModel);
        Task<string> RemoveLabel(LabelStatusModel labelStatusModel);
        Task<List<string>> ShowLabels(long UserId);
        List<NotesModel> LabelData(long UserId);
    }
}