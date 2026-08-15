import { computed, Service, Signal } from '@angular/core';
import { DummyUserInterface } from '../../models/dummy-user/dummy-user.interface';

@Service()
export class DummyUser {
    
    readonly user: Signal<DummyUserInterface> = computed<DummyUserInterface>(() => {
        const user: DummyUserInterface = { userId: 5, name: 'Logged In User' };
        return user;
    });

}