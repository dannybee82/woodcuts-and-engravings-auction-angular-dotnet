import { Component, effect, ElementRef, inject, input, InputSignal, OnInit, ResourceStatus, Signal, signal, viewChild, WritableSignal } from '@angular/core';
import { AuctionItem } from '../../models/auction/auction-item.interface';
import { CreateOrUpdateAuctionInterface } from '../../models/auction/create-or-update-auction.interface';
import { AuctionItemDetail } from '../../models/auction/auction-item-detail.interface';
import { ItemType, itemTypeArrForNewItems } from '../../models/enmus/item-type.enum';
import { form, min,  required, ValidationError, FormField, disabled, readonly } from '@angular/forms/signals';
import { Toastr } from '../../components/toast/toastr';
import { Toast } from '../../services/toast/toast';
import { LowerCasePipe, TitleCasePipe, I18nPluralPipe } from '@angular/common';
import { OpenFile } from '../../components/open-file/open-file';
import { OpenFiles } from '../../services/open-files/open-files';
import { NavigationEnd, Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { RESOURCE_CONFIG } from '../../services/generics/tokens/resource.config';
import { GenericsCreateService } from '../../services/generics/create/generics-create';
import { GenericsUpdateService } from '../../services/generics/update/generics-update';
import { GenericsByIdService } from '../../services/generics/by_id/generics-by-id';

@Component({
  selector: 'app-create-or-update-auction',
  imports: [FormField, Toastr, LowerCasePipe, TitleCasePipe, I18nPluralPipe, OpenFile],
  templateUrl: './create-or-update-auction.html',
  styleUrl: './create-or-update-auction.scss',
  providers: [
    GenericsCreateService<FormData>, GenericsUpdateService<FormData>, GenericsByIdService<AuctionItem>,
    {
      provide: RESOURCE_CONFIG,
      useValue: {
        controller: 'auction-items',
        methodById: 'GetById',
        methodCreate: 'Create',
        methodUpdate: 'Update'
      }
    }
  ]
})
export class CreateOrUpdateAuction implements OnInit {
  
  protected readonly id: InputSignal<number | undefined> = input.required<number | undefined>();
  private _apiurl: string = environment.endpoint;

  protected auctionLengthUpdateElement: Signal<ElementRef<HTMLSelectElement> | undefined> = viewChild<ElementRef<HTMLSelectElement> | undefined>('auctionLengthUpdateElement');

  protected allowedItems: ItemType[] = itemTypeArrForNewItems;
  protected auctionLengthDays: number[] = [14, 7, 3, 1];
  protected auctionLengthMapping = {
    "=1": "day",
    "other": "days"
  };
  protected isUpdateMode: WritableSignal<boolean> = signal<boolean>(false);
  protected imagePreview: WritableSignal<string> = signal('');
  private _file: File | undefined = undefined;
  private _auctionYoUpdate: WritableSignal<AuctionItem | undefined> = signal<AuctionItem | undefined>(undefined);
  protected isSubmitting: WritableSignal<boolean> = signal<boolean>(false);

  private _defaultData: CreateOrUpdateAuctionInterface = {
    id: undefined,
    isOpen: true,
    details: {
      id: undefined,
      title: '',
      description: '',
      itemType: ItemType.UNKNOWN
    },
    currentPrice: 10.00,
    auctionLength: 14
  };
  protected formModel: WritableSignal<CreateOrUpdateAuctionInterface> = signal<CreateOrUpdateAuctionInterface>(structuredClone(this._defaultData));
  protected form = form(this.formModel, (form) => {
    required(form.details.title, { message: 'A title is required' }),
    required(form.details.description, { message: 'A description is required' }),
    min(form.currentPrice, 0.01, { message: 'Price must be a minimum of 0.01' })
    min(form.auctionLength, 1, { message: 'Auction Length must be greater than 0' }),
    disabled(form.currentPrice, {  
      when: () => this.isUpdateMode() && ((this._auctionYoUpdate()?.bidCount ?? 0) > 0)
    }),
    readonly(form.currentPrice, {
      when: () => this.isUpdateMode() && ((this._auctionYoUpdate()?.bidCount ?? 0) > 0)
    });
  });

  protected readonly autionItemsService = inject(GenericsByIdService<AuctionItem>);
  private autionCreateService = inject(GenericsCreateService<FormData>);
  private autionUpdateService = inject(GenericsUpdateService<FormData>);
  private openFilesService = inject(OpenFiles);
  private toastrService = inject(Toast);
  private router = inject(Router);

  constructor() {
    effect(() => {
      const error: ResourceStatus = 'error';

      if (this.autionItemsService.error() && this.autionItemsService.status() === error && (this.autionItemsService.statusCode() ?? 0) >= 400) {  
        this.reset();
        this.toastrService.error('Can\'t load auction by Id');
        this.router.navigate(['create-or-update-auction', 'new']);  
      }

      const resolved: ResourceStatus = 'resolved';

      if(this.autionItemsService.data() && this.autionItemsService.status() === resolved && this.autionItemsService.statusCode() === 200) {
        this._auctionYoUpdate.set(this.autionItemsService.data());        
          
        if(this._auctionYoUpdate()?.bidCount ?? 0 > 0) {
          this.toastrService.warning('Can\'t change price, because amount of bids is: ' + this._auctionYoUpdate()!.bidCount);
        }

        if(this._auctionYoUpdate()) {
          this.formModel.set(this.convertData(this.autionItemsService.data()));          
          this.isUpdateMode.set(true);
          this.setSelectedAuctionLength();
        }
      }
    });
    
    effect(() => {
      const resolved: ResourceStatus = 'resolved';

      if(this.autionCreateService.status() === resolved && this.autionCreateService.statusCode() === 200) {
        this.toastrService.success('The new Auction has been created successfully');
        setTimeout(() => {          
          this.router.navigate(['/']);
        }, 3000);             
      }

      if(this.autionCreateService.error()) {
        this.toastrService.error('Can\'t create Auction');
        this.isSubmitting.set(false);
      }
    });

    effect(() => {
      const resolved: ResourceStatus = 'resolved';
      
      if(this.autionUpdateService.status() === resolved && this.autionUpdateService.statusCode() === 200) {
        this.toastrService.success('The Auction has been updated successfully');
        setTimeout(() => {          
          this.router.navigate(['/auctions-overview']);
        }, 3000);        
      }

      if(this.autionUpdateService.error()) {
        this.toastrService.error('Can\'t update Auction');
        this.isSubmitting.set(false);
      }
    });
  }

  ngOnInit(): void {
    this.loadById();

    this.router.events.subscribe((data) => {
      if(data instanceof NavigationEnd) {
        if(data.url.indexOf('create-or-update-auction/new') > -1) {          
          this.reset();
          this.router.navigate(['create-or-update-auction', 'new']);
        } else {
          this.reset();
          this.loadById();
        }
      }
    });
  }

  getImageUrl(): string {
    return `${this._apiurl}auction-items/${this._auctionYoUpdate()?.imageId ?? 0}`
  }

  submit($event: SubmitEvent): void {
    $event.preventDefault();
    this.isSubmitting.set(true);

    if(!this.form().valid) {
      const errors: string[] = this.form().errorSummary().map((err: ValidationError) => err.message).filter((item: string | undefined) => item !== undefined);
      const errorMessage: string = 'Form invalid:\n' + errors.join('\n');
      this.toastrService.error(errorMessage);
      this.isSubmitting.set(false);
      return;
    }

    if(!this._file && !this.isUpdateMode()) {
      this.toastrService.error('Please select an image file.');
      this.isSubmitting.set(false);
      return;
    }

    if(this.isUpdateMode() && !this._auctionYoUpdate()) {
      this.toastrService.error('Auction to update is invalid.');
      this.isSubmitting.set(false);
      return;
    }

    const data: CreateOrUpdateAuctionInterface = this.form().value();

    console.log(data);
    console.log(this.form.currentPrice().value() === data.currentPrice);

    if(this.isUpdateMode() && this._auctionYoUpdate()) {
      data.id = this._auctionYoUpdate()!.id;
      data.details.id = this._auctionYoUpdate()!.details.id;
    }
    
    if(this.isUpdateMode()) {
      this.autionUpdateService.entity.set(this.getFormData(data, this._file));
    } else {
      this.autionCreateService.entity.set(this.getFormData(data, this._file));
    }
  }

  loadFile($event: File): void {
    this.openFilesService.readFile($event).then((result: string | null) => {
      if (result) {
        if (
          this.openFilesService.isValidDataType(result) &&
          this.openFilesService.checkMaximumSize($event.size)
        ) {
          this._file = $event;
          this.imagePreview.set(result);
        } else {
          if (!this.openFilesService.isValidDataType(result)) {
            this.toastrService.error('File is invalid');
          } else if (!this.openFilesService.checkMaximumSize($event.size)) {
            this.toastrService.error('File exceeds 10 MB');
          }
        }
      } else {
        this.toastrService.error('Invalid file');
      }
    });
  }

  removeFile(): void {
    this._file = undefined;
    this.imagePreview.set('');
  }

  valueToInt($event: Event): void {
    if($event) {
      if($event.target) {
        const value: number = parseInt(($event.target as HTMLInputElement).value ?? '0');
        this.form.auctionLength().value.set(value);
      }
    }
  }

  private setSelectedAuctionLength(): void {
    if(this.auctionLengthUpdateElement()) {
      const nativeElement: HTMLSelectElement = this.auctionLengthUpdateElement()!.nativeElement;

      if(nativeElement) {        
        nativeElement.value = this._auctionYoUpdate()!.auctionLength.toString();
      }
    }
  }

  private convertData(data: AuctionItem): CreateOrUpdateAuctionInterface {
    const details: AuctionItemDetail = {
      id: data.details.id,
      title: data.details.title,
      description: data.details.description,
      itemType: data.details.itemType
    };

    const converted: CreateOrUpdateAuctionInterface = {
      id: data.id,
      isOpen: data.isOpen,
      details: details,
      currentPrice: data.currentPrice,
      auctionLength: data.auctionLength
    };

    return converted;
  }

  private reset(): void {
    this.autionItemsService.params.set(undefined);
    this.isUpdateMode.set(false);
    this.imagePreview.set('');
    this._file = undefined;
    this._auctionYoUpdate.set(undefined);
    this.formModel.set(structuredClone(this._defaultData));
  }

  private loadById(): void {
    if(this.id() && !isNaN(parseInt(this.id()?.toString() ?? ''))) {
      const params: Record<string, any> = {
        "id": this.id()
      };
      this.autionItemsService.params.set(params);
    } else {
      this.isUpdateMode.set(false);
      this._auctionYoUpdate.set(undefined);
    }
  }

  private getFormData(data: CreateOrUpdateAuctionInterface, file?: File): FormData {
    const form = new FormData();

    if(file) {
      form.append('file', file);
    }        
 
    if (data.id) {  
      form.append('Id', data.id.toString());
    }  
    form.append('IsOpen', String(data.isOpen));
    form.append('CurrentPrice', data.currentPrice.toString().replace(",", "."));  
    form.append('AuctionLength', data.auctionLength.toString());          
    
    if(data.details.id) {
      form.append('Details.Id', data.details.id.toString());
    }
    form.append('Details.Title', data.details.title);          
    form.append('Details.Description', data.details.description);  
    form.append('Details.itemType', data.details.itemType);     

    return form;
  }

}
