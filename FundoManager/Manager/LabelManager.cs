using FundoManager.Interfaces;
using FundooModels;
using FundooRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundoManager.Manager
{
    public class LabelManager : ILabelManager
    {
        private ILabelRepository _labelRepository;
        public LabelManager(ILabelRepository labelRepository)
        {
            _labelRepository = labelRepository;
        }
        public async Task<string> AddLabel(LabelStatusModel labelStatusModel)
        {
            try
            {
                return await _labelRepository.Add(labelStatusModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> RemoveLabel(LabelStatusModel labelStatusModel)
        {
            try
            {
                return await _labelRepository.Remove(labelStatusModel);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<string>> ShowLabels(long UserId)
        {
            try
            {
                return await _labelRepository.Show(UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NotesModel> LabelData(long UserId)
        {
            try
            {
                return _labelRepository.ShowLabelData(UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
