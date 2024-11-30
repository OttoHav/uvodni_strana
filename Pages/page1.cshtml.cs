using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using uvodni_strana.Services;


namespace uvodni_strana.Pages
{
    

    public class page1Model : PageModel
    {
        private readonly SqlQueryService _sqlQueryService;

        public page1Model(SqlQueryService sqlQueryService)
        {
            _sqlQueryService = sqlQueryService;
        }

        // Vlastnosti sv�zan� s formul��em
        [BindProperty]
        public string VstupNazevHlasovani { get; set; } = "N�zev hlasov�n�";
        [BindProperty]
        public string VstupFirma { get; set; } = "Firma";
        [BindProperty]
        public string VstupMisto { get; set; } = "M�sto";
        [BindProperty]
        public string VstupVedouci { get; set; } = "Vedouc� hlasov�n�";
        [BindProperty]
        public string VstupEmail { get; set; } = "e-mail";
        [BindProperty]
        public string VstupAuditor { get; set; } = "Auditor";
        [BindProperty]
        public string VstupEmailAuditor { get; set; } = "e-mail";

        public async Task<IActionResult> OnPostAsync()
        {
            //// Asynchronn� aktualizace
            //await _sqlQueryService.UpdateNameVoteAsync(VstupNazevHlasovani);

            await _sqlQueryService.UpdateAllVoteParamsAsync(
    VstupNazevHlasovani,
    VstupFirma,
    VstupMisto,
    VstupVedouci,
    VstupEmail,
    VstupAuditor,
    VstupEmailAuditor
);

            // Vr�t� aktu�ln� str�nku
            return Page();
        }


    }
}
