# eShepherd
## Avtorja:
    Benjamin Kovač Keber, 63190159 
    Tinkara Končan, 63190147

## Opis sistema
Najin sistem je namenjen manjšim in večjim rejcem ovac. Omogoča enostavno vodenje evidence o ovcah, ovnih, kotitvah, gonitvah, čredah in jagenjčkih. Rejci imajo enostaven pregled nad svojim staležem. Dodajajo lahko npr. nove ovce, imajo nadzor nad kotitvami in gonitvami in podobno. Narejena je tako, da je enostavna za uporabo. Nudi osnovne funkcionalnosti, kot so:
* dodajanje
* urejanje
* brisanje/odstranjevanje
* iskanje (po ID živali)
* pregled podrobnosti
* sortiranje
* ...

Za vsako ovco lahko pogledamo njene kotitve in gonitve, pri kotitvah lahko pogledamo, kateri jagenjčki so bili skoteni, pri čredah pa lahko vidimo, katere ovce so v določeni čredi. 

Aplikacija zahteva registracijo z uporabniškim imenom in geslom, tako da je s tem omogočena dodatna stopnja varnosti.

 
## Zaslonske slike
![home page](https://turtlesport191137813.files.wordpress.com/2021/01/eshepherd-ovce.png)
![ovce](https://turtlesport191137813.files.wordpress.com/2021/01/eshepherd_zacetna.png)
![aplikacija home screen](https://turtlesport191137813.files.wordpress.com/2021/01/homescreen.jpg)
![aplikacija seznam ovac](https://turtlesport191137813.files.wordpress.com/2021/01/app-ovce.jpg)
![aplikacija dodaj ovco](https://turtlesport191137813.files.wordpress.com/2021/01/app-add-ovca.jpg)
![aplikacija seznam kotitev](https://turtlesport191137813.files.wordpress.com/2021/01/app-kotitve.jpg)

## Podatkovna baza
![database](https://turtlesport191137813.files.wordpress.com/2021/01/diagram-zmanjsan.png)

## Razdelitev nalog
Na začetku sva sistem izdelovala skupaj (skupaj sva naredila modele in controllerje), nato pa sva si delo porazdelila. 
### Benjamin:
    - spletna aplikacija:
      - iskanje, listanje, filtriranje
      - azure baza
      - prenos podatkov med viewi
    - android:
      - prikazi seznamov
      - homescreen
      - sortiranje, iskanje
      - brisanje
      - prikaz specifičnih kotitev/gonitev/jagenjčkov
      - bottom navigation view
      - alerti
      - ohranjanje podatkov (savedInstanceState)
      - ...
### Tinkara:
    - spletna aplikacija:
      - azure baza
      - API
      - avtorizacija
    - android:
      - dodajanje modelov
      - urejanje modelov
      - podrobnosti modelov
      - brisanje
      - prejemanje/pošiljanje volley
      - ...
  
