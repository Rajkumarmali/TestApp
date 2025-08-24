import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule } from '@node_modules/@angular/common';
import { SessionServiceProxy, StuCourseServicesServiceProxy, StudentServicesServiceProxy } from '@shared/service-proxies/service-proxies';


@Component({
  selector: 'app-stu-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stu-profile.component.html',
  styleUrls: ['./stu-profile.component.css']
})
export class StuProfileComponent {
    constructor(private stuCourseServices:StuCourseServicesServiceProxy,private sessionService: SessionServiceProxy,private studentServices:StudentServicesServiceProxy,private changeDetector: ChangeDetectorRef){}
    email:string =''
    profile:any ='';
    enrolledCourse:any[] =[];
    enrolleModel:boolean = false;

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
    viewCourse() {
      this.stuCourseServices.getEnrolledCourses(this.email).subscribe({
        next:(res)=>{
          this.enrolledCourse = res;
          console.log(this.enrolledCourse);
          this.enrolleModel = true;
          this.changeDetector.detectChanges();

        },
        error:(err)=>{
          console.log(err);
        }
      })
  }
}
