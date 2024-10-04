import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PartnersService {

  constructor(private http:HttpClient) { }
  url = "https://localhost:7028/api/BusinessEntities"

  AddPartners(data:any){
    return this.http.post(`${this.url}/AddBusinessEntities`,data)
  }

  GetAllPartnersPaginated(PageSize:number,PageNumber:number,SortBy:string,SortOrder:string){
    return this.http.get(`${this.url}/ShowAllBusinessEntities`,
      {
        params: {
          PageSize: PageSize,     
          PageNumber: PageNumber, 
          SortBy: SortBy || 'Id',              
          SortOrder: SortOrder || 'asc'
        }
      }
    )
  }

  GetAllPartners(UserType:number){
    return this.http.get(`${this.url}/getAllBusinessEntities`,{
      params:{
        UserType:UserType
      }
    })
  }

  UpdatePartners(id:number,data:any){
    data.Id = id
    return this.http.put(`${this.url}/UpdateBusinessEntities`,data)
  }

  DeletePartner(id:number){
    return this.http.delete(`${this.url}/DeleteBusinessEntities/${id}`)
  }
}
