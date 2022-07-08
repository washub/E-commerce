import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { NavigationExtras, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor{

    constructor(private router:Router, private toastr:ToastrService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error=>{
                if(error){
                    if(error.status ===400){
                        if(error.error.errors){
                            return throwError(error.errors)
                        }
                        else{
                            this.toastr.error(error.error.message, error.error.statusCode)        
                        }
                    }
                    if(error.status ===401){
                        this.toastr.error(error.error.message, error.error.statusCode)        
                    }
                    if(error.status ===404|| error.status ===0){
                        this.router.navigate(['not-found'])
                    }
                    if(error.status ===500){
                        const extras:NavigationExtras = {
                            state : {error: error.error}
                        }
                        this.router.navigate(['server-error'], extras)
                    }
                }
                return throwError(error);
                
            })
        )
    }

}