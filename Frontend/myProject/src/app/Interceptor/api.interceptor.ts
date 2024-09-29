import { HttpInterceptorFn } from '@angular/common/http';

export const apiInterceptor: HttpInterceptorFn = (req, next) => {
  const storedUser = localStorage.getItem('authUser');
  let authToken = '';

  if (storedUser) {
    const userObj = JSON.parse(storedUser); 
    authToken = userObj.token;  
    console.log(authToken)
  }

    if (authToken) {
      const authReq = req.clone({
        setHeaders: { Authorization: `Bearer ${authToken}` }
      });
      return next(authReq);
    }

    return next(req);
};
