using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfRdsRepository : GenericRepository<TypeLibraryDbContext, RdsLibDm>, IEfRdsRepository
{
    private readonly TypeLibraryDbContext _context;
    private readonly IApplicationSettingsRepository _settings;

    public EfRdsRepository(TypeLibraryDbContext dbContext, IApplicationSettingsRepository settings) : base(dbContext)
    {
        _context = dbContext;
        _settings = settings;
    }

    /// <inheritdoc />
    public async Task ChangeState(State state, string id)
    {
        var rds = await GetAsync(id);
        rds.State = state;
        await SaveAsync();
        Detach(rds);
    }

    /// <inheritdoc />
    public IEnumerable<RdsLibDm> Get()
    {
        return GetAll();
    }

    /// <inheritdoc />
    public RdsLibDm Get(string id)
    {
        return FindBy(x => x.Id == id).FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<RdsLibDm> Create(RdsLibDm rds)
    {
        await CreateAsync(rds);
        await SaveAsync();

        Detach(rds);

        return rds;
    }

    /// <inheritdoc />
    public void ClearAllChangeTrackers()
    {
        Context?.ChangeTracker.Clear();
    }

    /// <inheritdoc />
    public async Task InitializeDb()
    {
        if (Get().ToList().IsNullOrEmpty())
        {
            var categoryId = Guid.NewGuid().ToString();

            await _context.Category.AddAsync(new CategoryLibDm
            {
                Id = categoryId,
                Name = "Oil and gas"
            });
            await SaveAsync();

            var rdsList = GetInitialRds();
            foreach (var rds in rdsList)
            {
                rds.Id = Guid.NewGuid().ToString();
                rds.Iri = $"{_settings.ApplicationSemanticUrl}/rds/{rds.Id}";
                rds.Created = DateTime.UtcNow;
                rds.CreatedBy = "System";
                rds.State = State.Approved;
                rds.CategoryId = categoryId;

                await CreateAsync(rds);
            }

            await SaveAsync();
        }
    }

    private List<RdsLibDm> GetInitialRds()
    {
        var rds = new List<RdsLibDm>
        {
            new() {RdsCode = "A", Name = "Drilling system"},
            new() {RdsCode = "B", Name = "Production system"},
            new() {RdsCode = "C", Name = "Transportation system"},
            new() {RdsCode = "D", Name = "Processing system"},
            new() {RdsCode = "E", Name = "Injection system"},
            new() {RdsCode = "F", Name = "Storage system"},
            new() {RdsCode = "G", Name = "Fixation system"},
            new() {RdsCode = "H", Name = "Safety system"},
            new() {RdsCode = "J", Name = "Infrastructure system"},
            new() {RdsCode = "W", Name = "Interconnection complex"},
            new() {RdsCode = "X", Name = "Transition complex"},
            new() {RdsCode = "Y", Name = "Onshore complex"},
            new() {RdsCode = "Z", Name = "Offshore complex"},
            new() {RdsCode = "HA", Name = "Fluid Supply System"},
            new() {RdsCode = "HC", Name = "Solid matter supply system"},
            new() {RdsCode = "HD", Name = "Electrical Power Supply System"},
            new() {RdsCode = "HE", Name = "Mechanical Energy Supply System"},
            new() {RdsCode = "HF", Name = "Thermal Energy Supply System"},
            new() {RdsCode = "JA", Name = "Fluid Transportation System"},
            new() {RdsCode = "JC", Name = "Solid Material Transportation System"},
            new() {RdsCode = "JE", Name = "Electrical Power Distribution System"},
            new() {RdsCode = "JF", Name = "Thermal energy transport/distribution system"},
            new() {RdsCode = "JG", Name = "Thermal Energy Transportation System"},
            new() {RdsCode = "KA", Name = "Flow Controlling System"},
            new() {RdsCode = "KB", Name = "Threshold controlling system"},
            new() {RdsCode = "KC", Name = "Separation System"},
            new() {RdsCode = "KD", Name = "Mixing system"},
            new() {RdsCode = "KE", Name = "Pumping System"},
            new() {RdsCode = "KF", Name = "Transforming system"},
            new() {RdsCode = "KG", Name = "Signal transformer system"},
            new() {RdsCode = "KH", Name = "Solid matter transformation system"},
            new() {RdsCode = "KJ", Name = "Thermal exchange system"},
            new() {RdsCode = "KK", Name = "Compression System"},
            new() {RdsCode = "KL", Name = "Chemical Reactor System"},
            new() {RdsCode = "QA", Name = "Fluid storage system"},
            new() {RdsCode = "QD", Name = "Electrical energy storage system"},
            new() {RdsCode = "QF", Name = "Thermal energy storage system"},
            new() {RdsCode = "SA", Name = "Mechanical - electrical conversion system"},
            new() {RdsCode = "SB", Name = "Mechanical - pressure - kinetic conversion system"},
            new() {RdsCode = "SC", Name = "Mechanical - enthalpy conversion system"},
            new() {RdsCode = "SD", Name = "Chemical - enthalpy conversion system"},
            new() {RdsCode = "SE", Name = "Chemical - electrical conversion system"},
            new() {RdsCode = "WB", Name = "High Voltage Transport"},
            new() {RdsCode = "WD", Name = "Low Voltage Transport"},
            new() {RdsCode = "WG", Name = "Communication Transport"},
            new() {RdsCode = "WM", Name = "Fluid open conduit Transport"},
            new() {RdsCode = "WP", Name = "Fluid closed conduit Transport"},
            new() {RdsCode = "WQ", Name = "Mechanical energy Transport"},
            new() {RdsCode = "WV", Name = "Thermal energy Transport"},
            new() {RdsCode = "WZ", Name = "Multiple kinds combined Transport "},
            new() {RdsCode = "XB", Name = "High Voltage  Interface"},
            new() {RdsCode = "XD", Name = "Low Voltage  Interface"},
            new() {RdsCode = "XG", Name = "Communication  Interface"},
            new() {RdsCode = "XK", Name = "Fluid open conduit  Interface"},
            new() {RdsCode = "XM", Name = "Fluid closed conduit  Interface"},
            new() {RdsCode = "XN", Name = "Mechanical energy  Interface"},
            new() {RdsCode = "XV", Name = "Thermal energy  Interface"},
            new() {RdsCode = "XZ", Name = "Multiple kinds combined  Interface"},
            new() {RdsCode = "AAA", Name = "Underground reservoir"},
            new() {RdsCode = "BAA", Name = "Working space"},
            new() {RdsCode = "BBA", Name = "Integrated extraction entity"},
            new() {RdsCode = "BBB", Name = "Drilling entity"},
            new() {RdsCode = "BBC", Name = "Well interface entity"},
            new() {RdsCode = "BBD", Name = "Transportation entity"},
            new() {RdsCode = "BBE", Name = "Routing entity"},
            new() {RdsCode = "BBF", Name = "Processing entity"},
            new() {RdsCode = "BBG", Name = "Matter storage entity"},
            new() {RdsCode = "BBH", Name = "Disposal entity"},
            new() {RdsCode = "BBJ", Name = "Injection entity"},
            new() {RdsCode = "BFA", Name = "Flow transmitter"},
            new() {RdsCode = "BGA", Name = "Position transmitter"},
            new() {RdsCode = "BLA", Name = "Level transmitter"},
            new() {RdsCode = "BPA", Name = "Absolute pressure transmitter"},
            new() {RdsCode = "BPC", Name = "Differential pressure transmitter"},
            new() {RdsCode = "BTA", Name = "Temperature transmitter"},
            new() {RdsCode = "CAA", Name = "Storage space"},
            new() {RdsCode = "CCA", Name = "Rechargeable battery"},
            new() {RdsCode = "CLA", Name = "Pool"},
            new() {RdsCode = "CMA", Name = "Tank"},
            new() {RdsCode = "DAA", Name = "Equipment space"},
            new() {RdsCode = "EAA", Name = "Connecting space"},
            new() {RdsCode = "EBA", Name = "Electric boiler"},
            new() {RdsCode = "EGC", Name = "Heat Exchange System"},
            new() {RdsCode = "EGD", Name = "Plate Heat Exchange System"},
            new() {RdsCode = "EGE", Name = "Spiral Heat Exchange System"},
            new() {RdsCode = "EGF", Name = "Tubular Heat Exchange System"},
            new() {RdsCode = "EMB", Name = "Furnace System"},
            new() {RdsCode = "EMC", Name = "Burner System"},
            new() {RdsCode = "EMD", Name = "Boiler System"},
            new() {RdsCode = "EME", Name = "Steam Generating System"},
            new() {RdsCode = "EMF", Name = "Flaring System"},
            new() {RdsCode = "EQB", Name = "Cooling panel (condenser)"},
            new() {RdsCode = "FCA", Name = "Fuse, overcurrent"},
            new() {RdsCode = "FCB", Name = "Circuit-breaker, overcurrent"},
            new() {RdsCode = "FLA", Name = "Safety valve"},
            new() {RdsCode = "FLB", Name = "Safety damper"},
            new() {RdsCode = "FLD", Name = "Rupture disc"},
            new() {RdsCode = "FLE", Name = "Expansion tank"},
            new() {RdsCode = "GAA", Name = "AC Generating System"},
            new() {RdsCode = "GAB", Name = "DC Generating System"},
            new() {RdsCode = "GAX", Name = "Custom Generating System"},
            new() {RdsCode = "GBA", Name = "Electric battery generating system"},
            new() {RdsCode = "GBB", Name = "Fuel cell generating system"},
            new() {RdsCode = "GPA", Name = "Positive Displacement Pumping System"},
            new() {RdsCode = "GPB", Name = "Centrifugal Pumping System"},
            new() {RdsCode = "GPC", Name = "Eductor Pumping System"},
            new() {RdsCode = "GPE", Name = "Rotary Pumping System"},
            new() {RdsCode = "GPF", Name = "Agitator, Stirrer, Impeller"},
            new() {RdsCode = "GPX", Name = "Custom Pumping System"},
            new() {RdsCode = "GQA", Name = "Reciprocating Compressing System"},
            new() {RdsCode = "GQB", Name = "Fan System"},
            new() {RdsCode = "GQC", Name = "Gas Ejector System"},
            new() {RdsCode = "GQD", Name = "Centrifugal Compressing System"},
            new() {RdsCode = "GQE", Name = "Rotary Compressing System"},
            new() {RdsCode = "GQF", Name = "Axial Compressing System"},
            new() {RdsCode = "GQG", Name = "Blower System"},
            new() {RdsCode = "GQX", Name = "Custom Compression System"},
            new() {RdsCode = "HMA", Name = "Gravitational Separating System"},
            new() {RdsCode = "HMB", Name = "Centrifuge Separating System"},
            new() {RdsCode = "HMC", Name = "Cyclone Separating System"},
            new() {RdsCode = "HMD", Name = "Gas Liquid Separating System"},
            new() {RdsCode = "HME", Name = "Scrubbing Separating System"},
            new() {RdsCode = "HMF", Name = "Coalescing System"},
            new() {RdsCode = "HMX", Name = "Custom Separating System"},
            new() {RdsCode = "HPA", Name = "Drying System"},
            new() {RdsCode = "HPB", Name = "Distillation System"},
            new() {RdsCode = "HPC", Name = "Evaporating System"},
            new() {RdsCode = "HPD", Name = "Stripping Distillation System"},
            new() {RdsCode = "HPE", Name = "Vacuum Distillation System"},
            new() {RdsCode = "HPF", Name = "Stabilizing Distillation System"},
            new() {RdsCode = "HQA", Name = "Skimmer System"},
            new() {RdsCode = "HQB", Name = "Filter System"},
            new() {RdsCode = "HQC", Name = "Sieve System"},
            new() {RdsCode = "HQD", Name = "Mechanical Separation System"},
            new() {RdsCode = "HRA", Name = "Electrostatic Separation System"},
            new() {RdsCode = "HRB", Name = "Magnetic Separation System"},
            new() {RdsCode = "HSA", Name = "Ion Exchange System"},
            new() {RdsCode = "HSB", Name = "Absorbing System"},
            new() {RdsCode = "HSC", Name = "Adsorbing System"},
            new() {RdsCode = "HSD", Name = "Contacting System"},
            new() {RdsCode = "HVB", Name = "Flocculator"},
            new() {RdsCode = "HWA", Name = "Kneading System"},
            new() {RdsCode = "HWB", Name = "Humidifying System"},
            new() {RdsCode = "HWC", Name = "Rotary Mixing System"},
            new() {RdsCode = "HWD", Name = "Static Mixing System"},
            new() {RdsCode = "HWX", Name = "Custom Mixing System"},
            new() {RdsCode = "HXA", Name = "Chemical reactor"},
            new() {RdsCode = "MAA", Name = "AC Electric Motor System"},
            new() {RdsCode = "MAB", Name = "Stepper Motor System"},
            new() {RdsCode = "MAC", Name = "DC Electric Motor System"},
            new() {RdsCode = "MAX", Name = "Custom Motor System"},
            new() {RdsCode = "MLC", Name = "Wind Turbine System"},
            new() {RdsCode = "MLD", Name = "Water Turbine System"},
            new() {RdsCode = "MLE", Name = "Expander System"},
            new() {RdsCode = "MMA", Name = "Hydraulic Moving System"},
            new() {RdsCode = "MSA", Name = "Otto Cycle Drive System"},
            new() {RdsCode = "MSB", Name = "Diesel Drive System"},
            new() {RdsCode = "MSC", Name = "Wankel Drive System"},
            new() {RdsCode = "MSD", Name = "Gas Turbine Drive System"},
            new() {RdsCode = "PAA", Name = "Processing space"},
            new() {RdsCode = "QAB", Name = "Circuit breaker, electrical circuit control"},
            new() {RdsCode = "QMA", Name = "Shutoff valve, fluid on/off control"},
            new() {RdsCode = "QNA", Name = "Control valve, fluid regulation control"},
            new() {RdsCode = "RAC", Name = "Electrical resistor"},
            new() {RdsCode = "RMA", Name = "Non-return valve, fluid one-way barrier"},
            new() {RdsCode = "RQA", Name = "Insulation, thermal"},
            new() {RdsCode = "RQC", Name = "Noise barrier"},
            new() {RdsCode = "TAA", Name = "Electrical Transforming System"},
            new() {RdsCode = "TAB", Name = "DC/DC Converting System"},
            new() {RdsCode = "TAC", Name = "Frequency converter, electrical"},
            new() {RdsCode = "TAD", Name = "Phase Shifting System"},
            new() {RdsCode = "TBA", Name = "Electrical Rectifying System (AC-DC)"},
            new() {RdsCode = "TBB", Name = "Electrical Inverting System (DC-AC)"},
            new() {RdsCode = "TBC", Name = "Electrical Bidirectional Converting System"},
            new() {RdsCode = "TLA", Name = "Gear, mechanical"},
            new() {RdsCode = "TLB", Name = "Torque converter"},
            new() {RdsCode = "ULQ", Name = "Skid, Package foundation"},
            new() {RdsCode = "WPA", Name = "Fluid Transportation System"},
            new() {RdsCode = "XEE", Name = "Earth connection"},
            new() {RdsCode = "XMC", Name = "Converging Tee System"},
            new() {RdsCode = "XMD", Name = "Diverging Tee System "}
        };
        return rds;
    }
}