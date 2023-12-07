using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using JetBrains.Annotations;

namespace EMG
{
    public class MyDb// : MonoBehaviour
    {
        static MyDb instance = GetInstance();
        public static MyDb GetInstance() { return ((instance == null) ? (instance = new MyDb()) : (instance)); }
        private MyDb() { }


        


        //private string dbName = "URI=file:Inventory.db";
        private string dbName = "URI=file:" + Application.dataPath + "/DB/Inventory.db";
        //string connectionString = "URI=file:" + Application.dataPath + "/YourDatabaseName.db";


        







        // Start is called before the first frame update
        void Start()
        {
            /*List<double> doubles = new List<double>();


            List<long> longs = new List<long>();
            List<int> ints = new List<int>();
            List<bool> bools = new List<bool>();

            List<double> doubles2 = new List<double>();
            List<long> longs2 = new List<long>();
            List<int> ints2 = new List<int>();


            string[] Chinaarr = new string[8];
            Chinaarr[0] = "pin";
            Chinaarr[1] = "debtturn";
            Chinaarr[2] = "debtlimit";
            Chinaarr[3] = "debtrepaid";
            Chinaarr[4] = "interestrate";
            Chinaarr[5] = "interestpaid";

            string pin = "1";

            //china
            doubles.Add(1231423);
            doubles.Add(22320);
            doubles.Add(3043240);
            doubles.Add(12234245);
            doubles.Add(2.2);

            //imf
            doubles.Add(2);
            doubles.Add(200000);
            doubles.Add(30000);
            doubles.Add(12345);
            doubles.Add(2.2);

            //arabs
            doubles.Add(2);
            doubles.Add(200000);
            doubles.Add(30000);
            doubles.Add(12345);
            doubles.Add(2.2);

            //localbanks
            doubles.Add(2);
            doubles.Add(200000);
            doubles.Add(30000);
            doubles.Add(12345);
            doubles.Add(2.2);

            //country
            doubles.Add(2560222222);
            doubles.Add(45533);


            //people
            longs.Add(5000000000000); //0 long
            doubles.Add(452);
            ints.Add(234); //int 0
            doubles.Add(789567);
            doubles.Add(0.2312);
            doubles.Add(1.2312);
            doubles.Add(2323.2323);

            //trader
            doubles.Add(3.2);
            doubles.Add(342);

            //Account
            doubles.Add(123);


            //forextrade
            doubles.Add(1);
            doubles.Add(2);
            doubles.Add(3);

            //blackmarket
            doubles.Add(6);
            ints.Add(5); // ints 1
            ints.Add(4);  // ints 2

            //sportscontest

            ints.Add(98); // ints 1
            ints.Add(89);  // ints 2

            //Public Holiday

            ints.Add(48); // ints 3
            ints.Add(84);  // ints 4


            //govt

            doubles.Add(11);
            doubles.Add(22);
            doubles.Add(33);
            doubles.Add(44);
            doubles.Add(55);
            doubles.Add(66);
            doubles.Add(77);
            doubles.Add(88);
            doubles.Add(99);
            doubles.Add(101);

            doubles.Add(111);
            doubles.Add(121);
            doubles.Add(131);
            doubles.Add(141);
            doubles.Add(151);
            doubles.Add(171);
            doubles.Add(181);
            doubles.Add(191);
            doubles.Add(201);
            doubles.Add(211);

            doubles.Add(221);
            doubles.Add(231);
            doubles.Add(241);
            doubles.Add(251);
            doubles.Add(261);
            doubles.Add(271);
            doubles.Add(281);
            doubles.Add(291);
            doubles.Add(301);
            doubles.Add(311);

            doubles.Add(321);
            doubles.Add(333);
            doubles.Add(352);
            doubles.Add(361);*/





            //CreateDB();
            //saveToDb(pin, doubles, ints, longs);
            //Debug.Log("Hello! ");
            //DisplayWeaopns(pin, doubles2, longs2, ints2);

        }

        public int CheckPin(string pin)
        {
            int exists = 0; // Default value assuming pin doesn't exist

            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    

                    command.CommandText = "SELECT 1 FROM China WHERE pin = @PinToCheck";
                    command.Parameters.AddWithValue("@PinToCheck", pin);

                    // ExecuteReader returns a DataReader object
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // If the reader has rows, the pin exists
                            exists = 1;
                        }
                    }
                }

                connection.Close();
            }

            return exists;
        }

        public void CreateDB()
        {
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "CREATE TABLE if not exists China (pin string, DebtTurn double, DebtLimit double, DebtRepaid double, InterestRate double, InterestPaid double, primary key (pin));";

                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE if not exists IMF (pin string, DebtTurn double, DebtLimit double, DebtRepaid double, InterestRate double, InterestPaid double, primary key (pin));";

                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE if not exists Arabs (pin string, DebtTurn double, DebtLimit double, DebtRepaid double, InterestRate double, InterestPaid double, primary key (pin));";

                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE if not exists LocalBank (pin string, DebtTurn double, DebtLimit double, DebtRepaid double, InterestRate double, InterestPaid double, primary key (pin));";

                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE if not exists Country (pin string, Assets double, SalesPlan double, primary key (pin));";

                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE if not exists People (pin string, Population Ineteger(15), PopGrowthRate double, PopHappiness int, EmployerPercent double, PublicEmPercent double, AvgIncome double, Inflation double, primary key (pin));";

                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE if not exists Trader (pin string, Imports double, Exports double, primary key (pin));";

                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE if not exists Account (pin string, Balance double, primary key (pin));";

                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE if not exists ForexMarket (pin string, DollaRrate double, DollarSupply double, DollarDemand double, primary key (pin));";

                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE if not exists BlackMarket (pin string, CashVolume double, ActionTurn int, PlanConduction int, primary key (pin));";

                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE if not exists Sportscontest (pin string, ActionTurn int, PlanConduction int, primary key (pin));";

                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE if not exists PublicHoliday (pin string, ActionTurn int, PlanConduction int, primary key (pin));";

                    command.ExecuteNonQuery();

                    command.CommandText = "CREATE TABLE if not exists Govt (pin string, NewLocalDebt double, NewChineseDebt double, NewImfDebt double, NewForexTrade double, TaxRate double, AvgsSalary double, ImpDutyRate double, ExpSubsidyRate double, NationalisationLevel double, NewDevelopmentFund double, NewWelfareSpending double, TaxesCollected double , DutiesCollected double , SalariesPaid double , SubsidiesPaid double , NationaProceeds double , InfrastructureThreshold double , OldDevelopmentFund double , OldWelfareFund double, OldForexTrade double, OldForexReturn double, OldChineseDebt double, OldImfDebt double, OldArabDebt double, OldLocalDebt double, ChineseInterest double, ImfInterest double, ArabInterest double, LocalInterest double, RaidSiezure double, DiversionCost double, AssetSales double, Treasure double, ForexReserve double, primary key (pin));";

                    command.ExecuteNonQuery();


                    Debug.Log("DB creation!");
                }
                connection.Close();
            }
        }
        public void saveToDb(string pin, List<double> doubles = null, List<int> ints = null, List<long> longs = null)
        {
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {

                    command.CommandText = "Insert into China (pin,debtturn,debtlimit,debtrepaid,interestrate,interestpaid) values ('" + pin + "', '" + doubles[0] + "', '" + doubles[1] + "', '" + doubles[2] + "', '" + doubles[3] + "', '" + doubles[4] + "');";


                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into IMF (pin,debtturn,debtlimit,debtrepaid,interestrate,interestpaid) values ('" + pin + "', '" + doubles[5] + "', '" + doubles[6] + "', '" + doubles[7] + "', '" + doubles[8] + "', '" + doubles[9] + "');";


                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into Arabs (pin,debtturn,debtlimit,debtrepaid,interestrate,interestpaid) values ('" + pin + "', '" + doubles[10] + "', '" + doubles[11] + "', '" + doubles[12] + "', '" + doubles[13] + "', '" + doubles[14] + "');";


                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into LocalBank (pin,debtturn,debtlimit,debtrepaid,interestrate,interestpaid) values ('" + pin + "', '" + doubles[15] + "', '" + doubles[16] + "', '" + doubles[17] + "', '" + doubles[18] + "', '" + doubles[19] + "');";


                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into Country (pin,Assets,SalesPlan) values ('" + pin + "', '" + doubles[20] + "', '" + doubles[21] + "');";


                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into People (pin, Population, PopGrowthRate, PopHappiness, EmployerPercent, PublicEmPercent, AvgIncome , Inflation ) values ('" + pin + "', '" + longs[0] + "', '" + doubles[22] + "', '" + ints[0] + "', '" + doubles[23] + "', '" + doubles[24] + "', '" + doubles[25] + "', '" + doubles[26] + "');";


                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into Trader (pin, Imports, Exports) values ('" + pin + "', '" + doubles[27] + "', '" + doubles[28] + "');";

                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into Account (pin, Balance) values ('" + pin + "', '" + doubles[29] + "');";

                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into ForexMarket (pin, DollarRate, DollarSupply, DollarDemand) values ('" + pin + "', '" + doubles[30] + "', '" + doubles[31] + "', '" + doubles[32] + "');";

                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into BlackMarket (pin, CashVolume, ActionTurn,PlanConduction) values ('" + pin + "', '" + doubles[33] + "', '" + ints[1] + "', '" + ints[2] + "');";

                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into SportsContest (pin, ActionTurn,PlanConduction) values ('" + pin + "','" + ints[3] + "', '" + ints[4] + "');";

                    command.ExecuteNonQuery();


                    command.CommandText = "Insert into PublicHoliday (pin,ActionTurn,PlanConduction) values ('" + pin + "','" + ints[5] + "', '" + ints[6] + "');";

                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into govt (pin , NewLocalDebt , NewChineseDebt , NewImfDebt , NewForexTrade , TaxRate , AvgsSalary , ImpDutyRate , ExpSubsidyRate , NationalisationLevel , NewDevelopmentFund , NewWelfareSpending , TaxesCollected  , DutiesCollected  , SalariesPaid  , SubsidiesPaid  , NationaProceeds  , InfrastructureThreshold  , OldDevelopmentFund  , OldWelfareFund , OldForexTrade , OldForexReturn , OldChineseDebt , OldImfDebt , OldArabDebt , OldLocalDebt , ChineseInterest , ImfInterest , ArabInterest , LocalInterest , RaidSiezure , DiversionCost , AssetSales , Treasure , ForexReserve ) values ('" + pin + "','" + doubles[34] + "','" + doubles[35] + "','" + doubles[36] + "','" + doubles[37] + "','" + doubles[38] + "','" + doubles[39] + "','" + doubles[40] + "','" + doubles[41] + "','" + doubles[42] + "','" + doubles[43] + "','" + doubles[44] + "','" + doubles[45] + "','" + doubles[46] + "','" + doubles[47] + "','" + doubles[48] + "','" + doubles[49] + "','" + doubles[50] + "','" + doubles[51] + "','" + doubles[52] + "','" + doubles[53] + "','" + doubles[54] + "','" + doubles[55] + "','" + doubles[56] + "','" + doubles[57] + "','" + doubles[58] + "','" + doubles[59] + "','" + doubles[60] + "','" + doubles[61] + "','" + doubles[62] + "','" + doubles[63] + "','" + doubles[64] + "','" + doubles[65] + "','" + doubles[66] + "','" + doubles[67] + "');";

                    command.ExecuteNonQuery();



                    //command.ExecuteNonQuery();
                }
                connection.Close();
            }

        }
        public void DisplayWeaopns(string pin, ref List<double> doubles, ref List<long> longs2, ref List<int> ints2)
        {
            //double debtLimit = 0.0;

            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM China WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            doubles.Add(Convert.ToDouble(reader["DebtLimit"]));
                            doubles.Add(Convert.ToDouble(reader["DebtTurn"]));
                            doubles.Add(Convert.ToDouble(reader["DebtRepaid"]));
                            doubles.Add(Convert.ToDouble(reader["InterestRate"]));

                            doubles.Add(Convert.ToDouble(reader["InterestPaid"]));
                            //doubles.Add(Convert.ToDouble(reader["DebtPaid"]));

                            //Debug.Log("Debt Limit: " + doubles[0]);
                            //Debug.Log("Debt Turn: " + doubles[1]);
                            //Debug.Log("Debt Repaid: " + doubles[2]);
                            //Debug.Log("Interest Rate: " + doubles[3]);
                            //Debug.Log("Debt Paid: " + doubles[4]);


                        }

                        reader.Close();
                    }



                    command.CommandText = "SELECT * FROM IMF WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            doubles.Add(Convert.ToDouble(reader["DebtLimit"]));
                            doubles.Add(Convert.ToDouble(reader["DebtTurn"]));
                            doubles.Add(Convert.ToDouble(reader["DebtRepaid"]));
                            doubles.Add(Convert.ToDouble(reader["InterestRate"]));

                            doubles.Add(Convert.ToDouble(reader["InterestPaid"]));
                            //doubles.Add(Convert.ToDouble(reader["DebtPaid"]));

                            //Debug.Log("Debt Limit: " + doubles[5]);
                            //Debug.Log("Debt Turn: " + doubles[6]);
                            //Debug.Log("Debt Repaid: " + doubles[7]);
                            //Debug.Log("Interest Rate: " + doubles[8]);
                            //Debug.Log("Debt Paid: " + doubles[9]);


                        }

                        reader.Close();
                    }

                    command.CommandText = "SELECT * FROM Arabs WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            doubles.Add(Convert.ToDouble(reader["DebtLimit"]));
                            doubles.Add(Convert.ToDouble(reader["DebtTurn"]));
                            doubles.Add(Convert.ToDouble(reader["DebtRepaid"]));
                            doubles.Add(Convert.ToDouble(reader["InterestRate"]));

                            doubles.Add(Convert.ToDouble(reader["InterestPaid"]));
                            //doubles.Add(Convert.ToDouble(reader["DebtPaid"]));

                            //Debug.Log("Debt Limit: " + doubles[10]);
                            //Debug.Log("Debt Turn: " + doubles[11]);
                            //Debug.Log("Debt Repaid: " + doubles[12]);
                            //Debug.Log("Interest Rate: " + doubles[13]);
                            //Debug.Log("Debt Paid: " + doubles[14]);


                        }

                        reader.Close();
                    }


                    command.CommandText = "SELECT * FROM LocalBank WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            doubles.Add(Convert.ToDouble(reader["DebtLimit"]));
                            doubles.Add(Convert.ToDouble(reader["DebtTurn"]));
                            doubles.Add(Convert.ToDouble(reader["DebtRepaid"]));
                            doubles.Add(Convert.ToDouble(reader["InterestRate"]));

                            doubles.Add(Convert.ToDouble(reader["InterestPaid"]));
                            //doubles.Add(Convert.ToDouble(reader["DebtPaid"]));

                            //Debug.Log("Debt Limit: " + doubles[15]);
                            //Debug.Log("Debt Turn: " + doubles[16]);
                            //Debug.Log("Debt Repaid: " + doubles[17]);
                            //Debug.Log("Interest Rate: " + doubles[18]);
                            //Debug.Log("Debt Paid: " + doubles[19]);


                        }

                        reader.Close();
                    }

                    command.CommandText = "SELECT * FROM Country WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            doubles.Add(Convert.ToDouble(reader["Assets"]));
                            doubles.Add(Convert.ToDouble(reader["SalesPlan"]));


                            //Debug.Log("Assets: " + doubles[20]);
                            //Debug.Log("Sales Plan: " + doubles[21]);



                        }

                        reader.Close();
                    }


                    command.CommandText = "SELECT * FROM People WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            longs2.Add(Convert.ToInt64(reader["Population"]));
                            doubles.Add(Convert.ToDouble(reader["PopGrowthRate"]));
                            ints2.Add(Convert.ToInt16(reader["PopHappiness"]));
                            doubles.Add(Convert.ToDouble(reader["EmployerPercent"]));
                            doubles.Add(Convert.ToDouble(reader["PublicEmPercent"]));
                            doubles.Add(Convert.ToDouble(reader["AvgIncome"]));
                            doubles.Add(Convert.ToDouble(reader["Inflation"]));


                            //Debug.Log("Population: " + longs2[0]);
                            //Debug.Log("PopGrowthRate: " + doubles[22]);
                            //Debug.Log("PopHappiness: " + ints2[0]);
                            //Debug.Log("EmployerPercent: " + doubles[23]);
                            //Debug.Log("PublicEmPercent: " + doubles[24]);
                            //Debug.Log("AvgIncome: " + doubles[25]);
                            //Debug.Log("Inflation: " + doubles[26]);



                        }

                        reader.Close();
                    }

                    command.CommandText = "SELECT * FROM Trader WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            doubles.Add(Convert.ToDouble(reader["Imports"]));
                            doubles.Add(Convert.ToDouble(reader["Exports"]));



                            //Debug.Log("Imports: " + doubles[27]);
                            //Debug.Log("Exports: " + doubles[28]);



                        }

                        reader.Close();
                    }

                    command.CommandText = "SELECT * FROM Account WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            doubles.Add(Convert.ToDouble(reader["Balance"]));




                            //Debug.Log("Balance: " + doubles[29]);




                        }

                        reader.Close();
                    }

                    command.CommandText = "SELECT * FROM ForexMarket WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            doubles.Add(Convert.ToDouble(reader["DollarRate"]));
                            doubles.Add(Convert.ToDouble(reader["DollarSupply"]));
                            doubles.Add(Convert.ToDouble(reader["DollarDemand"]));




                            //Debug.Log("DollarRate: " + doubles[30]);
                            //Debug.Log("DollarSupply: " + doubles[31]);
                            //Debug.Log("DollarDemand: " + doubles[32]);



                        }

                        reader.Close();
                    }

                    command.CommandText = "SELECT * FROM BlackMarket WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            doubles.Add(Convert.ToDouble(reader["CashVolume"]));
                            ints2.Add(Convert.ToInt16(reader["ActionTurn"]));
                            ints2.Add(Convert.ToInt16(reader["PlanConduction"]));




                            //Debug.Log("CashVolume: " + doubles[33]);
                            //Debug.Log("ActionTurn: " + ints2[1]);
                            //Debug.Log("PlanConduction: " + ints2[2]);



                        }

                        reader.Close();
                    }

                    command.CommandText = "SELECT * FROM Sportscontest WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {


                            ints2.Add(Convert.ToInt16(reader["ActionTurn"]));
                            ints2.Add(Convert.ToInt16(reader["PlanConduction"]));





                            //Debug.Log("ActionTurn: " + ints2[3]);
                            //Debug.Log("PlanConduction: " + ints2[4]);



                        }

                        reader.Close();
                    }
                    command.CommandText = "SELECT * FROM PublicHoliday WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {


                            ints2.Add(Convert.ToInt16(reader["ActionTurn"]));
                            ints2.Add(Convert.ToInt16(reader["PlanConduction"]));





                            //Debug.Log("ActionTurn: " + ints2[5]);
                            //Debug.Log("PlanConduction: " + ints2[6]);



                        }

                        reader.Close();
                    }

                    command.CommandText = "SELECT * FROM Govt WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {




                            doubles.Add(Convert.ToDouble(reader["NewLocalDebt"]));
                            doubles.Add(Convert.ToDouble(reader["NewChineseDebt"]));
                            doubles.Add(Convert.ToDouble(reader["NewImfDebt"]));
                            doubles.Add(Convert.ToDouble(reader["NewForexTrade"]));
                            doubles.Add(Convert.ToDouble(reader["TaxRate"]));
                            doubles.Add(Convert.ToDouble(reader["AvgsSalary"]));
                            doubles.Add(Convert.ToDouble(reader["ImpDutyRate"]));
                            doubles.Add(Convert.ToDouble(reader["ExpSubsidyRate"]));
                            doubles.Add(Convert.ToDouble(reader["NationalisationLevel"]));
                            doubles.Add(Convert.ToDouble(reader["NewDevelopmentFund"]));
                            doubles.Add(Convert.ToDouble(reader["NewWelfareSpending"]));
                            doubles.Add(Convert.ToDouble(reader["TaxesCollected"]));
                            doubles.Add(Convert.ToDouble(reader["DutiesCollected"]));

                            doubles.Add(Convert.ToDouble(reader["SalariesPaid"]));
                            doubles.Add(Convert.ToDouble(reader["SubsidiesPaid"]));
                            doubles.Add(Convert.ToDouble(reader["NationaProceeds"]));
                            doubles.Add(Convert.ToDouble(reader["InfrastructureThreshold"]));
                            doubles.Add(Convert.ToDouble(reader["OldDevelopmentFund"]));
                            doubles.Add(Convert.ToDouble(reader["OldWelfareFund"]));
                            doubles.Add(Convert.ToDouble(reader["OldForexTrade"]));
                            doubles.Add(Convert.ToDouble(reader["OldForexReturn"]));
                            doubles.Add(Convert.ToDouble(reader["OldChineseDebt"]));


                            doubles.Add(Convert.ToDouble(reader["OldImfDebt"]));
                            doubles.Add(Convert.ToDouble(reader["OldArabDebt"]));
                            doubles.Add(Convert.ToDouble(reader["OldLocalDebt"]));
                            doubles.Add(Convert.ToDouble(reader["ChineseInterest"]));
                            doubles.Add(Convert.ToDouble(reader["ImfInterest"]));
                            doubles.Add(Convert.ToDouble(reader["ArabInterest"]));
                            doubles.Add(Convert.ToDouble(reader["LocalInterest"]));
                            doubles.Add(Convert.ToDouble(reader["RaidSiezure"]));
                            doubles.Add(Convert.ToDouble(reader["DiversionCost"]));

                            doubles.Add(Convert.ToDouble(reader["AssetSales"]));
                            doubles.Add(Convert.ToDouble(reader["Treasure"]));
                            doubles.Add(Convert.ToDouble(reader["ForexReserve"]));






                            //Debug.Log("NewLocalDebt: " + doubles[34]);
                            //Debug.Log("NewChineseDebt: " + doubles[35]);
                            //Debug.Log("NewImfDebt: " + doubles[36]);
                            //Debug.Log("NewForexTrade: " + doubles[37]);
                            //Debug.Log("TaxRate: " + doubles[38]);
                            //Debug.Log("AvgsSalary: " + doubles[39]);
                            //Debug.Log("ImpDutyRate: " + doubles[40]);

                            //Debug.Log("ExpSubsidyRate: " + doubles[41]);
                            //Debug.Log("NationalisationLevel: " + doubles[42]);
                            //Debug.Log("NewDevelopmentFund: " + doubles[43]);
                            //Debug.Log("NewWelfareSpending: " + doubles[44]);
                            //Debug.Log("TaxesCollected: " + doubles[45]);
                            //Debug.Log("DutiesCollected: " + doubles[46]);
                            //Debug.Log("SalariesPaid: " + doubles[47]);
                            //Debug.Log("SubsidiesPaid: " + doubles[48]);
                            //Debug.Log("NationaProceeds: " + doubles[49]);
                            //Debug.Log("InfrastructureThreshold: " + doubles[50]);
                            //Debug.Log("OldDevelopmentFund: " + doubles[51]);
                            //Debug.Log("OldWelfareFund: " + doubles[52]);
                            //Debug.Log("OldForexTrade: " + doubles[53]);
                            //Debug.Log("OldForexReturn: " + doubles[54]);
                            //Debug.Log("OldChineseDebt: " + doubles[55]);


                            //Debug.Log("OldImfDebt: " + doubles[56]);
                            //Debug.Log("OldArabDebt: " + doubles[57]);
                            //Debug.Log("OldLocalDebt: " + doubles[58]);
                            //Debug.Log("ChineseInterest: " + doubles[59]);
                            //Debug.Log("ImfInterest: " + doubles[60]);
                            //Debug.Log("ArabInterest: " + doubles[61]);
                            //Debug.Log("LocalInterest: " + doubles[62]);
                            //Debug.Log("RaidSiezure: " + doubles[63]);
                            //Debug.Log("DiversionCost: " + doubles[65]);

                            //Debug.Log("AssetSales: " + doubles[65]);

                            //Debug.Log("Treasure: " + doubles[66]);
                            //Debug.Log("ForexReserve: " + doubles[67]);








                        }

                        reader.Close();
                    }
                    

                }

                connection.Close();
            }
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE * FROM China WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE * FROM IMF WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE * FROM Arabs WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE * FROM LocalBank WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    command.ExecuteNonQuery();


                    

                    command.CommandText = "DELETE * FROM LocalBank WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE * FROM Country WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE * FROM People WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE * FROM Account WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE * FROM Trader WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE * FROM BlackMarket WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);

                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE * FROM Sportscontest WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);


                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE * FROM PublicHoliday WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);


                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE * FROM Govt WHERE pin = @pin";
                    command.Parameters.AddWithValue("@pin", pin);


                    command.ExecuteNonQuery();


                }

                connection.Close();
            }


        }
    }

}