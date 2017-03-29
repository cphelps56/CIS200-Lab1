// Name: Colin Phelps
// Lab: 1
// Due Date: 5/18/15
// CIS 200-10
/* This program initializes an array of invoices, fills the invoices with data,
then uses LINQ to order the data and select certain ranges of data.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    public class LinqTest
    {
        public static void Main(string[] args)
        {
            // initialize array of invoices
            Invoice[] invoices = { 
                new Invoice( 83, "Electric sander", 7, 57.98M ), 
                new Invoice( 24, "Power saw", 18, 99.99M ), 
                new Invoice( 7, "Sledge hammer", 11, 21.5M ), 
                new Invoice( 77, "Hammer", 76, 11.99M ), 
                new Invoice( 39, "Lawn mower", 3, 79.5M ), 
                new Invoice( 68, "Screwdriver", 106, 6.99M ), 
                new Invoice( 56, "Jig saw", 21, 11M ), 
                new Invoice( 3, "Wrench", 34, 7.5M ) };

            foreach (var invoice in invoices) ;

            // sort invoices by part description
            var partDescriptionSorted =
                from i in invoices
                orderby i.PartDescription
                select i;

            // header
            Console.WriteLine("Invoices sorted by part description:");
            
            // Display invoices sorted by part description
            foreach (var invoice in partDescriptionSorted)
                Console.WriteLine(invoice);

            // sort invoices by price
            var priceSorted =
                from i in invoices
                orderby i.Price
                select i;

            // header
            Console.WriteLine("\nInvoices sorted by price:");

            // display invoices sorted by price
            foreach (var invoice in priceSorted)
                Console.WriteLine(invoice);

            // use LINQ to sort invoices by quantity and select part description and quantity
            var quantitySorted =
                from i in invoices
                orderby i.Quantity
                select new { i.PartDescription, Quantity = i.Quantity };

            // header
            Console.WriteLine("\nInvoices sorted by quantity:");
            Console.WriteLine(string.Format("{0,-20}{1,10}", "Part Description", "Quantity"));

            // display quantity and part description of invoices sorted by quantity
            foreach (var invoice in quantitySorted)
                Console.WriteLine(string.Format("{0,-20}{1,10}", invoice.PartDescription, invoice.Quantity));

            // use linq to calculate invoice totals, order by total, and select the part description and invoice total
            var invoiceTotalSorted =
                from i in invoices
                let total = (i.Quantity * i.Price)
                orderby total
                select new { i.PartDescription, invoiceTotal = total };

            // header
            Console.WriteLine("\nInvoices sorted by invoice total:");
            Console.WriteLine(string.Format("{0,-20}{1,15}", "Part Description", "Invoice Total"));

            // display part description and invoice total of invoices
            foreach (var invoice in invoiceTotalSorted)
                Console.WriteLine(string.Format("{0,-20}{1,15:C}", invoice.PartDescription,invoice.invoiceTotal));

            const int MIN_INVOICE_TOTAL = 200; // constant to hold the minimum of the invoice total range
            const int MAX_INVOICE_TOTAL = 500; // constant to hold the maximum of the invoice total range

            // use LINQ to filter a range of invoice totals and select the invoice totals
            var totalBtw200and500 =
                from i in invoiceTotalSorted
                where i.invoiceTotal >= MIN_INVOICE_TOTAL && i.invoiceTotal <= MAX_INVOICE_TOTAL
                select i.invoiceTotal;

            // header
            Console.WriteLine(string.Format("\nList of invoice totals between {0:C} and {1:C}:",MIN_INVOICE_TOTAL,MAX_INVOICE_TOTAL));

            // display the invoice total for invoices with totals between 200 and 500
            foreach (var invoice in totalBtw200and500)
                Console.WriteLine(string.Format("{0:C}",invoice));

            Console.ReadLine();
        }
    }
}
