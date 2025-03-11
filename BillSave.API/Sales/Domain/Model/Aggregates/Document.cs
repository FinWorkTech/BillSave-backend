using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Domain.Model.ValueObjects;
using BillSave.API.Shared.Domain.Model.ValueObjects;

namespace BillSave.API.Sales.Domain.Model.Aggregates;

/// <summary>
/// This class represents the document aggregate root.
/// </summary>
public partial class Document
{
    public int Id { get; }
    public DocumentCode Code { get; private set; }
    
    public SimpleDate IssueDate { get; private set; }
    public SimpleDate DueDate { get; private set; }
    
    public Rate Rate { get; private set; }
    public Currency Currency { get; private set; }
    public decimal NominalAmount { get; private set; }
    public decimal EffectiveAnnualCostRate { get; private set; }
    
    public int PortfolioId { get; }

    public Document()
    {
        Code = new DocumentCode();
        
        IssueDate = new SimpleDate(DateTime.Now);
        DueDate = new SimpleDate(DateTime.Now);
        
        Rate = new Rate(0,"Nominal"); 
        Currency = new Currency("USD");

        PortfolioId = 0;
        NominalAmount = 0;
        EffectiveAnnualCostRate = 0;
    }
    
    public Document(string code, SimpleDate issueDate, 
        SimpleDate dueDate, Rate rate, Currency currency, decimal nominalAmount, decimal effectiveAnnualCostRate ,int portfolioId)
    {
        Code = new DocumentCode(code);
        IssueDate = issueDate;
        DueDate = dueDate;
        
        Rate = rate;
        Currency = currency;
        NominalAmount = nominalAmount;
        EffectiveAnnualCostRate = effectiveAnnualCostRate;
        
        PortfolioId = portfolioId;
    }
    
    /// <summary>
    /// Constructor that initializes a Document from a command.
    /// </summary>
    public Document(CreateDocumentCommand command) : this()
    { 
        Code = new DocumentCode(command.Code);
        
        IssueDate = new SimpleDate(command.IssueDate);
        DueDate = new SimpleDate(command.DueDate);
        
        Currency = new Currency(command.Currency);
        Rate = Rate.Create(command.RateValue, command.RateType);
        NominalAmount = command.NominalAmount;
        
        PortfolioId = command.PortfolioId;
    }
    
    /// <summary>
    /// Constructor that initializes a Document from a command.
    /// </summary>
    /// <param name="effectiveAnnualCostRate"></param>
    public void UpdateEffectiveAnnualCostRate(decimal effectiveAnnualCostRate)
    {
        EffectiveAnnualCostRate = effectiveAnnualCostRate;
    }
    
    /// <summary>
    /// Update the document with the given command.
    /// </summary>
    /// <param name="command">
    /// The command to update the document.
    /// </param>
    public void UpdateDocument(UpdateDocumentCommand command)
    {
        Code = new DocumentCode(command.Code);
        
        DueDate = new SimpleDate(command.DueDate);
        IssueDate = new SimpleDate(command.IssueDate);
        
        NominalAmount = command.NominalAmount;
        Currency = new Currency(command.Currency);
        Rate = new Rate(command.RateValue, command.RateType);
    }
}