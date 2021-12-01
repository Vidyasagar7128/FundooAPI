﻿using FundoManager.Interfaces;
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
        private readonly ILabelRepository _labelRepository;
        public LabelManager(ILabelRepository labelRepository)
        {
            _labelRepository = labelRepository;
        }
        public async Task<string> CreateLabel(CreateLabelModel createLabelModel)
        {
            try
            {
                return await _labelRepository.CreateLabel(createLabelModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Show Labels List
        /// </summary>
        /// <param name="createLabelModel"></param>
        /// <returns></returns>
        public async Task<List<string>> ShowLabelList(long UserId)
        {
            try
            {
                return await _labelRepository.LabelList(UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Edit Labels Name
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="newLabel"></param>
        /// <returns></returns>
        public async Task<string> EditLabel(EditLabelModel editLabelModel)
        {
            try
            {
                return await _labelRepository.EditLabelName(editLabelModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Delete Labels
        /// </summary>
        /// <param name="LabelName"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<string> DeleteLabel(string LabelName, long UserId)
        {
            try
            {
                return await _labelRepository.DeleteLabels(LabelName, UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NotesModel> ShowLabelLisData(long UserId, string LabelName)
        {
            try
            {
                return _labelRepository.ShowlabelNotes(UserId,LabelName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> DelLabel(long userId, string labelNames)
        {
            try
            {
                return await _labelRepository.Delete(userId, labelNames);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
