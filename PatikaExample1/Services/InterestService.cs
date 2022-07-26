﻿using Microsoft.Extensions.Options;
using PatikaExample1.IServices;
using PatikaExample1.Models;
//using InterestRate = 14.00 ;
namespace PatikaExample1.Services
{
    
    public class InterestService : IInterestService
    {
        

        private readonly InterestConfig _interestConfig;

        public InterestService(IOptions<InterestConfig> interestConfig)
        {
            _interestConfig = interestConfig.Value;
        }

        /// <summary>
        /// This method calculates the interest. If interest rate is null, current TCMB interest rate will be use for calculation
        /// I have been use A = P+(P*r*t) formul for calculating interest.
        /// </summary>
        /// <param name="interestRate"></param>
        /// <param name="totalAmount"></param>
        /// <param name="period">Year</param>
        /// <returns></returns>
        public object CalculateInterest(double? interestRate , int? totalAmount , int? period)
        {
            var configValue = _interestConfig.Rate;
            if(interestRate == null)
                interestRate = configValue;
            if (totalAmount == null || totalAmount <= 0)
                return new ArgumentNullException("Total Amount can not be null or negative");
            if (period == null || period <= 0)
                return new ArgumentNullException("Period can not be null or negative");

            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddYears(period ?? 0);

            var monthCount = ((startDate.Year - endDate.Year) * 12) + startDate.Month - endDate.Month;
            var totalPayment = totalAmount + (totalAmount * interestRate * period);

            return new InterestModel
            {
                InterestRate = interestRate ?? 0,
                TotalAmount = totalAmount ?? 0,
                MonthlyPayment = totalPayment / monthCount ?? 0 ,
                TotalInterest = totalPayment - totalAmount ?? 0,
                TotalPayment = totalPayment ?? 0

            };



            return null;

        }

     
    }
}
