import { Component, input, InputSignal, output, OutputEmitterRef } from '@angular/core';

@Component({
  selector: 'app-confirm-modal',
  imports: [],
  templateUrl: './confirm-modal.html',
  styleUrl: './confirm-modal.scss',
})
export class ConfirmModal {
  title: InputSignal<string> = input.required();
  message: InputSignal<string> = input.required();
  messageHtml: InputSignal<string> = input.required();
  additionalText: InputSignal<string> = input.required();
  cancelText: InputSignal<string> = input.required();
  confirmText: InputSignal<string> = input.required();

  confirm: OutputEmitterRef<boolean> = output<boolean>();
}
