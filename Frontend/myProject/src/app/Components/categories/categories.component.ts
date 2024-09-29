import { Component, inject, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PartnersService } from '../../Services/partners.service';
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
import { Partner } from '../../Models/Partners';
import { PageEvent,MatPaginatorModule } from '@angular/material/paginator';
import { OnInit } from '@angular/core';
import { CategoriesService } from '../../Services/categories.service';
@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule,MatSortModule,MatFormFieldModule,MatInputModule,CommonModule,
    MatButtonModule,MatDatepickerModule,MatNativeDateModule,FormsModule,ReactiveFormsModule,
    MatDialogModule,ReactiveFormsModule,FormsModule,MatPaginatorModule,MatTableModule],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})
export class CategoriesComponent implements OnInit {
  ngOnInit(): void {
    this.LoadPartners(this.pageSize,this.pageNumber,this.sortBy,this.sortDirection)
    console.log(this.dataSource)
  }

  categoryHttp = inject(CategoriesService)
  toaster = inject(ToastrService)
  isFormVisible = false
  submitType = ""
  selectedPartner:any
  CategoriesForm = new FormGroup({
    Name: new FormControl("",[Validators.required]),
  })

  submitCategories(){
    if(this.submitType=="update"){
      this.categoryHttp.UpdatePartners(this.selectedPartner.Id,this.CategoriesForm.value)
      .subscribe({
        next: response=>{
          this.toaster.success("Success","updated")
        },
        error: err=>{
          this.toaster.error("failed","something went wrong")
        }
      })
    }
    else{
      this.categoryHttp.AddCategories(this.CategoriesForm.value)
      .subscribe({
        next: response=>{
          this.toaster.success("Success")
          this.LoadPartners(this.pageSize,this.pageNumber,this.sortBy,this.sortDirection)
        },
        error: err=>{
          this.toaster.error("failed","something went wrong")
        }
      })
    }
    this.isFormVisible = false
    this.submitType = ""
  }

  toggleForm(){
    this.isFormVisible = !this.isFormVisible
    this.submitType = ""
  }

  totalRecords: number = 100; // Declare totalRecords here
  displayedColumns: string[] = ['Name','ProductCount', 'Action'];
  Partner!: Partner[];
  dataSource = new MatTableDataSource<Partner>(this.Partner);
  pageSize = 10
  pageNumber = 0
  sortBy: string = 'Id';
  sortDirection: string = 'asc';
  @ViewChild(MatSort) sort!: MatSort;
  dialog = inject(MatDialog);

  ngAfterViewInit() {
    // Now, the paginator is initialized, so we can safely load employees
    //this.dataSource.paginator = this.paginator;
    this.sort.sortChange.subscribe((event)=>{
      console.log(event)
      this.sortBy = event.active,
      this.sortDirection = event.direction || "asc"
      console.log(this.sortDirection)
      this.LoadPartners(this.pageSize,this.pageNumber,this.sortBy,this.sortDirection)
    })
  }

  changingPage(event:PageEvent){
    this.LoadPartners(this.pageSize,this.pageNumber,this.sortBy,this.sortDirection)
  }

  AddCategories(){
    this.CategoriesForm.reset({
      Name: '',           
    });
    this.toggleForm()
  }

  LoadPartners(pageSize: number, pageIndex: number,sortBy:string,sortDirection:string) {
    console.log(sortBy)
    this.categoryHttp.GetAllPartnersPaginated(pageSize, pageIndex + 1,sortBy,sortDirection).subscribe((response: any) => {
      console.log(response)
      this.dataSource.data = response.Data as Partner[];  // Explicitly cast the data to Partner[]
      this.totalRecords = response.TotalItemInDatabase; 
      this.pageSize = pageSize;
      this.pageNumber = pageIndex      
    });
  }

  editCategory(id:number){
    this.selectedPartner = this.dataSource.data.find(partner => partner.Id === id);
    console.log(this.selectedPartner)
  if (this.selectedPartner) {
    // Set the form fields with the partner's data
    this.CategoriesForm.setValue({
      Name: this.selectedPartner.Name,
    });
    this.submitType = "update"
    // Toggle the form visibility to show the edit form
    this.isFormVisible = true;}
  
  }

  deleteCategory(id:number){
    this.categoryHttp.DeletePartner(id)
    .subscribe({
      next: response=>{
        this.toaster.success("Success")
        this.LoadPartners(this.pageSize,this.pageNumber,this.sortBy,this.sortDirection);
      },
      error: err=>{
        this.toaster.error("failed","something went wrong")
      }
    })
  }
}

