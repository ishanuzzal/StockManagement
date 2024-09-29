import { inject, Inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../Services/user.service';

export const authGuard: CanActivateFn = (route, state) => {
  const auth = inject(UserService)
  const router = inject(Router)
  if(auth.isLoggedIn()==false){
    router.navigateByUrl("/login")
    return false;
  }
  return true;
};
