﻿using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        #region Constructor

        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        #endregion

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();

            if (_customerDiscountRepository.IsExist(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var customerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate, startDate, endDate,
                command.Reason);

            _customerDiscountRepository.Add(customerDiscount);
            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();

            var customerDiscount = _customerDiscountRepository.Get(command.Id);
            if (customerDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_customerDiscountRepository.IsExist(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            customerDiscount.Edit(command.ProductId, command.DiscountRate, startDate, endDate,
                command.Reason);

            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}