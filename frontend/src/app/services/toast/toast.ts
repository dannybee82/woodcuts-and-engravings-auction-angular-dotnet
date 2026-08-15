import { computed, Service, signal, Signal, WritableSignal } from '@angular/core';
import { ToastMessage } from '../../models/toast/toast-message.interface';
import { ToastType } from '../../models/toast/toast-type.enum';

@Service()
export class Toast {
  
  private _position: 'toast-center' | 'toast-start' | 'toast-end' | 'toast-top toast-start' | 'toast-top toast-center' | 'toast-top toast-end' | 'toast-start toast-middle' | 'toast-center toast-middle' | 'toast-end toast-middle' = 'toast-center';

  private _defaultToast: ToastMessage = {
    message: '',
    type: ToastType.NONE,
    position: this._position
  };
  
  private _currentToast: WritableSignal<ToastMessage> = signal<ToastMessage>(this._defaultToast);
  readonly toastState: Signal<ToastMessage> = computed(() => this._currentToast());

  success(message: string): void {
    this._currentToast.set({
      message: message,
      type: ToastType.SUCCESS,
      position: this._position
    });
  }

  warning(message: string): void {
    this._currentToast.set({
      message: message,
      type: ToastType.WARNING,
      position: this._position
    });
  }

  error(message: string): void {
    this._currentToast.set({
      message: message,
      type: ToastType.ERROR,
      position: this._position
    });
  }

  reset(): void {
    this._currentToast.set(this._defaultToast);
  }

}