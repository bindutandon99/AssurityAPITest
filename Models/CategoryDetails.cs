
namespace AssurityAPITests.Models
{


    /// <summary>
    /// Represents the details of a category, such as name, ID, and various other settings.
    /// </summary>
    public class CategoryDetails
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }
        public bool CanListAuctions { get; set; }
        public bool CanListClassifieds { get; set; }
        public bool CanRelist { get; set; }
        public string? LegalNotice { get; set; }
        public int DefaultDuration { get; set; }
        public List<int>? AllowedDurations { get; set; }
        public Fees? Fees { get; set; }
        public int FreePhotoCount { get; set; }
        public int MaximumPhotoCount { get; set; }
        public bool IsFreeToRelist { get; set; }
        public List<Promotion>? Promotions { get; set; }
        public List<object>? EmbeddedContentOptions { get; set; }
        public int MaximumTitleLength { get; set; }
        public int AreaOfBusiness { get; set; }
        public int DefaultRelistDuration { get; set; }
        public bool CanUseCounterOffers { get; set; }
    }


    /// <summary>
    /// Represents the details of a fees, which is associated with category
    /// </summary>
    public class Fees
    {

        public float Bundle { get; set; }
        public float EndDate { get; set; }
        public float Feature { get; set; }
        public float Gallery { get; set; }
        public float Listing { get; set; }
        public float Reserve { get; set; }
        public float Subtitle { get; set; }
        public float TenDays { get; set; }
        public List<ListingFeeTier>? ListingFeeTiers { get; set; }

        public float SecondCategory { get; set; }

    }


    /// <summary>
    /// Represents the details of a listingfeetier, which is a list associated with fees
    /// </summary>           
    public class ListingFeeTier
      {
           public float MinimumTierPrice { get; set; }
           public float FixedFee { get; set; }
      }


    /// <summary>
    /// Represents the details of promotion, which is a list associated with category
    /// </summary>
    public class Promotion
     {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Price {  get; set; }
        public float OriginalPrice {  get; set; }
        public int MinimumPhotoCount { get; set; }
       
     }
  
}

