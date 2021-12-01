using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interfaces
{
    public interface ILabelRepository
    {
        Task<string> CreateLabel(CreateLabelModel createLabel);
        Task<List<string>> LabelList(long UserId);
        Task<string> EditLabelName(EditLabelModel editLabelModel);
        Task<string> DeleteLabels(string LabelName, long UserId);
        List<NotesModel> ShowlabelNotes(long UserId, string LabelName);
        Task<string> Delete(long userId, string labelNames);
    }
}