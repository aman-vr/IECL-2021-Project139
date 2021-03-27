using Microsoft.AspNetCore.Mvc.Rendering;
using Paycompute.Entity;
using Paycompute.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paycompute.Services.Implementation
{
    public class PayComputationService : IPayComputationService
    {
        private readonly ApplicationDbContext _context;
        private decimal contractualEarnings;
        private decimal overtimeHours;
        public PayComputationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            return contractualEarnings = (hoursWorked >= contractualHours) ? (hourlyRate * contractualHours) : (hourlyRate * hoursWorked);
            //if(hoursWorked >= contractualHours)
            //  contractualEarnings = hourlyRate * contractualHours;
            //else
            //  contractualEarnings = hourlyRate * hoursWorked;
            //return contractualEarnings;
        }

        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
            await _context.PaymentRecords.AddAsync(paymentRecord);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PaymentRecord> GetAll() 
            => _context.PaymentRecords.OrderBy(p => p.EmployeeId);

        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var allTaxYear = _context.TaxYears.Select(taxYears => new SelectListItem 
            { 
                Text = taxYears.YearOfTax,
                Value = taxYears.Id.ToString()
            });
            return allTaxYear;
        }

        public PaymentRecord GetById(int id) 
            => _context.PaymentRecords.Where(pay => pay.Id == id).FirstOrDefault();

        public TaxYear GetTaxYearById(int id)
            => _context.TaxYears.Where(year=> year.Id == id).FirstOrDefault();
        public decimal NetPay(decimal totalEarnings, decimal totalDeduction) 
            => totalEarnings - totalDeduction;

        public decimal OvertimeEarnings(decimal overtimeRate, decimal overTimeHours) 
            => overTimeHours * overtimeRate;
        public decimal OvertimeHours(decimal hoursWorked, decimal contractualHours) 
            => overtimeHours = (hoursWorked > contractualHours) ? (hoursWorked - contractualHours) : (0.00m);

        public decimal OvertimeRate(decimal hourlyRate) 
            => hourlyRate * 1.5m;

        public decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFees)
            => tax + nic + studentLoanRepayment + unionFees;

        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings)
            => overtimeEarnings + contractualEarnings;
    }
}