using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Interfaces
{
    public interface ILabelRepository
    {
        IConfiguration Configuration { get; set; }

        Task<string> Add(LabelStatusModel labelStatusModel);
        Task<string> Remove(LabelStatusModel labelStatusModel);
        Task<List<string>> Show(long UserId);
        List<NotesModel> ShowLabelData(long UserId);
    }
}