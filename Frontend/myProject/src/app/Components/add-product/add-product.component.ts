import { Component, inject } from '@angular/core';
import { CategoriesService } from '../../Services/categories.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PartnersService } from '../../Services/partners.service';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../Services/product.service';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { PopUpComponent } from '../pop-up/pop-up.component';
@Component({
  selector: 'app-add-product',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.css'
})
export class AddProductComponent {
  private categoryService = inject(CategoriesService);
  private entitiesService = inject(PartnersService);
  private productService = inject(ProductService);
  toaster = inject(ToastrService)
  productForm!: FormGroup;
  private fb = inject(FormBuilder);
  categories: categories[] = [];
  businessEntities: any[] = [];
  private dialog = inject(MatDialog);
  private reportBlob: Blob | null = null; 
  ngOnInit(): void {
    this.initForm();
    this.loadCategories();
    this.loadBusinessEntities();
  }

  initForm(): void {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      sku: ['', Validators.required],
      description: ['', Validators.maxLength(200)],
      stockAmount: [0, [Validators.required, Validators.min(0)]],
      unitPrice: [0, [Validators.required, Validators.min(0)]],
      unitType: ['', [Validators.required, Validators.maxLength(20)]],
      categoriesId: ['', Validators.required],
      businessEntitiesId: ['', Validators.required]
    });
  }
 
  loadBusinessEntities(): void {
    this.entitiesService.GetAllPartners(1).subscribe((res: any) => {
      this.businessEntities = res;
      console.log(this.businessEntities)
      // if (this.businessEntities.length > 0) {
      //   this.productForm.patchValue({
      //     businessEntitiesName: this.businessEntities[0].name 
      //   });
      // }
    });
  }

  loadCategories(){
    this.categoryService.GetAllCategories().subscribe((res:any)=>{
      this.categories = res;
      console.log(this.categories)
      // if (this.categories.length > 0) {
      //   this.productForm.patchValue({
      //     categoriesName: this.categories[0].name // Adjust 'name' to the correct property
      //   });
      // }
    })
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const formData = this.productForm.value;
      formData.sku = formData.sku.toLowerCase();
      console.log(formData)
      this.productService.AddProduct(formData).subscribe({
        next:(res:any)=>{
          console.log(res)
          this.reportBlob = res;
          this.toaster.success("Success")
          this.popUpComponent();
        },
        error:(err:any)=>{
          console.log(err)
        }
      })
      // Here you would typically call a service to save the product
    }
  }
  popUpComponent(): void {
    const dialogRef = this.dialog.open(PopUpComponent, {
      width: '400px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result?.action === 'generate') {
        this.generateSalesReport();
      } else {
        console.log('Report generation cancelled');
      }
    });
  }

  generateSalesReport(): void {
    // Implement the logic for generating a PDF report
    if (this.reportBlob) {
      const url = window.URL.createObjectURL(this.reportBlob);
      const link = document.createElement('a');
      link.href = url;
      link.download = `ProductBuyReport_${new Date().toISOString()}.pdf`;
      link.click();
      window.URL.revokeObjectURL(url); // Clean up URL after download
    } else {
      console.error('No report available to download.');
      this.toaster.error('No report available to download.');
    }
  }
}

interface categories{
  Id:number;
  Name:string;
}
