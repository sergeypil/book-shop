import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { StoreService } from "./store.service";


@Injectable()
export class AuthActivator implements CanActivate {

    constructor(private store: StoreService, private router: Router) {

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        if (this.store.loginRequired) {
            this.router.navigate(["login"])
            return false;
        }
        else {
            return true;
        }
    }

}