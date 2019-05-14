using BlazorDemo.Shared;
using Dapper;
using Dapper.Oracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemo.Server.Services
{
    public class LotService
    {
        private readonly OracleConnection _connection;

        public LotService(OracleConnection connection)
        {
            _connection = connection;
        }


        public IEnumerable<MainLotItem> CardItems(LotFilter filter, int? userId = null)
        { 
            var parameters = new OracleDynamicParameters();

            parameters.Add("o_Result", null, OracleMappingType.RefCursor, ParameterDirection.Output);
            parameters.Add("p_From", filter.from);
            parameters.Add("p_To", filter.to);
            parameters.Add("p_Region_Id", filter.regionId);
            parameters.Add("p_Company_Id", filter.companyId);
            parameters.Add("p_is_allowed_juridical", filter.isAllowedJuridic.HasValue && filter.isAllowedJuridic.Value ? 1 : 0);
            parameters.Add("p_is_allowed_individual", filter.isAllowedIndividual.HasValue && filter.isAllowedIndividual.Value ? 1 : 0);
            parameters.Add("p_price_from", filter.startPrice);
            parameters.Add("p_price_to", filter.endPrice);
            parameters.Add("p_Full_Numbers", filter.fullNumbers, OracleMappingType.Varchar2, size: 512);
            parameters.Add("p_is_today", filter.isToday.HasValue && filter.isToday.Value ? (int?)1 : null);
            parameters.Add("p_User_Id", null);
            parameters.Add("p_Except_Ids_Arr", filter.exceptIdsArr);

            using (var con = _connection)
            {
                return con.Query<MainLotItem>("Front.Load_Lot_Cards", parameters, commandType: CommandType.StoredProcedure) ;
            }
        }

        public MainLotListing Filter(LotFilter filter)
        {
            var model = new MainLotListing();
            var parameters = new OracleDynamicParameters();

            parameters.Add("o_result", null, OracleMappingType.RefCursor, ParameterDirection.Output);
            parameters.Add("o_Ending_Today_Lots_Count", null, OracleMappingType.Decimal, ParameterDirection.Output);
            parameters.Add("o_Active_Lots_Count", null, OracleMappingType.Decimal, ParameterDirection.Output);
            parameters.Add("o_My_Offers_Count", null, OracleMappingType.Decimal, ParameterDirection.Output);
            parameters.Add("o_Price_Range_Min", null, OracleMappingType.Decimal, ParameterDirection.Output);
            parameters.Add("o_Price_Range_Max", null, OracleMappingType.Decimal, ParameterDirection.Output);
            parameters.Add("p_Card_Size", filter.to - filter.from);
            parameters.Add("p_Region_Id", filter.regionId);
            parameters.Add("p_Company_Id", filter.companyId);
            parameters.Add("p_is_allowed_juridical", filter.isAllowedJuridic.HasValue && filter.isAllowedJuridic.Value ? 1 : 0);
            parameters.Add("p_is_allowed_individual", filter.isAllowedIndividual.HasValue && filter.isAllowedIndividual.Value ? 1 : 0);
            parameters.Add("p_price_from", filter.startPrice);
            parameters.Add("p_price_to", filter.endPrice);
            parameters.Add("p_Full_Numbers", filter.fullNumbers, OracleMappingType.Varchar2, size: 512);
            parameters.Add("p_is_today", filter.isToday.HasValue && filter.isToday.Value ? (int?)1 : null);
            parameters.Add("p_User_Id", null);

            using (var con = _connection)
            {
                model.Items = con.Query<MainLotItem>("Front.Get_Filtered_Lots", parameters, commandType: CommandType.StoredProcedure) ;

                model.EndingTodayLotsCount = (int?)parameters.Get<decimal?>("o_Ending_Today_Lots_Count");
                model.ActiveLotsCount = (int?)parameters.Get<decimal?>("o_Active_Lots_Count");
                model.MyOffersCount = (int?)parameters.Get<decimal?>("o_My_Offers_Count");
                model.PriceRangeMin = (int?)parameters.Get<decimal?>("o_Price_Range_Min");
                model.PriceRangeMax = (int?)parameters.Get<decimal?>("o_Price_Range_Max");
            }

            return model;
        }

    }
}
