import { CommonModule } from '@angular/common';
import { Component, inject, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import {MatTableModule,MatTableDataSource } from '@angular/material/table';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { PageEvent,MatPaginatorModule } from '@angular/material/paginator';
import { OnInit } from '@angular/core';
import { ProductService } from '../../Services/product.service';
import { Product } from '../../Models/Products';
import { ActivatedRoute, Router } from '@angular/router';
import { saveAs } from 'file-saver';  

@Component({
  selector: 'app-show-all-product',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule,MatSortModule,MatFormFieldModule,MatInputModule,CommonModule,
    MatButtonModule,MatDatepickerModule,MatNativeDateModule,FormsModule,ReactiveFormsModule,
    MatDialogModule,ReactiveFormsModule,FormsModule,MatPaginatorModule,MatTableModule
  ],
  templateUrl: './show-all-product.component.html',
  styleUrl: './show-all-product.component.css'
})
export class ShowAllProductComponent implements OnInit {
  // Inject the required services
  private productHttp = inject(ProductService);
  private activatedRoute = inject(ActivatedRoute);
  private router = inject(Router);

  SearchValues: any = {
    InSearchMode: false,
    SKU: ''
  };
  toaster = inject(ToastrService);
  isFormVisible = false;

  totalRecords: number = 100;
  displayedColumns: string[] = ['Name', 'SKU', 'Description', 'StockAmount', 'UnitPrice', 'CategoryName', 'StockLevel', 'Action'];
  Product!: Product[];
  dataSource = new MatTableDataSource<Product>(this.Product);
  pageSize = 10;
  pageNumber = 0;
  sortBy: string = 'Id';
  sortDirection: string = 'asc';
  @ViewChild(MatSort) sort!: MatSort;
  dialog = inject(MatDialog);

  ngOnInit(): void {
    // Listen for URL parameters and handle accordingly
    this.activatedRoute.queryParams.subscribe(params => {
      const skuQuery = params['SKU'];

      if (skuQuery) {
        // If a search query is present, set search mode and perform search
        this.SearchValues.SKU = skuQuery;
        this.SearchValues.InSearchMode = true;
        this.search();
      } else {
        // If no search query, load the default product list
        this.SearchValues.InSearchMode = false;
        this.LoadProduct(this.pageSize, this.pageNumber, this.sortBy, this.sortDirection);
      }
    });
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe((event) => {
      this.sortBy = event.active;
      this.sortDirection = event.direction || 'asc';
      this.LoadProduct(this.pageSize, this.pageNumber, this.sortBy, this.sortDirection);
    });
  }

  changingPage(event: PageEvent) {
    this.pageNumber = event.pageIndex;
    this.LoadProduct(this.pageSize, this.pageNumber, this.sortBy, this.sortDirection);
  }

  LoadProduct(pageSize: number, pageIndex: number, sortBy: string, sortDirection: string) {
    this.productHttp.GetAllPaginatedProducts(pageSize, pageIndex + 1, sortBy, sortDirection).subscribe((response: any) => {
      this.dataSource.data = response.Data;
      this.totalRecords = response.TotalItemInDatabase;
    });
  }

  search() {
    this.productHttp.SearchProduct(this.SearchValues.SKU, this.pageSize, this.pageNumber + 1, this.sortBy, this.sortDirection)
    .subscribe((response: any) => {
      console.log(response)
      this.dataSource.data = response.Data;
      this.totalRecords = response.TotalItemInDatabase;

      // Update the URL with the search query parameter
      this.router.navigate([], {
        relativeTo: this.activatedRoute,
        queryParams: { SKU: this.SearchValues.SKU },
        queryParamsHandling: 'merge', // Merge with other possible query params
      });
    });
  }

  editProduct(id: number) {
    console.log(id);
  }

  deleteProduct(id: number) {
    console.log(id);
  }

  InventoryStatus(){
    this.productHttp.DownLoadInventoryReport().subscribe((response:any)=>{
      this.productHttp.DownLoadInventoryReport().subscribe((response: Blob) => {
        this.downloadFile(response, 'InventoryReport.pdf');
      });
    })
  }

  downloadFile(data: Blob, filename: string) {
      const blob = new Blob([data], { type: 'application/pdf' });
      saveAs(blob, filename);  // Use FileSaver.js to trigger download
    
  }
}
