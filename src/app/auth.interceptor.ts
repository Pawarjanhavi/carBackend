import { HttpInterceptorFn } from '@angular/common/http';
 
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  // Retrieve the token from localStorage or your AuthService
  const token = localStorage.getItem('token');
 
  // If the token exists, clone the request and add the Authorization header
  if (token) {
    const clonedRequest = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
 
    // Pass the cloned request with the Authorization header
    return next(clonedRequest);
  }
 
  // If no token, pass the original request
  return next(req);
};