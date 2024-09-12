//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Gibs.Domain.Entities;
//using Gibs.Infrastructure;
//using Naicom.ApiV1;

//namespace Gibs.Api.Services
//{
//    public class NaicomService
//    {
//        private readonly Client _client;
//        private readonly GibsContext _uow;

//        public NaicomService(Settings settings, GibsContext gibsContext)
//        {
//            _uow = gibsContext;
//            _client = CreateClient(settings.Naicom.SID, 
//                                   settings.Naicom.Token, 
//                                   settings.Naicom.IsTest);

//            static Client CreateClient(string sid, string token, bool isTest)
//            {
//                if (isTest)
//                    return Client.CreateTestClient(sid, token, 20000);

//                return Client.CreateLiveClient(sid, token, 20000);
//            }
//        }

//        public async Task<NaicomRecord?> PolicyStatus(string policyNo)
//        {
//            var policy = await _uow.Policies.AsNoTracking()
//                                   .Where(p => p.PolicyNo == policyNo)
//                                   .Include(p => p.Histories)
//                                   .ThenInclude(h => h.DebitNote)
//                                   .SingleOrDefaultAsync() 
//                ?? throw new ArgumentOutOfRangeException(nameof(policyNo), "Policy was not found");

//            var ph = policy.Current 
//                ?? throw new Exception("Policy History is missing");

//            if (ph.DebitNote == null)
//                throw new Exception("Debit Note is missing");

//            return ph.DebitNote.Naicom;
//        }

//        public async Task<int> PolicyRecord(string policyNo)
//        {
//            var policy = await _uow.Policies 
//                                   .Where(p => p.PolicyNo == policyNo)
//                                   .Include(p => p.Histories)
//                                   .ThenInclude(h => h.Sections)
//                                   .Include(p => p.Histories)
//                                   .ThenInclude(h => h.DebitNote)
//                                   .Include(p => p.Customer)
//                                   .Include(p => p.Product)
//                                   .SingleOrDefaultAsync() 
//                ?? throw new ArgumentOutOfRangeException(nameof(policyNo), "Policy was not found");

//            NaicomRecord naicom;
//            var ph = policy.Current;
//            var classID = policy.Product.ClassID;

//            if (ph is null)
//                return -1;

//            if (ph.DebitNote is null)
//                return -2;

//            try
//            {
//                var postResponse = classID switch
//                {
//                    "V" => await RecordPolicy(ph, Auto(ph)),
//                    "B" => await RecordPolicy(ph, Bond(ph)),
//                    "E" => await RecordPolicy(ph, Engineer(ph)),
//                    "F" => await RecordPolicy(ph, Fire(ph)),
//                    "G" => await RecordPolicy(ph, Casualty(ph)),
//                    "M" => await RecordPolicy(ph, Marine(ph)),
//                    "O" => await RecordPolicy(ph, Oil(ph)),

//                    "H" => await RecordPolicy(ph, Marine(ph)),   // aviation   -> marine
//                    "PP" => await RecordPolicy(ph, Fire(ph)),    // package    -> fire
//                    "MH" => await RecordPolicy(ph, Marine(ph)),  // marinehull -> marine
//                    _ => throw new Exception($"Unknown ClassID [{classID}]"),
//                };

//                naicom = ToNaicomDetail(postResponse);
//                return await SaveNaicomToDb(ph.DebitNote, naicom);

//                static NaicomRecord ToNaicomDetail(PostResponse response)
//                {
//                    if (response.IsSucceed)
//                        return NaicomRecord.Success(response.PolicyUniqueID);

//                    return NaicomRecord.FailedPermanent(response.ErrorMessages, response.RequestPayload);
//                }
//            }
//            catch (NotImplementedException)
//            {
//                naicom = NaicomRecord.FailedPermanent($"NaicomNotImplemented {classID}", string.Empty);
//                return await SaveNaicomToDb(ph.DebitNote, naicom);
//            }
//            catch (System.Net.Http.HttpRequestException ex)
//            {
//                naicom = NaicomRecord.FailedPermanent($"HttpRequestException {ex.Message}");
//                return await SaveNaicomToDb(ph.DebitNote, naicom);
//            }
//            catch (Exception ex)
//            {
//                //var payload = (res == null) ? string.Empty : res.RequestPayload;
//                naicom = NaicomRecord.FailedPermanent($"NaicomUnknownError: {ex.Message}", ex.ToString());
//                return await SaveNaicomToDb(ph.DebitNote, naicom);
//            }

//            Task<int> SaveNaicomToDb(DebitNote dn, NaicomRecord naicom)
//            {
//                dn.UpdateNaicom(naicom);
//                return _uow.SaveChangesExAsync(GibsContext.SystemAccount);
//            }
//        }

//        public async Task<bool> PolicyDelete(string policyNo)
//        {
//            var policy = await _uow.Policies.AsNoTracking()
//                                   .Where(p => p.PolicyNo == policyNo)
//                                   .Include(p => p.Histories)
//                                   .SingleOrDefaultAsync() 
//                ?? throw new ArgumentOutOfRangeException(nameof(policyNo), "Policy was not found");

//            var result = await _client.PolicyDelete(policy.Current.NaicomID);

//            return result.IsSucceed;
//        }

//        private Task<PostResponse> RecordPolicy<T>(PolicyHistory ph, T details) where T : Detail, new()
//        {
//            var px = new Policy<T>(ph.Policy.Product.NaicomTypeID, 
//                                   ph.Business.StartDate.ToDateTime(),
//                                   ph.Business.EndDate.ToDateTime(),
//                                   $"{ph.PolicyNo}/{ph.DebitNoteNo}", 
//                                   ph.Policy.Product.ProductName, 
//                                   details);

//            px.Details.Premium = ph.Premium.GrossPremium;
//            px.Details.InsuredValue = ph.Premium.SumInsured;
//            px.Details.CommissionFee = ph.Premium.Commission;
//            px.Details.ExtraFee = 0;

//            //px.Details.Endorsements = null;
//            //px.Details.PremiumNote = null;
//            //px.Details.Conditions = null;
//            //px.Details.Exceptions = null;
//            //px.Details.Preamble = null;
//            //px.Details.Terms = null;

//            return _client.PolicyRecord(px);
//        }

//        #region Map Naicom Details
//        private static Auto Auto(PolicyHistory ph)
//        {
//            var coverType = ph.Sections.First().GetFieldValue("CoverType");
//            var c = ph.Policy.Customer;
//            var dx = new Auto
//            {
//                CoverageType = ToEnum2(coverType),
//                OrgType = OrganizationTypeEnum.COMMERCIAL,
//                OrgName = c.LastName,
//                OrgID = c.CustomerID,

//                OwnerType = ToEnum(c.CustomerType),
//                OwnerLicense = c.KycNumber,
//                PersonNameLast = c.LastName,
//                PersonNameFirst = c.FirstName,
//                PersonNameMiddle = c.OtherName,
//                AddressLine = c.Address,
//                Phone = c.PhoneNumber1,
//                Email = c.Email,
//                CityLGA = c.CityLGA,
//                State = c.StateID,
//                PostCode = 11755,
//            };

//            if (c.KycNumber == "")
//                dx.OwnerLicense = "9809282AF";

//            if (c.FirstName == "")
//                dx.PersonNameFirst = "NIL";

//            if (c.Address == "")
//                dx.AddressLine = "TBA";

//            if (c.PhoneNumber1 == "")
//                dx.Phone = "080577777777";

//            if (c.Email == "")
//                dx.Email = "info@cornerstone.com.ng";

//            foreach (var sec in ph.Sections)
//            {
//                var insured = new AutoInsured()
//                {
//                    //EngineNumber,ChasisNumber,VehicleUsage
//                    //StateOfIssue,CoverType

//                    VehicleID = sec.DeclarationNo,                  // sec.Field20,
//                    PlateNo = sec.GetFieldValue("VehicleRegNo"),    // sec.Field1,
//                    RegNo = sec.GetFieldValue("VehicleRegNo"),      // sec.Field1,
//                    RegDate = ph.Business.StartDate.ToDateTime(),   // DN.StartDate,
//                    RegExpDate = ph.Business.EndDate.ToDateTime(),  // DN.EndDate,
//                    RegMileage = 15667,
//                    AutoType = ToEnum3(sec.GetFieldValue("VehicleTypeID")), // AutoTypeEnum.CAR
//                    AutoMake = sec.GetFieldValue("VehicleMake"),    // sec.Field23,
//                    AutoModel = sec.GetFieldValue("VehicleModel"),  // sec.Field24,
//                    AutoColor = sec.GetFieldValue("VehicleColour"), // sec.Field4,
//                    AutoYear = sec.GetFieldValue("ManufactureYear", 1999),
//                    AutoNote = sec.GetFieldValue("ChasisNumber"),   // 0,
//                    EngineCap = sec.GetFieldValue("EngineCapacityHP"),  // sec.Field7,
//                    SeatCap = sec.GetFieldValue("NumberOfSeats"),   // 12,
//                };
//                dx.Insured.Add(insured);
//            }
//            return dx;

//            static AutoCoverageTypeEnum ToEnum2(string? coverType)
//            {
//                switch (coverType)
//                {
//                    case "COMPREHENSIVE":
//                        return AutoCoverageTypeEnum.COMPREHENSIVE;

//                    case "THIRD_PARTY_ONLY":
//                    case "THIRD PARTY ONLY":
//                    case "third party":
//                    case "Thirdparty":
//                        return AutoCoverageTypeEnum.THIRD_PARTY;

//                    case "THIRD_PARTY_FIRE_THEFT":
//                    case "THIRD PARTY, FIRE & THEFT":
//                    case "third party fire and theft":
//                        return AutoCoverageTypeEnum.INJURY_PROTECTION;

//                    default:
//                        return AutoCoverageTypeEnum.OTHER;
//                }
//            }

//            static AutoOwnerTypeEnum ToEnum(CustomerTypeEnum? custType)
//            {
//                switch (custType)
//                {
//                    case CustomerTypeEnum.INDIVIDUAL:
//                        return AutoOwnerTypeEnum.ORG;

//                    case CustomerTypeEnum.CORPORATE:
//                        return AutoOwnerTypeEnum.PERSON;

//                    default:
//                        return AutoOwnerTypeEnum.PERSON;
//                }
//            }

//            static AutoTypeEnum ToEnum3(string? vehicleTypeID)
//            {
//                //VAN, BUS, JEEP, MINI_BUS, MINI_TRUCK, MID_TRUCK, TRUNK_TRUCK, MOTORCYCLE, SALOON, TRICYCLE, TRACTOR, OTHER
//                switch (vehicleTypeID)
//                {
//                    case "SALOON":
//                        return AutoTypeEnum.CAR;
//                    case "VAN":
//                    case "BUS":
//                    case "MINI_BUS":
//                        return AutoTypeEnum.BUS;
//                    case "TRICYCLE":
//                    case "MOTORCYCLE":
//                        return AutoTypeEnum.MOTORCYCLE;
//                    case "JEEP":
//                        return AutoTypeEnum.OFFROAD;
//                    case "MID_TRUCK":
//                    case "MINI_TRUCK":
//                    case "TRUNK_TRUCK":
//                        return AutoTypeEnum.TRUCK;
//                    default:
//                        return AutoTypeEnum.OTHER;
//                }
//            }
//        }

//        private static Bond Bond(PolicyHistory ph)
//        {
//            throw new NotImplementedException();
//        }

//        private static Casualty Casualty(PolicyHistory ph)
//        {
//            var c = ph.Policy.Customer;
//            var dx = new Casualty
//            {
//                PersonalName = c.LastName,
//                PersonalAddress = c.Address,     //TBA
//                PersonalSpecialisation = "PhD",
//                PersonalDateBirth = ph.Business.StartDate.ToDateTime(),
//                PersonalSex = Naicom.ApiV1.GenderEnum.MALE,

//                ContactPhone = c.PhoneNumber1,   //08011111111
//                ContactEmail = c.Email,          //info@cornerstone.com.ng

//                //PremisesName = PD.Field9,        //TBA
//                //PremisesLocation = c.Remarks,    //Lagos
//                //PremisesOccupation = PD.Field11, //TBA
//                //PremisesBusinessType = "Commercial",
//                //PremisesBusinessHour = "8am-9pm",

//                //TransitSchedule = PD.Field12, //TBA
//                //TransitRoute = PD.Field13, //TBA
//                //SafeStrongRoomDetail = PD.Field14, //TBA
//                //SafeGuardInfo = PD.Field15, //TBA
//                //GoodPropertyInfo = PD.Field16, //TBA
//                //GoodTransitMethod = PD.Field17, //TBA
//                //GoodTransitVehicleInfo = PD.Field18, //TBA
//                //WorkDescription = PD.Field19, //TBA
//                //WorkLocation = PD.Field20, //TBA
//                //WorkEmployeeInfo = PD.Field21, //TBA
//                //CompanyInfo = PD.Field22, //TBA
//                //CompanyDirectorInfo = PD.Field23, //TBA
//                //ProfessionalActivityInfo = PD.Field24, //TBA
//                //ProfessionalEmployeeInfo = PD.Field25, //TBA
//                //AccidentCoverType = PD.Field26, //TBA
//                //AccidentBeneficiaryInfo = PD.Field27, //TBA
//                //AccidentInsuredBenefit = PD.Field28, //TBA
//                //AccidentInsuredPersons = PD.InsuredName, //TBA
//                //AccountInfo = PD.Field29, //TBA
//                //EmployeeInfo = PD.Field30, //TBA
//                //PublicRiskInfo = PD.Field31, //TBA
//                //WorkCondition = PD.Field32, //TBA
//                //EstimatedWages = PD.Field33, //TBA
//                //EstimatedEarnings = PD.Field34, //TBA
//                //RiskManagementDetail = PD.Field36 //TBA
//            };
//            return dx;
//        }

//        private static Engineer Engineer(PolicyHistory ph)
//        {
//            throw new NotImplementedException();
//        }

//        private static Fire Fire(PolicyHistory ph)
//        {
//            var c = ph.Policy.Customer;
//            var dx = new Fire
//            {
//                //customer
//                CustomerEmail = c.Email,
//                CustomerName = c.FullName,
//                CustomerPhone = c.PhoneNumber1,
//                //building
//                CustomerBuildingDoorNo = "202",
//                CustomerBuildingName = c.FullName,
//                CustomerBuildingAddressLine = c.Address,
//                CustomerBuildingAddressCityLGA = c.CityLGA,
//                CustomerBuildingAddressState = c.StateID,
//                CustomerBuildingAddressPostCode = 11755,
//                //property
//                PropertyBusiness = "n/a",       //PD.Field4;
//                PropertyConstruction = "n/a",   //PD.Field5;
//                PropertyConstructionValue = ph.Premium.SumInsured, //PD.SumInsured;
//                PropertyContent = "n/a",        //PD.Field6;
//                PropertyContentValue = ph.Premium.SumInsured, //PD.SumInsured;
//                //fire
//                FireCoverageDetail = "n/a",     //PD.Field6;
//                FireProtectionDetail = "n/a",   //PD.Field7;
//                FireHistory = "n/a",            //PD.Field6;
//                //buglary
//                BurglaryCoverageDetail = "n/a", //PD.Field10;
//                BurglaryAntiTheftDetail = "n/a", //PD.Field11;
//                BurglaryHistory = "n/a",        //PD.Field12;
//                //house
//                HouseBuildingType = HouseBuildingTypeEnum.PRIVATE_DEVELLING,
//                HouseCoverageDetail = "n/a",    //PD.Field14;
//                HouseSecurityDetail = "n/a",    //PD.Field15;
//                HouseHistory = "n/a"            //PD.Field16;
//            };
//            return dx;
//        }

//        private static Marine Marine(PolicyHistory ph)
//        {
//            var c = ph.Policy.Customer;
//            var dx = new Marine
//            {
//                CoverageType = "general",
//                OwnerName = c.FullName,
//                AddressLine = c.Address,
//                Phone = c.PhoneNumber1,
//                Email = c.Email,
//                CityLGA = c.CityLGA,
//                State = c.StateID,
//                PostCode = 11755,
//            };

//            if (c.Address == "")
//                dx.AddressLine = "TBA";

//            if (c.PhoneNumber1 == "")
//                dx.Phone = "080577777777";

//            if (c.Email == "")
//                dx.Email = "info@cornerstone.com.ng";

//            foreach (var sec in ph.Sections)
//            {
//                var insured = new MarineInsured()
//                {
//                    //VessleLength = 0,
//                    //VessleBreadth = 0,
//                    //VessleGrossTonage = 0,
//                    //VessleCapacityCargo = 0,
//                    //VessleMaximumSpeed = 0,
//                    VessleCapacityPassenger = 24,
//                    VesslePurchaseYear = 2005,
//                    VessleBuildYear = 2012,
//                    VessleNote = "TBA",
//                    VessleType = sec.GetFieldValue("_CertificateType"), //PD.Field2,
//                    VessleName = sec.GetFieldValue("VoyageFrom"),       //PD.Location
//                    VessleRegNo = sec.GetFieldValue("CertificateNo"),   //PD.ENDTNum
//                    VessleInsuredValue = sec.ItemSumInsured,            //PD.RiskSum
//                    VesslePurchaseValue = sec.ItemSumInsured,           //PD.RiskSum
//                    VessleMakeModel = sec.GetFieldValue("SubjectMatter"),//PD.SectionID 
//                    VessleUsage = sec.GetFieldValue("_BasisOfValuation"),//PD.Field13
//                };
//                dx.Insured.Add(insured);
//            }
//            return dx;
//        }

//        private static Oil Oil(PolicyHistory ph)
//        {
//            throw new NotImplementedException();
//        }
//        #endregion
//    }
//}
