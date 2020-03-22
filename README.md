# Evolent Health
Designed and implemented a production ready application for maintaining contact information
---
# Technologies / Tools Used 
 - WebAPI 2
 - Entity Framework
 - AutoMapper
 - Unity IOC
 - MSTest
 - JsonWebTokens  
 ---
## Funcationalities Implemented
- Add a contact
- Search a contact
- List contacts
- Edit contact
- Delete contact

---
## Prerequisites  
- DotNet Framework 4.5.2 or above
- Visual Studio 15 or above
- SQL Server
- Fiddler / Postman 

---
## Execution Steps

- Token Generation : The application uses JWT authentication.So for generating token below route is to be used, 
                      'api/JWT/GetToken?Username=yourname'
- Calling APIs     : Pass above generated Token in header for accessing the APIs using below routes,                    
                     
                      Adding a Contact                           
                          url : [ 'api/Contact/'] 
                          Use HTTPPost method and Contact details in Request Body.
                      
                      Editing a Contact                           
                          url : [ 'api/Contact/id'] 
                          Use HTTPPut method  and Contact details in Request Body.
                          
                       Get Contacts                           
                          url : [ 'api/Contact/'] 
                          Use HTTPGet method 
                          
                      Delete Contact
                          url : [ 'api/Contact/id'] 
                          Use HTTPDelete method
                          
           
