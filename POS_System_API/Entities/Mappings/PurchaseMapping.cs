using POS_System_API.Entities.DTOs;
using POS_System_API.Entities.Models;
using POS_System_API.Entities.Mappings;

namespace POS_System_API.Entities.Mappings
{
    public static class PurchaseMapping
    {
        public static PurchaseDTO ConvertToPurchaseDTO(this Purchase purchase)
        {
            return new PurchaseDTO
            {
                Id = purchase.Id,
                InvoiceNumber = purchase.InvoiceNumber,
                GrossTotal = purchase.GrossTotal,
                Discount = purchase.Discount,
                SalesTax = purchase.SalesTax,
                OtherCharges = purchase.OtherCharges,
                NetTotal = purchase.NetTotal,
                IsReturn = purchase.IsReturn,
                PurchaseProducts = purchase.PurchaseProducts != null ? purchase.PurchaseProducts.ConvertToPurchaseProductDTO() : null,
            };
        }

        public static List<PurchaseDTO> ConvertToPurchaseDTO(this List<Purchase> purchases)
        {
            return purchases.Select(purchase => purchase.ConvertToPurchaseDTO()).ToList();
        }
    }
}
