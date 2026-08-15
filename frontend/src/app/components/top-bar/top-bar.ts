import { Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { DummyUserInterface } from '../../models/dummy-user/dummy-user.interface';
import { DummyUser } from '../../services/dummy-user/dummy-user';

@Component({
  selector: 'app-top-bar',
  imports: [],
  templateUrl: './top-bar.html',
  styleUrl: './top-bar.scss',
})
export class TopBar implements OnInit {

  protected userName: WritableSignal<string> = signal('');
  protected userId: WritableSignal<number> = signal(0);

  private dummyUserService = inject(DummyUser);

  ngOnInit(): void {
    this.userName.set(this.dummyUserService.user().name);
    this.userId.set(this.dummyUserService.user().userId);
  }
}
