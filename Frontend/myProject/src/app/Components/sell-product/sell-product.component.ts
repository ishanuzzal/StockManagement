import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { PartnersService } from '../../Services/partners.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../Services/product.service';
import { CommonModule } from '@angular/common';
import { PopUpComponent } from '../pop-up/pop-up.component';

@Component({
  selector: 'app-sell-product',
  standalone: true,
  imports: [ReactiveFormsModule,MatFormFieldModule,MatInputModule,MatButtonModule,CommonModule],
  templateUrl: './sell-product.component.html',
  styleUrl: './sell-product.component.css'
})
export class SellProductComponent {

  private entitiesService = inject(PartnersService);
  toaster = inject(ToastrService);
  activatedRoute = inject(ActivatedRoute);
  private productService = inject(ProductService);

  sellProductForm = new FormGroup({
    Id: new FormControl(0, [Validators.required]),
    Name: new FormControl('', [Validators.required]),
    Qty: new FormControl(0, [Validators.required]),
    UnitPrice: new FormControl(0, [Validators.required]),
    bussinessEntitiesId: new FormControl('', [Validators.required])
  }); 

  bussinessEntities: any[] = [];
  private dialog = inject(MatDialog);
  private reportBlob: Blob | null = null; 

  ngOnInit(): void {
    // Retrieve query parameters and populate the form
    this.activatedRoute.queryParams.subscribe(params => {
      if (params) {
        this.sellProductForm.patchValue({
          Id: params['Id'],
          Name: params['Name'],
          UnitPrice: params['UnitPrice'],
          Qty: params['StockAmount']
        });
      }
    });
    this.loadBusinessEntities();
  }

  onSubmit(): void {
    if (this.sellProductForm.valid) {
      const formData = this.sellProductForm.value;
      console.log('Product Sell Data:', formData);
      this.productService.SellProduct(formData).subscribe({
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
    } else {
      this.toaster.error('Please fill in all required fields.');
    }
  }

  popUpComponent(): void {
    const dialogRef = this.dialog.open(PopUpComponent, {
      width: '400px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result?.action === 'generate') {
        this.generateBuyReport();
      } else {
        console.log('Report generation cancelled');
      }
    });
  }

  generateBuyReport(): void {
    // Implement the logic for generating a PDF report
    if (this.reportBlob) {
      const url = window.URL.createObjectURL(this.reportBlob);
      const link = document.createElement('a');
      link.href = url;
      link.download = `ProductSellReport_${new Date().toISOString()}.pdf`;
      link.click();
      window.URL.revokeObjectURL(url); // Clean up URL after download
    } else {
      console.error('No report available to download.');
      this.toaster.error('No report available to download.');
    }
  }

  loadBusinessEntities(): void {
    this.entitiesService.GetAllPartners(0).subscribe((res: any) => {
      this.bussinessEntities = res;
      console.log(this.bussinessEntities)
    });
  }
}
