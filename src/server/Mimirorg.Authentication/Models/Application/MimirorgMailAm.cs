using System.ComponentModel.DataAnnotations;
using System.Text;
using Mimirorg.Authentication.Attributes;

namespace Mimirorg.Authentication.Models.Application;

public class MimirorgMailAm
{
    [Display(Name = "Subject")]
    [Required(ErrorMessage = "{0} is required")]
    public required string Subject { get; set; }

    [Display(Name = "FromName")]
    [Required(ErrorMessage = "{0} is required")]
    public required string FromName { get; set; }

    [Display(Name = "FromEmail")]
    [Required(ErrorMessage = "{0} is required")]
    public required string FromEmail { get; set; }

    [Display(Name = "ToName")]
    [Required(ErrorMessage = "{0} is required")]
    public required string ToName { get; set; }

    [Display(Name = "ToEmail")]
    [Required(ErrorMessage = "{0} is required")]
    public required string ToEmail { get; set; }

    [Display(Name = "PlainTextContent")]
    [RequiredOne("HtmlContent")]
    public string? PlainTextContent { get; set; }

    [Display(Name = "HtmlContent")]
    [RequiredOne("PlainTextContent")]
    public string? HtmlContent { get; set; }

    public override string ToString()
    {
        var txt = new StringBuilder();
        txt.AppendLine($"From: {FromName} ({FromEmail})");
        txt.AppendLine($"To: {ToName} ({ToEmail})");
        txt.AppendLine($"Subject: {Subject}");
        txt.AppendLine();
        txt.AppendLine();

        if (!string.IsNullOrWhiteSpace(PlainTextContent))
        {
            txt.AppendLine($"PlainTextContent: {PlainTextContent}");
            txt.AppendLine();
            txt.AppendLine();
        }

        if (!string.IsNullOrWhiteSpace(HtmlContent))
            txt.AppendLine($"{HtmlContent}");

        return txt.ToString();
    }
}