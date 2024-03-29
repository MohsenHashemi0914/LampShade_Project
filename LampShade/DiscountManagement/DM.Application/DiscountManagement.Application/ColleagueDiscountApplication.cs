﻿using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        #region Constructor

        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        #endregion

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();

            if (_colleagueDiscountRepository.IsExist(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var colleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Add(colleagueDiscount);
            _colleagueDiscountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();

            var colleagueDiscount = _colleagueDiscountRepository.Get(command.Id);
            if (colleagueDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_colleagueDiscountRepository.IsExist(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            colleagueDiscount.Edit(command.ProductId, command.DiscountRate);

            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var colleagueDiscount = _colleagueDiscountRepository.Get(id);
            if (colleagueDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            colleagueDiscount.Remove();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var colleagueDiscount = _colleagueDiscountRepository.Get(id);
            if (colleagueDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            colleagueDiscount.Restore();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}