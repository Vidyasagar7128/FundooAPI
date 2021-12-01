using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundoManager.Interfaces
{
    public interface ILabelManager
    {
        Task<string> CreateLabel(CreateLabelModel createLabelModel);
        Task<List<string>> ShowLabelList(long UserId);
        Task<string> EditLabel(EditLabelModel editLabelModel);
        Task<string> DeleteLabel(string LabelName, long UserId);
        List<NotesModel> ShowLabelLisData(long UserId, string LabelName);
        Task<string> DelLabel(long userId, string labelNames);
    }
}