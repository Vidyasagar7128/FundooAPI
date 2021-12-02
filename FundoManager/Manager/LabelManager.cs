// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Gaikwad Vidyasagar"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundoManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundoManager.Interfaces;
    using FundooModels;
    using FundooRepository.Interfaces;

    /// <summary>
    /// LabelManager handles the ILabelRepository interface
    /// </summary>
    public class LabelManager : ILabelManager
    {
        /// <summary>
        /// created variable type of ILabelRepository
        /// </summary>
        private readonly ILabelRepository _labelRepository;

        /// <summary>
        /// assign values to class private variables
        /// </summary>
        /// <param name="labelRepository">passing ILabelRepository parameter</param>
        public LabelManager(ILabelRepository labelRepository)
        {
            this._labelRepository = labelRepository;
        }

        /// <summary>
        /// creating label for user
        /// </summary>
        /// <param name="createLabelModel">passing CreateLabelModel</param>
        /// <returns>rstring return</returns>
        public async Task<string> CreateLabel(CreateLabelModel createLabelModel)
        {
            try
            {
                return await this._labelRepository.CreateLabel(createLabelModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// List of labels
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <returns>list return</returns>
        public async Task<List<string>> ShowLabelList(long userId)
        {
            try
            {
                return await this._labelRepository.LabelList(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// edit labels
        /// </summary>
        /// <param name="editLabelModel">passing EditLabelModel</param>
        /// <returns>string return</returns>
        public async Task<string> EditLabel(EditLabelModel editLabelModel)
        {
            try
            {
                return await this._labelRepository.EditLabelName(editLabelModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete Labels
        /// </summary>
        /// <param name="labelName">passing labelName</param>
        /// <param name="userId">passing userId</param>
        /// <returns>string return</returns>
        public async Task<string> DeleteLabel(string labelName, long userId)
        {
            try
            {
                return await _labelRepository.DeleteLabels(labelName, userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// show Labels
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <param name="labelName">passing labelName</param>
        /// <returns>list return</returns>
        public List<NotesModel> ShowLabelLisData(long userId, string labelName)
        {
            try
            {
                return this._labelRepository.ShowlabelNotes(userId,labelName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// delete Label
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <param name="labelName">passing labelName</param>
        /// <returns>string return</returns>
        public async Task<string> DelLabel(long userId, string labelNames)
        {
            try
            {
                return await this._labelRepository.Delete(userId, labelNames);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
