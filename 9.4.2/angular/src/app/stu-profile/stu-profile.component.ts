import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule } from '@node_modules/@angular/common';
import { SessionServiceProxy, StudentServicesServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-stu-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stu-profile.component.html',
  styleUrl: './stu-profile.component.css'
})
export class StuProfileComponent {
    constructor(private sessionService: SessionServiceProxy,private studentServices:StudentServicesServiceProxy,private changeDetector: ChangeDetectorRef){}
    email:string =''
    profile:any ='';

    ngOnInit():void{
       this.loadEmail();
    }

    loadEmail():void{
      this.sessionService.getCurrentLoginInformations().subscribe({
        next:(res)=>{
           this.email = res.user.emailAddress;
            this.loadProfile();
            console.log(this.email);
        },
        error:(err)=>{
          console.log(err);
        }
       })
    }

    loadProfile():void{
       this.studentServices.getStudentByEmain(this.email).subscribe({
        next:(res)=>{
           this.profile = res;
           console.log(this.profile);
           this.changeDetector.detectChanges();
        } ,
        error:(err)=>{
          console.log(err);
        }
       })
    }
}
