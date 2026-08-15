import { Component, effect, inject, OnDestroy, OnInit, signal, WritableSignal } from '@angular/core';
import { Toast } from '../../services/toast/toast';
import { ToastMessage } from '../../models/toast/toast-message.interface';
import { ToastType } from '../../models/toast/toast-type.enum';

@Component({
  selector: 'app-toast',
  imports: [],
  templateUrl: './toastr.html',
  styleUrl: './toastr.scss',
})
export class Toastr implements OnInit, OnDestroy {

  toastType: WritableSignal<ToastType> = signal(ToastType.NONE);
  message: WritableSignal<string> = signal('');
  position: WritableSignal<string> = signal('');
  isVisible: WritableSignal<boolean> = signal(false);

  private timeouts: Set<number> = new Set<number>();

  private toastService = inject(Toast);

  constructor() {
    effect(() => {
      const state: ToastMessage = this.toastService.toastState();

      this.toastType.set(state.type);
      this.message.set(state.message);
      this.position.set(state.position);
      this.isVisible.set(true);

      const timeout = setTimeout(() => {
        this.reset();
      }, 5000); 
      this.timeouts.add(timeout);
    });
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.toastService.reset();
    this.reset();
  }

  getEnum(): typeof ToastType {
    return ToastType;
  }

  private reset(): void {
    this.isVisible.set(false);
    this.toastType.set(ToastType.NONE);
    this.message.set('');
    this.position.set('');
    this.toastService.reset();
    Array.from(this.timeouts).forEach((timeout: number) => {
      clearTimeout(timeout);
    });
  }

}