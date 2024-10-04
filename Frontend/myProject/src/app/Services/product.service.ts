import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  url = `https://localhost:7028/api/Product`
  constructor(private http:HttpClient) { }

  GetAllPaginatedProducts(pageSize:number,pageIndex:number,sortBy:string,sortDirection:string){
    return this.http.get(`${this.url}/ShowAllProduct`,{
      params:{
        pageSize:pageSize,
        pageNumber:pageIndex,
        sortBy:sortBy,
        sortOrder:sortDirection
      }
    })
  }

  AddProduct(data:any){
    return this.http.post(`${this.url}/BuyProduct`, data, { responseType: 'blob' });
  }

  SearchProduct(SKU:string,pageSize:number,pageIndex:number,sortBy:string,sortDirection:string){
    return this.http.get(`${this.url}/GetProductQuery`,{
      params:{
        pageSize:pageSize,
        pageNumber:pageIndex,
        sortBy:sortBy,
        sortOrder:sortDirection,
        SKU:SKU
      }
    })
  }

  DownLoadInventoryReport(){
    return this.http.get(`${this.url}/GetInventoryReport`,{
       responseType: 'blob'
    })
  }
}
