# Uni Skilltest

Dette er et repository opprettet i forbindelse med Uni Skilltest. 
Prosjektet er et ufullstendig løsningsforslag

# Min forståelse av oppgaven:
1. Det skal lages en "Kontaktliste" applikasjon hvor bruker kan gjøre følgende:
- Login/logout
- Få ut liste over kontakter med filter
- Legge til ny kontakt
- Endre/slette eksisterende kontakt

2. For å kunne gjøre kall mot api'et må bruker gjøre følgende:
- Registrere og deploye applikasjon i Softrig (Blant annet sette opp Clientid, ClientSecret, RedirectUri, Scopes og State)
- Authentisere seg ved login i applikasjon. Handlingsforløp:
  - 1. Bruker sendes til https://test-login.softrig.com/connect/authorize og logger inn, velger selskap og authentiseres
    2. Bruker redirectes tilbake til angitt RedirectUri med authCode som header i Url.
    3. Bruker sender auth code, Client ID og ClientSecret til https://test-login.softrig.com/connect/token
    4. Server responderer tilbake til bruker med en authtoken
- Når du har token brukes denne i RequestHeader sammen med CompanyKey'en din (Selskapsidentifikator inne på Softrig) for å kunne gjøre kall mot apiet


# Problemer jeg har støtt på:

Dette er første gangen jeg prøver meg på en slik authentiseringsprosess, og har derfor måttet søke mye rundt på nettet for å finne ut om hvordan ting henger sammen.
Jeg prøvde å ta utgangspunkt i to av de ulike dokumentasjonene på https://developer.softrig.com/wiki/authentication/getting-started

1. Softrig dokumentasjonen (SPA application)
   - Generelt: Sleit ganske lenge med å komme til authentication og ble til slutt nødt til å prøve på "https://web.postman.co/". Kom tilslutt til rette da jeg isteden for å opprette en SPA client i softrig heller lagde en Web Client slik at jeg fikk en ClientSecret jeg kunne bruke for å sende requesten. Mulig det er brukerfeil av meg og at jeg burde ha klart å komme til autentiserings siden uten å sende med ClientSecret. 
   - Step 2: '@angular/http' er deprecated. måtte derfor finne tilsvarende funksjoner i '@angular/common/http. Ikke alt som var det samme her lenger'
   - Step 3: Til å være helt fersk til authentiseringsprosesser var det vanskelig å se sammenheng i step 3 å ha en kontekst til auth.html og silent-renew.html

2. Softrig dokumentasjon (Auth code)
   - Multiple tenant request og Appendix C: Fulgte utgangspunktet i Appendix C, men fikk ikke til å få ut token før jeg la til at CompanyKey skulle legges til i DefaultRequestHeaders (Såg det i seksjon "Multiple tenant request")
   - NuGet pakker: Flere av de er deprecated, men har ikke gitt problemer
 
 
