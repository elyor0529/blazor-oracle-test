using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorDemo.Shared
{
    public class ListFilter
    {
        public int from { get; set; }

        public int to { get; set; }

        public string keyword { get; set; }

    }

    public class LotFilter : ListFilter
    {

        public int? regionId { get; set; }

        public int? companyId { get; set; }

        public decimal? startPrice { get; set; }

        public decimal? endPrice { get; set; }

        public bool? isAllowedJuridic { get; set; }

        public bool? isAllowedIndividual { get; set; }

        public string fullNumbers { get; set; }

        public bool? isToday { get; set; }

        public bool? isMyOffer { get; set; }

        public string exceptIdsArr { get; set; }

    }
     
    public class MainLotItem 
    {
        public int Id { get; set; }

        public string Display_Id { get; set; }

        public int Region_Id { get; set; }
        public string Region_Name { get; set; }

        public string Number_Prefix { get; set; }
        public string Number_Code { get; set; }
        public string Number_Body { get; set; }

        public decimal Start_Price { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public decimal? Current_Price { get; set; }

        public string Seller_Company_Logo { get; set; }
        public string Seller_Company_Name { get; set; }
        public int Seller_Company_Id { get; set; }

        public bool Is_Today { get; set; }
        public long Left_Time { get; set; }

        public long Company_Lots_Count { get; set; }
        public long Total_Lots_Count { get; set; }

        public long Position { get; set; }
        public long Total => Company_Lots_Count;
        public long Records => Total_Lots_Count;

    }

    public class MainLotListing
    {

        public IEnumerable<MainLotItem> Items { get; set; }

        public int? EndingTodayLotsCount { get; set; }

        public int? ActiveLotsCount { get; set; }

        public int? MyOffersCount { get; set; }

        public decimal? PriceRangeMin { get; set; }

        public decimal? PriceRangeMax { get; set; }

    }
}
