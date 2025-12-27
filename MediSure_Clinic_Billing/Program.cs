using System;

namespace MediSure_Clinic_Billing
{
    
    // ====== ENTITY CLASS : PatientBill ======
    
    class PatientBill
    {
        // Properties using get and set
        public string BillId { get; set; }
        public string PatientName { get; set; }
        public bool HasInsurance { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal LabCharges { get; set; }
        public decimal MedicineCharges { get; set; }

        // Calculated properties
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPayable { get; set; }

        // Method to calculate billing amounts
        public void CalculateBill()
        {
            // Calculate gross amount
            GrossAmount = ConsultationFee + LabCharges + MedicineCharges;

            // Apply insurance discount
            if (HasInsurance)
            {
                DiscountAmount = GrossAmount * 0.10m;
            }
            else
            {
                DiscountAmount = 0;
            }

            // Final payable amount
            FinalPayable = GrossAmount - DiscountAmount;
        }

        // Method to print bill details
        public void PrintBill()
        {
            Console.WriteLine("----------- Last Bill -----------");
            Console.WriteLine("Bill Id           : " + BillId);
            Console.WriteLine("Patient Name      : " + PatientName);
            Console.WriteLine("Insured           : " + (HasInsurance ? "Yes" : "No"));
            Console.WriteLine("Consultation Fee  : " + ConsultationFee.ToString("0.00"));
            Console.WriteLine("Lab Charges       : " + LabCharges.ToString("0.00"));
            Console.WriteLine("Medicine Charges  : " + MedicineCharges.ToString("0.00"));
            Console.WriteLine("Gross Amount      : " + GrossAmount.ToString("0.00"));
            Console.WriteLine("Discount Amount   : " + DiscountAmount.ToString("0.00"));
            Console.WriteLine("Final Payable     : " + FinalPayable.ToString("0.00"));
            Console.WriteLine("--------------------------------");
        }
    }

    
    // ====== MAIN PROGRAM ======

    class Program
    {
        // Static storage (ONLY ONE bill allowed)
        static PatientBill LastBill;
        static bool HasLastBill = false;

        static void Main()
        {
            int choice;

            do
            {
                Console.WriteLine("\n================== MediSure Clinic Billing ==================");
                Console.WriteLine("1. Create New Bill");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
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
                        CreateBill();
                        break;

                    case 2:
                        ViewBill();
                        break;

                    case 3:
                        ClearBill();
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

        
        // ====== CREATE BILL METHOD ======
        
        static void CreateBill()
        {
            PatientBill bill = new PatientBill();

            Console.Write("Enter Bill Id: ");
            bill.BillId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(bill.BillId))
            {
                Console.WriteLine("Bill Id cannot be empty.");
                return;
            }

            Console.Write("Enter Patient Name: ");
            bill.PatientName = Console.ReadLine();

            Console.Write("Is the patient insured? (Y/N): ");
            string insuranceInput = Console.ReadLine();

            bill.HasInsurance = insuranceInput.Equals("Y", StringComparison.OrdinalIgnoreCase);

            Console.Write("Enter Consultation Fee: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal consultFee) || consultFee <= 0)
            {
                Console.WriteLine("Consultation fee must be greater than zero.");
                return;
            }
            bill.ConsultationFee = consultFee;

            Console.Write("Enter Lab Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal labCharges) || labCharges < 0)
            {
                Console.WriteLine("Lab charges cannot be negative.");
                return;
            }
            bill.LabCharges = labCharges;

            Console.Write("Enter Medicine Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal medCharges) || medCharges < 0)
            {
                Console.WriteLine("Medicine charges cannot be negative.");
                return;
            }
            bill.MedicineCharges = medCharges;

            // Calculate bill
            bill.CalculateBill();

            // Store last bill
            LastBill = bill;
            HasLastBill = true;

            Console.WriteLine("\nBill created successfully.");
            Console.WriteLine("Gross Amount   : " + bill.GrossAmount.ToString("0.00"));
            Console.WriteLine("Discount Amount: " + bill.DiscountAmount.ToString("0.00"));
            Console.WriteLine("Final Payable  : " + bill.FinalPayable.ToString("0.00"));
        }

        
        // ====== VIEW BILL METHOD ======
        
        static void ViewBill()
        {
            if (!HasLastBill)
            {
                Console.WriteLine("No bill available. Please create a new bill first.");
                return;
            }

            LastBill.PrintBill();
        }

        
        // ====== CLEAR BILL METHOD ======
        
        static void ClearBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }
    }
}
