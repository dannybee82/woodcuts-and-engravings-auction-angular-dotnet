import { Component, computed, input, InputSignal, OnDestroy, OnInit, output, OutputEmitterRef, signal, Signal, WritableSignal } from '@angular/core';

@Component({
  selector: 'app-countdown',
  imports: [],
  templateUrl: './countdown.html',
  styleUrl: './countdown.scss',
})
export class Countdown implements OnInit, OnDestroy {

  readonly secondsRemaining: InputSignal<number> = input.required()
  
  private _targetDate = new Date(Date.now());   
  private _remainingMs: WritableSignal<number> = signal<number>(0);
  protected minutes: Signal<number> = computed(() => Math.max(0, Math.floor((this._remainingMs() % 3_600_000) / 60_000)));  
  protected seconds: Signal<number> = computed(() => Math.max(0, Math.floor((this._remainingMs() % 60_000) / 1000)));
  private _cleanUpInterval: Set<number> = new Set<number>();

  readonly isFinished: OutputEmitterRef<boolean> = output<boolean>();

  ngOnInit(): void {
    this.setupCounter();
  }

  ngOnDestroy(): void {
    Array.from(this._cleanUpInterval).forEach((i: number) => {
      try {
        clearInterval(i);
      } catch {}
    });
  }
  
  private setupCounter(): void {
    this._targetDate = new Date(Date.now() + (this.secondsRemaining() * 1000));
    this._remainingMs.set(this._targetDate.getTime() - Date.now());

    const intervalId = setInterval(() => {  
      const diff = this._targetDate.getTime() - Date.now();  
      this._remainingMs.set(Math.max(0, diff));  
      if (diff <= 0) 
      {
        clearInterval(intervalId);
        this.isFinished.emit(true);
      }  
    }, 1000);
    this._cleanUpInterval.add(intervalId);
  }

}
