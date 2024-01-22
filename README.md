# InvoiceAPI
Jednoduché API o 4 endpointoch, ktoré implementuje CRUD operácie pre entitu Faktúra(Invoice).

## Model Invoice entity
Model reprezentujúci entitu Faktúry. 

- *Uuid (Guid)* Jednoznačne identifikuje záznam Invoice entity
- *CreationDate (DateTime?)* Dátum vytvorenia faktúry
- *UpdateDate (DataTime?)* Dátum aktualizácie faktúry
- *Ammount (double)* Čiastka 
- *SupplierFullName (string?)* Meno a priezvisko dodávateľa
- *SupplierIco (string?)* IČO dodávateľa
- *PurchaserFullName (string?)* Meno a priezvisko odberateľa
- *PurchaserIco (string?)* IČO odberateľa
- *IssueDate (DateTime?)* Dátum vystavenia faktúry
- *DueDate (DateTime?)* Dátum splatnosti faktúry
- *FulfillmentDate (DateTime?)* Dátum uskutočnenia plnenia
- *PaymentType (enum Payment)* Forma úhrady faktúry

## Update Invoice Model
Model s dátami, ktoré môžu byť aktualizované na Faktúre.

- *CreationDate (DateTime?)* Dátum vytvorenia faktúry
- *UpdateDate (DataTime?)* Dátum aktualizácie faktúry
- *Ammount (double?)* Čiastka 
- *SupplierFullName (string?)* Meno a priezvisko dodávateľa
- *SupplierIco (string?)* IČO dodávateľa
- *PurchaserFullName (string?)* Meno a priezvisko odberateľa
- *PurchaserIco (string?)* IČO odberateľa
- *IssueDate (DateTime?)* Dátum vystavenia faktúry
- *DueDate (DateTime?)* Dátum splatnosti faktúry
- *FulfillmentDate (DateTime?)* Dátum uskutočnenia plnenia
- *PaymentType (Payment?)* Forma úhrady faktúry

## API endpointy

### POST /api/v1/Invoices
Po prevolaní endpointu sa vytvorí nový záznam entity Invoice podľa prijatých dát a uloží sa do databázy.

#### Možné odpovede
- *201 Created* značí, že záznam bol vytvorení úspešne
- *409 Conflict* značí, že záznam s Uuid už existuje v databáze
- *400 BadRequest* značí, že dáta podľa ktorých má byť záznam vytvorení, sú nevalidné
- *500 InternalServerError* značí, že došlo k internej chybe programu pri vytváraní záznamu

### GET /api/v1/Invoices{id}
Po prevolaní endpointu sa vráti záznam entity Invoice nájdenej v databáze podľa zadaného Uuid.

#### Možné odpovede
- *200 OK* značí, že záznam entity Invoice bol nájdený podľa Uuid a vrátení uživateľovi
- *404 Not Found* značí, že záznam entity Invoice nebol nájdený podľa poskytnutého Uuid

### PUT /api/v1/Invoices{id}
Po prevolaní endpointu sa aktualizuje záznam entity Invoice, identifikovaný pomocou Uuid, podľa poskytnutých dát.

#### Možné odpovede
- *204 No Content* značí, že záznam bol úspešne aktualizovaný
- *404 Not Found* značí, že záznam nebol nájdený podľa Uuid
- *409 Conflict* značí, že záznam s Uuid už existuje v databáze
- *400 BadRequest* značí, že dáta podľa ktorých má byť záznam aktualizovaný, sú nevalidné
- *500 InternalServerError* značí, že došlo k internej chybe programu pri aktualizovaní záznamu


### DELETE /api/v1/Invoices{id}
Po prevolaní endpointu sa záznam entity Invoice, identifikovanej pomocou Uuid, vymaže z databázy.

#### Možné odpovede
- *204 No Content* značí, že záznam entity Invoice bol úspešne zmazaný v databáze
- *404 Not Found* značí, že záznam entity Invoice nebol nájdený podľa poskytnutého Uuid
- *500 InternalServerError* značí, že došlo k internej chybe programu pri mazaní záznamu
