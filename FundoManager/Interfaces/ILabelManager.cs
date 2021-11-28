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
        /// new Implement
        Task<string> CreateLabel(CreateLabelModel createLabelModel);
        Task<List<string>> ShowLabelList(long UserId);
        Task<string> EditLabel(EditLabelModel editLabelModel);
        Task<string> DeleteLabel(string LabelName, long UserId);
    }
}