using System;

namespace QuickMart_Profit_Calculato
{
    
    // =======ENTITY CLASS : SaleTransaction========


    // Represents ONE sale transaction
    class SaleTransaction
    {
        // Properties using get and set
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal SellingAmount { get; set; }

        // Calculated values
        public string? ProfitOrLossStatus { get; set; }
        public decimal ProfitOrLossAmount { get; set; }
        public decimal ProfitMarginPercent { get; set; }

        // Method to calculate profit or loss
        public void CalculateProfitLoss()
        {
            if (SellingAmount > PurchaseAmount)
            {
                ProfitOrLossStatus = "PROFIT";
                ProfitOrLossAmount = SellingAmount - PurchaseAmount;
            }
            else if (SellingAmount < PurchaseAmount)
            {
                ProfitOrLossStatus = "LOSS";
                ProfitOrLossAmount = PurchaseAmount - SellingAmount;
            }
            else
            {
                ProfitOrLossStatus = "BREAK-EVEN";
                ProfitOrLossAmount = 0;
            }

            // Profit margin percentage calculation
            ProfitMarginPercent = (ProfitOrLossAmount / PurchaseAmount) * 100;
        }

        // Method to display invoice-style output
        public void PrintInvoice()
        {
            Console.WriteLine("-------------- Last Transaction --------------");
            Console.WriteLine("Invoice No      : " + InvoiceNo);
            Console.WriteLine("Customer Name   : " + CustomerName);
            Console.WriteLine("Item Name       : " + ItemName);
            Console.WriteLine("Quantity        : " + Quantity);
            Console.WriteLine("Purchase Amount : " + PurchaseAmount.ToString("0.00"));
            Console.WriteLine("Selling Amount  : " + SellingAmount.ToString("0.00"));
            Console.WriteLine("Status          : " + ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amt : " + ProfitOrLossAmount.ToString("0.00"));
            Console.WriteLine("Profit Margin % : " + ProfitMarginPercent.ToString("0.00"));
            Console.WriteLine("----------------------------------------------");
        }
    }

    // ========MAIN PROGRAM CLASS========
    class Program
    {
        // Static storage (ONLY ONE transaction allowed)
        static SaleTransaction LastTransaction;
        static bool HasLastTransaction = false;

        static void Main()
        {
            int choice;

            do
            {
                Console.WriteLine("\n================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                // Validate menu input
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        CreateTransaction();
                        break;

                    case 2:
                        ViewTransaction();
                        break;

                    case 3:
                        RecalculateTransaction();
                        break;

                    case 4:
                        Console.WriteLine("Thank you. Application closed normally.");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            } while (choice != 4);
        }

        
        //======= CREATE TRANSACTION METHOD =======
        static void CreateTransaction()
        {
            SaleTransaction t = new SaleTransaction();

            Console.Write("Enter Invoice No: ");
            t.InvoiceNo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(t.InvoiceNo))
            {
                Console.WriteLine("Invoice No cannot be empty.");
                return;
            }

            Console.Write("Enter Customer Name: ");
            t.CustomerName = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            t.ItemName = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Quantity must be greater than zero.");
                return;
            }
            t.Quantity = qty;

            Console.Write("Enter Purchase Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal purchase) || purchase <= 0)
            {
                Console.WriteLine("Purchase amount must be greater than zero.");
                return;
            }
            t.PurchaseAmount = purchase;

            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal selling) || selling < 0)
            {
                Console.WriteLine("Selling amount cannot be negative.");
                return;
            }
            t.SellingAmount = selling;

            // Calculate profit/loss
            t.CalculateProfitLoss();

            // Save transaction
            LastTransaction = t;
            HasLastTransaction = true;

            Console.WriteLine("\nTransaction saved successfully.");
            Console.WriteLine("Status: " + t.ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amount: " + t.ProfitOrLossAmount.ToString("0.00"));
            Console.WriteLine("Profit Margin (%): " + t.ProfitMarginPercent.ToString("0.00"));
        }

        
        // ====== VIEW TRANSACTION METHOD ======
    
        static void ViewTransaction()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create one first.");
                return;
            }

            LastTransaction.PrintInvoice();
        }

        
        // ====== RECALCULATE METHOD ======
        
        static void RecalculateTransaction()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create one first.");
                return;
            }

            LastTransaction.CalculateProfitLoss();
            Console.WriteLine("Profit/Loss recalculated successfully.");
            LastTransaction.PrintInvoice();
        }
    }
}
