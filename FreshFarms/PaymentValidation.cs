﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FreshFarms
{
    class PaymentValidation
    {
        //takes in the total payment
        //displays total payment, amount paid, change
        //no return
        public static double CashPayment(double totalCost)
        {
            double received, change;

            Console.WriteLine("Cash payment option selected.");
            Console.Write("Please enter cash amount received: ");

            do
            {
                received = Math.Round(CheckPositiveDouble(Validator.ValidateDouble()), 2);
                if (received < totalCost)
                {
                    Console.Write("Value lower than total cost. Please ask for a larger payment: ");
                }
            } while (received < totalCost);
           

            change = Math.Round(received - totalCost, 2);

            Console.WriteLine($"Amount owed: {totalCost}");
            Console.WriteLine($"Amount paid: {received}");
            Console.WriteLine($"Change: {change}");

            return received;
        }

        //takes in total cost
        //validates check info through regex
        //displays total cost, amount paid, change
        //no return
        public static double CheckPayment(double totalCost)
        {
            double received, change;

            Console.WriteLine("Check payment option selected.");

            Console.Write("Please enter cash amount written: ");

            do
            {
                received = Math.Round(CheckPositiveDouble(Validator.ValidateDouble()), 2);
                if (received < totalCost)
                {
                    Console.Write("Value lower than total cost. Please ask for a larger payment: ");
                }
            } while (received < totalCost);

            //9 digits, first digit only from 0-3
            Console.Write("Please enter the routing number (9 digits): ");
            RegexValidate(@"(^[0-3]{1}[0-9]{8}$)");

            //10-12 digits
            Console.Write("Please enter the account number (10-12 digits): ");
            RegexValidate(@"(^[0-9]{10,12}$)");

            //4 digits
            Console.Write("Please enter the check number (4 digits): ");
            RegexValidate(@"(^[0-9]{4}$)");

            change = Math.Round(received - totalCost, 2);

            Console.WriteLine($"Amount owed: {totalCost}");
            Console.WriteLine($"Amount paid: {received}");
            Console.WriteLine($"Change: {change}");

            return received;
        }

        //takes in total cost
        //validates card number using regex (3 types)
        //asks for cash back
        //displays total cost, cash back, amount paid
        //no return
        public static double CardPayment(double totalCost)
        {
            string cashBackSelect;
            double received, cashBack = 0.00;

            Console.WriteLine("Card payment option selected.");
            Console.WriteLine("Please be aware that only Mastercard, Discover, and American Express are accepted at this time.");

            //Mastercard: first digit 5, 16 digits long
            //American Express: first digit 3, second digit 4 or 7, 15 digits long
            //Discover: first digit 6, 16 digits long
            Console.Write("Please enter the card number: ");
            RegexValidate(@"(^[5]{1}[0-9]{15}$|^[3]{1}[4,7]{1}[0-9]{13}$|^[6]{1}[0-9]{15}$)");

            Console.Write("Cash back requested? (y/n): ");
            cashBackSelect = Console.ReadLine().ToLower();

            while (cashBackSelect != "y" && cashBackSelect != "n")
            {
                Console.Write("Invalid input. Please select 'y' or 'n': ");
                cashBackSelect = Console.ReadLine().ToLower();
            }

            if (cashBackSelect == "y")
            {
                Console.Write("Please write cash back amount: ");
                cashBack = CheckPositiveDouble(Validator.ValidateDouble());
            }

            received = totalCost + cashBack;

            Console.WriteLine($"Amount owed: {totalCost}");
            Console.WriteLine($"Cash back: {cashBack}");
            Console.WriteLine($"Amount paid: {received}");

            return received;
        }

        //takes in a string for a regex expression
        //asks for input
        //returns input string if successfully matched
        public static string RegexValidate(string regex)
        {
            string input = Console.ReadLine();

            while (!Regex.Match(input, regex).Success)
            {
                Console.Write("Please enter a valid number: ");
                input = Console.ReadLine();
            }

            return input;
        }

        public static double CheckPositiveDouble(double input)
        {
            while (input < 0)
            {
                Console.Write("Please only use positve values: ");
                input = Validator.ValidateDouble();
            }

            return input;
        }

        public static double PaymentOptions(double grandTotal)
        {
            string selection;
            double amountPaid = 0.00;
            bool repeat;

            Console.WriteLine("Available payment Options:");
            Console.WriteLine();
            Console.WriteLine("1. Cash");
            Console.WriteLine("2. Check");
            Console.WriteLine("3. Card");
            Console.WriteLine();
            Console.Write("Please select a payment option: ");

            do
            {
                selection = Console.ReadLine();

                if (selection == "1")
                {
                    //DEFAULT VALUE HARDCODED. REPLACE WITH PROPER PARAMETER WHEN AVAILABLE
                    amountPaid = CashPayment(grandTotal);
                    repeat = false;
                }
                else if (selection == "2")
                {
                    //DEFAULT VALUE HARDCODED. REPLACE WITH PROPER PARAMETER WHEN AVAILABLE
                    amountPaid = CheckPayment(grandTotal);
                    repeat = false;
                }
                else if (selection == "3")
                {
                    //DEFAULT VALUE HARDCODED. REPLACE WITH PROPER PARAMETER WHEN AVAILABLE
                    amountPaid = CardPayment(grandTotal);
                    repeat = false;
                }
                else
                {
                    Console.Write("Invalid option. Please select the number of an option listed: ");
                    repeat = true;
                }
            } while (repeat);

            return amountPaid;
        }

        //Subtracts input total cost from input amount received and returns change
        public static double GetChange (double totalCost, double amountReceived)
        {
            double change = Math.Round(amountReceived - totalCost, 2);
            return change;
        }
    }
}
