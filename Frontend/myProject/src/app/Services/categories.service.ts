import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  constructor(private http:HttpClient) { }
  url = "https://localhost:7028/api/Category"

  AddCategories(data:any){
    return this.http.post(`${this.url}/AddCategories`,data)
  }

  GetAllCategoriesPaginated(PageSize:number,PageNumber:number,SortBy:string,SortOrder:string){
    return this.http.get(`${this.url}/ShowAllCategories`,
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

  GetAllCategories(){
    return this.http.get(`${this.url}/GetAllCategories`)
  }

  UpdatePartners(id:number,data:any){
    data.Id = id
    return this.http.put(`${this.url}/UpdateCategories`,data)
  }

  DeletePartner(id:number){
    return this.http.delete(`${this.url}/DeleteCategories/${id}`)
  }
}
