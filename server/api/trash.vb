bRefreshData = false;

dim clientSession As aaFactMES.aaClientSession;
dim Result As aaFactMES.Result;
dim sql as string;
dim DS as System.Data.DataSet;
dim row as System.Data.DataRow;
dim attr as integer;
dim attr_str as string;
dim val as double;
dim val_str as string;
dim currentDate as System.DateTime;
dim ent_block as string;
dim ent_sorption as string;
dim ent_pump_station_ps as string;
dim ent_pump_station_ls as string;


dim cm_ent_denitr as string;
dim cm_wo_block as string; ' WO для ЦМК - блоки
dim cm_PumpStation_code as string; 'Код насосной станции ЦМК

dim def_user as string; 'Пользователь от которого происходит автоматический учет
dim cm_ent_stor_acid as string; 'Хранилище кислоты на ЦМК
dim cm_ent_stor_acid2 as string; 'Хранилище кислоты на ЦМК
dim cm_ent_stor_acid3 as string; 'Хранилище кислоты на ЦМК

dim cm_wo_desorption as string;
dim cm_ent_desorption as string;
dim cm_ent_desorb_sol as string;
dim cm_wo_desorb_sol as string;

dim cm_wo_sorption as string; ' WO для  Центральный Мынкудука - добыча
dim cm_stor_re as string;
dim cm_stor_an as string; 'Хранилище аммиачной селитры на ЦМК


'### ГТП
ent_block = "S007M0#BLK01"; '  Центральный Мынкудук ГТП
ent_pump_station_ps = "S007M0#PST01#900000"; ' Центральный Мынкудук пескоотстойник ПР
'### УППР
ent_sorption = "S007P1#CSR00"; ' Сорбция
ent_pump_station_ls = "S007M0#PST01#800000"; ' Центральный Мынкудук пескоотстойник ВР

' select dbo.fn_Z_GetConsumptionByPeriod('S001P1#CSR00', 'Product Solution', '2017-12-06', 'month', 5, 'SERVERS\DMSYSTEM')
cm_stor_re = "S007P0#SRE01";

'### Получаем текущую дату
currentDate = System.DateTime.Now();
cm_ent_desorb_sol = "S007P0#PNS01"; ' Приготовление десорбирущего раствора
cm_wo_desorb_sol = cm_ent_desorb_sol + "_" +DateSelected.ToString("yyyyMMdd");

'### Формируем заготовку для WO
cm_wo_sorption = ent_sorption+"_"+DateSelected.ToString("yyyyMMdd");
'### Формируем заготовку для расхода кислоты


cm_ent_desorption = "S007P2#CDS01"; ' Десорбция ЦМК ''!!!!! Добавить объект S007P2#CDS00
cm_wo_desorption = cm_ent_desorption+"_"+DateSelected.ToString("yyyyMMdd");

cm_ent_denitr = "S007P0#PDS01";

cm_wo_block = ent_block+"_"+DateSelected.ToString("yyyyMMdd");

cm_ent_stor_acid = "S007M0#SSA01";	'ССК Центр
cm_ent_stor_acid2 = "S007M0#SSA02";	'ССК Запад
cm_ent_stor_acid3 = "S007M0#SSA03";	'ССК Восток

cm_stor_an = "S007P0#SAN01";


'### Код насосной станции ЦМК
cm_PumpStation_code = "S007M0#PST01";

'### Пользователь от которого происходит автоматический учет
def_user = "SERVERS\DMSYSTEM";

'### Получаем сессию
Result = aaFactMES.aaClientSession.GetInstance();
clientSession = result.Value;
'### Если сессия получена
if (clientSession <> null) then
	' Проверяем, есть ли wo по Канжугану ГТП, если нет - блокируем ввод по ЦМК ГТП
	sql = 	"select wo_id from wo where wo_id='" + cm_wo_sorption + "_1'";
	' Запрашиваем данные
	Result = clientSession.GetDSbySQL(sql);
	if result.Success then
		DS = Result.DataSet_value;
		if({(DS.Tables(0).Rows.Count < 1) or }(DateSelected.Date > currentDate.Date)) then
			' Блокируем
			ES_CM_ProductSolutionInputCellM.disable = true;
			ES_CM_AcidRemainingInputCellStorT.disable = true;
			ES_CM_AcidConsumptionDataUPPRInputCellT.disable = true;
			ES_CM_ProductionRichEluateCellInputKGU.disable = true;
			ES_CM_SapltpetreCellInputKG.disable = true;
		else
			' Разблокируем
			ES_CM_ProductSolutionInputCellM.disable = false;
			ES_CM_AcidRemainingInputCellStorT.disable = false;
			ES_CM_AcidConsumptionDataUPPRInputCellT.disable = false;
			ES_CM_ProductionRichEluateCellInputKGU.disable = false;
			ES_CM_SapltpetreCellInputKG.disable = false;

			LogWarning("Refresh");

			' Получаем плановые данные по объему ПР  Центральный Мынкудук за две смены 
			sql = "SELECT [dbo].[fn_Z_GetProductionPlanByPeriod] ('"+ent_block+"','"+DateSelected.ToString("yyyy-MM-dd")+"','Day') as req_qty";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = row(0).ToString().Replace(",",".");
				SetCustomPropertyValue("ES_CM_ProductSolutionPlanM.Value", val_str, true);
				LogMessage("Получаем плановые данные по объему ПР  Центральный Мынкудук за две смены:"+val_str);
			next;
			endif;

			' Получаем плановую концентрацию ПР  Центральный Мынкудук за две смены 
			sql = "SELECT [dbo].[fn_Z_GetProductionPlanPeriodByAttr] ('"+ent_block+"','"+DateSelected.ToString("yyyy-MM-dd")+"','Day','WO_LAB_PS_P') as conc_plan";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = row(0).ToString().Replace(",",".");
				SetCustomPropertyValue("ES_CM_ProductSolutionConcPlanMG.Value", val_str, true);
				LogMessage("Получаем плановую концентрацию ПР  Центральный Мынкудук за две смены:"+val_str);
			next;
			endif;

			' Получаем плановую концентрацию ВР  Центральный Мынкудук за две смены 
			sql = "SELECT [dbo].[fn_Z_GetProductionPlanPeriodByAttr] ('"+ent_sorption+"','"+DateSelected.ToString("yyyy-MM-dd")+"','Day','WO_LAB_LS_P') as conc_plan";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = row(0).ToString().Replace(",",".");
				SetCustomPropertyValue("ES_CM_LeachSolutionConcPlanMG.Value", val_str, true);
				LogMessage("Получаем плановую концентрацию ВР  Центральный Мынкудук за две смены:"+val_str);
			next;
			endif;

			' Получаем фактические данные по ПР ЦМК за две смены
			sql = "select dbo.fn_Z_GetConsumptionByPeriod('"+ent_sorption+"', 'Product Solution', '"+ DateSelected.ToString("yyyy-MM-dd") + "', 'day', 1, default)";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = row(0).ToString().Replace(",",".");
				SetCustomPropertyValue("ES_CM_ProductSolutionDataM.Value", val_str, true);
				val = System.Convert.ToDouble(row(0).ToString());;
				ES_CM_ProductSolutionInputCellM.Value = val;
				ES_CM_ProductSolutionInputCellM.NewValue = val;
				LogMessage("Получаем фактические данные по ПР ЦМК за две смены:"+val_str);
			next;
			endif;

			' Получаем фактическую концентрацию ПР ЦМК
			sql = "select dbo.fn_Z_GetLastLabResultByDate(N'" + ent_pump_station_ps + "', N'Содержание урана', N'Product Solution', '" + DateSelected.ToString("yyyy-MM-dd") + "') as result";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				'val = System.Convert.ToDouble(row(0).ToString());
				SetCustomPropertyValue("ES_CM_ProductSolutionConcDataMG.Value", row(0).ToString().Replace(",","."), true);
				LogMessage("Получаем фактическую концентрацию ПР ЦМК:"+row(0).ToString().Replace(",","."));
			next;
			endif;

			' Получаем фактическую концентрацию ВР ЦМК
			sql = "select dbo.fn_Z_GetLastLabResultByDate(N'" + ent_pump_station_ls + "', N'Содержание урана', N'Leach Solution', '" + DateSelected.ToString("yyyy-MM-dd") + "') as result";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				'val = System.Convert.ToDouble(row(0).ToString());
				SetCustomPropertyValue("ES_CM_LeachSolutionConcDataMG.Value", row(0).ToString().Replace(",","."), true);
				LogMessage("Получаем фактическую концентрацию ВР ЦМК:"+row(0).ToString().Replace(",","."));
			next;
			endif;

			dim sumPrihod as float;
			sumPrihod = 0;
			' Получаем фактические данные по приходу Кислоты на СЖР ЦМК за две смены (по данным учета)
			sql = "select dbo.fn_Z_GetTransferByPeriod('"+cm_ent_stor_acid+"', 'Sulphuric Acid', '"+DateSelected.ToString("yyyy-MM-dd")+"', 'day', '"+def_user+"')";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
				DS = Result.DataSet_value;
				for each row in DS.Tables(0).Rows
					sumPrihod  = sumPrihod + row(0);
				next;
			endif;
			sql = "select dbo.fn_Z_GetTransferByPeriod('"+cm_ent_stor_acid2+"', 'Sulphuric Acid', '"+DateSelected.ToString("yyyy-MM-dd")+"', 'day', '"+def_user+"')";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
				DS = Result.DataSet_value;
				for each row in DS.Tables(0).Rows
					sumPrihod  = sumPrihod + row(0);
				next;
			endif;
			sql = "select dbo.fn_Z_GetTransferByPeriod('"+cm_ent_stor_acid3+"', 'Sulphuric Acid', '"+DateSelected.ToString("yyyy-MM-dd")+"', 'day', '"+def_user+"')";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
				DS = Result.DataSet_value;
				for each row in DS.Tables(0).Rows
					sumPrihod  = sumPrihod + row(0);
				next;
			endif;
			val_str = System.Math.Round(sumPrihod,3).ToString().Replace(",",".");
			LogMessage("Получаем фактические данные по приходу Кислоты на СЖР ЦМК за две смены (по данным учета):" + val_str);
			SetCustomPropertyValue("ES_CM_AcidReceiveDataStorT.Value", val_str, true);

			' Получаем фактические данные по потреблению Кислоты на ГТП ЦМК за две смены (по данным учета)
			sql = "select dbo.fn_Z_GetConsumptionByPeriod('"+ent_block+"', 'Sulphuric Acid', '"+DateSelected.ToString("yyyy-MM-dd")+"', 'day', default, '"+def_user+"')";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = System.Math.Round(row(0),3).ToString().Replace(",",".");
				LogMessage("Получаем фактические данные по потреблению Кислоты на ГТП ЦМК за две смены (по данным учета):" + val_str);
				SetCustomPropertyValue("ES_CM_AcidConsumptionGTPDataT.Value", val_str, true);
			next;
			endif;

			' Получаем фактические данные по потреблению Кислоты на ГТП ЦМК за две смены 
			sql = "select dbo.fn_Z_GetConsumptionByPeriod('"+ent_block+"', 'Sulphuric Acid', '"+DateSelected.ToString("yyyy-MM-dd")+"', 'day', default, default)";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val = System.Convert.ToDouble(System.Math.Round(row(0),3).ToString());
				CM_AcidConsumptionGTPStorT_Prev = val;
				LogMessage("Получаем фактические данные по потреблению Кислоты на ГТП ЦМК за две смены:"+val.ToString());
			next;
			endif;

			' Получаем фактические данные по потреблению Кислоты на УППР ЦМК за две смены (по данным учета)
			sql = "select dbo.fn_Z_GetConsumptionByPeriod('"+cm_ent_denitr+"', 'Sulphuric Acid', '"+DateSelected.ToString("yyyy-MM-dd")+"', 'day', default, '"+def_user+"')";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = System.Math.Round(row(0),3).ToString().Replace(",",".");
				LogMessage("Получаем фактические данные по потреблению Кислоты на УППР ЦМК за две смены (по данным учета):" + val_str);
				SetCustomPropertyValue("ES_CM_AcidConsumptionDataUPPRT.Value", val_str, true);
			next;
			endif;

			' Получаем фактические данные по потреблению Кислоты на УППР ЦМК за две смены 
			sql = "select dbo.fn_Z_GetConsumptionByPeriod('"+cm_ent_denitr+"', 'Sulphuric Acid', '"+DateSelected.ToString("yyyy-MM-dd")+"', 'day', default, default)";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val = System.Convert.ToDouble(System.Math.Round(row(0),3).ToString());
				ES_CM_AcidConsumptionDataUPPRInputCellT.Value = val;
				ES_CM_AcidConsumptionDataUPPRInputCellT.NewValue = val;
				LogMessage("Получаем фактические данные по потреблению Кислоты на УППР ЦМК за две смены:"+val.ToString());
			next;
			endif;

			' Получаем фактические данные по остатку Кислоты СЖР ЦМК по данным учета на начало суток 
			sql = "select dbo.fn_Z_GetItemInvByDate('"+cm_ent_stor_acid+"', 'Sulphuric Acid', '"+DateSelected.ToString("yyyy-MM-dd 08:00:00")+"') as StartDay";
			LogMessage("Моисеев: "+sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = System.Math.Round(row(0),3).ToString().Replace(",",".");
				LogMessage("Получаем остаток Кислоты на начало суток СЖР ЦМК:"+val_str);
				SetCustomPropertyValue("ES_CM_AcidRemainingDataStartT.Value", val_str, true);
			next;
			endif;

			' Получаем фактические данные по остатку Кислоты СЖР ЦМК на конец суток 
			val = System.Math.Round(ES_CM_AcidRemainingDataStartT.Value + ES_CM_AcidReceiveDataStorT.Value - CM_AcidConsumptionGTPStorT_Prev - ES_CM_AcidConsumptionDataUPPRInputCellT.Value,3);
			LogMessage("Получаем остаток Кислоты на конец суток СЖР ЦМК:"+val.ToString());
			ES_CM_AcidRemainingInputCellStorT.Value = val;
			ES_CM_AcidRemainingInputCellStorT.NewValue = val;

			' Получаем фактические данные по остатку Кислоты СЖР ЦМК с уровнемера на конец суток 
			sql = "select dbo.fn_Z_GetStorageQtyByDate('"+cm_ent_stor_acid+"', 'Sulphuric Acid', '"+DateSelected.ToString("yyyy-MM-dd 20:00:00")+"', default) as EndDay";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = System.Math.Round(row(0),3).ToString().Replace(",",".");
				LogMessage("Получаем остаток Кислоты на конец суток СЖР ЦМК (с уровнемера):"+val_str);
				SetCustomPropertyValue("ES_CM_AcidRemainingDataLogT.Value", val_str, true);
			next;
			endif;

			' Получаем плановые данные по выпуску ТД  Центральный Мынкудук за две смены 
			sql = "SELECT [dbo].[fn_Z_GetProductionPlanByPeriod] ('"+cm_ent_desorption+"','"+DateSelected.ToString("yyyy-MM-dd")+"','Day') as req_qty";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = row(0).ToString().Replace(",",".");
				SetCustomPropertyValue("ES_CM_ProductionRichEluatePlanKGU.Value", val_str, true);
				LogMessage("Получаем плановые данные по выпуску ТД  Центральный Мынкудук за две смены:"+val_str);
			next;
			endif;

			' Получаем фактические данные по выпущенному товарному десорбату ЦМК за две смены 
			sql = "select dbo.fn_Z_GetProductionByPeriod('"+cm_ent_desorption+"', 'Rich Eluate', '"+DateSelected.ToString("yyyy-MM-dd")+"', 'day', 2, default)";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val = System.Convert.ToDouble(row(0).ToString());
				ES_CM_ProductionRichEluateCellInputKGU.Value = val;
				ES_CM_ProductionRichEluateCellInputKGU.NewValue = val;
				val_str = row(0).ToString().Replace(",",".");
				SetCustomPropertyValue("ES_CM_ProductionRichEluateDataKGU.Value", val_str, true);
				LogMessage("Получаем фактические данные по выпущенному товарному десорбату ЦМК за две смены:"+val.ToString());
			next;
			endif;


			' Получаем норматив аммиачной селитры
			sql = "SELECT [dbo].[fn_Z_GetNormativeFromBOM] ('"+cm_ent_desorb_sol+"','"+DateSelected.ToString("yyyy-MM-dd")+"','Ammonium Nitrate') as norm_plan";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val = System.Convert.ToDouble(row(0).ToString());
				CM_NormAmmoniumNitrate = val;
				LogMessage("Получаем норматив аммиачной селитры:"+val);
			next;
			endif;

			' Получаем фактические данные по потреблению Аммиачной селитры ЦМК УППР за две смены (по данным учета)
			sql = "select dbo.fn_Z_GetConsumptionByPeriod('"+cm_ent_desorb_sol+"', 'Ammonium Nitrate', '"+DateSelected.ToString("yyyy-MM-dd")+"', 'day', default, '"+def_user+"')";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = row(0).ToString().Replace(",",".");
				LogMessage("Получаем фактические данные по потреблению Аммиачной селитры ЦМК УППР за две смены (по данным учета):" + val_str);
				SetCustomPropertyValue("ES_CM_SapltpetreDataKG.Value", val_str, true);
			next;
			endif;

			' Получаем фактические данные по остатку Аммиачной селитры ЦМК на начало суток 
			sql = "select dbo.fn_Z_GetItemInvByDate('"+cm_stor_an+"', 'Ammonium Nitrate', '"+DateSelected.ToString("yyyy-MM-dd 08:00:00")+"') as StartDay";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = row(0).ToString().Replace(",",".");
				LogMessage("Получаем остаток Аммиачной селитры на начало суток ЦМК:" + val_str);
				SetCustomPropertyValue("ES_CM_SaltPetreRemainingDataStart.Value", val_str, true);
			next;
			endif;

			' Получаем фактические данные по приходу Аммиачной селитры ЦМК за две смены
			sql = "declare @d datetime SET @d = '" + DateSelected.ToString("yyyy-MM-dd") + " 08:00:00' SELECT ISNULL((select SUM(qty_txd) from item_transfer where to_ent_id=(select ent_id from ent where ent_name='" + cm_stor_an + "') and transfer_time_local >= @d and transfer_time_local < DATEADD(HOUR, 24, @d)),0)";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = row(0).ToString().Replace(",",".");
				SetCustomPropertyValue("ES_CM_SaltPetreReceiveData.Value", val_str, true);
				LogMessage("Получаем фактические данные по приходу Аммиачной селитры ЦМК за две смены:"+val_str);
			next;
			endif;

			' Получаем фактические данные по потреблению Ионита ЦМК УППР за две смены (по данным учета)
			'sql = "select dbo.fn_Z_GetConsumptionByPeriod('"+ent_sorption+"', 'New Ionite', '"+DateSelected.ToString("yyyy-MM-dd")+"', 'day', default, '"+def_user+"')";
			'LogMessage(sql);
			' Запрашиваем данные
			'Result = clientSession.GetDSbySQL(sql);
			'if result.Success then
			'DS = Result.DataSet_value;
			'for each row in DS.Tables(0).Rows
			'	val_str = row(0).ToString().Replace(",",".");
			'	LogMessage("Получаем фактические данные по потреблению Ионита ЦМК УППР за две смены (по данным учета):" + val_str);
			'	SetCustomPropertyValue("ES_CM_IoniteDataKG.Value", val_str, true);
			'next;
			'endif;

			' Получаем фактические данные по потреблению Аммиачной селитры Канжуган УППР за две смены 
			sql = "select dbo.fn_Z_GetConsumptionByPeriod('"+cm_ent_desorb_sol+"', 'Ammonium Nitrate', '"+DateSelected.ToString("yyyy-MM-dd")+"', 'day', default, default)";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val = System.Convert.ToDouble(row(0).ToString());
				ES_CM_SapltpetreCellInputKG.Value = val;
				ES_CM_SapltpetreCellInputKG.NewValue = val;
				LogMessage("Получаем фактические данные по потреблению Аммиачной селитры ЦМК УППР за две смены:"+val.ToString());
			next;
			endif;
			
			' Получаем фактические данные по потреблению Ионита ЦМК УППР за две смены 
			'sql = "select dbo.fn_Z_GetConsumptionByPeriod('"+cm_ent_sorption+"', 'New Ionite', '"+DateSelected.ToString("yyyy-MM-dd")+"', 'day', default, default)";
			'LogMessage(sql);
			' Запрашиваем данные
			'Result = clientSession.GetDSbySQL(sql);
			'if result.Success then
			'DS = Result.DataSet_value;
			'for each row in DS.Tables(0).Rows
			'	val = System.Convert.ToDouble(row(0).ToString());
			'	ES_CM_IoniteCellInputKG.Value = val;
			'	ES_CM_IoniteCellInputKG.NewValue = val;
			'	LogMessage("Получаем фактические данные по потреблению Ионита ЦМК УППР за две смены:"+val.ToString());
			'next;
			'endif;

			' Получаем фактические данные по остатку ТД на конец суток 
			sql = "select dbo.fn_Z_GetItemInvByDate('"+cm_stor_re+"', 'Rich Eluate', '"+DateSelected.AddDays(1).ToString("yyyy-MM-dd 08:00:00")+"') as EndDay";
			LogMessage(sql);
			' Запрашиваем данные
			Result = clientSession.GetDSbySQL(sql);
			if result.Success then
			DS = Result.DataSet_value;
			for each row in DS.Tables(0).Rows
				val_str = row(0).ToString().Replace(",",".");
				LogMessage("Получаем остаток ТД на конец суток ЦМК:" + val_str);
				SetCustomPropertyValue("ES_CM_RemainingRichEluateDataKGU.Value", val_str, true);
			next;
			endif;

		endif;
	endif;
endif;

