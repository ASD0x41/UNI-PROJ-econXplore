using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EMG
{
    public static class CONSTANTS
    {
        public static readonly int COOLDOWN_TURNS = 3;
        public static readonly double DOLLAR_RATE_MOD = 1.0;
        public static readonly double DOLLAR_SUPPLY = 15_000_000_000.00;
        public static readonly double DOLLAR_DEMAND = 4_500_000_000_000.00;

        public static readonly double INFLATION_MOD_PER_DOLLAR = 1;  //
        public static readonly double INDUSTRY_PROCEEDS_PER_CAPITA = 1;  //
        public static readonly double MAINTENANCE_PER_UNIT_INFRASTRUCTURE = 0.0001;   //

        public static readonly double DEBT_OWED_CHINA = 0.075;
        public static readonly double DEBT_OWED_IMF = 0.05;
        public static readonly double DEBT_OWED_ARABS = 0.025;

        public static readonly double STARTING_DEBT_CHINA = 40_000_000_000;
        public static readonly double STARTING_DEBT_IMF = 20_000_000_000;
        public static readonly double STARTING_DEBT_ARABS = 20_000_000_000;
        public static readonly double STARTING_DEBT_LOCAL_BANK = 20_000_000_000;

        public static readonly double STARTING_TAX_RATE = 15.00;
        public static readonly double STARTING_DUTY_RATE = 5.00;
        public static readonly double STARTING_SUBSIDY_RATE = 5.00;
        public static readonly double STARTING_NAT_LEVEL = 50.00;
        public static readonly double STARTING_AVG_SALARY = 100_000.00;

        public static readonly double STARTING_DEBT_LIMIT_CHINA = 40_000_000_000;   //
        public static readonly double STARTING_DEBT_LIMIT_IMF = 20_000_000_000; //
        public static readonly double STARTING_DEBT_LIMIT_ARABS = 20_000_000_000;   //
        public static readonly double STARTING_DEBT_LIMIT_LOCAL = 20_000_000_000;  //

        public static readonly double INTEREST_RATE_CHINA = 0.075;
        public static readonly double INTEREST_RATE_IMF = 0.05;
        public static readonly double INTEREST_RATE_ARABS = 0.025;
        public static readonly double INTEREST_RATE_LOCAL_BANK = 0.075;

        public static readonly double DEBT_LIMIT_MOD_CHINA = 1.0;
        public static readonly double DEBT_LIMIT_MOD_IMF = 0.5;
        public static readonly double DEBT_LIMIT_MOD_ARABS = 0.1;
        public static readonly double DEBT_LIMIT_MOD_LOCAL_BANK = 0.1;

        public static readonly long POPULATION = 240_000_000;
        public static readonly double POP_GROWTH_RATE = 0.005;

        public static readonly double ASSETS = 350_000_000_000.00;

        public static readonly double IMPORTS = 60_000_000_000.00;
        public static readonly double EXPORTS = 35_000_000_000.00;

        public static readonly double IMPORTS_PER_CAPITA = 250;
        public static readonly double EXPORTS_PER_ASSETS = 0.1;

        public static readonly double BLACK_MARKET_VOLUME = 25_000_000_000.00;
        public static readonly double RAID_RATIO = 0.1;

        public static readonly double WELFARE_PER_CAPITA_MAX = 0.00001;

        public static readonly double EVENT_COST_SPORTS_FESTIVAL = 0.00001;
        public static readonly double EVENT_COST_PUBLIC_HOLIDAY = 0.00001;
        public static readonly double EVENT_COST_CULTURAL_FESTIVAL = 0.00001;

        public static readonly double EVENT_IMP_SPORTS_FESTIVAL = 1; //
        public static readonly double EVENT_IMP_PUBLIC_HOLIDAY = 1;  //
        public static readonly double EVENT_IMP_CULTURAL_FESTIVAL = 1;   //

        public static readonly double RAID_ANGER = 1;    //

        public static readonly double STARTING_TREASURY_BALANCE = 50_000_000_000_000.00;
        public static readonly double STARTING_FOREX_RESERVES = 10_000_000_000.00;

        public static readonly int STARTING_POP_HAPPINESS = 50;
        public static readonly double STARTING_EMP_PERCENT = 50.00;
        public static readonly double STARTING_PUBLIC_RATIO = 50.00;
        public static readonly double STARTING_AVG_INCOME = 100_000;
        public static readonly double STARTING_INFLATION = 50.00;

        public static readonly double STARTING_DOLLAR_RATE = 275.00;
        public static readonly double REMIT_PER_CAPITA = 300;
    }

    interface IDataHandler
    {
        void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null);
        void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null);
    }

    interface ISubscriber
    {
        void Update();
    }

    interface IFinancialBody
    {
        double GetDebtOwed();
        double GetDebtLimit();
        double GetDebtRepaid();
        double GetInterestRate();
        double GetInterestPaid();

        bool IsAllowed();
        double TakeDebt(double amount);
        double RepayDebt(double amount);
        double PayInterest(double amount);
    }
    abstract class FinancialBody : IFinancialBody, ISubscriber
    {
        protected double debtTurn = 0;
        protected double debtOwed = 0;
        protected double debtLimit = 0;
        protected double debtRepaid = 1;
        protected double interestRate = 0;
        protected double interestPaid = 0;

        public double GetDebtOwed() { return debtOwed; }
        public double GetDebtLimit()
        {
            if (debtLimit > 0)
                return debtLimit;
            else
                return 0;
        }
        public double GetDebtRepaid() { return debtRepaid; }
        public double GetInterestRate() { return interestRate; }
        public double GetInterestPaid() { return interestPaid; }


        public bool IsAllowed()
        {
             IGameGlobal theGame = Game.GetInstance();
            if (theGame.GetTurn() - debtTurn > CONSTANTS.COOLDOWN_TURNS)
                return true;
            else
                return false;
        }
        public double TakeDebt(double amount)
        {
            if (IsAllowed())
            {
                if (amount >= 0)
                {
                    double payable = Math.Min(amount, debtLimit);
                    debtOwed += payable;
                    return payable;
                }
                else
                    return -1;
            }
            else
                return 0;
        }
        public double RepayDebt(double amount)
        {
            if (amount >= 0)
            {
                double payable = Math.Min(amount, debtOwed);
                double excess = amount - payable;
                debtOwed -= payable;
                debtRepaid += payable;
                return amount - payable;
            }
            else
                return -1;
        }
        public double PayInterest(double amount)
        {
            if (amount >= 0)
            {
                double payable = Math.Min(amount, interestRate * debtOwed);
                double excess = amount - payable;
                interestPaid += payable;
                return amount - payable;
            }
            else
                return -1;
        }

        public abstract void Update();
    }

    class China : FinancialBody, IDataHandler
    {
        static China instance = null;
        public static China GetInstance() => instance == null ? instance = new China() : instance;
        private China() { debtLimit = CONSTANTS.STARTING_DEBT_LIMIT_CHINA; interestPaid = 0; interestRate = CONSTANTS.INTEREST_RATE_CHINA; debtRepaid = 1; debtOwed = CONSTANTS.STARTING_DEBT_CHINA; }

        public override void Update()
        {
            IGovtForFinancialBody debtor = Govt.GetInstance();
            debtor.GetCountryFinanceReport(out double assets, out double liabilities, out double forex, out double remit);
            debtLimit = (assets - liabilities) * CONSTANTS.DEBT_LIMIT_MOD_CHINA;
        }
        public void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            doubles.Add(debtTurn);
            doubles.Add(debtOwed);
            doubles.Add(debtLimit);
            doubles.Add(debtRepaid);
            doubles.Add(interestRate);
            doubles.Add(interestPaid);
        }
        
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            debtTurn = doubles[counter1++];
            debtOwed = doubles[counter1++];
            debtLimit = doubles[counter1++];
            debtRepaid = doubles[counter1++];
            interestRate = doubles[counter1++];
            interestPaid = doubles[counter1++];
        }
    }
    class IMF : FinancialBody, IDataHandler
    {
        static IMF instance = null;
        public static IMF GetInstance() => instance == null ? instance = new IMF() : instance;
        private IMF() { debtLimit = CONSTANTS.STARTING_DEBT_LIMIT_IMF; interestPaid = 0; interestRate = CONSTANTS.INTEREST_RATE_IMF; debtRepaid = 1; debtOwed = CONSTANTS.STARTING_DEBT_IMF; }

        public override void Update()
        {
            IGovtForFinancialBody debtor = Govt.GetInstance();
            debtor.GetCountryFinanceReport(out double assets, out double liabilities, out double forex, out double remit);
            debtLimit = (assets - liabilities) * CONSTANTS.DEBT_LIMIT_MOD_IMF + forex;
        }

        public void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            doubles.Add(debtTurn);
            doubles.Add(debtOwed);
            doubles.Add(debtLimit);
            doubles.Add(debtRepaid);
            doubles.Add(interestRate);
            doubles.Add(interestPaid);
        }
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            debtTurn = doubles[counter1++];
            debtOwed = doubles[counter1++];
            debtLimit = doubles[counter1++];
            debtRepaid = doubles[counter1++];
            interestRate = doubles[counter1++];
            interestPaid = doubles[counter1++];
        }
    }
    class Arabs : FinancialBody, IDataHandler
    {
        static Arabs instance = null;
        public static Arabs GetInstance() => instance == null ? instance = new Arabs() : instance;
        private Arabs() { debtLimit = CONSTANTS.STARTING_DEBT_LIMIT_ARABS; interestPaid = 0; interestRate = CONSTANTS.INTEREST_RATE_ARABS; debtRepaid = 1; debtOwed = CONSTANTS.STARTING_DEBT_ARABS; }

        public override void Update()
        {
             IGovtForFinancialBody debtor = Govt.GetInstance();
            debtor.GetCountryFinanceReport(out double assets, out double liabilities, out double forex, out double remit);
            debtLimit = (assets - liabilities - debtOwed) * CONSTANTS.DEBT_LIMIT_MOD_ARABS;
        }
        public void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            doubles.Add(debtTurn);
            doubles.Add(debtOwed);
            doubles.Add(debtLimit);
            doubles.Add(debtRepaid);
            doubles.Add(interestRate);
            doubles.Add(interestPaid);
        }
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            debtTurn = doubles[counter1++];
            debtOwed = doubles[counter1++];
            debtLimit = doubles[counter1++];
            debtRepaid = doubles[counter1++];
            interestRate = doubles[counter1++];
            interestPaid = doubles[counter1++];
        }
    }

    class LocalBank : FinancialBody, IDataHandler
    {
        static LocalBank instance = null;
        public static LocalBank GetInstance() => instance == null ? instance = new LocalBank() : instance;
        private LocalBank() { debtLimit = CONSTANTS.STARTING_DEBT_LIMIT_LOCAL; interestPaid = 0; interestRate = CONSTANTS.INTEREST_RATE_LOCAL_BANK; debtRepaid = 1; debtOwed = CONSTANTS.STARTING_DEBT_LOCAL_BANK; }

        public override void Update()
        {
             IGovtForFinancialBody debtor = Govt.GetInstance();
            debtor.GetCountryFinanceReport(out double assets, out double liabilities, out double forex, out double remit);
            debtLimit = (assets - debtOwed) * CONSTANTS.DEBT_LIMIT_MOD_LOCAL_BANK;
        }
        public void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            doubles.Add(debtTurn);
            doubles.Add(debtOwed);
            doubles.Add(debtLimit);
            doubles.Add(debtRepaid);
            doubles.Add(interestRate);
            doubles.Add(interestPaid);
        }
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            debtTurn = doubles[counter1++];
            debtOwed = doubles[counter1++];
            debtLimit = doubles[counter1++];
            debtRepaid = doubles[counter1++];
            interestRate = doubles[counter1++];
            interestPaid = doubles[counter1++];
        }
    }


    interface ICountry
    {
        double GetAssets();

        void PlanSale(double amount);
        void CancelSale();
        double SellAssets();

        void ImproveInfrastructure(double amount);
        void DeteriorateInfrastructure(double amount);

        void Update();
    }
    class Country : ICountry, ISubscriber, IDataHandler
    {
        static Country instance = null;
        public static Country GetInstance() => instance == null ? instance = new Country() : instance;
        private Country() { }

        double assets = CONSTANTS.ASSETS;
        double salePlan = 0;

        public double GetAssets()
        {
            return assets;
        }

        public void PlanSale(double amount)
        {
            if (amount >= 0)
                salePlan = amount;
        }

        public void CancelSale()
        {
            salePlan = 0;
        }

        public double SellAssets()
        {
            if (salePlan >= 0)
            {
                double proceeds = Math.Min(salePlan, assets);
                assets -= proceeds;
                salePlan = 0;
                return proceeds;
            }
            else
                return -1;
        }

        public void ImproveInfrastructure(double amount)
        {
            if (amount >= 0)
                assets += amount;
        }
        public void DeteriorateInfrastructure(double amount)
        {
            if (amount >= 0)
                assets -= amount;
        }

        

        public void Update()
        {
            IGovtForAssetMgmt govt = Govt.GetInstance();
            double proceeds = SellAssets();
            govt.InformAssetSale(proceeds);
            Debug.Log("Sale: " + salePlan.ToString());
            Debug.Log("Assets: " + assets.ToString());
        }
        public void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            doubles.Add(assets);
            doubles.Add(salePlan);
        }
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            assets = doubles[counter1++];
            salePlan = doubles[counter1++];
        }
    }

    interface IPeople
    {
        long GetPop();
        long GetEmployedPop();
        double GetAvgIncome();

        double GetPublicRatio();

        long GetPublicEmps();
        double GetUnEmpRate();
        int GetPopHappiness();

        double AdjustPublicPrivateRatio(double newPercent);
    }

    interface IPeopleForRaid
    {
        void InformRaid(double anger);
    }

    interface IPeopleForEvent
    {
        void InformEvent(double popularity);
    }

    interface IPeopleForAssets
    {
        void InformSale(double proceeds);
    }

    class People : IPeople, ISubscriber, IDataHandler, IPeopleForRaid, IPeopleForEvent, IPeopleForAssets
    {
        static People instance = null;
        public static People GetInstance() => instance == null ? instance = new People() : instance;

        private People() { }

        long population = CONSTANTS.POPULATION;
        double popGrowthRate = CONSTANTS.POP_GROWTH_RATE;
        int popHappiness = CONSTANTS.STARTING_POP_HAPPINESS;

        double employedPercent = CONSTANTS.STARTING_EMP_PERCENT;
        double publicEmpPercent = CONSTANTS.STARTING_PUBLIC_RATIO;

        double avgIncome = CONSTANTS.STARTING_AVG_INCOME;
        double inflation = CONSTANTS.STARTING_INFLATION;

        public long GetPop()
        {
            return population;
        }

        public void GrowPopulation()
        {
            population += (long)(population * popGrowthRate);
        }

        public long GetEmployedPop()
        {
            return (long)(population * employedPercent / 100);
        }

        public long GetPublicEmps()
        {
            return (long)(GetEmployedPop() * publicEmpPercent / 100);
        }

        public double GetUnEmpRate()
        {
            return 100.00 - employedPercent;
        }

        public int GetPopHappiness()
        {
            return popHappiness;
        }

        public double GetPublicRatio()
        {
            return publicEmpPercent;
        }
        public double AdjustPublicPrivateRatio(double newPercent)
        {
            double diff = newPercent - publicEmpPercent;
            double proceeds = -diff * GetEmployedPop() * CONSTANTS.INDUSTRY_PROCEEDS_PER_CAPITA;
            if (diff > 0)
                return proceeds;
            else if (diff < 0)
                return -proceeds;
            else
                return 0;
        }

        public double GetAvgIncome() => avgIncome;

        public void InformRaid(double anger)
        {
            popHappiness -= (int)anger;  //
        }

        public void InformEvent(double popularity)
        {
            popHappiness += (int)popularity;
        }

        public void InformSale(double proceeds)
        {
            popHappiness += (int)proceeds;
        }

        public void Update()
        {
            GrowPopulation();
            ReComputePopHappiness();
        }


        void ReComputePopHappiness()
        {
            IGovtForPeople govt = Govt.GetInstance();
            IGameOutcome game = Game.GetInstance();
            govt.GetPublicIndicators(out double salary, out double dollarRate, out double taxRate, out double welfare, out double natLevel);
            popHappiness += (int)(salary + welfare - taxRate - dollarRate + natLevel); //
            if (popHappiness <= 0)
                game.InformRevolt();
        }

        public void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {

            longs.Add(population);
            doubles.Add(popGrowthRate);
            doubles.Add(employedPercent);
            doubles.Add(avgIncome);
            doubles.Add(publicEmpPercent);
            ints.Add(popHappiness);
            doubles.Add(inflation);

        }
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            population = longs[counter3++];
            popGrowthRate = doubles[counter1++];
            employedPercent = doubles[counter1++];
            avgIncome = doubles[counter1++];
            publicEmpPercent = doubles[counter1++];
            popHappiness = ints[counter2++];
            inflation = doubles[counter1++];

        }
    }

    interface ITraders
    {
        double GetImports();
        double GetExports();
    }

    class Traders : ITraders, ISubscriber, IDataHandler
    {
        static Traders instance = null;
        public static Traders GetInstance() => instance == null ? instance = new Traders() : instance;

        private Traders()
        {

        }


        double imports = CONSTANTS.IMPORTS;
        double exports = CONSTANTS.EXPORTS;

        public double GetImports()
        {
            return imports;
        }
        public double GetExports()
        {
            return exports;
        }
        public void Update()
        {
            ICountry country = Country.GetInstance();
            IPeople people = People.GetInstance();

            imports = people.GetPop() * CONSTANTS.IMPORTS_PER_CAPITA;
            exports = country.GetAssets() * CONSTANTS.EXPORTS_PER_ASSETS;
        }

        public void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            doubles.Add(imports);
            doubles.Add(exports);
        }
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            imports = doubles[counter1++];
            exports = doubles[counter1++];
        }
    }

    interface IAccount
    {
        double GetBalance();
        double Debit(double amount);
        double Credit(double amount);

        void ResetBalance(double amount);
    }
    class Account : IAccount
    {
        double balance = 0;

        public Account(double starting_balance = 0)
        {
            balance = starting_balance;
        }

        public double GetBalance() { return balance; }
        public double Debit(double amount)
        {
             IGameOutcome theGame = Game.GetInstance();
            if (amount >= 0)
            {
                double payable = Math.Min(amount, balance);
                balance -= payable;
                if (amount > balance)
                    theGame.InformBankruptcy();
                return payable;
            }
            else
                return -1;
        }
        public double Credit(double amount)
        {
            if (amount >= 0)
            {
                balance += amount;
                return 0;
            }
            else
                return -1;
        }


        public void ResetBalance(double initBalance)
        {
            balance = initBalance;
        }

    }


    interface IForexMarket
    {
        double GetDollarRate();
        double ConvertPKR2USD(double rupees);
        double ConvertUSD2PKR(double dollars);

        void Update();
    }

    class ForexMarket : IForexMarket, IDataHandler
    {
        static ForexMarket instance = null;
        public static ForexMarket GetInstance() => instance == null ? instance = new ForexMarket() : instance;
        private ForexMarket() { }

        double dollarRate = CONSTANTS.STARTING_DOLLAR_RATE;
        double dollarSupply = CONSTANTS.DOLLAR_SUPPLY;
        double dollarDemand = CONSTANTS.DOLLAR_DEMAND;

        public double GetDollarRate() { return dollarRate; }

        public double ConvertPKR2USD(double rupees)
        {
            dollarDemand += rupees / dollarRate;
            double oldRate = dollarRate;
            Update();
            double dollars = rupees * (dollarRate + oldRate) / 2;
            return dollars;
        }

        public double ConvertUSD2PKR(double dollars)
        {
            dollarSupply += dollars;
            double oldRate = dollarRate;
            Update();
            double rupees = dollars * (dollarRate + oldRate) / 2;
            return rupees;
        }

        public void Update()
        {
            dollarRate = dollarDemand / dollarSupply * CONSTANTS.DOLLAR_RATE_MOD;
        }

        public void GetData(List<double> doubles, List<int> ints, List<long> longs, List<bool> bools = null)
        {
            doubles.Add(dollarRate);
            doubles.Add(dollarSupply);
            doubles.Add(dollarDemand);
        }
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            dollarRate = doubles[counter1++];
            dollarSupply = doubles[counter1++];
            dollarDemand = doubles[counter1++];
        }
    }


    interface IBlackMarket
    {
        bool IsAllowed();
        bool IsRaidedRecently();
        void PlanRaid();
        void CancelRaid();

        double Raid();
    }
    class BlackMarket : IBlackMarket, ISubscriber, IDataHandler
    {
        static BlackMarket instance = null;
        public static BlackMarket GetInstance() => instance == null ? instance = new BlackMarket() : instance;
        private BlackMarket() { }

        double cashVolume = CONSTANTS.BLACK_MARKET_VOLUME;
        int actionTurn = -CONSTANTS.COOLDOWN_TURNS;
        bool planConduction = false;

        public bool IsAllowed()
        {
             IGameGlobal theGame = Game.GetInstance();
            if (theGame.GetTurn() - actionTurn > CONSTANTS.COOLDOWN_TURNS)
                return true;
            else
                return false;
        }

        public bool IsRaidedRecently()
        {
             IGameGlobal theGame = Game.GetInstance();
            if (theGame.GetTurn() == actionTurn)
                return true;
            else
                return false;
        }

        public void PlanRaid()
        {
            planConduction = true;
        }

        public void CancelRaid()
        {
            planConduction = false;
        }

        
        
        public double Raid()
        {
            if (IsAllowed())
            {
                IGameGlobal theGame = Game.GetInstance();
                actionTurn = theGame.GetTurn();
                planConduction = false;
                return cashVolume * CONSTANTS.RAID_RATIO;
            }
            else
                return 0;
        }

        public void Update()
        {
             IGovtForBlackMarket govt = Govt.GetInstance();
             IPeopleForRaid people = People.GetInstance();
            double proceeds = Raid();
            govt.InformRaidProceedings(proceeds);
            people.InformRaid(CONSTANTS.RAID_ANGER);
        }

        public void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            doubles.Add(cashVolume);
            ints.Add(actionTurn);
            if (planConduction == true)
                ints.Add(1);
            else
                ints.Add(0);
        }
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            cashVolume = doubles[counter1++];
            actionTurn = ints[counter2++];
            if (ints[counter2++] == 1)
                planConduction = true;
            else
                planConduction = false;
        }
    }


    interface IPublicEvent
    {
        bool IsAllowed();
        bool IsConductedRecently();
        void Plan();
        void Cancel();
        void Conduct();
    }
    abstract class PublicEvent : IPublicEvent
    {
        protected int actionTurn = -CONSTANTS.COOLDOWN_TURNS;
        protected bool planConduction = false;

        
        public bool IsAllowed()
        {
             IGameGlobal theGame = Game.GetInstance();
            if (theGame.GetTurn() - actionTurn > CONSTANTS.COOLDOWN_TURNS)
                return true;
            else
                return false;
        }

        public bool IsConductedRecently()
        {
             IGameGlobal theGame = Game.GetInstance();
            if (theGame.GetTurn() == actionTurn)
                return true;
            else
                return false;
        }

        public void Plan()
        {
            planConduction = true;
        }

        public void Cancel()
        {
            planConduction = false;
        }

        public void Conduct()
        {
            if (IsAllowed())
            {
                IGameGlobal theGame = Game.GetInstance();
                actionTurn = theGame.GetTurn();
                planConduction = false;
            }
        }
    }
    class SportsContest : PublicEvent, ISubscriber, IDataHandler
    {
        static SportsContest instance = null;
        public static SportsContest GetInstance() => instance == null ? instance = new SportsContest() : instance;
        private SportsContest() { }

        public void Update()
        {
             IGovtForPublicEvent govt = Govt.GetInstance();
             IPeopleForEvent people = People.GetInstance();
            Conduct();
            govt.InformEventConduction(CONSTANTS.EVENT_COST_SPORTS_FESTIVAL);
            people.InformEvent(CONSTANTS.EVENT_IMP_SPORTS_FESTIVAL);
        }

        public void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            ints.Add(actionTurn);
            if (planConduction == true)
                ints.Add(1);
            else
                ints.Add(0);
            //bools.Add(planConduction);

        }
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            actionTurn = ints[counter2++];
            if (ints[counter2++] == 1)
                planConduction = true;
            else
                planConduction = false;
            //planConduction = bool[counter4++];
        }



    }
    class PublicHoliday : PublicEvent, ISubscriber, IDataHandler
    {
        static PublicHoliday instance = null;
        public static PublicHoliday GetInstance() => instance == null ? instance = new PublicHoliday() : instance;
        private PublicHoliday() { }

        public void Update()
        {
            IGovtForPublicEvent govt = Govt.GetInstance();
            IPeopleForEvent people = People.GetInstance();
            Conduct();
            govt.InformEventConduction(CONSTANTS.EVENT_COST_PUBLIC_HOLIDAY);
            people.InformEvent(CONSTANTS.EVENT_IMP_PUBLIC_HOLIDAY);
        }

        public void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            ints.Add(actionTurn);
            if (planConduction == true)
                ints.Add(1);
            else
                ints.Add(0);
        }
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            actionTurn = ints[counter2++];
            if (ints[counter2++] == 1)
                planConduction = true;
            else
                planConduction = false;
        }

    }
    class CulturalFestival : PublicEvent, ISubscriber, IDataHandler
    {
        static CulturalFestival instance = null;
        public static CulturalFestival GetInstance() => instance == null ? instance = new CulturalFestival() : instance;
        private CulturalFestival() { }

        public void Update()
        {
             IGovtForPublicEvent govt = Govt.GetInstance();
             IPeopleForEvent people = People.GetInstance();
            Conduct();
            govt.InformEventConduction(CONSTANTS.EVENT_COST_CULTURAL_FESTIVAL);
            people.InformEvent(CONSTANTS.EVENT_IMP_CULTURAL_FESTIVAL);
        }

        public void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            ints.Add(actionTurn);
            if (planConduction == true)
                ints.Add(1);
            else
                ints.Add(0);
        }
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            actionTurn = ints[counter2++];
            if (ints[counter2++] == 1)
                planConduction = true;
            else
                planConduction = false;
        }
    }


    interface IGovtMiscControls
    {
        void GetDiversionDetails(out bool sports, out bool holiday, out bool festival);
        void ConductDiversion(IPublicEvent publicEvent);
        void CancelDiversion(IPublicEvent publicEvent);

        bool GetBlackMarketDetails();
        void RaidBlackMarket();
        void CancelRaid();

        double GetAssetDetails();
        void SellAssets(double amount);
        void CancelSale();
    }
    interface IGovtLocalDebtControls
    {
        void GetLocalDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
        void ManageLocalDebt(double amount);
    }
    interface IGovtChineseDebtControls
    {
        void GetChineseDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
        void ManageChineseDebt(double amount);
    }
    interface IGovtIMFDebtControls
    {
        void GetIMFDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
        void ManageIMFDebt(double amount);
    }
    interface IGovtArabDebtControls
    {
        void GetArabDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
        void ManageArabDebt(double amount);
    }
    interface IGovtCurrencyExchangeControls
    {
        void GetCurrencyExchangeDetails(out double treasuryBal, out double dollarRate, out double forexReservesBal);
        void TradeForex(double amount);
    }
    interface IGovtBudgetControls
    {
        void AdjustTaxRate(double newVal);
        void AdjustAvgSalary(double newVal);
        void AdjustImpDutyRate(double newVal);
        void AdjustExpSubsidyRate(double newVal);
        void AdjustNationalisationLevel(double newVal);

        void AdjustDevelopmentFund(double newVal);
        void AdjustWelfareSpending(double newVal);

        double GetTaxRate();
        double GetAvgSalary();
        double GetImpDutyRate();
        double GetExpSubsidyRate();
        double GetNationalisationLevel();
    }
    interface IGovtInternalProfile
    {
        void GetForexTradeDetailsRupees(out double bought, out double sold);
        void GetNationalisationDetails(out double privatisationProceeds, out double nationalisationFund);
        void GetGovtRevenueDetails(out double taxes, out double duties);
        void GetGovtExpenseDetails(out double salaries, out double subsidies, out double welfare, out double devFund);
        void GetLocalPaymentDetails(out double debtTaken, out double debtRepaid, out double interestPaid);
        void GetMiscFinancialDetails(out double diversion, out double raidProceeds);
    }
    interface IGovtExternalProfile
    {
        void GetForexTradeDetailsDollars(out double bought, out double sold);
        void GetTradeDetails(out double imports, out double exports, out double remit);
        void GetForeignDebtPayments(out double china, out double arabs, out double imf);
        void GetForeignDebtReceipts(out double china, out double arabs, out double imf);
        void GetForeignInterestPayments(out double china, out double arabs, out double imf);
        double GetAssetSales();
    }
    interface IGovtMiscProfile
    {
        void GetMiscDetails(out double pop, out double popHappiness, out double unemployment, out double inflation);
    }

    interface IGovtForBlackMarket
    {
        void InformRaidProceedings(double amount);
    }
    interface IGovtForPublicEvent
    {
        void InformEventConduction(double amount);
    }
    interface IGovtForAssetMgmt
    {
        void InformAssetSale(double amount);
    }
    interface IGovtForFinancialBody
    {
        void GetCountryFinanceReport(out double assets, out double liabilities, out double forex, out double remit);
    }

    interface IGovtForPeople
    {
        void GetPublicIndicators(out double salary, out double dollarRate, out double taxRate, out double welfare, out double natLevel);
    }

    class Govt : IGovtMiscControls, IGovtBudgetControls, IGovtInternalProfile, IGovtExternalProfile, IGovtCurrencyExchangeControls, IGovtLocalDebtControls, IGovtChineseDebtControls, IGovtIMFDebtControls, IGovtArabDebtControls, IGovtMiscProfile, IGovtForBlackMarket, IGovtForPublicEvent, IGovtForAssetMgmt, ISubscriber, IGovtForFinancialBody, IDataHandler, IGovtForPeople
    {
        static Govt instance = null;
        public static Govt GetInstance() => ((instance == null) ? (instance = new Govt()) : (instance));
        private Govt() { }

        // IGovtMiscControls:

         IPublicEvent sportsContest = SportsContest.GetInstance();
         IPublicEvent publicHoliday = PublicHoliday.GetInstance();
         IPublicEvent culturalFestival = CulturalFestival.GetInstance();
         IBlackMarket blackMarket = BlackMarket.GetInstance();
         ICountry country = Country.GetInstance();

        public void GetDiversionDetails(out bool sports, out bool holiday, out bool festival)
        {
            sports = sportsContest.IsAllowed();
            holiday = publicHoliday.IsAllowed();
            festival = culturalFestival.IsAllowed();
        }
        public void ConductDiversion(IPublicEvent publicEvent)
        {
            publicEvent.Plan();
        }
        public void CancelDiversion(IPublicEvent publicEvent)
        {
            publicEvent.Cancel();
        }

        public bool GetBlackMarketDetails()
        {
            return blackMarket.IsAllowed();
        }
        public void RaidBlackMarket()
        {
            blackMarket.PlanRaid();
        }
        public void CancelRaid()
        {
            blackMarket.CancelRaid();
        }

        public double GetAssetDetails()
        {
            return country.GetAssets();
        }
        public void SellAssets(double amount)
        {
            country.PlanSale(amount);
        }
        public void CancelSale()
        {
            country.CancelSale();
        }

        // IGovtLocalDebtControls:

         IFinancialBody localBank = LocalBank.GetInstance();
        double newlocaldebt = 0;

        public void GetLocalDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed)
        {
            debtOwed = localBank.GetDebtOwed();
            interestRate = localBank.GetInterestRate();
            debtLimit = localBank.GetDebtLimit();
            allowed = localBank.IsAllowed();
        }
        public void ManageLocalDebt(double amount)
        {
            newlocaldebt = amount;
        }

        // IGovtChineseDebtControls:

         IFinancialBody china = China.GetInstance();
        double newChinesedebt = 0;

        public void GetChineseDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed)
        {
            debtOwed = china.GetDebtOwed();
            interestRate = china.GetInterestRate();
            debtLimit = china.GetDebtLimit();
            allowed = china.IsAllowed();
        }
        public void ManageChineseDebt(double amount)
        {
            newChinesedebt = amount;
        }

        // IGovtIMFDebtControls:

         IFinancialBody imf = IMF.GetInstance();
        double newIMFdebt = 0;

        public void GetIMFDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed)
        {
            debtOwed = imf.GetDebtOwed();
            interestRate = imf.GetInterestRate();
            debtLimit = imf.GetDebtLimit();
            allowed = imf.IsAllowed();
        }
        public void ManageIMFDebt(double amount)
        {
            newIMFdebt = amount;
        }

        // IGovtArabDebtControls:

         IFinancialBody arabs = Arabs.GetInstance();
        double newArabdebt = 0;

        public void GetArabDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed)
        {
            debtOwed = arabs.GetDebtOwed();
            interestRate = arabs.GetInterestRate();
            debtLimit = arabs.GetDebtLimit();
            allowed = arabs.IsAllowed();
        }
        public void ManageArabDebt(double amount)
        {
            newArabdebt = amount;
        }

        // IGovtCurrencyExchangeControls:

         IAccount treasury = new Account(CONSTANTS.STARTING_TREASURY_BALANCE);
         IAccount forexReserve = new Account(CONSTANTS.STARTING_FOREX_RESERVES);
         IForexMarket forexMarket = ForexMarket.GetInstance();
        double newForexTrade = 0;

        public void GetCurrencyExchangeDetails(out double treasuryBal, out double dollarRate, out double forexReservesBal)
        {
            treasuryBal = treasury.GetBalance();
            dollarRate = forexMarket.GetDollarRate();
            forexReservesBal = forexReserve.GetBalance();
        }
        public void TradeForex(double amount)
        {
            newForexTrade = amount;
        }

        // IGovtBudgetControls:

        double taxRate = CONSTANTS.STARTING_TAX_RATE;
        double avgSalary = CONSTANTS.STARTING_AVG_SALARY;
        double impDutyRate = CONSTANTS.STARTING_DUTY_RATE;
        double expSubsidyRate = CONSTANTS.STARTING_SUBSIDY_RATE;
        double nationalisationLevel = CONSTANTS.STARTING_NAT_LEVEL;

        public void AdjustTaxRate(double newVal)
        {
            if (newVal >= 0 && newVal < 100)
                taxRate = newVal;
        }
        public void AdjustAvgSalary(double newVal)
        {
            if (newVal > 0)
                avgSalary = (newVal / 100) * CONSTANTS.STARTING_AVG_INCOME * 2;
        }
        public void AdjustImpDutyRate(double newVal)
        {
            if (newVal >= 0 && newVal < 100)
                impDutyRate = newVal;
        }
        public void AdjustExpSubsidyRate(double newVal)
        {
            if (newVal >= 0 && newVal <= 100)
                expSubsidyRate = newVal;
        }
        public void AdjustNationalisationLevel(double newVal)
        {
            if (newVal >= 0 && newVal <= 100)
                nationalisationLevel = newVal;
        }

        double newDevelopmentFund = 0;
        double newWelfareSpending = 0;

        public void AdjustDevelopmentFund(double newVal)
        {
            if (newVal >= 0)
                newDevelopmentFund = newVal;
        }
        public void AdjustWelfareSpending(double newVal)
        {
            if (newVal >= 0)
                newWelfareSpending = newVal;
        }

        public double GetTaxRate()
        {
            return taxRate;
        }
        public double GetAvgSalary()
        {
            return avgSalary;
        }
        public double GetImpDutyRate()
        {
            return impDutyRate;
        }
        public double GetExpSubsidyRate()
        {
            return expSubsidyRate;
        }
        public double GetNationalisationLevel()
        {
            return nationalisationLevel;
        }

        // END TURN:

        public void Update()
        {
            CollectTaxes();
            CollectDuties();
            PaySalaries();
            PaySubsidies();

            UpdateNationalisationLevel();
            DevelopInfrastructure();
            SpendOnWelfare();
            TradeForex();

            HandleForeignDebt();
            HandleLocalDebt();
            PayForeignInterest();
            PayLocalInterest();
        }

        double taxesCollected = 0;
        double dutiesCollected = 0;
        double salariesPaid = 0;
        double subsidiesPaid = 0;
        double nationalProceeds = 0;

        void CollectTaxes()
        {
            taxesCollected = taxRate / 100 * people.GetEmployedPop() * people.GetAvgIncome();
            treasury.Credit(taxesCollected);
        }
        void CollectDuties()
        {
            dutiesCollected = (impDutyRate / 100) * traders.GetImports() * forexMarket.GetDollarRate();
            treasury.Credit(dutiesCollected);
        }
        void PaySalaries()
        {
            salariesPaid = avgSalary * people.GetPublicEmps();
            treasury.Debit(salariesPaid);
        }
        void PaySubsidies()
        {
            subsidiesPaid = (expSubsidyRate / 100) * traders.GetExports() * forexMarket.GetDollarRate();
            treasury.Debit(subsidiesPaid);
            //Debug.Log(subsidiesPaid);

        }

        void UpdateNationalisationLevel()
        {
            double proceeds = people.AdjustPublicPrivateRatio(nationalisationLevel);
            if (proceeds > 0)
                treasury.Credit(proceeds);
            else
                treasury.Debit(proceeds);
            nationalProceeds = proceeds;
        }

        double infrastructureThreshold = 0;
        double oldDevelopmentFund = 0;
        double oldwelfareSpending = 0;

        void DevelopInfrastructure()
        {
            infrastructureThreshold = country.GetAssets() * people.GetPublicRatio() / 100 * CONSTANTS.MAINTENANCE_PER_UNIT_INFRASTRUCTURE;
            double diff = newDevelopmentFund - infrastructureThreshold;
            if (diff >= 0)
                country.ImproveInfrastructure(diff / forexMarket.GetDollarRate());
            else
                country.DeteriorateInfrastructure(-(diff / forexMarket.GetDollarRate()) / CONSTANTS.MAINTENANCE_PER_UNIT_INFRASTRUCTURE);
            oldDevelopmentFund = newDevelopmentFund;
            newDevelopmentFund = 0;
        }
        void SpendOnWelfare()
        {
            treasury.Debit(newWelfareSpending);
            oldwelfareSpending = newWelfareSpending;
            newWelfareSpending = 0;
        }

        double oldForexTrade = 0;
        double oldForexReturn = 0;

        double oldChineseDebt = 0;
        double oldIMFDebt = 0;
        double oldArabDebt = 0;
        double oldLocalDebt = 0;
        double remittances = 0;

        void TradeForex()   // +ive: dollars bought; -ive: dollars sold
        {
            if (newForexTrade > 0)
            {
                newForexTrade = Math.Min(newForexTrade, treasury.GetBalance());
                treasury.Debit(newForexTrade);
                double dollars = forexMarket.ConvertPKR2USD(newForexTrade);
                forexReserve.Credit(dollars);
                oldForexReturn = dollars;
                oldForexTrade = newForexTrade;
                newForexTrade = 0;
            }
            else if (newForexTrade < 0)
            {
                newForexTrade = Math.Min(-newForexTrade, forexReserve.GetBalance());
                forexReserve.Debit(newForexTrade);
                double rupees = forexMarket.ConvertUSD2PKR(newForexTrade);
                treasury.Credit(rupees);
                oldForexReturn = -rupees;
                oldForexTrade = -newForexTrade;
                newForexTrade = 0;
            }

            remittances = people.GetEmployedPop() * (100 - people.GetPublicRatio()) / 100 * CONSTANTS.REMIT_PER_CAPITA;
            forexReserve.Credit(remittances);
        }


        void HandleForeignDebt()
        {
            if (newChinesedebt > 0)
            {
                double received = china.TakeDebt(newChinesedebt);
                forexReserve.Credit(received);
                oldChineseDebt = received;
                newChinesedebt = 0;
            }
            else if (newChinesedebt < 0)
            {
                double paid = -newChinesedebt - china.RepayDebt(-newChinesedebt);
                forexReserve.Debit(paid);
                oldChineseDebt = -paid;
                newChinesedebt = 0;
            }

            if (newIMFdebt > 0)
            {
                double received = imf.TakeDebt(newIMFdebt);
                forexReserve.Credit(received);
                oldIMFDebt = received;
                newIMFdebt = 0;
            }
            else if (newIMFdebt < 0)
            {
                double paid = -newIMFdebt - imf.RepayDebt(-newIMFdebt);
                forexReserve.Debit(paid);
                oldIMFDebt = -paid;
                newIMFdebt = 0;
            }

            if (newArabdebt > 0)
            {
                double received = arabs.TakeDebt(newArabdebt);
                forexReserve.Credit(received);
                oldArabDebt = received;
                newArabdebt = 0;
            }
            else if (newArabdebt < 0)
            {
                double paid = -newArabdebt - arabs.RepayDebt(-newArabdebt);
                forexReserve.Debit(paid);
                oldArabDebt = -paid;
                newArabdebt = 0;
            }
        }
        void HandleLocalDebt()
        {
            if (newlocaldebt > 0)
            {
                double received = localBank.TakeDebt(newlocaldebt);
                treasury.Credit(received);
                oldLocalDebt = received;
                newlocaldebt = 0;
            }
            else if (newlocaldebt < 0)
            {
                double paid = -newlocaldebt - localBank.RepayDebt(-newlocaldebt);
                treasury.Debit(paid);
                oldLocalDebt = -paid;
                newlocaldebt = 0;
            }
        }

        double chineseInterest = 0;
        double IMFinterest = 0;
        double arabInterest = 0;
        double localInterest = 0;

        void PayForeignInterest()
        {
            chineseInterest = china.GetDebtOwed() * china.GetInterestRate();
            IMFinterest = imf.GetDebtOwed() * imf.GetInterestRate();
            arabInterest = arabs.GetDebtOwed() * arabs.GetInterestRate();

            forexReserve.Debit(chineseInterest + IMFinterest + arabInterest);

            china.PayInterest(chineseInterest);
            imf.PayInterest(IMFinterest);
            arabs.PayInterest(arabInterest);
        }
        void PayLocalInterest()
        {
            localInterest = localBank.GetDebtOwed() * localBank.GetInterestRate();

            treasury.Debit(localInterest);

            localBank.PayInterest(localInterest);
        }

        // IGovtInternalProfile:

        double raidSiezure = 0;
        double diversionCost = 0;

        public void GetForexTradeDetailsRupees(out double bought, out double sold)
        {
            bought = oldForexReturn < 0 ? -oldForexReturn : 0;
            sold = oldForexTrade > 0 ? oldForexTrade : 0;
        }
        public void GetNationalisationDetails(out double privatisationProceeds, out double nationalisationFund)
        {
            privatisationProceeds = nationalProceeds > 0 ? nationalProceeds : 0;
            nationalisationFund = nationalProceeds < 0 ? -nationalProceeds : 0;
        }
        public void GetGovtRevenueDetails(out double taxes, out double duties)
        {
            taxes = taxesCollected;
            duties = dutiesCollected;
        }
        public void GetGovtExpenseDetails(out double salaries, out double subsidies, out double welfare, out double devFund)
        {
            salaries = salariesPaid;
            subsidies = subsidiesPaid;
            welfare = oldwelfareSpending;
            devFund = oldDevelopmentFund;
        }
        public void GetLocalPaymentDetails(out double debtTaken, out double debtRepaid, out double interestPaid)
        {
            debtTaken = oldLocalDebt > 0 ? oldLocalDebt : 0;
            debtRepaid = oldLocalDebt < 0 ? -oldLocalDebt : 0;
            interestPaid = localInterest;
        }
        public void GetMiscFinancialDetails(out double diversion, out double raidProceeds)
        {
            diversion = diversionCost;
            raidProceeds = raidSiezure;
        }

        // IGovtExternalProfile:

        ITraders traders = Traders.GetInstance();
        double assetSales = 0;

        public void GetForexTradeDetailsDollars(out double bought, out double sold)
        {
            bought = sold = 0;
            if (oldForexReturn > 0)
                bought = oldForexReturn;
            else if (oldForexTrade < 0)
                sold = -oldForexTrade;
        }
        public void GetTradeDetails(out double imports, out double exports, out double remit)
        {
            imports = traders.GetImports();
            exports = traders.GetExports();
            remit = remittances;
        }
        public void GetForeignDebtPayments(out double china, out double arabs, out double imf)
        {   // issue
            china = oldChineseDebt < 0 ? -oldChineseDebt : 0;
            arabs = oldArabDebt < 0 ? -oldArabDebt : 0;
            imf = oldIMFDebt < 0 ? -oldIMFDebt : 0;
        }
        public void GetForeignDebtReceipts(out double china, out double arabs, out double imf)
        {
            china = oldChineseDebt > 0 ? oldChineseDebt : 0;
            arabs = oldArabDebt > 0 ? oldArabDebt : 0;
            imf = oldIMFDebt > 0 ? oldIMFDebt : 0;
        }
        public void GetForeignInterestPayments(out double china, out double arabs, out double imf)
        {
            china = chineseInterest;
            arabs = arabInterest;
            imf = IMFinterest;
        }
        public double GetAssetSales()
        {
            return assetSales;
        }

        // IGovtMiscProfile:

         IPeople people = People.GetInstance();

        public void GetMiscDetails(out double pop, out double popHappiness, out double unemployment, out double inflation)
        {
            pop = people.GetPop();
            popHappiness = people.GetPopHappiness();
            unemployment = people.GetUnEmpRate();
            inflation = forexMarket.GetDollarRate() * CONSTANTS.INFLATION_MOD_PER_DOLLAR;
        }


        // IGovtForBlackMarket:

        public void InformRaidProceedings(double amount)
        {
            raidSiezure = amount;
        }

        // IGovtForPublicEvent:

        public void InformEventConduction(double amount)
        {
            diversionCost = amount;
        }

        // IGovtForAssetMgmt:

        public void InformAssetSale(double amount)
        {
            assetSales = amount;
        }

        // IGovtForFinancialBody:

        public void GetCountryFinanceReport(out double assets, out double liabilities, out double forex, out double remit)
        {
            assets = country.GetAssets();
            liabilities = china.GetDebtOwed() + imf.GetDebtOwed() + arabs.GetDebtOwed() + localBank.GetDebtOwed() / forexMarket.GetDollarRate();
            forex = forexReserve.GetBalance();
            remit = remittances;
        }

        // IGovtForPeople:

        public void GetPublicIndicators(out double salary, out double dollarRate, out double taxRate, out double welfare, out double natLevel)
        {
            salary = avgSalary;
            taxRate = this.taxRate;
            welfare = newWelfareSpending;
            dollarRate = forexMarket.GetDollarRate();
            natLevel = nationalisationLevel;
        }

        public void GetData(List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            doubles.Add(newlocaldebt);
            doubles.Add(newChinesedebt);
            doubles.Add(newIMFdebt);
            doubles.Add(newForexTrade);
            doubles.Add(taxRate);
            doubles.Add(avgSalary);
            doubles.Add(impDutyRate);
            doubles.Add(expSubsidyRate);
            doubles.Add(nationalisationLevel);
            doubles.Add(newDevelopmentFund);
            doubles.Add(newWelfareSpending);
            doubles.Add(taxesCollected);
            doubles.Add(dutiesCollected);
            doubles.Add(salariesPaid);
            doubles.Add(subsidiesPaid);
            doubles.Add(nationalProceeds);
            doubles.Add(infrastructureThreshold);
            doubles.Add(oldDevelopmentFund);
            doubles.Add(oldwelfareSpending);
            doubles.Add(oldForexTrade);
            doubles.Add(oldForexReturn);
            doubles.Add(oldChineseDebt);
            doubles.Add(oldIMFDebt);
            doubles.Add(oldArabDebt);
            doubles.Add(oldLocalDebt);
            doubles.Add(chineseInterest);
            doubles.Add(IMFinterest);
            doubles.Add(arabInterest);
            doubles.Add(localInterest);
            doubles.Add(raidSiezure);
            doubles.Add(diversionCost);
            doubles.Add(assetSales);

            doubles.Add(treasury.GetBalance());
            doubles.Add(forexReserve.GetBalance());
        }
        public void SetData(ref int counter1, ref int counter2, ref int counter3, ref int counter4, List<double> doubles = null, List<int> ints = null, List<long> longs = null, List<bool> bools = null)
        {
            newlocaldebt = doubles[counter1++];
            newChinesedebt = doubles[counter1++];
            newIMFdebt = doubles[counter1++];
            newArabdebt = doubles[counter1++];
            newForexTrade = doubles[counter1++];
            taxRate = doubles[counter1++];
            avgSalary = doubles[counter1++];
            impDutyRate = doubles[counter1++];
            expSubsidyRate = doubles[counter1++];
            nationalisationLevel = doubles[counter1++];
            newDevelopmentFund = doubles[counter1++];
            newWelfareSpending = doubles[counter1++];
            taxesCollected = doubles[counter1++];
            dutiesCollected = doubles[counter1++];
            salariesPaid = doubles[counter1++];
            subsidiesPaid = doubles[counter1++];
            nationalProceeds = doubles[counter1++];
            infrastructureThreshold = doubles[counter1++];
            oldDevelopmentFund = doubles[counter1++];
            oldwelfareSpending = doubles[counter1++];
            oldForexTrade = doubles[counter1++];
            oldForexReturn = doubles[counter1++];
            oldChineseDebt = doubles[counter1++];
            oldIMFDebt = doubles[counter1++];
            oldArabDebt = doubles[counter1++];
            oldLocalDebt = doubles[counter1++];
            chineseInterest = doubles[counter1++];
            IMFinterest = doubles[counter1++];
            arabInterest = doubles[counter1++];
            localInterest = doubles[counter1++];
            raidSiezure = doubles[counter1++];
            diversionCost = doubles[counter1++];
            assetSales = doubles[counter1++];

            treasury.ResetBalance(doubles[counter1++]);
            forexReserve.ResetBalance(doubles[counter1++]);
        }

    }

    interface IDatabaseHandler
    {
        void SaveData(string userKey, int turn);
        int LoadData(string userKey, bool flag);
    }

    class DatabaseHandler : IDatabaseHandler
    {
        static DatabaseHandler instance = GetInstance();
        public static DatabaseHandler GetInstance() { return instance == null ? instance = new DatabaseHandler() : instance; }
        private DatabaseHandler()
        {

        }

        public void SaveData(string key, int turn)
        {

            IDataHandler china = China.GetInstance();
            IDataHandler imf = IMF.GetInstance();
            IDataHandler arab = Arabs.GetInstance();
            IDataHandler localbank = LocalBank.GetInstance();
            IDataHandler country= Country.GetInstance();
            IDataHandler people = People.GetInstance();
            IDataHandler trader = Traders.GetInstance();
            IDataHandler forexMarket = ForexMarket.GetInstance();
            IDataHandler blackmarket = BlackMarket.GetInstance();
            IDataHandler sportcontest = SportsContest.GetInstance();
            IDataHandler publicholiday = PublicHoliday.GetInstance();

            IDataHandler govt = Govt.GetInstance();
            //IDataHandler publicevent = PublicEvent.GetInstance();



            List<double> listOfDoubles = new List<double>();
            List<int> listOfInt = new List<int>();
            List<long> listOfLong = new List<long>();
            List<bool> listOfBool = new List<bool>();


            china.GetData(doubles: listOfDoubles);
            imf.GetData(doubles: listOfDoubles);
            arab.GetData(doubles: listOfDoubles);
            localbank.GetData(doubles: listOfDoubles);
            country.GetData(doubles: listOfDoubles);
            people.GetData(doubles: listOfDoubles, ints: listOfInt, longs: listOfLong);
            trader.GetData(doubles: listOfDoubles);
            forexMarket.GetData(doubles: listOfDoubles);
            blackmarket.GetData(doubles: listOfDoubles, ints: listOfInt, bools: listOfBool);
            sportcontest.GetData(ints: listOfInt, bools: listOfBool);
            publicholiday.GetData(ints: listOfInt, bools: listOfBool);
            govt.GetData(doubles: listOfDoubles);


            MyDb mydb = MyDb.GetInstance();
            mydb.saveToDb(key, listOfDoubles, listOfInt, listOfLong);


            


            // cloud save

        }

        public int LoadData(string userKey, bool flag)
        {
            // if flag, from db, otherwise defaults

            // cloud load

            //IDataHandler forexMarket = ForexMarket.GetInstance();

            IDataHandler china = China.GetInstance();
            IDataHandler imf = IMF.GetInstance();
            IDataHandler arab = Arabs.GetInstance();
            IDataHandler localbank = LocalBank.GetInstance();
            IDataHandler country = Country.GetInstance();
            IDataHandler people = People.GetInstance();
            IDataHandler trader = Traders.GetInstance();
            IDataHandler forexMarket = ForexMarket.GetInstance();
            IDataHandler blackmarket = BlackMarket.GetInstance();
            IDataHandler sportcontest = SportsContest.GetInstance();
            IDataHandler publicholiday = PublicHoliday.GetInstance();
            IDataHandler govt = Govt.GetInstance();


            List<double> listOfDoubles = new List<double>();
            List<int> listOfInt = new List<int>();
            List<long> listOfLong = new List<long>();
            List<bool> listOfBool = new List<bool>();

            MyDb mydb = MyDb.GetInstance();
            mydb.DisplayWeaopns(key, listOfDoubles, listOfInt, listOfLong);



            int counter1 = 0;
            int counter2 = 0;
            int counter3 = 0;
            int counter4 = 0;


            china.SetData(ref counter1, ref counter2, ref counter3, ref counter4, doubles: listOfDoubles);
            imf.SetData(ref counter1, ref counter2, ref counter3, ref counter4, doubles: listOfDoubles);
            arab.SetData(ref counter1, ref counter2, ref counter3, ref counter4, doubles: listOfDoubles);
            localbank.SetData(ref counter1, ref counter2, ref counter3, ref counter4, doubles: listOfDoubles);
            country.SetData(ref counter1, ref counter2, ref counter3, ref counter4, doubles: listOfDoubles);
            people.SetData(ref counter1, ref counter2, ref counter3, ref counter4, doubles: listOfDoubles, ints: listOfInt, longs: listOfLong);
            trader.SetData(ref counter1, ref counter2, ref counter3, ref counter4, doubles: listOfDoubles);
            forexMarket.SetData(ref counter1, ref counter2, ref counter3, ref counter4, doubles: listOfDoubles);
            blackmarket.SetData(ref counter1, ref counter2, ref counter3, ref counter4, doubles: listOfDoubles, ints: listOfInt, bools: listOfBool);
            sportcontest.SetData(ref counter1, ref counter2, ref counter3, ref counter4, ints: listOfInt, bools: listOfBool);
            publicholiday.SetData(ref counter1, ref counter2, ref counter3, ref counter4, ints: listOfInt, bools: listOfBool);
            govt.SetData(ref counter1, ref counter2, ref counter3, ref counter4, doubles: listOfDoubles);

            //forexMarket.SetData(doubles: listOfDoubles);

            // return turn here return turn;
            return 0;
        }
    }

    interface IGame
    {
        void Continue();
        void NewGame();
        void Resign();
        void Exit();
        int GetTurn();
        void EndTurn();


        bool CheckVictory();
        bool CheckResignation();
        bool CheckRevolt();
        bool CheckBankruptcy();
    }

    interface IGameGlobal
    {
        int GetTurn();
    }

    interface IGameOutcome
    {
        void InformBankruptcy();
        void InformRevolt();
    }

    interface IPublisher
    {
        void AddSubscriber(ISubscriber newSubscriber);
        void RemoveSubscriber(ISubscriber newSubscriber);
        void NotifySubscribers();
    }

    class Game : IGame, IGameGlobal, IPublisher, IGameOutcome
    {
        static Game instance = GetInstance();
        public static Game GetInstance() { return ((instance == null) ? (instance = new Game()) : (instance)); }
        private Game()
        {
            AddSubscriber(SportsContest.GetInstance());
            AddSubscriber(PublicHoliday.GetInstance());
            AddSubscriber(CulturalFestival.GetInstance());
            AddSubscriber(BlackMarket.GetInstance());

            AddSubscriber(China.GetInstance());
            AddSubscriber(IMF.GetInstance());
            AddSubscriber(Arabs.GetInstance());
            AddSubscriber(LocalBank.GetInstance());

            AddSubscriber(Country.GetInstance());
            AddSubscriber(Traders.GetInstance());
            AddSubscriber(People.GetInstance());
            AddSubscriber(Govt.GetInstance());
        }

        int turn = 0;

        public int GetTurn() => turn;

        List<ISubscriber> subscribers = new List<ISubscriber>();

        public void AddSubscriber(ISubscriber newSubscriber)
        {
            subscribers.Add(newSubscriber);
        }

        public void RemoveSubscriber(ISubscriber oldSubscriber)
        {
            subscribers.Remove(oldSubscriber);
        }

        public void NotifySubscribers()
        {
            foreach (ISubscriber sub in subscribers)
                sub.Update();
        }

        string userKey;

         IDatabaseHandler dbhandle = DatabaseHandler.GetInstance();

        public void Continue()
        {
            turn = dbhandle.LoadData(userKey, true);
        }
        public void NewGame()
        {
            turn = dbhandle.LoadData(userKey, false);
            EndTurn();  //
        }
        
        public void Exit()
        {
            dbhandle.SaveData(userKey, turn);
        }
        public void EndTurn()
        {
            NotifySubscribers();
            turn++;

            if (turn == 21)
            {
                victory = true;
                // Victory popup + back to menu
            }
        }




        private bool resignation = false;
        private bool bankruptcy = false;
        private bool revolt = false;
        private bool victory = false;

        public bool CheckVictory() => victory;
        public bool CheckRevolt() => revolt;
        public bool CheckResignation() => resignation;
        public bool CheckBankruptcy() => bankruptcy;

        public void Resign()
        {
            resignation = true;
            // Defeat via resignation popup + back to menu
        }

        public void InformBankruptcy()
        {
            bankruptcy = true;
            // Defeat via bankruptcy popup + back to menu
        }
        public void InformRevolt()
        {
            revolt = true;
            // Defeat via revolt popup + back to menu
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            IGame TheGame = Game.GetInstance();
        }
    }
}
