using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApplication_console
{
    class Loan
    {
        private double loanAmount;
        private double rate;
        private int numPayments;
        private double balance;
        private double totalInterestPaid;
        private double paymentAmount;
        private double principal;
        private double monthInterest;

        //Default constructor 
        public Loan()

        { }

        // Constructor  

        public Loan(double loan, double interestRate, int years)
        {
            loanAmount = loan;
            if (interestRate < 1)
                rate = interestRate;
            else   // In case directions aren't folloed 
                rate = interestRate / 100;
            numPayments = 12 * years;
            totalInterestPaid = 0;
            DeterminePaymentAmount();

        }


        // Property accessing payment amount 
        public double PaymentAmount
        {
            get
            {
                return paymentAmount;
            }
        }
        // property setting and returning loan amount' 
        public double LoanAmount
        {
            set
            {
                loanAmount = value;
            }
            get
            {
                return loanAmount;
            }
        }
        // property setting and returning rate 
        public double Rate
        {
            set
            {
                rate = value;
            }
            get
            {
                return rate;
            }
        }

        /** 
          Property to set the numPayments, given years to finance. 
         * Returns the number of years using number of payments 
         */
        public int Years
        {

            set
            {
                numPayments = value * 12;
            }
            get
            {
                return numPayments / 12;
            }
        }
        // Property for accessing total interest to be paid 
        public double TotalInterestPaid
        {
            get
            {
                return totalInterestPaid;
            }
        }

        /* 
         * Determine payment amount based on number of years, 
         * loan amount, and rate* 
         * **/

        public void DeterminePaymentAmount()
        {
            double term;

            term = Math.Pow((1 + rate / 12.0), numPayments);
            paymentAmount = (loanAmount * rate / 12.0 * term) / (term - 1.0);
        }

        // Returns a string containing an amortization table. 

        public string ReturnAmortizationSchedule()
        {
            string aSchedule = "Month\t\tInt.\t\tPrin.\t\tNew";
            aSchedule += "\nNo.\t\tPd.\t\tPd.\t\tBalance\n";
            aSchedule += "______\t\t______\t\t_______\t_________\n";
            balance = loanAmount;
            for (int month = 1; month <= numPayments; month++)
            {
                CalculateMonthCharges(month, numPayments);
                aSchedule += month
                            + "\t\t"
                            + monthInterest.ToString("N2")
                            + "\t\t"
                            + principal.ToString("N2")
                            + "\t\t"
                            + balance.ToString("C")
                            + "\n";
            }
            return aSchedule;
        }

        //Calculates monthly interest and new balance 
        public void CalculateMonthCharges(int month, int numPayments)
        {

            double payment = paymentAmount;
            monthInterest = rate / 12 * balance;
            if (month == numPayments)
            {
                principal = balance;
                payment = balance + monthInterest;
            }
            else
            {
                principal = payment - monthInterest;
            }
            balance -= principal;

        }


        // Calculates interest paid over the life of the loan. 
        public void DetermineTotalInterestPaid()
        {
            totalInterestPaid = 0;
            balance = loanAmount;
            for (int month = 1; month <= numPayments; month++)
            {
                CalculateMonthCharges(month, numPayments);
                totalInterestPaid += monthInterest;
            }
        }

        // return information about the loan 
        public override string ToString()
        {
            return "\nLoan Amount: " +
                     loanAmount.ToString("C") +
                     "\nInterest Rate: " + rate +
                     "\nNumber of Years for Loan: " +
                     (numPayments / 12) +
                     "\nMonthly payment: " +
                     paymentAmount.ToString("C");
        }

    }
}
