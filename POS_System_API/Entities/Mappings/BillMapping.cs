using POS_System_API.Entities.DTOs;
using POS_System_API.Entities.Models;

namespace POS_System_API.Entities.Mappings
{
    public static class BillMapping
    {
        public static BillDTO ConvertToBillDTO(this Bill bill)
        {
            return new BillDTO
            {
                Id = bill.Id,
                InvoiceNumber = bill.InvoiceNumber,
                CustomerName = bill.CustomerName,
                GrossTotal = bill.GrossTotal,
                Discount = bill.Discount,
                SalesTax = bill.SalesTax,
                NetTotal = bill.NetTotal,
                IsReturn = bill.IsReturn,
                BillProducts = bill.BillProducts != null ? bill.BillProducts.ConvertToBillProductDTO() : null,
            };
        }

        public static List<BillDTO> ConvertToBillDTO(this List<Bill> bills)
        {
            return bills.Select(bill => bill.ConvertToBillDTO()).ToList();
        }
    }
}
