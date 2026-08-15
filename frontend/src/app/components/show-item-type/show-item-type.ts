import { Component, input, InputSignal } from '@angular/core';
import { ItemType } from '../../models/enmus/item-type.enum';

@Component({
  selector: 'app-show-item-type',
  imports: [],
  templateUrl: './show-item-type.html',
  styleUrl: './show-item-type.scss',
})
export class ShowItemType {

  readonly itemType: InputSignal<ItemType> = input.required<ItemType>();
  readonly size: InputSignal<number> = input.required<number>();
  readonly cssJustify: InputSignal<string> = input.required<string>();

  getEnum(): typeof ItemType {
    return ItemType;
  }

}
