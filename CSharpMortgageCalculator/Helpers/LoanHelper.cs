using CSharpMortgageCalculator.Models;
using System;

namespace CSharpMortgageCalculator.Helpers
{
    public class LoanHelper
    {
        public Loan GetPayments(Loan loan)
        {
            //Calculate Monthly Payment
            loan.Payment = CalcPayment(loan.Amount, loan.Rate, loan.Term);

            //Create a Loop from 1-Term
            var balance = loan.Amount;
            var totalInterest = 0.0m;
            var monthlyInterest = 0.0m;
            var monthlyPrincipal = 0.0m;
            var monthlyRate = CalcMonthlyRate(loan.Rate);

            //Loop Over Each Month Till Reach Term of Loan
            for (int month = 1; month <= loan.Term; month++)
            {
                monthlyInterest = CalcMonthlyInterest(balance, monthlyRate);
                totalInterest += monthlyInterest;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                LoanPayment loanPayment = new();

                loanPayment.Month = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyPrincipal = monthlyPrincipal;
                loanPayment.MonthlyInterest = monthlyInterest;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance;

                //Push Object Into Loan Model
                loan.Payments.Add(loanPayment);


            }

            loan.TotalInterest = totalInterest;
            loan.TotalInterest = loan.Amount + totalInterest;

            return loan;
            //Create Payment Schedule

        decimal CalcPayment(decimal amount, decimal rate, int term)
        {
        var monthlyRate = CalcMonthlyRate(rate);

        var rateD = Convert.ToDouble(monthlyRate);

        var amountD = Convert.ToDouble(amount);

        var paymentD = (amountD * rateD) / (1 - Math.Pow(1 + rateD, -term));

        return Convert.ToDecimal(paymentD);
        }

        decimal CalcMonthlyRate(decimal rate)
        {
        return rate / 1200;
        }

        decimal CalcMonthlyInterest(decimal balance, decimal monthlyRate)
        {
            return balance * monthlyRate;
        }

        }
    }
}
