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

        // Vlastnosti svázané s formuláøem
        [BindProperty]
        public string VstupNazevHlasovani { get; set; } = "Název hlasování";
        [BindProperty]
        public string VstupFirma { get; set; } = "Firma";
        [BindProperty]
        public string VstupMisto { get; set; } = "Místo";
        [BindProperty]
        public string VstupVedouci { get; set; } = "Vedoucí hlasování";
        [BindProperty]
        public string VstupEmail { get; set; } = "e-mail";
        [BindProperty]
        public string VstupAuditor { get; set; } = "Auditor";
        [BindProperty]
        public string VstupEmailAuditor { get; set; } = "e-mail";

        public async Task<IActionResult> OnPostAsync()
        {
            //// Asynchronní aktualizace
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

            // Vrátí aktuální stránku
            return Page();
        }


    }
}
